using System;
using DSInternals.DataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.DataStore.Test
{
    [TestClass]
    public class AttributeMetatadaCollectionTester
    {
        [TestMethod]
        public void AttributeMetatadaCollection_Serialization()
        {
            // TODO: Split into more tests.
            var collection1 = new AttributeMetadataCollection();
            Assert.AreEqual(collection1.Count, 0);
            collection1.Update(123, Guid.NewGuid(), DateTime.Now, 100);
            collection1.Update(321, Guid.NewGuid(), DateTime.Now, 200);
            Assert.AreEqual(collection1.Count, 2);
            byte[] buffer1 = collection1.ToByteArray();
            int size1 = buffer1.Length;
            var collection2 = new AttributeMetadataCollection(buffer1);
            Assert.AreEqual(collection1.Count, collection2.Count);
            Assert.AreEqual(collection1.ToString(), collection2.ToString());
        }

        [TestMethod]
        public void AttributeMetatadaCollection_Update()
        {
            throw new AssertInconclusiveException();
            // collection.Update(321, Guid.NewGuid(), DateTime.Now, 200);
        }

    }
}
