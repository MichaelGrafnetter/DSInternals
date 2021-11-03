using System;
using System.IO;

namespace DSInternals.Common.Data
{
    public class BitLockerRecoveryInformation
    {
        public BitLockerRecoveryInformation(DirectoryObject dsObject)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, nameof(dsObject));

            throw new NotImplementedException();
            //dsObject.ReadAttribute(CommonDirectoryAttributes.Name, out this.FVE_Name);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.CommonName, out this.FVE_CommonName);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.FVEVolumeGuid, out this.FVE_VolumeGuid);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryGuid, out this.FVE_RecoveryGuid);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.FVEKeyPackage, out this.FVE_KeyPackage);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryPassword, out this.FVE_RecoveryPassword);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.WhenChanged, out this.FVE_WhenChanged);
            //dsObject.ReadAttribute(CommonDirectoryAttributes.WhenCreated, out this.FVE_WhenCreated);
        }

        public Guid VolumeGuid
        {
            get;
            private set;
        }

        public Guid RecoveryGuid
        {
            get;
            private set;
        }

        public string RecoveryPassword
        {
            get;
            private set;
        }

        public byte[] KeyPackage
        {
            get;
            private set;
        }

        public DateTime WhenChanged
        {
            get;
            private set;
        }

        public DateTime WhenCreated
        {
            get;
            private set;
        }

        /// <summary>
        /// Saves the BitLocker recovery information to an appropriate file in the current directory.
        /// </summary>
        public void Save()
        {
            this.Save(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Saves the BitLocker Recovery Information to an appropriate file in the specified directory.
        /// </summary>
        /// <param name="directoryPath">Directory to save the recovery information to.</param>
        public void Save(string directoryPath)
        {
            throw new NotImplementedException();
            //if (exportKeysPath != null && exportKeysPath.Length > 0)
            //{
            //    string exportBasePath = exportKeysPath + '.';
            //    if (oDN != null && oDN.Length > 0)
            //    {
            //        exportBasePath += oDN + '.';
            //    }
            //    exportBasePath += this.FVE_RecoveryGuid;

            //    string exportKPpath = exportBasePath + ".KeyPackage.bin";
            //    string exportRPpath = exportBasePath + ".RecoveryPassword.txt";

            //    using (FileStream fs = File.Create(exportKPpath))
            //    {
            //        fs.Write(this.FVE_KeyPackage, 0, this.FVE_KeyPackage.Length);
            //        fs.Close();
            //    }

            //    using (FileStream fs = File.Create(exportRPpath))
            //    {
            //        byte[] rp = new UTF8Encoding(true).GetBytes(this.FVE_RecoveryPassword);
            //        fs.Write(rp, 0, rp.Length);
            //        fs.Close();
            //    }
            // }
        }
    }
}
