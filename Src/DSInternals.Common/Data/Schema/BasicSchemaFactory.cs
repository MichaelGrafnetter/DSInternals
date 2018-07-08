namespace DSInternals.Common.Data
{
    using System.Collections.Generic;

    public static class BasicSchemaFactory
    {
        public static BasicSchema CreateSchema()
        {
            var attributes = new List<ISchemaAttribute>();
            attributes.Add(new BasicSchemaAttribute(CommonDirectoryAttributes.NameId, CommonDirectoryAttributes.Name, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.SAMAccountNameId, CommonDirectoryAttributes.SAMAccountName, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.SamAccountTypeId, CommonDirectoryAttributes.SamAccountType, AttributeSyntax.Int));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.NTHashId, CommonDirectoryAttributes.NTHash, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.NTHashHistoryId, CommonDirectoryAttributes.NTHashHistory, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.LMHashId, CommonDirectoryAttributes.LMHash, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.LMHashHistoryId, CommonDirectoryAttributes.LMHashHistory, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.SupplementalCredentialsId, CommonDirectoryAttributes.SupplementalCredentials, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.IsDeletedId, CommonDirectoryAttributes.IsDeleted, AttributeSyntax.Bool));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.SIDHistoryId, CommonDirectoryAttributes.SIDHistory, AttributeSyntax.Sid));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.DisplayNameId, CommonDirectoryAttributes.DisplayName, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.DescriptionId, CommonDirectoryAttributes.Description, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.GivenNameId, CommonDirectoryAttributes.GivenName, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.SurnameId, CommonDirectoryAttributes.Surname, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.UserAccountControlId, CommonDirectoryAttributes.UserAccountControl, AttributeSyntax.Int));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.LastLogonId, CommonDirectoryAttributes.LastLogon, AttributeSyntax.Int64));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.UserPrincipalNameId, CommonDirectoryAttributes.UserPrincipalName, AttributeSyntax.UnicodeString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.PrimaryGroupIdId, CommonDirectoryAttributes.PrimaryGroupId, AttributeSyntax.Int));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.CurrentValueId, CommonDirectoryAttributes.CurrentValue, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.SecurityDescriptorId, CommonDirectoryAttributes.SecurityDescriptor, AttributeSyntax.SecurityDescriptor));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.AdminCountId, CommonDirectoryAttributes.AdminCount, AttributeSyntax.Int));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.ServicePrincipalNameId, CommonDirectoryAttributes.ServicePrincipalName, AttributeSyntax.String));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.PKIRoamingTimeStampId, CommonDirectoryAttributes.PKIRoamingTimeStamp, AttributeSyntax.OctetString));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.PKIDPAPIMasterKeysId, CommonDirectoryAttributes.PKIDPAPIMasterKeys, AttributeSyntax.DNWithBinary));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.PKIAccountCredentialsId, CommonDirectoryAttributes.PKIAccountCredentials, AttributeSyntax.DNWithBinary));
            attributes.Add((ISchemaAttribute)new BasicSchemaAttribute(CommonDirectoryAttributes.KeyCredentialLinkId, CommonDirectoryAttributes.KeyCredentialLink, AttributeSyntax.DNWithBinary));

            return new BasicSchema(attributes);
        }
    }
}
