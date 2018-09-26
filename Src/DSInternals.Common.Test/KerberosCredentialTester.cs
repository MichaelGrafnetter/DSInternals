namespace DSInternals.Common.Test
{
    using DSInternals.Common.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class KerberosCredentialTester
    {
        [TestMethod]
        public void KerberosCredential_Vector1()
        {
            byte[] blob = "0300000001000000200020003800000000000000000000000300000008000000580000000000000000000000000000000000000000000000410044004100540055004d002e0043004f004d0075007300650072003000320013f8fd37d557a401".HexToBinary();
            var credential = new KerberosCredential(blob);

            // Serialize the structure
            byte[] newBlob = credential.ToByteArray();

            // Try to parse it again
            var newCredential = new KerberosCredential(newBlob);

            // Check that we have the same key material
            Assert.AreEqual(credential.DefaultSalt, newCredential.DefaultSalt);
            Assert.AreEqual(credential.Credentials[0].ToString(), newCredential.Credentials[0].ToString());

            // Check binary equality
            Assert.AreEqual(blob.ToHex(), newBlob.ToHex());
        }

        [TestMethod]
        public void KerberosCredential_Vector2()
        {
            byte[] blob = "03000000010001001c001c004c000000000000000000000003000000080000006800000000000000000000000300000008000000700000000000000000000000000000000000000000000000410044004100540055004d002e0043004f004d007400650073007400453820da83b6d64a453820da83b6d64a".HexToBinary();
            var credential = new KerberosCredential(blob);

            // Serialize the structure
            byte[] newBlob = credential.ToByteArray();

            // Try to parse it again
            var newCredential = new KerberosCredential(newBlob);

            // Check that we have the same key material
            Assert.AreEqual(credential.DefaultSalt, newCredential.DefaultSalt);
            Assert.AreEqual(credential.Credentials[0].ToString(), newCredential.Credentials[0].ToString());

            // Check binary equality
            Assert.AreEqual(blob.ToHex(), newBlob.ToHex());
        }

        [TestMethod]
        public void KerberosCredential_Vector3()
        {
            byte[] blob = "0300000001000100400040004c000000000000000000000003000000080000008c00000000000000000000000300000008000000940000000000000000000000000000000000000000000000410044004100540055004d002e0043004f004d0068006f00730074006c006f006e002d006400630031002e00610064006100740075006d002e0063006f006d007091ce8545613d31a4cd57ea0b3d404a".HexToBinary();
            var credential = new KerberosCredential(blob);

            // Serialize the structure
            byte[] newBlob = credential.ToByteArray();

            // Try to parse it again
            var newCredential = new KerberosCredential(newBlob);

            // Check that we have the same key material
            Assert.AreEqual(credential.DefaultSalt, newCredential.DefaultSalt);
            Assert.AreEqual(credential.Credentials[0].ToString(), newCredential.Credentials[0].ToString());

            // Check binary equality
            Assert.AreEqual(blob.ToHex(), newBlob.ToHex());
        }

        [TestMethod]
        public void KerberosCredential_W2k3_Vector1()
        {
            byte[] blob = "030000000200000030003000500000000000000000000000030000000800000080000000000000000000000001000000080000008800000000000000000000000000000000000000000000000000000043004f004e0054004f0053004f002e0043004f004d00410064006d0069006e006900730074007200610074006f007200aed02c52204ca2ceaed02c52204ca2ce00000000000000000000000000000000".HexToBinary();
            var credential = new KerberosCredential(blob);

            // Check that the structure has been parsed correctly.
            Assert.AreEqual("CONTOSO.COMAdministrator", credential.DefaultSalt);
            Assert.AreEqual(2, credential.Credentials.Length);
            Assert.AreEqual(KerberosKeyType.DES_CBC_MD5, credential.Credentials[0].KeyType);

            // Serialize the structure
            byte[] newBlob = credential.ToByteArray();

            // Note that we are not expecting binary equality, because Windows Server 2003 used to add some redundand padding to the end of the structure.
            Assert.AreEqual(blob.Length - 20, newBlob.Length);
        }

        [TestMethod]
        public void KerberosCredential_W2k3_Vector2()
        {
            byte[] blob = "03000000020002004a004a007800000000000000000000000300000008000000c200000000000000000000000100000008000000ca00000000000000000000000300000008000000d200000000000000000000000100000008000000da00000000000000000000000000000000000000000000000000000043004f004e0054004f0053004f002e0043004f004d0068006f0073007400770069006e0032006b00330072003200650065002e0063006f006e0074006f0073006f002e0063006f006d00d9b33eb064e385dfd9b33eb064e385dff191e9a7b561525df191e9a7b561525d00000000000000000000000000000000".HexToBinary();
            var credential = new KerberosCredential(blob);

            // Check that the structure has been parsed correctly.
            Assert.AreEqual("CONTOSO.COMhostwin2k3r2ee.contoso.com", credential.DefaultSalt);
            Assert.AreEqual(2, credential.Credentials.Length);
            Assert.AreEqual(KerberosKeyType.DES_CBC_MD5, credential.Credentials[0].KeyType);
            Assert.AreEqual(2, credential.OldCredentials.Length);

            // Serialize the structure
            byte[] newBlob = credential.ToByteArray();

            // Note that we are not expecting binary equality, because Windows Server 2003 used to add some redundand padding to the end of the structure.
            Assert.AreEqual(blob.Length - 20, newBlob.Length);
        }
    }
}