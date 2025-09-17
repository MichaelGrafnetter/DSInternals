﻿namespace DSInternals.Common.Data
{
    using System;
    using System.Collections.Generic;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Schema;

    /// <summary>
    /// Represents a user account in Active Directory with extended properties and credential information.
    /// </summary>
    public class DSUser : DSAccount
    {
        /// <summary>
        /// Initializes a new instance of the DSUser class.
        /// </summary>
        /// <param name="dsObject">The directory object containing the user data.</param>
        /// <param name="netBIOSDomainName">The NetBIOS domain name.</param>
        /// <param name="pek">The directory secret decryptor for handling encrypted data.</param>
        /// <param name="propertySets">The set of properties to load from the directory object.</param>
        /// <exception cref="ArgumentException">Thrown when the object is not a user account.</exception>
        public DSUser(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek, AccountPropertySets propertySets = AccountPropertySets.All) : base(dsObject, netBIOSDomainName, pek, propertySets)
        {
            if (this.SamAccountType != SamAccountType.User)
            {
                throw new ArgumentException("The object is not a user.");
            }

            if (propertySets.HasFlag(AccountPropertySets.RoamedCredentials))
            {
                // Credential Roaming
                this.LoadRoamedCredentials(dsObject);
            }

            if (propertySets.HasFlag(AccountPropertySets.GenericUserInfo))
            {
                this.LoadGenericUserAccountInfo(dsObject);
            }

            if (propertySets.HasFlag(AccountPropertySets.Manager))
            {
                // This is a linked value, so it takes multiple seeks to load it.
                this.LoadManager(dsObject);
            }
        }

        /// <summary>
        /// Gets the date and time when roamed credentials were created for this user.
        /// </summary>
        public DateTime? RoamedCredentialsCreated
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date and time when roamed credentials were last modified for this user.
        /// </summary>
        public DateTime? RoamedCredentialsModified
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the roamed credentials for this user account, including certificates and private keys.
        /// </summary>
        public RoamedCredential[] RoamedCredentials
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the display name for this <see cref="DSAccount"/>.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the given name for the <see cref="DSAccount"/>.
        /// </summary>
        public string GivenName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the surname for the user <see cref="DSAccount"/>. 
        /// </summary>
        public string Surname
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the initials of the user.
        /// </summary>
        public string Initials
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state or province where the user is located.
        /// </summary>
        public string State
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the street address of the user.
        /// </summary>
        public string StreetAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the city where the user is located.
        /// </summary>
        public string City
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the postal code (ZIP code) for the user's location.
        /// </summary>
        public string PostalCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the country where the user is located.
        /// </summary>
        public string Country
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the post office box number for the user's mailing address.
        /// </summary>
        public string PostOfficeBox
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the employee ID for the user.
        /// </summary>
        public string EmployeeID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the employee number for the user.
        /// </summary>
        public string EmployeeNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the office location for the user.
        /// </summary>
        public string Office
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the primary telephone number for the user.
        /// </summary>
        public string TelephoneNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the email address for the user.
        /// </summary>
        public string Email
        {
            get;
            private set;
        }

        public string HomePhone
        {
            get;
            private set;
        }

        public string Pager
        {
            get;
            private set;
        }

        public string Mobile
        {
            get;
            private set;
        }

        public string IpPhone
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


        /// <summary>
        /// Loads credential roaming objects and timestamps.
        /// </summary>
        protected void LoadRoamedCredentials(DirectoryObject dsObject)
        {
            // These attributes have been added in Windows Server 2008, so they might not be present on older DCs.
            byte[] roamingTimeStamp;
            dsObject.ReadAttribute(CommonDirectoryAttributes.PKIRoamingTimeStamp, out roamingTimeStamp);

            if (roamingTimeStamp == null)
            {
                // This account does not have roamed credentials, so we skip their processing
                return;
            }

            // The 16B of the value consist of two 8B actual time stamps.
            long createdTimeStamp = BitConverter.ToInt64(roamingTimeStamp, 0);
            long modifiedTimeStamp = BitConverter.ToInt64(roamingTimeStamp, sizeof(long));

            this.RoamedCredentialsCreated = DateTime.FromFileTime(createdTimeStamp);
            this.RoamedCredentialsModified = DateTime.FromFileTime(modifiedTimeStamp);

            byte[][] masterKeyBlobs;
            dsObject.ReadLinkedValues(CommonDirectoryAttributes.PKIDPAPIMasterKeys, out masterKeyBlobs);

            byte[][] credentialBlobs;
            dsObject.ReadLinkedValues(CommonDirectoryAttributes.PKIAccountCredentials, out credentialBlobs);

            // Parse the blobs and combine them into one array.
            var credentials = new List<RoamedCredential>();

            if (masterKeyBlobs != null)
            {
                foreach (var blob in masterKeyBlobs)
                {
                    credentials.Add(new RoamedCredential(blob, this.SamAccountName, this.Sid));
                }
            }

            if (credentialBlobs != null)
            {
                foreach (var blob in credentialBlobs)
                {
                    credentials.Add(new RoamedCredential(blob, this.SamAccountName, this.Sid));
                }
            }

            this.RoamedCredentials = credentials.ToArray();
        }

        protected void LoadGenericUserAccountInfo(DirectoryObject dsObject)
        {
            // DisplayName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.DisplayName, out string displayName);
            this.DisplayName = displayName;

            // GivenName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.GivenName, out string givenName);
            this.GivenName = givenName;

            // Surname:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Surname, out string surname);
            this.Surname = surname;

            // Initials:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Initials, out string initials);
            this.Initials = initials;

            // EmployeeID:
            dsObject.ReadAttribute(CommonDirectoryAttributes.EmployeeId, out string employeeID);
            this.EmployeeID = employeeID;

            // EmployeeNumber:
            dsObject.ReadAttribute(CommonDirectoryAttributes.EmployeeNumber, out string employeeNumber);
            this.EmployeeNumber = employeeNumber;

            // Email:
            dsObject.ReadAttribute(CommonDirectoryAttributes.EmailAddress, out string email);
            this.Email = email;

            // StreetAddress:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Street, out string streetAddress);
            this.StreetAddress = streetAddress;

            // City:
            dsObject.ReadAttribute(CommonDirectoryAttributes.City, out string city);
            this.City = city;

            // Postal Code:
            dsObject.ReadAttribute(CommonDirectoryAttributes.PostalCode, out string postalCode);
            this.PostalCode = postalCode;

            // State:
            dsObject.ReadAttribute(CommonDirectoryAttributes.State, out string state);
            this.State = state;

            // Country:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Country, out string country);
            this.Country = country;

            // PostOfficeBox:
            dsObject.ReadAttribute(CommonDirectoryAttributes.PostOfficeBox, out string postOfficeBox);
            this.PostOfficeBox = postOfficeBox;

            // Office:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Office, out string office);
            this.Office = office;

            // Tel:
            dsObject.ReadAttribute(CommonDirectoryAttributes.TelephoneNumber, out string tel);
            this.TelephoneNumber = tel;

            // HomeTel:
            dsObject.ReadAttribute(CommonDirectoryAttributes.HomePhone, out string homeTel);
            this.HomePhone = homeTel;

            // PagerNumber:
            dsObject.ReadAttribute(CommonDirectoryAttributes.PagerNumber, out string pagerNumber);
            this.Pager = pagerNumber;

            // Mobile:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Mobile, out string mobile);
            this.Mobile = mobile;

            // IpTel:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IpPhone, out string ipTel);
            this.IpPhone = ipTel;

            // WebPage:
            dsObject.ReadAttribute(CommonDirectoryAttributes.WebPage, out string webPage);
            this.WebPage = webPage;

            // JobTitle:
            dsObject.ReadAttribute(CommonDirectoryAttributes.JobTitle, out string jobTitle);
            this.JobTitle = jobTitle;

            // Department:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Department, out string department);
            this.Department = department;

            // Company:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Company, out string company);
            this.Company = company;

            // HomeDirectory:
            dsObject.ReadAttribute(CommonDirectoryAttributes.HomeDirectory, out string homeDirectory);
            this.HomeDirectory = homeDirectory;

            // HomeDrive:
            dsObject.ReadAttribute(CommonDirectoryAttributes.HomeDrive, out string homeDrive);
            this.HomeDrive = homeDrive;

            // UnixHomeDirectory:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UnixHomeDirectory, out string unixHomeDirectory, unicode: false);
            this.UnixHomeDirectory = unixHomeDirectory;

            // ProfilePath:
            dsObject.ReadAttribute(CommonDirectoryAttributes.ProfilePath, out string profilePath);
            this.ProfilePath = profilePath;

            // ScriptPath:
            dsObject.ReadAttribute(CommonDirectoryAttributes.ScriptPath, out string scriptPath);
            this.ScriptPath = scriptPath;

            // Notes / Comment:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Comment, out string notes);
            this.Notes = notes;
        }

        protected void LoadManager(DirectoryObject dsObject)
        {
            dsObject.ReadAttribute(CommonDirectoryAttributes.Manager, out DistinguishedName manager);
            this.Manager = manager?.ToString();
        }
    }
}
