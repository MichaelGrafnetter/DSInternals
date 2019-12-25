using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class ValidatorTester
    {
        // TODO: Implement more Validator tests.

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validator_AssertEquals_NotEqualStrings()
        {
            Validator.AssertEquals("MIB1", "RSA2", "param");
        }

        [TestMethod]
        public void Validator_AssertEquals_EqualStrings()
        {
            Validator.AssertEquals("MIB1", "MIB1", "param");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validator_AssertEquals_NotEqualChars()
        {
            Validator.AssertEquals('%', '/', "param");
        }

        [TestMethod]
        public void Validator_AssertEquals_EqualChars()
        {
            Validator.AssertEquals('%', '%', "param");
        }
    }
}
