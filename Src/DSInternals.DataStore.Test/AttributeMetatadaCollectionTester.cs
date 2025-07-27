using System;
using System.Linq;
using DSInternals.Common;
using DSInternals.Common.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.DataStore.Test
{
    [TestClass]
    public class AttributeMetatadaCollectionTester
    {
        private static string EmptyStructure = "01000000000000000000000000000000";

        [TestMethod]
        public void AttributeMetatadaCollection_CreateEmpty()
        {
            var metadata = new AttributeMetadataCollection();
            Assert.AreEqual(0, metadata.Count);
            Assert.AreEqual(1, metadata.Unknown);
            Assert.AreEqual(EmptyStructure, metadata.ToByteArray().ToHex());
            Assert.AreEqual(String.Empty, metadata.ToString());
        }

        [TestMethod]
        public void AttributeMetatadaCollection_ParseEmpty()
        {

            var metadata = new AttributeMetadataCollection(EmptyStructure.HexToBinary());
            Assert.AreEqual(0, metadata.Count);
            Assert.AreEqual(1, metadata.Unknown);
            Assert.AreEqual(EmptyStructure, metadata.ToByteArray().ToHex());
            Assert.AreEqual(String.Empty, metadata.ToString());
        }


        [TestMethod]
        public void AttributeMetatadaCollection_Serialization()
        {
            // Create an empty collection
            var collection1 = new AttributeMetadataCollection();

            // Add new elements
            collection1.Update(AttributeType.SupplementalCredentials, Guid.NewGuid(), DateTime.Now, 100);
            collection1.Update(AttributeType.UserAccountControl, Guid.NewGuid(), DateTime.Now, 200);
            Assert.AreEqual(2, collection1.Count);

            // Serialize
            byte[] buffer1 = collection1.ToByteArray();

            // Deserialize
            var collection2 = new AttributeMetadataCollection(buffer1);
            Assert.AreEqual(collection1.Count, collection2.Count);
            Assert.AreEqual(collection1.Unknown, collection2.Unknown);
            Assert.AreEqual(collection1.ToString(), collection2.ToString());
        }

        [TestMethod]
        public void AttributeMetatadaCollection_Update()
        {
            // Prepare the common values
            Guid invocationId = Guid.NewGuid();
            DateTime now = DateTime.Now;
            int usn = 33729;

            // Add some unique entries in sorted order
            var metadata = new AttributeMetadataCollection();
            metadata.Update(AttributeType.ObjectClass, invocationId, now, usn);
            metadata.Update(AttributeType.Surname, invocationId, now, usn);
            metadata.Update(AttributeType.GivenName, invocationId, now, usn);
            metadata.Update(AttributeType.InstanceType, invocationId, now, usn);
            metadata.Update(AttributeType.DisplayName, invocationId, now, usn);
            metadata.Update(AttributeType.UserAccountControl, invocationId, now, usn);
            metadata.Update(AttributeType.LMHash, invocationId, now, usn);
            metadata.Update(AttributeType.ObjectSid, invocationId, now, usn);
            metadata.Update(AttributeType.LMHashHistory, invocationId, now, usn);
            metadata.Update(AttributeType.SamAccountName, invocationId, now, usn);
            metadata.Update(AttributeType.SamAccountType, invocationId, now, usn);
            metadata.Update(AttributeType.UserPrincipalName, invocationId, now, usn);
            metadata.Update(AttributeType.ObjectCategory, invocationId, now, usn);
            metadata.Update(AttributeType.PKIRoamingTimeStamp, invocationId, now, usn);
            Assert.AreEqual(14, metadata.Count);

            // Modify an existing attribute
            metadata.Update(AttributeType.UserAccountControl, invocationId, now, ++usn);
            Assert.AreEqual(14, metadata.Count);

            // Add a new attribute
            metadata.Update(AttributeType.SupplementalCredentials, invocationId, now, ++usn);
            Assert.AreEqual(15, metadata.Count);

            // Check the order and uniqueness of attribute ids
            var attributeIds = metadata.Attributes;
            CollectionAssert.AllItemsAreUnique(attributeIds.ToArray());
            CollectionAssert.AreEqual(attributeIds.OrderBy(item => item).ToArray(), attributeIds.ToArray());
        }
    }
}
