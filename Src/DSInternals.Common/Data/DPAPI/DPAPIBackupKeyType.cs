using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Common.Data
{
    public enum DPAPIBackupKeyType : byte
    {
        Unknown = 0,
        LegacyKey,
        RSAKey,
        PreferredLegacyKeyPointer,
        PreferredRSAKeyPointer
    }
}
