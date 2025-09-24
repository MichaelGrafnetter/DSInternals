namespace DSInternals.SAM.Interop;

/// <summary>
/// User Information Class
/// <para>The USER_INFORMATION_CLASS enumeration indicates how to interpret the Buffer parameter for SamrSetInformationUser, SamrQueryInformationUser, SamrSetInformationUser2, and SamrQueryInformationUser2.</para>
/// <see>http://msdn.microsoft.com/en-us/library/cc245617.aspx</see>
/// </summary>
internal enum SamUserInformationClass : int
{
    GeneralInformation = 1,
    PreferencesInformation = 2,
    LogonInformation = 3,
    LogonHoursInformation = 4,
    AccountInformation = 5,
    NameInformation = 6,
    AccountNameInformation = 7,
    FullNameInformation = 8,
    PrimaryGroupInformation = 9,
    HomeInformation = 10,
    ScriptInformation = 11,
    ProfileInformation = 12,
    AdminCommentInformation = 13,
    WorkStationsInformation = 14,
    ControlInformation = 16,
    ExpiresInformation = 17,
    Internal1Information = 18,
    ParametersInformation = 20,
    AllInformation = 21,
    Internal4Information = 23,
    Internal5Information = 24,
    Internal4InformationNew = 25,
    Internal5InformationNew = 26
}
