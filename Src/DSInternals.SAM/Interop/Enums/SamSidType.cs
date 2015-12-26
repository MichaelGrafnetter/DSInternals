using System;

namespace DSInternals.SAM.Interop
{
    // http://msdn.microsoft.com/en-us/library/windows/desktop/aa379601%28v=vs.85%29.aspx
    public enum SamSidType : int
    {
        User = 1,
        Group,
        Domain,
        Alias,
        WellKnownGroup,
        DeletedAccount,
        Invalid,
        Unknown,
        Computer,
        Label
    }
}
