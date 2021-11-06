namespace DSInternals.Common.Data
{
    using DSInternals.Common.Properties;
    using System;

    public class DSUser
    {
        public DSUser(DirectoryObject dsObject)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, nameof(dsObject));

            //if (!dsObject.IsUserAccount)
            //{
            //    throw new ArgumentException(Resources.ObjectNotAccountMessage);
            //}

            data_len = this.LoadGenericUserAccountInfo(dsObject);
        }

        public ulong data_len = 0;

        public string Initials
        {
            get;
            private set;
        }

        public string State
        {
            get;
            private set;
        }

        public string StreetAddress
        {
            get;
            private set;
        }

        public string City
        {
            get;
            private set;
        }

        public string PostalCode
        {
            get;
            private set;
        }

        public string Country
        {
            get;
            private set;
        }

        public string PostOfficeBox
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

        public string Manager
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

        public string Notes
        {
            get;
            private set;
        }

        protected ulong LoadGenericUserAccountInfo(DirectoryObject dsObject)
        {
            ulong ret = 0;

            // Initials:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Initials))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Initials, out string initials);
                if (!String.IsNullOrEmpty(initials))
                    ret += (ulong)initials.Length;
                this.Initials = initials;
            }

            // StreetAddress:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.StreetAddress))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.StreetAddress, out string streetAddress);
                if (!String.IsNullOrEmpty(streetAddress))
                    ret += (ulong)streetAddress.Length;
                this.StreetAddress = streetAddress;
            }

            // City:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.City))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.City, out string city);
                if (!String.IsNullOrEmpty(city))
                    ret += (ulong)city.Length;
                this.City = city;
            }

            // Postal Code:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.PostalCode))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.PostalCode, out string postalCode);
                if (!String.IsNullOrEmpty(postalCode))
                    ret += (ulong)postalCode.Length;
                this.PostalCode = postalCode;
            }

            // State:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.State))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.State, out string state);
                if (!String.IsNullOrEmpty(state))
                    ret += (ulong)state.Length;
                this.State = state;
            }

            // Country:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Country))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Country, out string country);
                if (!String.IsNullOrEmpty(country))
                    ret += (ulong)country.Length;
                this.Country = country;
            }

            // PostOfficeBox:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.PostOfficeBox))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.PostOfficeBox, out string postOfficeBox);
                if (!String.IsNullOrEmpty(postOfficeBox))
                    ret += (ulong)postOfficeBox.Length;
                this.PostOfficeBox = postOfficeBox;
            }
            // EmployeeID:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.EmployeeID))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.EmployeeID, out string employeeID);
                if (!String.IsNullOrEmpty(employeeID))
                    ret += (ulong)employeeID.Length;
                this.EmployeeID = employeeID;
            }

            // Office:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Office))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Office, out string office);
                if (!String.IsNullOrEmpty(office))
                    ret += (ulong)office.Length;
                this.Office = office;
            }

            // Tel:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Tel))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Tel, out string tel);
                if (!String.IsNullOrEmpty(tel))
                    ret += (ulong)tel.Length;
                this.Tel = tel;
            }

            // Email:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Email))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Email, out string email);
                if (!String.IsNullOrEmpty(email))
                    ret += (ulong)email.Length;
                this.Email = email;
            }

            // HomeTel:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.HomeTel))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.HomeTel, out string homeTel);
                if (!String.IsNullOrEmpty(homeTel))
                    ret += (ulong)homeTel.Length;
                this.HomeTel = homeTel;
            }

            // PagerNumber:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.PagerNumber))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.PagerNumber, out string pagerNumber);
                if (!String.IsNullOrEmpty(pagerNumber))
                    ret += (ulong)pagerNumber.Length;
                this.PagerNumber = pagerNumber;
            }

            // Mobile:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Mobile))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Mobile, out string mobile);
                if (!String.IsNullOrEmpty(mobile))
                    ret += (ulong)mobile.Length;
                this.Mobile = mobile;
            }

            // IpTel:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.IpTel))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.IpTel, out string ipTel);
                if (!String.IsNullOrEmpty(ipTel))
                    ret += (ulong)ipTel.Length;
                this.IpTel = ipTel;
            }

            // WebPage:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.WebPage))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.WebPage, out string webPage);
                if (!String.IsNullOrEmpty(webPage))
                    ret += (ulong)webPage.Length;
                this.WebPage = webPage;
            }

            // JobTitle:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.JobTitle))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.JobTitle, out string jobTitle);
                if (!String.IsNullOrEmpty(jobTitle))
                    ret += (ulong)jobTitle.Length;
                this.JobTitle = jobTitle;
            }

            // Department:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Department))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Department, out string department);
                if (!String.IsNullOrEmpty(department))
                    ret += (ulong)department.Length;
                this.Department = department;
            }

            // Company:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Company))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Company, out string company);
                if (!String.IsNullOrEmpty(company))
                    ret += (ulong)company.Length;
                this.Company = company;
            }

            // Manager:
            //if (dsObject.HasAttribute(CommonDirectoryAttributes.Manager))
            //{
            //    dsObject.ReadAttribute(CommonDirectoryAttributes.Manager, out byte[] binaryManager);
            //    string manager = dsObject.ParseDSDN(binaryManager);
            //    if (!String.IsNullOrEmpty(manager))
            //        ret += (ulong)manager.Length;
            //    this.Manager = manager;
            //}

            // HomeDirectory:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.HomeDirectory))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.HomeDirectory, out string homeDirectory);
                if (!String.IsNullOrEmpty(homeDirectory))
                    ret += (ulong)homeDirectory.Length;
                this.HomeDirectory = homeDirectory;
            }

            // HomeDrive:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.HomeDrive))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.HomeDrive, out string homeDrive);
                if (!String.IsNullOrEmpty(homeDrive))
                    ret += (ulong)homeDrive.Length;
                this.HomeDrive = homeDrive;
            }

            // UnixHomeDirectory:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.UnixHomeDirectory))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.UnixHomeDirectory, out string unixHomeDirectory);
                if (!String.IsNullOrEmpty(unixHomeDirectory))
                    ret += (ulong)unixHomeDirectory.Length;
                this.UnixHomeDirectory = unixHomeDirectory;
            }

            // ProfilePath:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.ProfilePath))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.ProfilePath, out string profilePath);
                if (!String.IsNullOrEmpty(profilePath))
                    ret += (ulong)profilePath.Length;
                this.ProfilePath = profilePath;
            }

            // ScriptPath:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.ScriptPath))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.ScriptPath, out string scriptPath);
                if (!String.IsNullOrEmpty(scriptPath))
                    ret += (ulong)scriptPath.Length;
                this.ScriptPath = scriptPath;
            }

            // Notes:
            if (dsObject.HasAttribute(CommonDirectoryAttributes.Notes))
            {
                dsObject.ReadAttribute(CommonDirectoryAttributes.Notes, out string notes);
                if (!String.IsNullOrEmpty(notes))
                    ret += (ulong)notes.Length;
                this.Notes = notes;
            }

            return ret;
        }
    }
}
