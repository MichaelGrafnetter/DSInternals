using DSInternals.Common;
using DSInternals.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
            collection1.Update(CommonDirectoryAttributes.SupplementalCredentialsId, Guid.NewGuid(), DateTime.Now, 100);
            collection1.Update(CommonDirectoryAttributes.UserAccountControlId, Guid.NewGuid(), DateTime.Now, 200);
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
            metadata.Update(CommonDirectoryAttributes.ObjectClassId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.SurnameId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.GivenNameId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.InstanceTypeId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.DisplayNameId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.UserAccountControlId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.LMHashId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.ObjectSidId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.LMHashHistoryId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.SAMAccountNameId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.SamAccountTypeId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.UserPrincipalNameId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.ObjectCategoryId, invocationId, now, usn);
            metadata.Update(CommonDirectoryAttributes.PKIRoamingTimeStampId, invocationId, now, usn);
            Assert.AreEqual(14, metadata.Count);

            // Modify an existing attribute
            metadata.Update(CommonDirectoryAttributes.UserAccountControlId, invocationId, now, ++usn);
            Assert.AreEqual(14, metadata.Count);

            // Add a new attribute
            metadata.Update(CommonDirectoryAttributes.SupplementalCredentialsId, invocationId, now, ++usn);
            Assert.AreEqual(15, metadata.Count);

            // Check the order and uniqueness of attribute ids
            var attributeIds = metadata.Attributes;
            CollectionAssert.AllItemsAreUnique(attributeIds.ToArray());
            CollectionAssert.AreEqual(attributeIds.OrderBy(item => item).ToArray(), attributeIds.ToArray());
        }
    }
}
