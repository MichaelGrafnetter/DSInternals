using DSInternals.Common.Data;

namespace DSInternals.Common.Test;

[TestClass]
public class ManagedPasswordTester
{
    [TestMethod]
    public void ManagedPassword_Vector1()
    {
        // Sample value taken from a GMSA in AD
        byte[] blob = "01000000220100001000000012011a011609f270f541c315ffee9fcd22a98447b5c6e6fb7151cb020a2b017bb4e003647949967fc96f7c9ec3426b80901bb9c162867cbc68c520c4d7a431c3d9a670f8aa41d2ae5c0c08f27f8698b90c18a5a576e9933fb0cadaf8e661be2f58308c580866b1ae582ee50a9aa7c5d65a312dbbc3542c51c7e0b2d4c61e9763de481d9963367273aa72b53c2e402e31c6cd38e7785ad06639cdfa07738d19ae20c370e06787ad2f600823c505fc9dd32b3f06505da37b86b298d3650140af83c1f01c907964d182ea0efb19e74c949f58123fdecb41f78ed0eabbde31bb46afd3134da82550380ed36038d100f71095404a97e52d661dbe4f74deef4122a102dca698960000864973f96017000086eba24660170000".HexToBinary();
        // Its corresponding NT hash, also taken from AD
        string expectedHash = "1fe07f47bfa7f511d902ed5cfb79cc4d";
        // Try parsing the blob
        ManagedPassword pwd = ManagedPassword.Parse(blob);
        string actualHash = pwd.CurrentNTHash.ToHex(false);
        Assert.AreEqual(expectedHash, actualHash);
        Assert.IsNull(pwd.PreviousNTHash);
    }
}
