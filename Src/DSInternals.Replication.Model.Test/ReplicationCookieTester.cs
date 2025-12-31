using System.Runtime.Serialization;

namespace DSInternals.Replication.Model.Test;

[TestClass]
public class ReplicationCookieTester
{
    [TestMethod]
    public void ReplicationCookie_Equals_Vector1()
    {
        var cookie1 = new ReplicationCookie("DC=adatum,DC=com");
        var cookie2 = new ReplicationCookie("DC=adatum,DC=com");
        Assert.IsTrue(cookie1.Equals((object)cookie2));
        Assert.IsTrue(cookie1.Equals(cookie2));
        Assert.IsTrue(cookie1 == cookie2);
        Assert.IsFalse(cookie1 != cookie2);
    }

    [TestMethod]
    public void ReplicationCookie_Equals_Vector2()
    {
        Guid guid = Guid.NewGuid();
        var cookie1 = new ReplicationCookie("DC=adatum,DC=com", guid, 1, 2, 3);
        var cookie2 = new ReplicationCookie("DC=adatum,DC=com", guid, 1, 2, 3);
        Assert.IsTrue(cookie1.Equals((object)cookie2));
        Assert.IsTrue(cookie1.Equals(cookie2));
        Assert.IsTrue(cookie1 == cookie2);
        Assert.IsFalse(cookie1 != cookie2);
    }

    [TestMethod]
    public void ReplicationCookie_NotEquals()
    {
        var cookie1 = new ReplicationCookie("DC=adatum,DC=com");
        var cookie2 = new ReplicationCookie("DC=contoso,DC=com");
        Assert.IsFalse(cookie1.Equals((object)cookie2));
        Assert.IsFalse(cookie1.Equals(cookie2));
        Assert.IsFalse(cookie1 == cookie2);
        Assert.IsTrue(cookie1 != cookie2);
    }

    [TestMethod]
    public void ReplicationCookie_NotEqualsNull()
    {
        var cookie1 = new ReplicationCookie("DC=adatum,DC=com");
        var cookie2 = (ReplicationCookie)null;
        Assert.IsFalse(cookie1.Equals(cookie2));
        Assert.IsFalse(cookie1.Equals((object)cookie2));
        Assert.IsFalse(cookie1 == cookie2);
        Assert.IsTrue(cookie1 != cookie2);
    }

    [TestMethod]
    public void ReplicationCookie_Equals_Nulls()
    {
        var cookie1 = (ReplicationCookie)null;
        var cookie2 = (ReplicationCookie)null;
        Assert.IsTrue(cookie1 == cookie2);
        Assert.IsFalse(cookie1 != cookie2);
    }

    [TestMethod]
    public void ReplicationCookie_NotEqualsNonCookie()
    {
        var cookie = new ReplicationCookie("DC=adatum,DC=com");
        var str = "DC=adatum,DC=com";
        Assert.IsFalse(cookie.Equals(str));
        Assert.IsFalse(cookie.Equals((object)str));
    }

    [TestMethod]
    public void ReplicationCookie_NotEqualsNonCookieNull()
    {
        var cookie = new ReplicationCookie("DC=adatum,DC=com");
        string str = null;
        Assert.IsFalse(cookie.Equals(str));
        Assert.IsFalse(cookie.Equals((object)str));
    }

    [TestMethod]
    public void ReplicationCookie_Serialization()
    {
        Guid guid = Guid.NewGuid();
        var originalCookie = new ReplicationCookie("DC=adatum,DC=com", guid, 1, 2, 3);

        // Serialize
        var serializer = new DataContractSerializer(typeof(ReplicationCookie));
        byte[] binaryForm;
        using (var stream = new MemoryStream())
        {
            serializer.WriteObject(stream, originalCookie);
            binaryForm = stream.ToArray();
        }

        // Deserialize
        ReplicationCookie deserializedCookie;
        using (var stream = new MemoryStream(binaryForm))
        {
            deserializedCookie = (ReplicationCookie)serializer.ReadObject(stream);
        }

        // Test that the deserialization worked
        Assert.AreEqual(originalCookie, deserializedCookie);
    }

    [TestMethod]
    public void ReplicationCookie_GetHashCode_Equal()
    {
        var cookie1 = new ReplicationCookie("DC=adatum,DC=com");
        var cookie2 = new ReplicationCookie("DC=adatum,DC=com");
        Assert.AreEqual(cookie1.GetHashCode(), cookie2.GetHashCode());
    }

    [TestMethod]
    public void ReplicationCookie_GetHashCode_NotEqual()
    {
        var cookie1 = new ReplicationCookie("DC=adatum,DC=com");
        var cookie2 = new ReplicationCookie("DC=contoso,DC=com");
        Assert.AreNotEqual(cookie1.GetHashCode(), cookie2.GetHashCode());
    }
}
