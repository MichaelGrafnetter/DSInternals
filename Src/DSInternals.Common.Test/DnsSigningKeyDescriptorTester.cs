using DSInternals.Common.Data;

namespace DSInternals.Common.Test;

[TestClass]
public class DnsSigningKeyDescriptorTester
{
    [TestMethod]
    public void DnsSigningKeyDescriptor_KSK()
    {
        string dnsZone = "contoso.com";
        byte[] binaryData = "010000002983ebe60c33d34d99892827434cb3d54d006900630072006f0073006f0066007400200053006f0066007400770061007200650020004b00650079002000530074006f0072006100670065002000500072006f0076006900640065007200000001000000010000000d0000000001000000000000803a0900803a0900002f0d0001000000805ce30301000000000000000000000049bd307dc2bade010000000000000000000000000000000000000000000000000000000000000000000000007b00460039003100440032003300380034002d0038003900350044002d0034003500410032002d0041004400460039002d004600300033003700360032003400410031003000370046007d000000000000007b00440044004600390034004500370045002d0030004500440038002d0034004200420042002d0039003400340037002d003000330046004500360036003200300032003800370034007d00000003000000000000000000".HexToBinary();

        DnsSigningKeyDescriptor descriptor = DnsSigningKeyDescriptor.Decode(dnsZone, binaryData);

        Assert.IsNotNull(descriptor);
        Assert.AreEqual(dnsZone, descriptor.ZoneName);
        Assert.AreEqual(256, descriptor.KeyLength);
        Assert.AreEqual(DnsSigningAlgorithm.P256_SHA256, descriptor.CryptoAlgorithm);
        Assert.AreEqual(Guid.Parse("{E6EB8329-330C-4DD3-9989-2827434CB3D5}"), descriptor.KeyId);
        Assert.AreEqual(Guid.Parse("{F91D2384-895D-45A2-ADF9-F037624A107F}"), descriptor.ActiveKey);
        Assert.AreEqual(Guid.Parse("{DDF94E7E-0ED8-4BBB-9447-03FE66202874}"), descriptor.StandbyKey);
        Assert.IsNull(descriptor.NextKey);
        Assert.AreEqual("Microsoft Software Key Storage Provider", descriptor.KeyStorageProvider);
        Assert.AreEqual(DnsSigningKeyType.KSK, descriptor.KeyType);
        Assert.AreEqual(TimeSpan.FromDays(755), descriptor.RolloverPeriod);
    }

    [TestMethod]
    public void DnsSigningKeyDescriptor_ZSK()
    {
        // Arrange
        string dnsZone = "contoso.com";
        byte[] binaryData = "0100000048318c2eb3042d46ae45d87fe631fcee4d006900630072006f0073006f0066007400200053006f0066007400770061007200650020004b00650079002000530074006f0072006100670065002000500072006f007600690064006500720000000100000000000000080000000004000000000000803a0900803a0900002f0d000000000000a7760001000000000000000000000070ae7c5033b0dc010000000000000000000000000000000000000000672c4d677a69dc0100000000000000007b00370034003800380044003000360033002d0035004200450042002d0034003900360032002d0039003100310042002d004200410041004300430046003100420042003900310044007d000000000000000000000000007b00310043003300370033003700450041002d0046003900410034002d0034003900300032002d0042003600410031002d004400450042004300360032003200370045003600320037007d00000003000000".HexToBinary();

        DnsSigningKeyDescriptor descriptor = DnsSigningKeyDescriptor.Decode(dnsZone, binaryData);

        Assert.IsNotNull(descriptor);
        Assert.AreEqual(dnsZone, descriptor.ZoneName);
        Assert.AreEqual(1024, descriptor.KeyLength);
        Assert.AreEqual(DnsSigningAlgorithm.RSA_SHA256, descriptor.CryptoAlgorithm);
        Assert.AreEqual(Guid.Parse("{2E8C3148-04B3-462D-AE45-D87FE631FCEE}"), descriptor.KeyId);
        Assert.AreEqual(Guid.Parse("{7488D063-5BEB-4962-911B-BAACCF1BB91D}"), descriptor.ActiveKey);
        Assert.AreEqual(Guid.Parse("{1C3737EA-F9A4-4902-B6A1-DEBC6227E627}"), descriptor.NextKey);
        Assert.IsNull(descriptor.StandbyKey);
        Assert.AreEqual("Microsoft Software Key Storage Provider", descriptor.KeyStorageProvider);
        Assert.AreEqual(DnsSigningKeyType.ZSK, descriptor.KeyType);
        Assert.AreEqual(TimeSpan.FromDays(90), descriptor.RolloverPeriod);
    }
}
