namespace DSInternals.Common.Data
{
    using DSInternals.Common.Properties;
    using System;

    public class GenericUserAccountInfo
    {
        public GenericUserAccountInfo(DirectoryObject dsObject)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, nameof(dsObject));

            if (!dsObject.IsUserAccount)
            {
                throw new ArgumentException(Resources.ObjectNotAccountMessage);
            }

            data_len = this.LoadGenericUserAccountInfo(dsObject);
        }

        public ulong data_len = 0;

        public string Initials
        {
            get;
            private set;
        }

        public string EmployeeID
        {
            get;
            private set;
        }

        public string Office
        {
            get;
            private set;
        }

        public string Tel
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }

        public string HomeTel
        {
            get;
            private set;
        }

        public string PagerNumber
        {
            get;
            private set;
        }

        public string Mobile
        {
            get;
            private set;
        }

        public string IpTel
        {
            get;
            private set;
        }

        public string WebPage
        {
            get;
            private set;
        }

        public string JobTitle
        {
            get;
            private set;
        }

        public string Department
        {
            get;
            private set;
        }

        public string Company
        {
            get;
            private set;
        }

        public string HomeDirectory
        {
            get;
            private set;
        }

        public string HomeDrive
        {
            get;
            private set;
        }

        public string UnixHomeDirectory
        {
            get;
            private set;
        }

        public string ProfilePath
        {
            get;
            private set;
        }

        public string ScriptPath
        {
            get;
            private set;
        }

        protected ulong LoadGenericUserAccountInfo(DirectoryObject dsObject)
        {
            ulong ret = 0;

            // Initials:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Initials, out string initials);
            if (!String.IsNullOrEmpty(initials)) ret += (ulong)initials.Length;
            this.Initials = initials;

            // EmployeeID:
            dsObject.ReadAttribute(CommonDirectoryAttributes.EmployeeID, out string employeeID);
            if (!String.IsNullOrEmpty(employeeID))
                ret += (ulong)employeeID.Length;
            this.EmployeeID = employeeID;

            // Office:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Office, out string office);
            if (!String.IsNullOrEmpty(office))
                ret += (ulong)office.Length;
            this.Office = office;

            // Tel:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Tel, out string tel);
            if (!String.IsNullOrEmpty(tel))
                ret += (ulong)tel.Length;
            this.Tel = tel;

            // Email:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Email, out string email);
            if (!String.IsNullOrEmpty(email))
                ret += (ulong)email.Length;
            this.Email = email;

            // HomeTel:
            dsObject.ReadAttribute(CommonDirectoryAttributes.HomeTel, out string homeTel);
            if (!String.IsNullOrEmpty(homeTel))
                ret += (ulong)homeTel.Length;
            this.HomeTel = homeTel;

            // PagerNumber:
            dsObject.ReadAttribute(CommonDirectoryAttributes.PagerNumber, out string pagerNumber);
            if (!String.IsNullOrEmpty(pagerNumber))
                ret += (ulong)pagerNumber.Length;
            this.PagerNumber = pagerNumber;

            // Mobile:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Mobile, out string mobile);
            if (!String.IsNullOrEmpty(mobile))
                ret += (ulong)mobile.Length;
            this.Mobile = mobile;

            // IpTel:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IpTel, out string ipTel);
            if (!String.IsNullOrEmpty(ipTel))
                ret += (ulong)ipTel.Length;
            this.IpTel = ipTel;

            // WebPage:
            dsObject.ReadAttribute(CommonDirectoryAttributes.WebPage, out string webPage);
            if (!String.IsNullOrEmpty(webPage))
                ret += (ulong)webPage.Length;
            this.WebPage = webPage;

            // JobTitle:
            dsObject.ReadAttribute(CommonDirectoryAttributes.JobTitle, out string jobTitle);
            if (!String.IsNullOrEmpty(jobTitle))
                ret += (ulong)jobTitle.Length;
            this.JobTitle = jobTitle;

            // Department:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Department, out string department);
            if (!String.IsNullOrEmpty(department))
                ret += (ulong)department.Length;
            this.Department = department;

            // Company:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Company, out string company);
            if (!String.IsNullOrEmpty(company))
                ret += (ulong)company.Length;
            this.Company = company;

            // HomeDirectory:
            dsObject.ReadAttribute(CommonDirectoryAttributes.HomeDirectory, out string homeDirectory);
            if (!String.IsNullOrEmpty(homeDirectory))
                ret += (ulong)homeDirectory.Length;
            this.HomeDirectory = homeDirectory;

            // HomeDrive:
            dsObject.ReadAttribute(CommonDirectoryAttributes.HomeDrive, out string homeDrive);
            if (!String.IsNullOrEmpty(homeDrive))
                ret += (ulong)homeDrive.Length;
            this.HomeDrive = homeDrive;

            // UnixHomeDirectory:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UnixHomeDirectory, out string unixHomeDirectory);
            if (!String.IsNullOrEmpty(unixHomeDirectory))
                ret += (ulong)unixHomeDirectory.Length;
            this.UnixHomeDirectory = unixHomeDirectory;

            // ProfilePath:
            dsObject.ReadAttribute(CommonDirectoryAttributes.ProfilePath, out string profilePath);
            if (!String.IsNullOrEmpty(profilePath))
                ret += (ulong)profilePath.Length;
            this.ProfilePath = profilePath;

            // ScriptPath:
            dsObject.ReadAttribute(CommonDirectoryAttributes.ScriptPath, out string scriptPath);
            if (!String.IsNullOrEmpty(scriptPath))
                ret += (ulong)scriptPath.Length;
            this.ScriptPath = scriptPath;

            return ret;
        }
    }
}
