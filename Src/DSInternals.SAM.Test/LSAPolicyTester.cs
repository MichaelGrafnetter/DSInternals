namespace DSInternals.SAM.Test
{
    using DSInternals.SAM;
    using DSInternals.SAM.Interop;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class LSAPolicyTester
    {
        [TestMethod]
        public void LsaPolicy_QueryDnsDomainInformation()
        {
            try
            {
                using (var policy = new LsaPolicy(LsaPolicyAccessMask.ViewLocalInformation))
                {
                    var result = policy.QueryDnsDomainInformation();
                }
            }
            catch(UnauthorizedAccessException e)
            {
                // This is expected.
                throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
            }
        }

        [TestMethod]
        public void LsaPolicy_QueryPrimaryDomainInformation()
        {
            try
            {
                using (var policy = new LsaPolicy(LsaPolicyAccessMask.ViewLocalInformation))
                {
                    var result = policy.QueryPrimaryDomainInformation();
                }
            }
            catch (UnauthorizedAccessException e)
            {
                // This is expected.
                throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
            }
        }

        [TestMethod]
        public void LsaPolicy_QueryAccountDomainInformation()
        {
            try
            {
                using (var policy = new LsaPolicy(LsaPolicyAccessMask.ViewLocalInformation))
                {
                    var result = policy.QueryAccountDomainInformation();
                }
            }
            catch (UnauthorizedAccessException e)
            {
                // This is expected.
                throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
            }
        }

        [TestMethod]
        public void LsaPolicy_QueryMachineAccountInformation()
        {
            try
            {
                using (var policy = new LsaPolicy(LsaPolicyAccessMask.ViewLocalInformation))
                {
                    var result = policy.QueryMachineAccountInformation();
                }
            }
            catch (UnauthorizedAccessException e)
            {
                // This is expected.
                throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
            }
        }

        [TestMethod]
        public void LsaPolicy_QueryLocalAccountDomainInformation()
        {
            try
            {
                using (var policy = new LsaPolicy(LsaPolicyAccessMask.ViewLocalInformation))
                {
                    var result = policy.QueryLocalAccountDomainInformation();
                }
            }
            catch (UnauthorizedAccessException e)
            {
                // This is expected.
                throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
            }
        }
    }
}