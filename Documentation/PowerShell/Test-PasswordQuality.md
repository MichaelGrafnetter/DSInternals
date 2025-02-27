---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Test-PasswordQuality.md
schema: 2.0.0
---

# Test-PasswordQuality

## SYNOPSIS
Performs AD audit, including checks for weak, duplicate, default and empty passwords. Accepts input from the Get-ADReplAccount and Get-ADDBAccount cmdlets.

## SYNTAX

### SingleFile (Default)
```
Test-PasswordQuality [-Account] <DSAccount> [-SkipDuplicatePasswordTest] [-IncludeDisabledAccounts]
 [-WeakPasswords <String[]>] [-WeakPasswordsFile <String>] [-WeakPasswordHashesFile <String>]
 [-WeakPasswordHashesSortedFile <String>] [<CommonParameters>]
```

### MultiFile
```
Test-PasswordQuality [-Account] <DSAccount> [-SkipDuplicatePasswordTest] [-IncludeDisabledAccounts]
 [-WeakPasswords <String[]>] [-WeakPasswordsFile <String>] [-WeakPasswordHashesFile <String>]
 [-WeakPasswordHashesSortedFilePath <String>] [<CommonParameters>]
```

## DESCRIPTION

The Test-PasswordQuality cmdlet is a simple tool for Active Directory password auditing. It can detect weak, duplicate, default, non-expiring or empty passwords and find accounts that are violating security best practices. The cmdlet accepts output of the Get-ADDBAccount and Get-ADReplAccount cmdlets, so both offline (ntds.dit) and online (DCSync) password analysis can be done.

Lists of leaked passwords that can be obtained from HaveIBeenPwned are fully supported. Be sure to download the list that is marked as "NTLM (ordered by hash)" and extract the archive to your HDD.

Although the cmdlet output is formatted in a human readable fashion, it is still an object, whose properties can be accessed separately (e.g. $result.WeakPassword) to produce a desired output. When scripted, it can be used to audit Active Directory passwords on a regular basis.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBAccount -All -DatabasePath ntds.dit -BootKey acdba64a3929261b04e5270c3ef973cf -Properties Secrets |
            Test-PasswordQuality -WeakPasswordHashesSortedFilePath P:\pwnedpasswords_ntlm
<# Sample Output:

Active Directory Password Quality Report
----------------------------------------

Passwords of these accounts are stored using reversible encryption:
  CONTOSO\smith
  CONTOSO\doe

LM hashes of passwords of these accounts are present:
  CONTOSO\hodge

These accounts have no password set:
  CONTOSO\test01
  CONTOSO\test02

Passwords of these accounts have been found in the dictionary:
  CONTOSO\Administrator

These groups of accounts have the same passwords:
  Group 1:
    CONTOSO\graham
    CONTOSO\graham_admin
  Group 2:
    CONTOSO\admin
    CONTOSO\sql_svc01

These user accounts have the SamAccountName as password:
  CONTOSO\sccm_admin

These computer accounts have default passwords:
  CONTOSO\DESKTOP27$

Kerberos AES keys are missing from these accounts:
  CONTOSO\sql_svc01

Kerberos pre-authentication is not required for these accounts:
  CONTOSO\jboss

Only DES encryption is allowed to be used with these accounts:
  CONTOSO\sql_svc01

These accounts are susceptible to the Kerberoasting attack:
  CONTOSO\Administrator
  CONTOSO\sp_svc01
  CONTOSO\sql_svc02

These administrative accounts (current or former ones) are allowed to be delegated:
  CONTOSO\AdatumAdmin
  CONTOSO\Administrator

Passwords of these accounts will never expire:
  CONTOSO\admin
  CONTOSO\sql_svc01

These accounts are not required to have a password:
  CONTOSO\gonzales

These accounts that require smart card authentication have a password:
  CONTOSO\smithj
  CONTOSO\jonesp
