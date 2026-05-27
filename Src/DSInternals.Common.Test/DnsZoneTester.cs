using DSInternals.Common.DNS;

namespace DSInternals.Common.Test;

[TestClass]
public class DnsZoneTester
{
    [TestMethod]
    public void DnsZone_IsReverseLookupZone_Forward()
    {
        var zone = DnsZone.Create("DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com", isSigned: false);
        Assert.AreEqual("contoso.com", zone.ZoneName);
        Assert.IsFalse(zone.IsReverseLookupZone);
    }

    [TestMethod]
    public void DnsZone_IsReverseLookupZone_Ip4Reverse()
    {
        var zone = DnsZone.Create("DC=1.168.192.in-addr.arpa,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com", isSigned: false);
        Assert.AreEqual("1.168.192.in-addr.arpa", zone.ZoneName);
        Assert.IsTrue(zone.IsReverseLookupZone);
    }

    [TestMethod]
    public void DnsZone_IsReverseLookupZone_Ip6Reverse()
    {
        var zone = DnsZone.Create("DC=0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.ip6.arpa,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com", isSigned: false);
        Assert.IsTrue(zone.IsReverseLookupZone);
    }

    [TestMethod]
    public void DnsZone_IsReverseLookupZone_CaseInsensitive()
    {
        var zone = DnsZone.Create("DC=1.168.192.IN-ADDR.ARPA,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com", isSigned: false);
        Assert.IsTrue(zone.IsReverseLookupZone);
    }
}
