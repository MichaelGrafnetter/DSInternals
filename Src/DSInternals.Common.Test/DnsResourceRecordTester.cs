namespace DSInternals.Common.Test
{
    using System;
    using DSInternals.Common.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DnsResoutceRecordTester
    {
        [TestMethod]
        public void DnsResoutceRecord_ZERO()
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
        public void DnsResoutceRecord_A1()
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
        public void DnsResoutceRecord_A2()
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
        public void DnsResoutceRecord_A3()
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
        public void DnsResoutceRecord_TXT()
        {
            byte[] input = "4600100005f00000a700000000000e10000000000000000019676f6f676c652d736974652d766572696669636174696f6e3d2b72584f78795a6f756e6e5a617341385a376f6144336331344a646a5339614b535776735231456255534951".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "@", input);
            Assert.AreEqual(ResourceRecordType.TXT, record.Type);
            Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
            Assert.AreEqual("( \"google-site-verification=\" \"rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ\" )", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_MX()
        {
            byte[] input = "16000f0005f00000a600000000000e1000000000aaba3800000a1203046d61696c07636f6e746f736f03636f6d00".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "@", input);
            Assert.AreEqual(ResourceRecordType.MX, record.Type);
            Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
            Assert.AreEqual("10 mail.contoso.com.", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_NS()
        {
            byte[] input = "1a00020005f000006200000000000e10000000000000000018030a636f6e746f736f2d646307636f6e746f736f03636f6d00".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "@", input);
            Assert.AreEqual(ResourceRecordType.NS, record.Type);
            Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
            Assert.AreEqual("contoso-dc.contoso.com.", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_SRV()
        {
            byte[] input = "2100210005f0000029000000000002580000000043b9380000000064018519030b636f6e746f736f2d64633207636f6e746f736f03636f6d00".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "_ldap._tcp", input);
            Assert.AreEqual(ResourceRecordType.SRV, record.Type);
            Assert.AreEqual(TimeSpan.FromMinutes(10), record.TTL);
            Assert.AreEqual("0 100 389  contoso-dc2.contoso.com.", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_AAAA()
        {
            byte[] input = "10001c0005f00000a800000000000e10000000000000000020010000130f0000000009c0876a130b".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "www", input);
            Assert.AreEqual(ResourceRecordType.AAAA, record.Type);
            Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
            Assert.AreEqual("2001:0:130f::9c0:876a:130b", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_CNAME()
        {
            byte[] input = "1b00050005f000002400000000000258000000006fb9380019030b636f6e746f736f2d64633207636f6e746f736f03636f6d00".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "ldap2", input);
            Assert.AreEqual(ResourceRecordType.CNAME, record.Type);
            Assert.AreEqual(TimeSpan.FromMinutes(10), record.TTL);
            Assert.AreEqual("contoso-dc2.contoso.com.", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_SOA()
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
        public void DnsResoutceRecord_NSEC()
        {
            throw new AssertInconclusiveException("NSEC record type is not yet supported.");
        }

        [TestMethod]
        public void DnsResoutceRecord_NSEC3()
        {
            throw new AssertInconclusiveException("NSEC3 record type is not yet supported.");
        }

        [TestMethod]
        public void DnsResoutceRecord_RR()
        {
            throw new AssertInconclusiveException("RR record type is not yet supported.");
        }

        [TestMethod]
        public void DnsResoutceRecord_RRSIG()
        {
            throw new AssertInconclusiveException("RRSIG record type is not yet supported.");
        }

        [TestMethod]
        public void DnsResoutceRecord_KEY()
        {
            throw new AssertInconclusiveException("KEY record type is not yet supported.");
        }

        [TestMethod]
        public void DnsResoutceRecord_TLSA()
        {
            throw new AssertInconclusiveException("TLSA record type is not yet supported.");
        }

        [TestMethod]
        public void DnsResoutceRecord_DHCID()
        {
            byte[] input = "2300310005f00000af00000000000e1000000000000000000001013920fe5d1dceb3fd0ba3379756a70d73b17009f41d58bddbfcd6a2503956d8da".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "PC01", input);
            Assert.AreEqual(ResourceRecordType.DHCID, record.Type);
            Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
            Assert.AreEqual("\\# 35 0001013920fe5d1dceb3fd0ba3379756a70d73b17009f41d58bddbfcd6a2503956d8da", record.Data);
            // Or natively in the BASE64 encoding: Assert.AreEqual("AAEBOSD+XR3Os/0LozeXVqcNc7FwCfQdWL3b/NaiUDlW2No=", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_CAA()
        {
            // Note: this record type is not natively supported by Microsoft
            byte[] input = "1200010105f00000ab00000000000e10000000000000000000056973737565656e74727573742e6e6574".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "@", input);
            Assert.AreEqual(257, (ushort) record.Type); // TYPE257
            Assert.AreEqual(TimeSpan.FromHours(1), record.TTL);
            Assert.AreEqual("\\# 18 00056973737565656e74727573742e6e6574", record.Data);
        }

        [TestMethod]
        public void DnsResoutceRecord_PTR()
        {
            byte[] input = "1b000c0005f0000015000000000004b000000000c7ba380019030b434f4e544f534f2d50433107636f6e746f736f03636f6d00".HexToBinary();
            var record = DnsResourceRecord.Create("0.0.10.in-addr.arpa", "7", input);
            Assert.AreEqual(ResourceRecordType.PTR, record.Type);
            Assert.AreEqual(TimeSpan.FromMinutes(20), record.TTL);
            Assert.AreEqual("CONTOSO-PC1.contoso.com.", record.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DnsResoutceRecord_Malformed1()
        {
            // A record missing the last byte
            byte[] input = "0400010005f000003000000000000e1000000000000000000ad500".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "www", input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DnsResoutceRecord_Malformed2()
        {
            // A record with an incorrect version (0x07 instead of 0x05)
            byte[] input = "0400010007f000003000000000000e1000000000000000000ad50004".HexToBinary();
            var record = DnsResourceRecord.Create("contoso.com", "www", input);
        }
    }
}
