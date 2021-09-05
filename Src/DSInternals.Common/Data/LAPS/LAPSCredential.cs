using System;
using System.IO;
using System.Text;

namespace DSInternals.Common.Data
{
    public class LAPSCredential
    {
        private string LAPS_AdminPassword;
        private long LAPT_AdminPasswordExpTime;
        private string LAPT_AdminPasswordExpTime_str;

        public LAPSCredential(DirectoryObject dsObject)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");

            byte[] admPwd;
            dsObject.ReadAttribute(CommonDirectoryAttributes.McsAdmPwd, out admPwd);
            this.LAPS_AdminPassword = Encoding.UTF8.GetString(admPwd);

            long? expTime;
            dsObject.ReadAttribute(CommonDirectoryAttributes.McsAdmPwdExpirationTime, out expTime);
            this.LAPT_AdminPasswordExpTime = (expTime != null) ? expTime.Value : 0;

            DateTime dt1 = new DateTime(this.LAPT_AdminPasswordExpTime, DateTimeKind.Utc);
            dt1 = dt1.AddYears(1600).ToLocalTime();
            this.LAPT_AdminPasswordExpTime_str = dt1.ToString();
        }

        public string AdmPwd
        {
            get
            {
                return this.LAPS_AdminPassword;
            }
        }

        public string AdmPwdExpTime
        {
            get
            {
                return this.LAPT_AdminPasswordExpTime_str;
            }
        }
    }
}
