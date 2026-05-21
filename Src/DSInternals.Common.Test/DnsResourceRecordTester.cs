using DSInternals.Common.DNS;

namespace DSInternals.Common.Test;
[TestClass]
public class DnsResourceRecordTester
{
    [TestMethod]
    public void DnsResourceRecord_ZERO()
    {
        // Tombstoned (deleted) record
        byte[] input = "080000000500000012000000000000000000000000000000ba27f848b37bdb01".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "PC07", input);
        Assert.AreEqual(ResourceRecordType.ZERO, record.Type);
        Assert.AreEqual(TimeSpan.Zero, record.TTL);
        Assert.AreEqual((uint)18, record.Serial);
        Assert.AreEqual("\\# 0 ; Tombstoned at 2025-02-10 11:59:49Z", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_A1()
    {
        byte[] input = "0400010005f000003000000000000e1000000000000000000ad50004".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "www", input);
        Assert.AreEqual(ResourceRecordType.A, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual(ResourceRecordRank.Zone, record.Rank);
        Assert.IsNull(record.TimeStamp);
        Assert.AreEqual("10.213.0.4", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_A2()
    {
        byte[] input = "0400010005f00000a6000000000002580000000041b938000ad50003".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "www", input);
        Assert.AreEqual(ResourceRecordType.A, record.Type);
        Assert.AreEqual(TimeSpan.FromMinutes(10), record.TTL);
        Assert.AreEqual((uint)166, record.Serial);
        Assert.AreEqual(ResourceRecordRank.Zone, record.Rank);
        Assert.AreEqual(2025, record.TimeStamp.Value.Year);
        Assert.AreEqual(1, record.TimeStamp.Value.Month);
        Assert.AreEqual("10.213.0.3", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_A3()
    {
        byte[] input = "0400010005f000002d000000000004b00000000044b938000ad50007".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "www", input);
        Assert.AreEqual(ResourceRecordType.A, record.Type);
        Assert.AreEqual(TimeSpan.FromMinutes(20), record.TTL);
        Assert.AreEqual((uint)45, record.Serial);
        Assert.AreEqual(ResourceRecordRank.Zone, record.Rank);
        Assert.AreEqual(2025, record.TimeStamp.Value.Year);
        Assert.AreEqual(1, record.TimeStamp.Value.Month);
        Assert.AreEqual("10.213.0.7", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_TXT()
    {
        byte[] input = "4600100005f00000a700000000000e10000000000000000019676f6f676c652d736974652d766572696669636174696f6e3d2b72584f78795a6f756e6e5a617341385a376f6144336331344a646a5339614b535776735231456255534951".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.TXT, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("( \"google-site-verification=\" \"rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ\" )", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_MX()
    {
        byte[] input = "16000f0005f00000a600000000000e1000000000aaba3800000a1203046d61696c07636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.MX, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("10 mail.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_NS()
    {
        byte[] input = "1a00020005f000006200000000000e10000000000000000018030a636f6e746f736f2d646307636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.NS, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("contoso-dc.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_SRV()
    {
        byte[] input = "2100210005f0000029000000000002580000000043b9380000000064018519030b636f6e746f736f2d64633207636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "_ldap._tcp", input);
        Assert.AreEqual(ResourceRecordType.SRV, record.Type);
        Assert.AreEqual(TimeSpan.FromMinutes(10), record.TTL);
        Assert.AreEqual("0 100 389  contoso-dc2.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_AAAA()
    {
        byte[] input = "10001c0005f00000a800000000000e10000000000000000020010000130f0000000009c0876a130b".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "www", input);
        Assert.AreEqual(ResourceRecordType.AAAA, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("2001:0:130f::9c0:876a:130b", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_CNAME()
    {
        byte[] input = "1b00050005f000002400000000000258000000006fb9380019030b636f6e746f736f2d64633207636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "ldap2", input);
        Assert.AreEqual(ResourceRecordType.CNAME, record.Type);
        Assert.AreEqual(TimeSpan.FromMinutes(10), record.TTL);
        Assert.AreEqual("contoso-dc2.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_SOA()
    {
        byte[] input = "4800060005f00000a600000000000e100000000000000000000000a500000384000002580001518000000e1018030a636f6e746f736f2d646307636f6e746f736f03636f6d0018030a686f73746d617374657207636f6e746f736f03636f6d00".HexToBinary();
        string expectedData = @"contoso-dc.contoso.com. hostmaster.contoso.com. (
                                                                            165          ; serial number
                                                                            900          ; refresh
                                                                            600          ; retry
                                                                            86400        ; expire
                                                                            3600       ) ; default TTL";

        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.SOA, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual(expectedData, record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_NSEC()
    {
        // NSEC: next domain "next.example.com." then bitmap covering A, NS, RRSIG, NSEC.
        byte[] input = "1c002f0005f00000a600000000000e1000000000000000001203046e657874076578616d706c6503636f6d000006600000000003".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.NSEC, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("next.example.com. A NS RRSIG NSEC", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_NSEC3()
    {
        // NSEC3 (MS-DNSP order: alg, flags, iter, saltlen, hashlen, salt, hash, bitmap).
        // Hash is 20 bytes of 0x88 → base32hex "h248h248h248h248h248h248h248h248".
        byte[] input = "2600320005f00000a600000000000e1000000000000000000100000c0414aabbccdd88888888888888888888888888888888888888880006600000000003".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.NSEC3, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("1 0 12 aabbccdd h248h248h248h248h248h248h248h248 A NS RRSIG NSEC", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_NSEC3PARAM()
    {
        // NSEC3PARAM: alg=1 (SHA-1), flags=0, iterations=12, salt=aabbccdd.
        byte[] input = "0900330005f00000a600000000000e1000000000000000000100000c04aabbccdd".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.NSEC3PARAM, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("1 0 12 aabbccdd", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_DS()
    {
        // DS: keytag=60485, algorithm=5 (RSASHA1), digest type=1 (SHA-1), SHA-1 digest sample from RFC 4034.
        byte[] input = "18002b0005f00000a600000000000e100000000000000000ec4505012bb183af5f22588179a53b0a98631fad1a292118".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "dskey", input);
        Assert.AreEqual(ResourceRecordType.DS, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("60485 5 1 2bb183af5f22588179a53b0a98631fad1a292118", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_RRSIG()
    {
        // RRSIG covering A records, alg=5, labels=3, original TTL=3600,
        // expiration=2024-01-31 23:59:59 UTC (0x65BADEFF), inception=2024-01-01 00:00:00 UTC (0x65920080),
        // key tag=12345, signer=example.com., signature=16 bytes of 0xAA.
        byte[] input = "31002e0005f00000a600000000000e1000000000000000000001050300000e1065badeff6592008030390d02076578616d706c6503636f6d00aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.RRSIG, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("A 5 3 3600 20240131235959 20240101000000 12345 example.com. qqqqqqqqqqqqqqqqqqqqqg==", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_SIG()
    {
        // SIG sample based on RFC 2535 section 7.2, encoded as DNS_RPC_RECORD_SIG.
        byte[] input = "6d00180005f00000a600000000000e100000000000000000001e010200000e1032cb25a532ae8844085f090203666f6f036e696c000086000cff1ddf360dc90c16d84338c1754576c94425c531fdfc647c1787d449788b13c58697c71449716ef2a85a6be30d30a67e2632d97fbc5e9163c08087737f7c9335ac4cc2a5c68be9cf61670933".HexToBinary();
        var record = DnsResourceRecord.Create("foo.nil", "foo", input);
        Assert.AreEqual(ResourceRecordType.SIG, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("NXT 1 2 3600 19970102030405 19961211100908 2143 foo.nil. AIYADP8d3zYNyQwW2EM4wXVFdslEJcUx/fxkfBeH1El4ixPFhpfHFElxbvKoWmvjDTCmfiYy2X+8XpFjwICHc398kzWsTMKlxovpz2FnCTM=", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_NXT()
    {
        // NXT sample based on RFC 2535 section 5.4, encoded as DNS_RPC_RECORD_NXT.
        byte[] input = "18001e0005f00000a600000000000e1000000000000000000200014082001003066d656469756d03666f6f036e696c00".HexToBinary();
        var record = DnsResourceRecord.Create("foo.nil", "big", input);
        Assert.AreEqual(ResourceRecordType.NXT, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("medium.foo.nil. A MX SIG NXT", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_NULL()
    {
        byte[] input = "03000a0005f00000a600000000000e100000000000000000010203".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.NULL, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("\\# 3 010203", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_DNSKEY_ZSK()
    {
        // DNSKEY ZSK: flags=256, protocol=3, algorithm=8 (RSASHA256), 8-byte mock key 1122334455667788
        byte[] input = "0c00300005f00000a600000000000e1000000000000000000100030811223344 55667788".Replace(" ", "").HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.DNSKEY, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("256 3 8 ESIzRFVmd4g= ; key id = 5469", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_DNSKEY_KSK()
    {
        // DNSKEY KSK: flags=257, protocol=3, algorithm=8 (RSASHA256), 8-byte mock key 1122334455667788
        byte[] input = "0c00300005f00000a600000000000e1000000000000000000101030811223344 55667788".Replace(" ", "").HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.DNSKEY, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("257 3 8 ESIzRFVmd4g= ; key id = 5470", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_KEY()
    {
        // KEY (DNS_TYPE 25): same wire format as DNSKEY, here with flags=256, protocol=3, algorithm=8
        byte[] input = "0c00190005f00000a600000000000e1000000000000000000100030811223344 55667788".Replace(" ", "").HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.KEY, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("256 3 8 ESIzRFVmd4g= ; key id = 5469", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_WKS()
    {
        // WKS: 192.0.2.10, TCP, with services smtp (port 25) and http (port 80).
        // RFC 1035 §3.4.2 bitmap (11 bytes): byte 3 = 0x40 (bit 1 from MSB → port 25), byte 10 = 0x80 (bit 0 from MSB → port 80).
        byte[] input = "10000b0005f00000a600000000000e100000000000000000c000020a060000004000000000000080".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "www", input);
        Assert.AreEqual(ResourceRecordType.WKS, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("192.0.2.10 tcp ( smtp http )", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_MINFO()
    {
        byte[] input = "30000e0005f00000a600000000000e10000000000000000018030a686f73746d617374657207636f6e746f736f03636f6d001403066572726f727307636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.MINFO, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("hostmaster.contoso.com. errors.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_RP()
    {
        byte[] input = "2c00110005f00000a600000000000e10000000000000000013030561646d696e07636f6e746f736f03636f6d0015030772702d696e666f07636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.RP, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("admin.contoso.com. rp-info.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_ATMA_AESA()
    {
        byte[] input = "1500220005f00000a600000000000e1000000000000000000239246f123456789abcdefa012300123456789a00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "atm", input);
        Assert.AreEqual(ResourceRecordType.ATMA, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("39.246f.123456789abcdefa0123.00123456789a.00", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_ATMA_E164()
    {
        byte[] input = "0c00220005f00000a600000000000e1000000000000000000131 2e 32 33 34 35 2e 36 37 38 39".Replace(" ", string.Empty).HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "atma-e164", input);
        Assert.AreEqual(ResourceRecordType.ATMA, record.Type);
        Assert.AreEqual("+1.2345.6789", record.Data);
    }

[TestMethod]
    public void DnsResourceRecord_NAPTR()
    {
        byte[] input = "4500230005f00000a600000000000e1000000000000000000064000a0175074532552b7369701b215e2e2a24217369703a696e666f406578616d706c652e636f6d2119030b7265706c6163656d656e74076578616d706c6503636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "enum", input);
        Assert.AreEqual(ResourceRecordType.NAPTR, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("100 10 \"u\" \"E2U+sip\" \"!^.*$!sip:info@example.com!\" replacement.example.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_WINS()
    {
        byte[] input = "180001ff05f00000a600000000000e1000000000000000000000010005000000100e0000020000000a0000010a000002".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(ResourceRecordType.WINS, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("LOCAL L5 C3600 ( 10.0.0.1 10.0.0.2 )", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_WINSR()
    {
        byte[] input = "1b0002ff05f00000a600000000000e1000000000000000000000000005000000100e00000d0207636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("0.0.10.in-addr.arpa", "@", input);
        Assert.AreEqual(ResourceRecordType.WINSR, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("L5 C3600 contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_TLSA()
    {
        // TLSA: usage=0, selector=0, matching type=1 (SHA-256), 32-byte SHA-256 sample from RFC 6698.
        byte[] input = "2300340005f00000a600000000000e100000000000000000000001d2abde240d7cd3ee6b4b28c54df034b97983a1d16e8a410e4561cb106618e971".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "_443._tcp.www", input);
        Assert.AreEqual(ResourceRecordType.TLSA, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("0 0 1 D2ABDE240D7CD3EE6B4B28C54DF034B97983A1D16E8A410E4561CB106618E971", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_DHCID()
    {
        byte[] input = "2300310005f00000af00000000000e1000000000000000000001013920fe5d1dceb3fd0ba3379756a70d73b17009f41d58bddbfcd6a2503956d8da".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "PC01", input);
        Assert.AreEqual(ResourceRecordType.DHCID, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("AAEBOSD+XR3Os/0LozeXVqcNc7FwCfQdWL3b/NaiUDlW2No=", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_CAA()
    {
        // Note: this record type is not natively supported by Microsoft
        byte[] input = "1200010105f00000ab00000000000e10000000000000000000056973737565656e74727573742e6e6574".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "@", input);
        Assert.AreEqual(257, (ushort)record.Type); // TYPE257
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual("\\# 18 00056973737565656e74727573742e6e6574", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_PTR()
    {
        byte[] input = "1b000c0005f0000015000000000004b000000000c7ba380019030b434f4e544f534f2d50433107636f6e746f736f03636f6d00".HexToBinary();
        var record = DnsResourceRecord.Create("0.0.10.in-addr.arpa", "7", input);
        Assert.AreEqual(ResourceRecordType.PTR, record.Type);
        Assert.AreEqual(TimeSpan.FromMinutes(20), record.TTL);
        Assert.AreEqual("CONTOSO-PC1.contoso.com.", record.Data);
    }

    [TestMethod]
    public void DnsResourceRecord_RecordWireFormat_A()
    {
        byte[] input = "0400010005f01000a600000000000e10000000000000000001020304".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "www", input);
        Assert.AreEqual(ResourceRecordType.A, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual(ResourceRecordFlags.RecordWireFormat, record.Flags);
        Assert.AreEqual("\\# 4 01020304", record.Data);
        StringAssert.Contains(record.ToString(), "TYPE1");
    }

    [TestMethod]
    public void DnsResourceRecord_RecordWireFormat_Empty()
    {
        byte[] input = "0000210005f01000a600000000000e100000000000000000".HexToBinary();
        var record = DnsResourceRecord.Create("contoso.com", "_ldap._tcp", input);
        Assert.AreEqual(ResourceRecordType.SRV, record.Type);
        Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
        Assert.AreEqual(ResourceRecordFlags.RecordWireFormat, record.Flags);
        Assert.AreEqual("\\# 0", record.Data);
        StringAssert.Contains(record.ToString(), "TYPE33");
    }

    [TestMethod]
    public void DnsResourceRecord_Malformed1()
    {
        // A record missing the last byte
        byte[] input = "0400010005f000003000000000000e1000000000000000000ad500".HexToBinary();
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => DnsResourceRecord.Create("contoso.com", "www", input));
    }

    [TestMethod]
    public void DnsResourceRecord_Malformed2()
    {
        // A record with an incorrect version (0x07 instead of 0x05)
        byte[] input = "0400010007f000003000000000000e1000000000000000000ad50004".HexToBinary();
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => DnsResourceRecord.Create("contoso.com", "www", input));
    }
}
