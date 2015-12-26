using DSInternals.Common;
using System;
using System.Runtime.InteropServices;

namespace DSInternals.SAM.Interop
{
    // See http://msdn.microsoft.com/en-us/library/cc245612.aspx
    [StructLayout(LayoutKind.Sequential)]
    internal struct SamUserInternal1Information
    {
        internal SamUserInternal1Information(byte[] ntHash, byte[] lmHash = null)
        {
            Validator.AssertNotNull(ntHash, "ntHash");
            Validator.AssertLength(ntHash, NTHashSize, "ntHash");

            this.NtOwfPassword = ntHash;
            this.NtPasswordPresent = true;
            
            if(lmHash != null)
            {
                Validator.AssertLength(lmHash, LMHashSize, "lmHash");
                this.LmOwfPassword = lmHash;
                this.LmPasswordPresent = true;
            }
            else
            {
                this.LmOwfPassword = null;
                this.LmPasswordPresent = false;
            }
            this.PasswordExpired = false;
        }
        internal const int NTHashSize = 16;
        
        internal const int LMHashSize = 16;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = NTHashSize)]
        internal byte[] NtOwfPassword;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMHashSize)]
        internal byte[] LmOwfPassword;
        
        [MarshalAs(UnmanagedType.I1)]
        internal bool NtPasswordPresent;
        
        [MarshalAs(UnmanagedType.I1)]
        internal bool LmPasswordPresent;
        
        [MarshalAs(UnmanagedType.I1)]
        internal bool PasswordExpired;
    }
}