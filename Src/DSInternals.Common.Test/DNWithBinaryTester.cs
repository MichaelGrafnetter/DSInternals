using System;
using DSInternals.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class DNWithBinaryTester
    {
        [TestMethod]
        public void DNWithBinary_Vector1()
        {
            // By converting the value back and forth, we should get the original string.
            string input = "B:828:0002000020000120717AE052FCCF546AAD0D51E878AAD69CE04FDC39F5A8D8E3CEBA6BCB4DA0E720000214B7474E61C1001D3E546CFED8E387CFC1AC86A2CA7B3CDCF1267614585E2A341B0103525341310008000003000000000100000000000000000000010001C1A78914457758B0B13C70C710C7F8548F3F9ED56AD4640B6E6A112655C98ECAC1CBD68A298F5686C08439428A97FE6FDF58D78EA481905182BAD684C2D9C5CDE1CDE34AA19742E8BBF58B953EAC4C562FCF598CC176B02DBE9FFFEF5937A65815C236F92892F7E511A1FEDD5483CB33F1EA715D68106180DED2432A293367114A6E325E62F93F73D7ECE4B6A2BCDB829D95C8645C3073B94BA7CB7515CD29042F0967201C6E24A77821E92A6C756DF79841ACBAAE11D90CA03B9FCD24EF9E304B5D35248A7BD70557399960277058AE3E99C7C7E2284858B7BF8B08CDD286964186A50A7FCBCC6A24F00FEE5B9698BBD3B1AEAD0CE81FEA461C0ABD716843A50100040101000500100006E377F547D0D20A4A8ACAE0501098BDE40200070100080008417BD66E6603D401080009417BD66E6603D401:CN = Admin,CN = Users,DC = contoso,DC = com";
            var result = DNWithBinary.Parse(input);
            Assert.AreEqual(414, result.Binary.Length);
            Assert.AreEqual(input, result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DNWithBinary_NullInput()
        {
            DNWithBinary.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DNWithBinary_EmptyInput()
        {
            DNWithBinary.Parse(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DNWithBinary_MalformedInput()
        {
            string input = "B:828::CN = Admin,CN=Users,DC=contoso,DC=com";
            DNWithBinary.Parse(input);
        }
    }
}
