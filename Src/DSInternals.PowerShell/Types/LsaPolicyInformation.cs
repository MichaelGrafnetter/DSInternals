using System.Security.Principal;
using DSInternals.SAM;

namespace DSInternals.PowerShell;

public sealed class LsaPolicyInformation
{
    public LsaDnsDomainInformation DnsDomain { get; set; }
    public LsaDomainInformation Domain { get; set; }
    public LsaDomainInformation LocalDomain { get; set; }
    public SecurityIdentifier MachineAccountSid { get; set; }
}
