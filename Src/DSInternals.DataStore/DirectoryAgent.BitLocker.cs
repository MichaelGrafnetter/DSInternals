namespace DSInternals.DataStore
{
    using System;
    using System.Collections.Generic;
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;

    public partial class DirectoryAgent : IDisposable
    {
        private const string ComputerNameSuffix = "$";

        public IEnumerable<BitLockerRecoveryInformation> GetBitlockerRecoveryInformation()
        {
            // TODO: Containerized seach
            foreach (var bitlockerInfo in this.FindObjectsByCategory(CommonDirectoryClasses.FVERecoveryInformation))
            {
                // RODCs and partial replicas on GCs do not contain recovery passwords
                if (bitlockerInfo.IsWritable)
                {
                    yield return new BitLockerRecoveryInformation(bitlockerInfo);
                }
            }
        }

        public BitLockerRecoveryInformation GetBitlockerRecoveryInformation(DistinguishedName dn)
        {
            // Validate the input
            Validator.AssertNotNull(dn, nameof(dn));

            // Find the object by DN
            var foundObject = this.FindObject(dn);

            // Read BitLocker properties
            return this.GetBitlockerRecoveryInformation(foundObject, dn);
        }

        public BitLockerRecoveryInformation GetBitlockerRecoveryInformation(Guid objectId)
        {
            // Find the object by objectGuid
            var foundObject = this.FindObject(objectId);

            // Read BitLocker properties
            return this.GetBitlockerRecoveryInformation(foundObject, objectId);
        }

        public BitLockerRecoveryInformation GetBitlockerRecoveryInformationByRecoveryGuid(Guid recoveryGuid)
        {
            // Find the object by recovery GUID
            var foundObject = this.FindObject(CommonDirectoryAttributes.FVERecoveryGuid, recoveryGuid);

            // Read BitLocker properties
            return this.GetBitlockerRecoveryInformation(foundObject, recoveryGuid);
        }

        private BitLockerRecoveryInformation GetBitlockerRecoveryInformation(DatastoreObject foundObject, object objectIdentifier)
        {
            // Check object type
            int recoveryInformationClassId = this.context.Schema.FindClassId(CommonDirectoryClasses.FVERecoveryInformation);
            foundObject.ReadAttribute(CommonDirectoryAttributes.ObjectCategory, out int? objectCategory);

            if (objectCategory != recoveryInformationClassId)
            {
                throw new DirectoryObjectOperationException("Target object class is not msFVE-RecoveryInformation.", objectIdentifier);
            }

            // Read BitLocker properties
            return new BitLockerRecoveryInformation(foundObject);
        }

        public IEnumerable<BitLockerRecoveryInformation> GetBitlockerRecoveryInformation(string computerName)
        {
            // Validate the input
            Validator.AssertNotNullOrEmpty(computerName, nameof(computerName));

            // Apped the $ character to the computer name if missing, to construct a proper SAM account name.
            string samAccountName = computerName.EndsWith(ComputerNameSuffix) ? computerName : (computerName + ComputerNameSuffix);

            // Find the computer object
            var computerObject = this.FindObject(samAccountName);

            // Find all children of type msFVE-RecoveryInformation
            int recoveryInformationClassId = this.context.Schema.FindClassId(CommonDirectoryClasses.FVERecoveryInformation);
            this.dataTableCursor.FindChildren(this.context.Schema);

            while (this.dataTableCursor.MoveNext())
            {
                var childObject = new DatastoreObject(this.dataTableCursor, this.context);
                childObject.ReadAttribute(CommonDirectoryAttributes.ObjectCategory, out int? objectCategory);

                if (objectCategory == recoveryInformationClassId)
                {
                    yield return new BitLockerRecoveryInformation(childObject);
                }
            }
        }
    }
}
