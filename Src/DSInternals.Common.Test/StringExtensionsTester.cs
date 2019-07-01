namespace DSInternals.Common.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTester
    {
        [TestMethod]
        public void StringExtensions_TrimEnd_Test1()
        {
            string input = "strsuffix";
            string result = input.TrimEnd("suffix");
            Assert.AreEqual("str", result);
        }

        [TestMethod]
        public void StringExtensions_TrimEnd_NullSuffix()
        {
            string input = "str";
            string result = input.TrimEnd(null);
            Assert.AreEqual("str", result);
        }

        [TestMethod]
        public void StringExtensions_TrimEnd_EmptySuffix()
        {
            string input = "str";
            string result = input.TrimEnd(string.Empty);
            Assert.AreEqual("str", result);
        }

        [TestMethod]
        public void StringExtensions_TrimEnd_EmptyInput()
        {
            string result = string.Empty.TrimEnd("suffix");
            Assert.AreEqual(string.Empty, result);
        }
    }
}