#>
```

Performs an offline credential hygiene audit of AD database against HIBP.

### Example 2
```powershell
PS C:\> $results = Get-ADReplAccount -All -Server LON-DC1 -Properties Secrets |
                   Test-PasswordQuality -WeakPasswords 'Pa$$w0rd','April2019' `
                                        -WeakPasswordHashesSortedFile pwned-passwords-ntlm-ordered-by-hash-v8.txt
```

Performs an online credential hygiene audit of AD against HIBP + a custom wordlist.

### Example 3
```powershell
PS C:\> $pwnedUsers = $accounts |
                      Test-PasswordQuality -WeakPasswordsFile rockyou.txt -SkipDuplicatePasswordTest |
                      Select-Object -ExpandProperty WeakPassword
```

Performs a dictionary attack against a set of accounts. The Test-PasswordQuality cmdlet always returns structured data.

### Example 4
```powershell
PS C:\> Get-ADDBAccount -All -DatabasePath ntds.dit -BootKey $key -Properties Secrets,DistinguishedName |
            where DistinguishedName -like '*OU=Employees,DC=contoso,DC=com' |
            Test-PasswordQuality -IncludeDisabledAccounts -WeakPasswordHashesSortedFilePath P:\pwnedpasswords_ntlm
```

Performs an offline credential hygiene audit of a selected OU from AD database against HIBP.

### Example 5
```powershell
PS C:\> $contosoAccounts = Get-ADReplAccount -All -Server LON-DC1.contoso.com
PS C:\> $adatumAccounts = Get-ADReplAccount -All -Server NYC-DC1.adatum.com -Credential (Get-Credential)
PS C:\> $contosoAccounts + $adatumAccounts | Test-PasswordQuality
<# Sample Output (Partial):

These groups of accounts have the same passwords:
  Group 1:
    ADATUM\smith
    ADATUM\doe
  Group 2:
    ADATUM\Administrator
    ADATUM\joe_admin
    CONTOSO\Administrator
    CONTOSO\joe_admin
#>
```

Performs a cross-forest duplicate password discovery. Any number of Get-ADReplAccount and Get-ADDBAccount cmdlet outputs can be combined together, as long as the computer has enough memory.

## PARAMETERS

### -Account
Active Directory account to check. The accounts are typically piped in from the Get-ADDBAccount and Get-ADReplAccount cmdlets.

```yaml
Type: DSAccount
Parameter Sets: (All)
Aliases: ADAccount, DSAccount

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -IncludeDisabledAccounts
Process even accounts that are disabled. Such accounts are skipped otherwise.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipDuplicatePasswordTest
Do not compare account hashes with each other.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswordHashesFile
Path to a file that contains NT hashes of weak passwords, one hash in HEX format per line. For performance reasons, the -WeakPasswordHashesSortedFile parameter should be used instead.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswordHashesSortedFile
Path to a file that contains NT hashes of weak passwords, one hash in HEX format per line. The hashes must be sorted alphabetically, because a binary search is performed. This parameter is typically used with a list of leaked password hashes from HaveIBeenPwned, v8 and earlier.

```yaml
Type: String
Parameter Sets: SingleFile
Aliases: HIBPFile, HaveIBeenPwnedFile

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswordHashesSortedFilePath
Path to a directory of files named as the first five characters of an NT hash (00000.txt - FFFFF.txt), each of which contain NT hashes of weak passwords, one hash in HEX format per line, starting with the 6th character in the hash. The hashes must be sorted alphabetically, because a binary search is performed. This parameter is typically used with a list of leaked password hashes from HaveIBeenPwned after v8.

```yaml
Type: String
Parameter Sets: MultiFile
Aliases: WeakPasswordHashesSortedDirectory, HIBPDirectory, HaveIBeenPwnedDirectory

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswords
List of passwords that are considered weak, e.g. Password123 or April2019. If more than a handful passwords are to be tested, the WeakPasswordsFile parameter should be used instead.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswordsFile
Path to a file that contains weak passwords, one password per line.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### DSInternals.Common.Data.DSAccount

## OUTPUTS

### DSInternals.PowerShell.PasswordQualityTestResult

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADReplAccount](Get-ADReplAccount.md)
