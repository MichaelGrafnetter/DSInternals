namespace DSInternals.SAM.Test;

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

    [TestMethod]
    public void LsaPolicy_SetDnsDomainInformation()
    {
        try
        {
            using (var policy = new LsaPolicy(LsaPolicyAccessMask.ViewLocalInformation | LsaPolicyAccessMask.TrustAdmin))
            {
                // Retrieve domain info
                var info = policy.QueryDnsDomainInformation();
                
                // Now try to set it to the same value.
                // BE CAREFUL WHEN TESTING THIS!!!
                policy.SetDnsDomainInformation(info);
            }
        }
        catch (UnauthorizedAccessException e)
        {
            // This is expected.
            throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
        }
    }

    [TestMethod]
    public void LsaPolicy_LsaRetrievePrivateData_Existing()
    {
        try
        {
            using (var policy = new LsaPolicy(LsaPolicyAccessMask.GetPrivateInformation))
            {
                policy.RetrievePrivateData("DPAPI_SYSTEM");
            }
        }
        catch (UnauthorizedAccessException e)
        {
            // This is expected.
            throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void LsaPolicy_LsaRetrievePrivateData_NonExisting()
    {
        try
        {
            using (var policy = new LsaPolicy(LsaPolicyAccessMask.GetPrivateInformation))
            {
                policy.RetrievePrivateData("bflmpsvz");
            }
        }
        catch (UnauthorizedAccessException e)
        {
            // This is expected.
            throw new AssertInconclusiveException("LSA-related tests require admin rights.", e);
        }
    }
}
