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
    }
}
