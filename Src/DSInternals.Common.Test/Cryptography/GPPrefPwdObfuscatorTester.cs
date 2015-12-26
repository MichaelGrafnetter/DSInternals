using DSInternals.Common;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;

namespace DSInternals.Common.Cryptography.Test
{
    [TestClass]
    public class GPPrefPwdObfuscatorTester
    {
        [TestMethod]
        public void GPPrefPwdObfuscator_Decrypt_TestVector1()
        {
            string encrypted = "v9NWtCCOKEUHkZBxakMd6HLzo4+DzuizXP83EaImqF8";
            string decrypted = "Pa$$w0rd";
            string result = GPPrefPwdObfuscator.Decrypt(encrypted);
            Assert.AreEqual(decrypted, result);
        }

        [TestMethod]
        public void GPPrefPwdObfuscator_Decrypt_TestVector2()
        {
            string encrypted = "v9NWtCCOKEUHkZBxakMd6IyXC7oVAVOIz0O6imVn+fM7rAFz8kC2EPSQSYob/r7+";
            string decrypted = "Pa$$w0rdPa$$w0rd";
            string result = GPPrefPwdObfuscator.Decrypt(encrypted);
            Assert.AreEqual(decrypted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GPPrefPwdObfuscator_Decrypt_NullInput()
        {
            string result = GPPrefPwdObfuscator.Decrypt(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GPPrefPwdObfuscator_Decrypt_EmptyInput()
        {
            string result = GPPrefPwdObfuscator.Decrypt(String.Empty);
        }
        [TestMethod]
        public void GPPrefPwdObfuscator_Encrypt_Test1()
        {
            SecureString password = "Pa$$w0rd".ToSecureString();
            string encrypted = "v9NWtCCOKEUHkZBxakMd6HLzo4+DzuizXP83EaImqF8";
            string result = GPPrefPwdObfuscator.Encrypt(password);
            Assert.AreEqual(encrypted, result);
        }
        [TestMethod]
        public void GPPrefPwdObfuscator_Encrypt_EmptyInput()
        {
            SecureString password = new SecureString();
            string result = GPPrefPwdObfuscator.Encrypt(password);
            Assert.AreEqual(String.Empty, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GPPrefPwdObfuscator_Encrypt_NullInput()
        {
            string result = GPPrefPwdObfuscator.Encrypt(null);
        }
    }
}
