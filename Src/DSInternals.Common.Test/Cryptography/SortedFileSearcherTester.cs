namespace DSInternals.Common.Cryptography.Test
{
    using System;
    using System.IO;
    using System.Text;
    using DSInternals.Common.Cryptography;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SortedFileSearcherTester
    {
        /// <summary>
        /// Sorted password hashes sample from HaveIBeenPwned
        /// </summary>
        private static readonly string PwnedPasswordHashesOrderedByHash =
@"1AE188AB6DF35626E09C77F2880CAD75:1
1AE188AE54DEEE65F98EA8DAF5C06389:5
1AE188B2771D3DD69D0F40F56042C609:3
1AE188B3D50128E5C274D45F49574283:2
1AE188B69E76232FF5CB5B80C862EC34:3
1AE188BCBA0C50B7FF9999255260F5DD:1
1AE188C58C0F34A6C421603CAA108DBE:1
1AE188CF07E3C0ED347DC63017B93D71:1
1AE188D9F3C60AACFA238B1F49634C25:2
1AE188DFA105DFFEEB473F0F8C93772D:1
1AE188F1BBF1FA061FAEC06F3E2EDB2E:1
1AE188FA1A967282AB3D8953DB6AF690:1
1AE188FF3AF08946C275C9A7E2B894F2:5
1AE1890379820DBD7006AE8542B76422:10
1AE1890A0775C64FA133E667782E95E8:1
1AE1890FDEE9B77FC51C148A4BC4D0B5:3
1AE189149D5258ABCC0AEE7AF0903C74:27
1AE18919D4D3046A4DF233BE2D18E57B:2
1AE1891C13191E09EECA390E41ABF424:1
1AE1891CE99B114EA3BE237F52AD5ADE:3
1AE1892E55FC8C053AA5E2F8F0E93866:2
1AE1892F25A8976E3BB1A6E79428B393:5
1AE1893AC7DB0F799B8B6F3CF9D335AF:2
1AE18943DEE3FC98195336CF11482A5F:2
1AE189443AB482B499AC61CF12FBFBB6:1
1AE1894DC94946469DB8FFE55AA5A16C:1
1AE1894F06D3EC94BA056285F14CEA88:3
1AE18963EE75671A5F0B89D7C45B6573:1
1AE18963F1468E88BC3E9595E77851E9:2
1AE1896804620C1F3D0C169FE0B7F9B4:47
1AE18968A19E9DA32F0B555DDEE9684E:1
1AE1896C474207EB710FDAA76653B104:3
1AE18981221192F5C58C27C0A61D60A7:5
1AE1898163661EA127791D6F3886F451:19
1AE18983822B301A8DC745E9D3040EFF:1
1AE189928B7750C514D50A6DC2BC1A6D:2
1AE18994419FB5502D6104C686F24F21:5
1AE189A260C9DDA7E0F5FEF34F92FF19:4
1AE189A6FFF7DB51548F12F063491E36:1";

        /// <summary>
        /// Unsorted password hashes sample from HaveIBeenPwned
        /// </summary>
        private static readonly string PwnedPasswordHashesOrderedByCount =
@"32ED87BDB5FDC5E9CBA88547376818D4:22390492
C22B315C040AE6E0EFEE3518D830362B:7481454
2D20D252A479F485CDF5E171D93985BF:3752262
8846F7EAEE8FB117AD06BDD830B7586C:3533661
5835048CE94AD0564E29A924A03510EF:2391888
7A21990FCD3D759941E45C490F143D5F:2308872
8AF326AA4850225B75C592D4CE19CCF5:2187868
3E24DCEAD23468CE597D6883C576F657:1138654
0D757AD173D2FC249CE19364FD64C8EC:1069077
3DBDE697D71690A769204BEB12283678:1014565
F2477A144DFF4F216AB81F2AC3E3207D:967018
69CBE3ACBC48A3A289E8CDB000C2B7A8:965645
F7EB9C06FAFAA23C4BCF22BA6781C1E2:945785";

        /// <summary>
        /// Sample input for better debugging experience
        /// </summary>
        private static readonly string PhoneticAlphabet =
@"Alpha
Bravo
Charlie
Delta
Echo
Foxtrot
Golf
Hotel
India
Juliet
Kilo
Lima
Mike
November
Oscar
Papa
Quebec
Romeo
Sierra
Tango
Uniform
Victor
Whiskey
X-ray
Yankee
Zulu";

        [TestMethod]
        [Timeout(5000)]
        public void SortedFileSearcher_StreamInput_Words()
        {
            byte[] binaryInput = Encoding.ASCII.GetBytes(PhoneticAlphabet);
            using (var stream = new MemoryStream(binaryInput))
            {
                var searcher = new SortedFileSearcher(stream);
                
                // Check that we can find all the words that are contained in the input
                string[] words = PhoneticAlphabet.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach(string word in words)
                {
                    Assert.IsTrue(searcher.FindString(word));
                }

                // Test some non-existing words 
                Assert.IsFalse(searcher.FindString("AAAA"));
                Assert.IsFalse(searcher.FindString("ZZZZ"));
                string[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Split();
                foreach (string letter in letters)
                {
                    Assert.IsFalse(searcher.FindString(letter));
                }
            }
        }

        [TestMethod]
        [Timeout(5000)]
        public void SortedFileSearcher_StreamInput_SortedHashes()
        {
            byte[] binaryInput = Encoding.ASCII.GetBytes(PwnedPasswordHashesOrderedByHash);
            using (var stream = new MemoryStream(binaryInput))
            {
                var searcher = new SortedFileSearcher(stream);

                // Find first
                Assert.IsTrue(searcher.FindString("1AE188AB6DF35626E09C77F2880CAD75"));

                // Find last
                Assert.IsTrue(searcher.FindString("1AE189A6FFF7DB51548F12F063491E36"));

                // Find middle
                Assert.IsTrue(searcher.FindString("1AE188FF3AF08946C275C9A7E2B894F2"));

                // Find non-existing
                Assert.IsFalse(searcher.FindString("F7EB9C06FAFAA23C4BCF22BA6781C1E2"));
            }
        }

        [TestMethod]
        [Timeout(5000)]
        public void SortedFileSearcher_StreamInput_UnsortedHashes()
        {
            // We just need to test that this does not end with an endless loop

            byte[] binaryInput = Encoding.ASCII.GetBytes(PwnedPasswordHashesOrderedByCount);
            using (var stream = new MemoryStream(binaryInput))
            {
                using (var searcher = new SortedFileSearcher(stream))
                {
                    searcher.FindString("1AE188AB6DF35626E09C77F2880CAD75");
                    searcher.FindString("69CBE3ACBC48A3A289E8CDB000C2B7A8");
                    searcher.FindString("1AE188FF3AF08946C275C9A7E2B894F2");
                    searcher.FindString("F7EB9C06FAFAA23C4BCF22BA6781C1E2");
                }
            }
        }

        [TestMethod]
        [Timeout(5000)]
        public void SortedFileSearcher_StreamInput_SingleLine()
        {
            byte[] binaryInput = Encoding.ASCII.GetBytes("Gamma");
            using (var stream = new MemoryStream(binaryInput))
            {
                using (var searcher = new SortedFileSearcher(stream))
                {
                    Assert.IsTrue(searcher.FindString("Gamma"));
                    Assert.IsFalse(searcher.FindString("Alpha"));
                    Assert.IsFalse(searcher.FindString("Omega"));
                }
            }
        }

        [TestMethod]
        [Timeout(5000)]
        public void SortedFileSearcher_StreamInput_Empty()
        {
            byte[] input = new byte[0];
            using (var stream = new MemoryStream(input))
            {
                using (var searcher = new SortedFileSearcher(stream))
                {
                    bool result = searcher.FindString("F7EB9C06FAFAA23C4BCF22BA6781C1E2");
                    Assert.IsFalse(result);
                }
            }
        }

        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SortedFileSearcher_StreamInput_Null()
        {
            var searcher = new SortedFileSearcher((Stream)null);
        }

        [TestMethod]
        [Timeout(5000)]
        public void SortedFileSearcher_FileInput()
        {
            string hashesFile = Path.GetTempFileName();
            try
            {
                File.WriteAllText(hashesFile, PwnedPasswordHashesOrderedByHash);
                using (var searcher = new SortedFileSearcher(hashesFile))
                {
                    // Find first
                    Assert.IsTrue(searcher.FindString("1AE188AB6DF35626E09C77F2880CAD75"));

                    // Find last
                    Assert.IsTrue(searcher.FindString("1AE189A6FFF7DB51548F12F063491E36"));

                    // Find middle
                    Assert.IsTrue(searcher.FindString("1AE188FF3AF08946C275C9A7E2B894F2"));

                    // Find non-existing (before first)
                    Assert.IsFalse(searcher.FindString("0AE188AB6DF35626E09C77F2880CAD75"));

                    // Find non-existing (in the middle)
                    Assert.IsFalse(searcher.FindString("1AE188FF3AF08946C275C9A7E2B894F1"));

                    // Find non-existing (after last)
                    Assert.IsFalse(searcher.FindString("F7EB9C06FAFAA23C4BCF22BA6781C1E2"));
                }
            }
            finally
            {
                File.Delete(hashesFile);
            }
        }
    }
}
