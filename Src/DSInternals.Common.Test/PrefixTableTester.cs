using System;
using DSInternals.Common.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class PrefixTableTester
    {
        // Sample prefix map after Exchange 2016 schema import.
        private static byte[] ExchangeBinaryPrefixTable = "060000006200000023480b002a864886f7140104b658664f310c002a864886f7140104b658668331090c002a864886f7140105b6583e83dc600a002a864886f714010614014b6d0a002a864886f714010614024f750b002a864886f7140106140183".HexToBinary();

        // Sample prefix map provided by Christoffer Andersson. Contains duplicate prefixes, which is a little bit strange.
        private static byte[] SamplePrefixTable = "0E000000D800000023480B002A864886F7140104B65866BE180B002A864886F7140105B6583E84670C002A864886F7140104B6586683E14A0C002A864886F7140104B65866816C3D0C002A864886F7140105B6583E81D62C0C002A864886F7140105B6583E83AE720A002A864886F7140106140152690A002A864886F71401061402905F0A002A864886F7140106180249160A002A864886F71401061801F16D0B002A864886F71401061D0201F15A0B002A864886F71401061D0202BB410B002A864886F7140106140183E92609002A864886F714010283".HexToBinary();

        [TestMethod]
        public void PrefixMap_Vector1()
        {
            var map = new PrefixTable(ExchangeBinaryPrefixTable);

            // Should contain 39 builtin prefixes + 6 new
            Assert.AreEqual(45, map.Count);

            // Test one of the decoded prefixes
            string oid1 = map.Translate((AttributeType)18467);
            Assert.IsNotNull(oid1);
            
            string prefix = map[18467];
            Assert.AreEqual("1.2.840.113556.1.4.7000.102", prefix);

            // Test non-existing prefix
            string oid2 = map.Translate(12345678);
            Assert.IsNull(oid2);
        }

        [TestMethod]
        public void PrefixMap_Vector2()
        {
            var map = new PrefixTable(SamplePrefixTable);

            // Should contain 39 builtin refixes + 14 additional
            Assert.AreEqual(53, map.Count);
        }

        [TestMethod]
        public void PrefixMap_TranstaleBuiltin()
        {
            var map = new PrefixTable();

            // givenName
            string oid = map.Translate(42);
            Assert.AreEqual("2.5.4.42", oid);

            // objectSid
            oid = map.Translate(589970);
            Assert.AreEqual("1.2.840.113556.1.4.146", oid);

            // searchFlags
            oid = map.Translate(131406);
            Assert.AreEqual("1.2.840.113556.1.2.334", oid);
            
            // Entry-TTL
            oid = map.Translate(1769475);
            Assert.AreEqual("1.3.6.1.4.1.1466.101.119.3", oid);
        }

        [TestMethod]
        public void PrefixMap_TranstaleUser()
        {
            var map = new PrefixTable(ExchangeBinaryPrefixTable);

            // ms-Exch-Admins
            string oid = map.Translate(827294608);
            Assert.AreEqual("1.2.840.113556.1.4.7000.102.50064", oid);

            // ms-Exch-Folder-Affinity-List
            oid = map.Translate(1210264401);
            Assert.AreEqual("1.2.840.113556.1.4.7000.102.11089", oid);
        }


        [TestMethod]
        public void PrefixMap_NullInput()
        {
            byte[] binaryPrefixMap = null;
            var map = new PrefixTable(binaryPrefixMap);
            
            // Should only contain 39 builtin prefixes
            Assert.AreEqual(39, map.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PrefixMap_EmptyInput()
        {
            byte[] binaryPrefixMap = new byte[0];
            var map = new PrefixTable(binaryPrefixMap);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PrefixMap_InvalidInput1()
        {
            byte[] binaryPrefixMap = { 1, 2, 3, 4, 5 };
            var map = new PrefixTable(binaryPrefixMap);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PrefixMap_InvalidInput2()
        {
            byte[] binaryPrefixMap = { 1, 2, 3, 4, 5, 6, 7, 8 };
            var map = new PrefixTable(binaryPrefixMap);
        }
    }
}
