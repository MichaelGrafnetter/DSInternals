using DSInternals.Common.Data;

namespace DSInternals.Common.Test;
[TestClass]
public class DistinguishedNameTester
{
    [TestMethod]
    public void DistinguishedName_DCNaming()
    {
        string dnStr = @"CN=John,OU=Employees,DC=adatum,DC=com";
        var dn = new DistinguishedName(dnStr);
        Assert.AreEqual(dn.ToString(), dnStr);
    }

    [TestMethod]
    public void DistinguishedName_EscapedComma()
    {
        string dnStr = @"CN=Doe\, John,OU=Employees,DC=adatum,DC=com";
        var dn = new DistinguishedName(dnStr);
        Assert.AreEqual(dnStr, dn.ToString());
    }

    [TestMethod]
    public void DistinguishedName_SpaceDot()
    {
        string dnStr = @"CN=John,OU=Employees,O=adatum Inc.,C=US";
        var dn = new DistinguishedName(dnStr);
        Assert.AreEqual(dn.ToString(), dnStr);
        Assert.AreEqual(4, dn.Components.Count);
    }

    [TestMethod]
    public void DistinguishedName_LongSpaces()
    {
        var dn = new DistinguishedName(@"    CN     =    John  , OU =  Employees,DC   =    adatum,DC = com");
        Assert.AreEqual(dn.ToString(), "CN=John,OU=Employees,DC=adatum,DC=com");
    }

    [TestMethod]
    public void DistinguishedName_Empty()
    {
        var dn = new DistinguishedName(String.Empty);
        Assert.AreEqual(dn.ToString(), String.Empty);
    }

