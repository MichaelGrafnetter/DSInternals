namespace DSInternals.Common.Cryptography.Test
{
    using DSInternals.Common;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HashEqualityComparerTester
    {
        private HashEqualityComparer comparer = HashEqualityComparer.GetInstance();

        [TestMethod]
        public void HashEqualityComparer_Equals_SameSize1()
        {
            byte[] vector1 = "92937945B518814341DE3F726500D4FF".HexToBinary();
            byte[] vector2 = (byte[]) vector1.Clone();
            bool result = comparer.Equals(vector1, vector2);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void HashEqualityComparer_Equals_SameSize2()
        {
            byte[] vector1 = "92937945B518814341DE3F726500D4FF".HexToBinary();
            byte[] vector2 = "92937945B518814341DE3F726500D4F0".HexToBinary();
            bool result = comparer.Equals(vector1, vector2);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void HashEqualityComparer_Equals_DifferentSize()
        {
            byte[] vector1 = "92937945B518814341DE3F726500D4FF".HexToBinary();
            byte[] vector2 = "92937945B518814341DE3F726500D4".HexToBinary();
            bool result = comparer.Equals(vector1, vector2);
            Assert.AreEqual(false, result);
        }


        [TestMethod]
        public void HashEqualityComparer_Equals_Null1()
        {
            bool result = comparer.Equals(null, new byte[0]);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void HashEqualityComparer_Equals_Null2()
        {
            bool result = comparer.Equals(new byte[0], null);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void HashEqualityComparer_Equals_NullBoth()
        {
            bool result = comparer.Equals(null, null);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void HashEqualityComparer_GetHashCode_Vector1()
        {
            var hash = "92937945B518814341DE3F726500D4FF".HexToBinary();
            Assert.AreEqual(1165595538, comparer.GetHashCode(hash));
        }

        [TestMethod]
        public void HashEqualityComparer_GetHashCode_Short1()
        {
            var hash = "A0".HexToBinary();
            Assert.AreEqual(160, comparer.GetHashCode(hash));
        }

        [TestMethod]
        public void HashEqualityComparer_GetHashCode_Short2()
        {
            var hash = "A0B2".HexToBinary();
            Assert.AreEqual(-19808, comparer.GetHashCode(hash));
        }

        [TestMethod]
        public void HashEqualityComparer_GetHashCode_Short3()
        {
            var hash = "A0B2C3".HexToBinary();
            Assert.AreEqual(-19808, comparer.GetHashCode(hash));
        }

        [TestMethod]
        public void HashEqualityComparer_GetHashCode_Empty()
        {
            Assert.AreEqual(0, this.comparer.GetHashCode(new byte[0]));
        }
        [TestMethod]
        public void HashEqualityComparer_GetHashCode_Null()
        {
            Assert.AreEqual(0, this.comparer.GetHashCode(null));
        }
    }
}
