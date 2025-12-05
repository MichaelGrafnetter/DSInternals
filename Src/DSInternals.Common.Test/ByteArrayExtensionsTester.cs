using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class ByteArrayExtensionsTester
    {
        [TestMethod]
        public void ByteArrayExtensions_Hex_DefaultCase()
        {
            string hex = "6d1358e70650cd0f";
            string result = hex.HexToBinary().ToHex();
            Assert.AreEqual(hex, result);
        }
        [TestMethod]
        public void ByteArrayExtensions_Hex_LowerCase()
        {
            string hex = "6d1358e70650cd0f";
            string result = hex.HexToBinary().ToHex(false);
            Assert.AreEqual(hex, result);
        }
        [TestMethod]
        public void ByteArrayExtensions_Hex_UpperCase()
        {
            string hex = "6D1358E70650CD0F";
            string result = hex.HexToBinary().ToHex(true);
            Assert.AreEqual(hex, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ByteArrayExtensions_HexToBinary_InvalidInput()
        {
            string hex = "eqwewqwty";
            byte[] result = hex.HexToBinary();
        }
        [TestMethod]
        public void ByteArrayExtensions_HexToBinary_NullInput()
        {
            string hex = null;
            byte[] result = hex.HexToBinary();
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ByteArrayExtensions_HexToBinary_EmptyInput()
        {
            string hex = string.Empty;
            byte[] result = hex.HexToBinary();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ByteArrayExtensions_ToHex_NullInput()
        {
            byte[] binary = null;
            string result = binary.ToHex();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ByteArrayExtensions_ToHex_EmptyInput()
        {
            byte[] binary = new byte[0];
            string result = binary.AsSpan().ToHex();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ByteArrayExtensions_ZeroFill_Input1()
        {
            byte[] input = new byte[] {2, 4, 0};
            byte[] expected = new byte[] {0, 0, 0};
            input.ZeroFill();
            CollectionAssert.AreEqual(expected, input);
        }
        [TestMethod]
        public void ByteArrayExtensions_ZeroFill_EmptyInput()
        {
            byte[] input = new byte[0];
            input.ZeroFill();
        }
    }
}
