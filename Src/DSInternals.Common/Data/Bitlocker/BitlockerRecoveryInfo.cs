using System;
using System.IO;
using System.Text;

namespace DSInternals.Common.Data
{
    public class BitlockerRecoveryInfo
    {
        private Guid? FVE_ObjectGUID;
        private string FVE_Name;
        private string FVE_CommonName;
        private DistinguishedName FVE_DistinguishedName;
        //private RawSecurityDescriptor FVE_securityDescriptor;
        private DistinguishedName FVE_OwnerDN;
        private Guid? FVE_VolumeGuid;
        private Guid? FVE_RecoveryGuid;
        private string FVE_RecoveryPassword;
        private byte[] FVE_KeyPackage;
        private long? FVE_WhenChanged;
        private long? FVE_WhenCreated;
        private string FVE_WhenChanged_str;
        private string FVE_WhenCreated_str;

        public BitlockerRecoveryInfo(DirectoryObject dsObject, string exportKeysPath)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");

            dsObject.ReadAttribute(CommonDirectoryAttributes.ObjectGUID, out this.FVE_ObjectGUID);
            dsObject.ReadAttribute(CommonDirectoryAttributes.Name, out this.FVE_Name);
            dsObject.ReadAttribute(CommonDirectoryAttributes.CommonName, out this.FVE_CommonName);

            if (dsObject.DistinguishedName == null)
                dsObject.ReadAttribute(CommonDirectoryAttributes.DN, out this.FVE_DistinguishedName);
            else
                this.FVE_DistinguishedName = new DistinguishedName(dsObject.DistinguishedName);

            //dsObject.ReadAttribute(CommonDirectoryAttributes.SecurityDescriptor, out this.FVE_securityDescriptor);
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVEVolumeGuid, out this.FVE_VolumeGuid);
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryGuid, out this.FVE_RecoveryGuid);
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVEKeyPackage, out this.FVE_KeyPackage);
            dsObject.ReadAttribute(CommonDirectoryAttributes.FVERecoveryPassword, out this.FVE_RecoveryPassword);
            dsObject.ReadAttribute(CommonDirectoryAttributes.WhenChanged, out this.FVE_WhenChanged);
            dsObject.ReadAttribute(CommonDirectoryAttributes.WhenCreated, out this.FVE_WhenCreated);

            if (this.FVE_WhenChanged != null)
            {
                DateTime dt1 = new DateTime(1601, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dt1 = dt1.AddSeconds(this.FVE_WhenChanged.Value).ToLocalTime();
                this.FVE_WhenChanged_str = dt1.ToString();
            }

            if (this.FVE_WhenCreated != null)
            {
                DateTime dt2 = new DateTime(1601, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dt2 = dt2.AddSeconds(this.FVE_WhenCreated.Value).ToLocalTime();
                this.FVE_WhenCreated_str = dt2.ToString();
            }

            string CN = (this.FVE_CommonName != null) ? this.FVE_CommonName : this.FVE_Name;
            string oDN = null;

            if (this.FVE_DistinguishedName != null)
            {
                string DN = this.FVE_DistinguishedName.ToString();
                oDN = DN.Replace("CN=" + CN + ",", "");
                if (oDN != null && oDN.Length > 0)
                {
                    this.FVE_OwnerDN = new DistinguishedName(oDN);
                }
            }

            if (exportKeysPath != null && exportKeysPath.Length > 0)
            {
                string exportBasePath = exportKeysPath + '.';
                if (oDN != null && oDN.Length > 0)
                {
                    exportBasePath += oDN + '.';
                }
                exportBasePath += this.FVE_RecoveryGuid;

                string exportKPpath = exportBasePath + ".KeyPackage.bin";
                string exportRPpath = exportBasePath + ".RecoveryPassword.txt";

                using (FileStream fs = File.Create(exportKPpath))
                {
                    fs.Write(this.FVE_KeyPackage, 0, this.FVE_KeyPackage.Length);
                    fs.Close();
                }

                using (FileStream fs = File.Create(exportRPpath))
                {
                    byte[] rp = new UTF8Encoding(true).GetBytes(this.FVE_RecoveryPassword);
                    fs.Write(rp, 0, rp.Length);
                    fs.Close();
                }
            }
        }

        public Guid ObjectGUID
        {
            get
            {
                return (this.FVE_ObjectGUID != null) ? this.FVE_ObjectGUID.Value : Guid.Empty;
            }
        }

        public string Name
        {
            get
            {
                return this.FVE_Name;
            }
        }

        public string CommonName
        {
            get
            {
                return this.FVE_CommonName;
            }
        }

        public string DistinguishedName
        {
            get
            {
                return (this.FVE_DistinguishedName != null) ? this.FVE_DistinguishedName.ToString() : null;
            }
        }

        public string OwnerDN
        {
            get
            {
                return (this.FVE_OwnerDN != null) ? this.FVE_OwnerDN.ToString() : null;
            }
        }
        /*
        public string SecurityDescriptor
        {
            get
            {
                return this.FVE_securityDescriptor.GetSddlForm(AccessControlSections.All);
            }
        }
        */
        public Guid VolumeGuid
        {
            get
            {
                return (this.FVE_VolumeGuid != null) ? this.FVE_VolumeGuid.Value : Guid.Empty;
            }
        }

        public Guid RecoveryGuid
        {
            get
            {
                return (this.FVE_RecoveryGuid != null) ? this.FVE_RecoveryGuid.Value : Guid.Empty;
            }
        }

        public string RecoveryPassword
        {
            get
            {
                return this.FVE_RecoveryPassword;
            }
        }

        public byte[] KeyPackage
        {
            get
            {
                return this.FVE_KeyPackage;
            }
        }

        public string WhenChanged
        {
            get
            {
                return this.FVE_WhenChanged_str;
            }
        }

        public string WhenCreated
        {
            get
            {
                return this.FVE_WhenCreated_str;
            }
        }
    }
}
