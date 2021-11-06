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

            // Load generic attribute values
            this.DistinguishedName = dsObject.DistinguishedName;
            this.ObjectGuid = dsObject.Guid;

            // whenCreated
            dsObject.ReadAttribute(CommonDirectoryAttributes.WhenCreated, out DateTime? whenCreated, true);
            this.WhenCreated = whenCreated.Value;

            // Load BitLocker-specific mandatory attribute values

            // ms-FVE-RecoveryGuid
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryGuid, out Guid? recoveryGuid);
            this.RecoveryGuid = recoveryGuid.Value;

            // ms-FVE-RecoveryPassword
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryPassword, out string recoveryPassword);
            this.RecoveryPassword = recoveryPassword;

            // Load BitLocker-specific optional attribute values

            // ms-FVE-VolumeGuid
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVEVolumeGuid, out Guid? volumeGuid);
            this.VolumeGuid = volumeGuid;

            // ms-FVE-KeyPackage
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVEKeyPackage, out byte[] keyPackage);
            this.KeyPackage = KeyPackage;
        }

        public string DistinguishedName
        {
            get;
            private set;
        }

        public Guid ObjectGuid
        {
            get;
            private set;
        }

        public Guid? VolumeGuid
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
