namespace DSInternals.DataStore
{
    using System;
    using System.Collections.Generic;
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Schema;
    using Microsoft.Database.Isam;

    public partial class DirectoryAgent : IDisposable
    {
        private const string ComputerNameSuffix = "$";

        public IEnumerable<BitLockerRecoveryInformation> GetBitLockerRecoveryInformation()
        {
            var recoveryGuidAttribute = this.context.Schema.FindAttribute(AttributeType.FVERecoveryGuid);

            if (recoveryGuidAttribute == null)
            {
                // The msFVE-RecoveryGuid attribute is not contained in the schema.
                yield break;
            }

            using (var cursor = this.context.OpenDataTable())
            {
                // The msFVE-RecoveryGuid index should only contain recovery keys and be much smaller than the objectCategory index.
                cursor.CurrentIndex = recoveryGuidAttribute.ContainerizedIndexName;

                while (cursor.MoveNext())
                {
                    var bitlockerInfo = new DatastoreObject(cursor, this.context);
                    yield return new BitLockerRecoveryInformation(bitlockerInfo);
                }
            }
        }

        public BitLockerRecoveryInformation GetBitLockerRecoveryInformation(DistinguishedName dn)
        {
            // Validate the input
            Validator.AssertNotNull(dn, nameof(dn));

            // Find the object by its distinguished name
            // This throws exception if the DN does not get resolved to dnt:
            DNTag dnTag = this.context.DistinguishedNameResolver.Resolve(dn);

            DNTag? recoveryInformationObjectCategory = this.context.Schema.FindObjectCategory(ClassType.FVERecoveryInformation);

            if (recoveryInformationObjectCategory.HasValue)
            {
                using (var cursor = this.context.OpenDataTable())
                {
                    cursor.CurrentIndex = DirectorySchema.DistinguishedNameTagIndex;
                    bool found = cursor.GotoKey(Key.Compose(dnTag));
                    if (found)
                    {
                        // Check object type
                        var foundObject = new DatastoreObject(cursor, this.context);
                        foundObject.ReadAttribute(CommonDirectoryAttributes.ObjectCategory, out DNTag? objectCategory);

                        if (objectCategory == recoveryInformationObjectCategory)
                        {
                            // Read BitLocker properties
                            return new BitLockerRecoveryInformation(foundObject);
                        }
                    }
                }
            }

            // If we got here, then we have not found any recovery object with that DN.
            throw new DirectoryObjectNotFoundException(dn);
        }

        public BitLockerRecoveryInformation GetBitLockerRecoveryInformation(Guid objectId)
        {
            Columnid? recoveryGuidColumn = this.context.Schema.FindColumnId(CommonDirectoryAttributes.FVERecoveryGuid);
            DNTag? domainNC = this.context.DomainController.DomainNamingContextDNT;

            if (domainNC.HasValue && recoveryGuidColumn != null)
            {
                // Find the object by objectGuid
                using (var cursor = this.context.OpenDataTable())
                {
                    // Use the multi-column nc_guid_Index index
                    cursor.CurrentIndex = DirectorySchema.PartitionedGuidIndex;
                    byte[] binaryGuid = objectId.ToByteArray();
                    bool found = cursor.GotoKey(Key.Compose(domainNC.Value, binaryGuid));

                    if (found)
                    {
                        Guid? recoveryGuid = cursor.RetrieveColumnAsGuid(recoveryGuidColumn);

                        // Check if the object type is indeed BitLocker recovery info
                        if (recoveryGuid.HasValue)
                        {
                            // Read BitLocker properties
                            var databaseObject = new DatastoreObject(cursor, this.context);
                            return new BitLockerRecoveryInformation(databaseObject);
                        }
                    }

                }
            }

            // In any other case we have not found the right object
            throw new DirectoryObjectNotFoundException(objectId);
        }

        public BitLockerRecoveryInformation GetBitLockerRecoveryInformationByRecoveryGuid(Guid recoveryGuid)
        {
            var recoveryGuidAttribute = this.context.Schema.FindAttribute(CommonDirectoryAttributes.FVERecoveryGuid);

            if (recoveryGuidAttribute != null)
            {
                using (var cursor = this.context.OpenDataTable())
                {
                    // Find the object by recovery GUID
                    cursor.CurrentIndex = recoveryGuidAttribute.IndexName;
                    byte[] binaryGuid = recoveryGuid.ToByteArray();
                    bool found = cursor.GotoKey(Key.Compose(binaryGuid));

                    if (found)
                    {
                        // Read BitLocker properties
                        var databaseObject = new DatastoreObject(cursor, this.context);
                        return new BitLockerRecoveryInformation(databaseObject);
                    }
                }
            }

            // In any other case we have not found the right object
            throw new DirectoryObjectNotFoundException(recoveryGuid);
        }

        public IEnumerable<BitLockerRecoveryInformation> GetBitLockerRecoveryInformation(string computerName)
        {
            var recoveryGuidAttribute = this.context.Schema.FindAttribute(CommonDirectoryAttributes.FVERecoveryGuid);

            if (recoveryGuidAttribute == null)
            {
                // The msFVE-RecoveryGuid attribute is not contained in the schema.
                yield break;
            }

            // The database must contain the domain partition.
            DNTag domainNC = this.context.DomainController.DomainNamingContextDNT ??
                throw new DirectoryObjectNotFoundException(computerName);

            // Validate the input
            Validator.AssertNotNullOrEmpty(computerName, nameof(computerName));

            // Apped the $ character to the computer name if missing, to construct a proper SAM account name.
            string samAccountName = computerName.EndsWith(ComputerNameSuffix) ? computerName : (computerName + ComputerNameSuffix);

            using (var cursor = this.context.OpenDataTable())
            {
                // Find the computer object by using the multi-column NC_Acc_Type_Name index
                cursor.CurrentIndex = DirectorySchema.PartitionedAccountNameIndex;
                bool found = cursor.GotoKey(Key.Compose(domainNC, SamAccountType.Computer, samAccountName));

                if (found)
                {
                    // Get the computer DNT
                    DNTag? computerDNT = cursor.RetrieveColumnAsDNTag(this.context.Schema.DistinguishedNameTagColumnId);

                    // We have found the computer object. Perform a containerized search for recovery keys.
                    cursor.CurrentIndex = recoveryGuidAttribute.ContainerizedIndexName;
                    cursor.FindRecords(MatchCriteria.EqualTo, Key.ComposeWildcard(computerDNT));

                    while (cursor.MoveNext())
                    {
                        var childObject = new DatastoreObject(cursor, this.context);
                        yield return new BitLockerRecoveryInformation(childObject);
                    }
                }
                else
                {
                    // We have not found the computer account.
                    throw new DirectoryObjectNotFoundException(computerName);
                }
            }
        }
    }
}
