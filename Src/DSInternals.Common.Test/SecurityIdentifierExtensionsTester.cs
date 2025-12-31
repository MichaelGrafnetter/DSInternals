using System.Security.Principal;

namespace DSInternals.Common.Test;

[TestClass]
public class SecurityIdentifierExtensionsTester
{
    [TestMethod]
    public void SecurityIdentifierExtensions_GetRid_UserSID()
    {
        SecurityIdentifier sid = new SecurityIdentifier("S-1-5-21-3180365339-800773672-3767752645-500");
        int rid = sid.GetRid();
        Assert.AreEqual(500, rid);
    }
    [TestMethod]
    public void SecurityIdentifierExtensions_GetBinaryForm_Test1()
    {
        SecurityIdentifier sid = new SecurityIdentifier("S-1-5-21-3180365339-800773672-3767752645-500");
        byte[] binary = sid.GetBinaryForm();
        SecurityIdentifier sid2 = new SecurityIdentifier(binary, 0);
        Assert.AreEqual(sid, sid2);
    }

    [TestMethod]
    public void SecurityIdentifierExtensions_GetBinaryForm_Test2()
    {
        SecurityIdentifier sid1 = new SecurityIdentifier("S-1-5-21-3180365339-800773672-3767752645-1234");
        SecurityIdentifier sid2 = sid1.GetBinaryForm(true).ToSecurityIdentifier(true);
        Assert.AreEqual(sid1, sid2);
    }
    [TestMethod]
    public void SecurityIdentifierExtensions_GetBinaryForm_Test3()
    {
        SecurityIdentifier sid1 = new SecurityIdentifier("S-1-5-21-3180365339-800773672-3767752645-1234");
        SecurityIdentifier sid2 = sid1.GetBinaryForm(false).ToSecurityIdentifier(false);
        Assert.AreEqual(sid1, sid2);
    }
}
