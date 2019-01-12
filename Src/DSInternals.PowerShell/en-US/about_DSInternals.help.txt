TOPIC
    about_DSInternals

SHORT DESCRIPTION
    The Directory Services Internals (DSInternals) PowerShell Module exposes
    several internal and undocumented features of Active Directory.

LONG DESCRIPTION
    The main features of the DSInternals PowerShell Module include:
    - Offline ntds.dit file manipulation, including hash dumping, password
    resets, group membership changes, SID History injection and
    enabling/disabling accounts.
    - Online password hash dumping through the Directory Replication Service
    Remote Protocol (MS-DRSR).
    - Active Directory password auditing that discovers accounts sharing the
    same passwords or having passwords in a public database like HaveIBeenPwned
    or in a custom dictionary.
    - Domain or local account password hash injection through the Security
    Account Manager Remote Protocol (MS-SAMR) or directly into the database.
    - LSA Policy modification through the Local Security Authority Remote
    Protocol (MS-LSAD / LSARPC).
    - Extracting credential roaming data and DPAPI domain backup keys, either
    online through MS-DRSR and LSARPC or offline from ntds.dit.
    - Bare-metal recovery of domain controllers from just IFM backups (ntds.dit
    + SYSVOL).
    - Password hash calculation, including NT hash, LM hash and kerberos keys.

NOTE
    Features exposed through these tools are not supported by Microsoft.
    Improper use might cause irreversible damage to domain controllers or
    negatively impact domain security.

SEE ALSO
    Get-ADDBAccount
    Get-ADReplAccount
    Test-PasswordQuality
    New-ADDBRestoreFromMediaScript
    ConvertTo-NTHash

