namespace DSInternals.ADSI.Test;
[TestClass]
public class AdsiClientTester
{
    [TestMethod]
    public void EscapeLdapFilterString_EmptyInput()
    {
        Assert.AreEqual(string.Empty, AdsiClient.EscapeLdapFilterString(string.Empty));
    }

    [TestMethod]
    public void EscapeLdapFilterString_PlainInputUnchanged()
    {
        string input = "Administrator";
        Assert.AreEqual(input, AdsiClient.EscapeLdapFilterString(input));
    }

    [TestMethod]
    public void EscapeLdapFilterString_SidString()
    {
        // SDDL SID strings contain only digits, 'S' and '-' — nothing to escape.
        string input = "S-1-5-21-3288850392-3299536932-2614793081-1000";
        Assert.AreEqual(input, AdsiClient.EscapeLdapFilterString(input));
    }

    [TestMethod]
    public void EscapeLdapFilterString_GuidString()
    {
        // GUIDs in "D" format contain only hex digits and '-' — nothing to escape.
        string input = "2f9e3c1a-7b6d-4e5f-9a8c-1b2d3e4f5a6b";
        Assert.AreEqual(input, AdsiClient.EscapeLdapFilterString(input));
    }

    [TestMethod]
    public void EscapeLdapFilterString_Backslash()
    {
        Assert.AreEqual("\\5c", AdsiClient.EscapeLdapFilterString("\\"));
    }

    [TestMethod]
    public void EscapeLdapFilterString_Asterisk()
    {
        Assert.AreEqual("\\2a", AdsiClient.EscapeLdapFilterString("*"));
    }

    [TestMethod]
    public void EscapeLdapFilterString_OpeningParenthesis()
    {
        Assert.AreEqual("\\28", AdsiClient.EscapeLdapFilterString("("));
    }

    [TestMethod]
    public void EscapeLdapFilterString_ClosingParenthesis()
    {
        Assert.AreEqual("\\29", AdsiClient.EscapeLdapFilterString(")"));
    }

    [TestMethod]
    public void EscapeLdapFilterString_NullCharacter()
    {
        Assert.AreEqual("\\00", AdsiClient.EscapeLdapFilterString("\0"));
    }

    [TestMethod]
    public void EscapeLdapFilterString_AllSpecialsTogether()
    {
        Assert.AreEqual("\\5c\\2a\\28\\29\\00", AdsiClient.EscapeLdapFilterString("\\*()\0"));
    }

    [TestMethod]
    public void EscapeLdapFilterString_MixedContent()
    {
        // A sAMAccountName containing each metacharacter interleaved with letters.
        string input = "user(name)*\\value";
        string expected = "user\\28name\\29\\2a\\5cvalue";
        Assert.AreEqual(expected, AdsiClient.EscapeLdapFilterString(input));
    }

    [TestMethod]
    public void EscapeLdapFilterString_InjectionAttempt()
    {
        // Classic LDAP injection payload: must not break out of the assertion value.
        string input = "*)(objectClass=*";
        string expected = "\\2a\\29\\28objectClass=\\2a";
        Assert.AreEqual(expected, AdsiClient.EscapeLdapFilterString(input));
    }

    [TestMethod]
    public void EscapeLdapFilterString_UnicodeIsPreserved()
    {
        // Non-ASCII characters are not on the RFC 4515 special-character list.
        string input = "Müller";
        Assert.AreEqual(input, AdsiClient.EscapeLdapFilterString(input));
    }

    [TestMethod]
    public void EscapeLdapFilterString_NullInputThrows()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => AdsiClient.EscapeLdapFilterString(null!));
    }
}