    [TestMethod]
    public void DistinguishedName_AllSpaces()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new DistinguishedName("     "));
    }

    [TestMethod]
    public void DistinguishedName_BlankRDNValue()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new DistinguishedName("CN=,OU=Employees,DC=adatum,DC=com"));
    }

    [TestMethod]
    public void DistinguishedName_MalformedRDN()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new DistinguishedName("CN=John,Employees,DC=adatum,DC=com"));
    }

    [TestMethod]
    public void DistinguishedName_HexEncodedBinaryValueSingle()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName("CN=#324312af34e4");
        Assert.AreEqual(dn.ToString(), "CN=#324312af34e4");
    }

    [TestMethod]
    public void DistinguishedName_HexEncodedBinaryValueBeginning()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName("CN=#324312af34e4,OU=Employees,DC=adatum,DC=com");
        Assert.AreEqual(dn.ToString(), "CN=#324312af34e4,OU=Employees,DC=adatum,DC=com");
    }

    [TestMethod]
    public void DistinguishedName_HexEncodedBinaryValueMiddle()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName("CN=John,OU=#324312af34e4,DC=adatum,DC=com");
        Assert.AreEqual(dn.ToString(), "CN=John,OU=#324312af34e4,DC=adatum,DC=com");
    }

    [TestMethod]
    public void DistinguishedName_HexEncodedBinaryValueEnd()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName("CN=John,OU=Employees,DC=adatum,DC=#324312af34e4");
        Assert.AreEqual(dn.ToString(), "CN=John,OU=Employees,DC=adatum,DC=#324312af34e4");
    }

    [TestMethod]
    public void DistinguishedName_RDNComponentCount()
    {
        var dn = new DistinguishedName("CN=John,OU=Employees,DC=adatum,DC=com");
        Assert.AreEqual(dn.Components.Count, 4);
    }

    [TestMethod]
    public void DistinguishedName_SpacesAtBeginningAndEnd()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName(@"CN=\     John    \ ");
        Assert.AreEqual(dn.ToString(), @"CN=\     John    \ ");
    }

    [TestMethod]
    public void DistinguishedName_HexEscapeNonSpecialCharacter()
    {
        // \20 is the hex escape for a space character.
        var dn = new DistinguishedName(@"CN=John\20Doe,OU=Employees,DC=adatum,DC=com");
        Assert.AreEqual("John Doe", dn.Components[0].Value);
        Assert.AreEqual(@"CN=John Doe,OU=Employees,DC=adatum,DC=com", dn.ToString());

        // \C3\B6 is the RFC 4514 byte-by-byte UTF-8 encoding of 'ö'. The parser must accept
        // hex-escaped input (some tools produce it), but the writer must emit the literal
        // Unicode character — DRSR IDL_DRSCrackNames rejects the hex-escaped form for
        // DS_FQDN_1779_NAME, which broke Get-ADReplAccount on domains with German umlauts
        // in site names (issue #224).
        var dn2 = new DistinguishedName(@"CN=142SRV-DC,CN=Servers,CN=L\C3\B6hne,CN=Sites,CN=Configuration,DC=adatum,DC=local");
        Assert.AreEqual("Löhne", dn2.Components[2].Value);
        Assert.AreEqual("CN=142SRV-DC,CN=Servers,CN=Löhne,CN=Sites,CN=Configuration,DC=adatum,DC=local", dn2.ToString());

        // Round-trip a DN that already uses literal Unicode — non-ASCII chars must be
        // preserved verbatim, not converted to hex byte escapes.
        var dn3 = new DistinguishedName("CN=Löhne,CN=Sites,CN=Configuration,DC=adatum,DC=local");
        Assert.AreEqual("Löhne", dn3.Components[0].Value);
        Assert.AreEqual("CN=Löhne,CN=Sites,CN=Configuration,DC=adatum,DC=local", dn3.ToString());
    }

    [TestMethod]
    public void DistinguishedName_UnescapedSpecialCharacter()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new DistinguishedName(@"CN=Winkin, Blinkin, and Nod,OU=Employees,DC=adatum,DC=com"));
    }

    [TestMethod]
    public void DistinguishedName_OIDs()
    {
        string dnStr1 = @"OID.3.43.128=John";
        string dnStr2 = @"3.43.128=John";
        var dn1 = new DistinguishedName(dnStr1);
        var dn2 = new DistinguishedName(dnStr2);
        Assert.AreEqual(dn1.ToString(), dnStr1);
        Assert.AreEqual(dn1.Components.Count, 1);
        Assert.AreEqual(dn2.ToString(), dnStr2);
        Assert.AreEqual(dn2.Components.Count, 1);
    }

    [TestMethod]
    public void DistinguishedName_QuotesSingle()
    {
        var dn = new DistinguishedName(@"OU=""John is cool""");
        Assert.AreEqual(dn.ToString(), "OU=John is cool");
    }

    [TestMethod]
    public void DistinguishedName_QuotesBeginning()
    {
        var dn = new DistinguishedName(@"OU=""John"",OU=is,OU=cool");
        Assert.AreEqual(dn.ToString(), "OU=John,OU=is,OU=cool");
    }

    [TestMethod]
    public void DistinguishedName_QuotesMiddle()
    {
        var dn = new DistinguishedName(@"OU=John,OU=""is"",OU=cool");
        Assert.AreEqual(dn.ToString(), "OU=John,OU=is,OU=cool");
    }

    [TestMethod]
    public void DistinguishedName_QuotesEnd()
    {
        var dn = new DistinguishedName(@"OU=John,OU=is,OU=""cool""");
        Assert.AreEqual(dn.ToString(), "OU=John,OU=is,OU=cool");
    }

    [TestMethod]
    public void DistinguishedName_UnescapedSpecialChars()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName(@"OU="",=+<>#; """);
        Assert.AreEqual(dn.ToString(), @"OU=\,\=\+\<\>\#\;\ ");
    }

    [TestMethod]
    public void DistinguishedName_EscapedSpecialChars()
    {
        throw new AssertInconclusiveException("Support for this type of DN has not yet been implemented.");
        var dn = new DistinguishedName(@"OU=""\,\=\+\<\>\#\;\\\ """);
        Assert.AreEqual(dn.ToString(), @"OU=\,\=\+\<\>\#\;\\\ ");
    }

    [TestMethod]
    public void DistinguishedName_UnterminatedQuotes()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new DistinguishedName(@"OU=""John is cool"));
    }

    [TestMethod]
    public void DistinguishedName_AddParent()
    {
        var dn = new DistinguishedName("CN=Users");
        dn.AddParent(new DistinguishedName("DC=adatum,DC=com"));
        Assert.AreEqual("CN=Users,DC=adatum,DC=com", dn.ToString());
    }

    [TestMethod]
    public void DistinguishedName_AddParent_Null()
    {
        var dn = new DistinguishedName("CN=Users");
        dn.AddParent((DistinguishedName)null);
        dn.AddParent((DistinguishedNameComponent)null);
        Assert.AreEqual("CN=Users", dn.ToString());
    }

    [TestMethod]
    public void DistinguishedName_AddChild()
    {
        var dn = new DistinguishedName("DC=adatum,DC=com");
        dn.AddChild("CN", "Users");
        Assert.AreEqual("CN=Users,DC=adatum,DC=com", dn.ToString());
    }

    [TestMethod]
    public void DistinguishedName_AddChild_Null()
    {
        var dn = new DistinguishedName("DC=adatum,DC=com");
        dn.AddChild((DistinguishedName)null);
        dn.AddChild((DistinguishedNameComponent)null);
        Assert.AreEqual("DC=adatum,DC=com", dn.ToString());
    }

    [TestMethod]
    public void DistinguishedName_Parent_Null()
    {
        var dn = new DistinguishedName();
        Assert.AreEqual(String.Empty, dn.Parent.ToString());
    }

    [TestMethod]
    public void DistinguishedName_Parent_RDN()
    {
        var dn = new DistinguishedName("CN=Users");
        Assert.AreEqual(String.Empty, dn.Parent.ToString());
    }

    [TestMethod]
    public void DistinguishedName_Parent_Vector1()
    {
        var dn = new DistinguishedName("DC=adatum,DC=com");
        Assert.AreEqual("DC=com", dn.Parent.ToString());
    }

    [TestMethod]
    public void DistinguishedName_Parent_Vector2()
    {
        var dn = new DistinguishedName("CN=Users,DC=adatum,DC=com");
        Assert.AreEqual("DC=adatum,DC=com", dn.Parent.ToString());
    }

    [TestMethod]
    public void DistinguishedName_RootNamingContext_Null()
    {
        var dn = new DistinguishedName();
        Assert.AreEqual(String.Empty, dn.RootNamingContext.ToString());
    }

    [TestMethod]
    public void DistinguishedName_RootNamingContext_RDN()
    {
        var dn = new DistinguishedName("CN=Users");
        Assert.AreEqual(String.Empty, dn.RootNamingContext.ToString());
    }

    [TestMethod]
    public void DistinguishedName_RootNamingContext_Self()
    {
        var dn = new DistinguishedName("DC=adatum,DC=com");
        Assert.AreEqual("DC=adatum,DC=com", dn.RootNamingContext.ToString());
    }

    [TestMethod]
    public void DistinguishedName_RootNamingContext_Vector1()
    {
        var dn = new DistinguishedName("CN=Users,DC=adatum,DC=com");
        Assert.AreEqual("DC=adatum,DC=com", dn.RootNamingContext.ToString());
    }

    [TestMethod]
    public void DistinguishedName_RootNamingContext_Vector2()
    {
        var dn = new DistinguishedName("OU=Employees,OU=Marketing,DC=adatum,DC=com");
        Assert.AreEqual("DC=adatum,DC=com", dn.RootNamingContext.ToString());
    }

    [TestMethod]
    public void DistinguishedName_RootNamingContext_Vector3()
    {
        var dn = new DistinguishedName("DC=LON-CL1,cn=MicrosoftDNS,DC=DomainDnsZones,DC=adatum,DC=com");
        Assert.AreEqual("DC=DomainDnsZones,DC=adatum,DC=com", dn.RootNamingContext.ToString());
    }
}
