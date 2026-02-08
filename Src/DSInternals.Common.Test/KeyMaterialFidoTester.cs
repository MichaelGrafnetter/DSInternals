using DSInternals.Common.Data;

namespace DSInternals.Common.Test;

/// <summary>
/// Contains tests for parsing the key material corresponding to KEY_USAGE_FIDO.
/// </summary>
[TestClass]
public class KeyMaterialFidoTester
{
    [TestMethod]
    public void FidoKeyMaterial_Parse_FIDO_Input1()
    {
        string encodedKeyMaterial = "eyJ2ZXJzaW9uIjoxLCJhdXRoRGF0YSI6Ik5XeWUxS0NUSWJscFh4NnZrWUlEOGJWZmFKMm1IN3lXR0V3VmZkcG9ESUhGQUFBQUdjdHBTQjZQOTBBNWsrd0tKeW1oVktnQUVCckljaURTekdqanNLcmRTelZJdElHbEFRSURKaUFCSVZnZ29uTkNlY2EwZE5LTzlaWXBGdjlvMCtlZ0VGQ1ZTeXJ0UmN1NndrMStoT2dpV0NDNWE3MFI0ZlNFZ0ZpNWxnQTRBVVBmN1Y0Q2p5VWcvb1VWTFEzem5kZnc1YUZyYUcxaFl5MXpaV055WlhUMSIsIng1YyI6WyJNSUlDdlRDQ0FhV2dBd0lCQWdJRUdLeEd3REFOQmdrcWhraUc5dzBCQVFzRkFEQXVNU3d3S2dZRFZRUURFeU5aZFdKcFkyOGdWVEpHSUZKdmIzUWdRMEVnVTJWeWFXRnNJRFExTnpJd01EWXpNVEFnRncweE5EQTRNREV3TURBd01EQmFHQTh5TURVd01Ea3dOREF3TURBd01Gb3diakVMTUFrR0ExVUVCaE1DVTBVeEVqQVFCZ05WQkFvTUNWbDFZbWxqYnlCQlFqRWlNQ0FHQTFVRUN3d1pRWFYwYUdWdWRHbGpZWFJ2Y2lCQmRIUmxjM1JoZEdsdmJqRW5NQ1VHQTFVRUF3d2VXWFZpYVdOdklGVXlSaUJGUlNCVFpYSnBZV3dnTkRFek9UUXpORGc0TUZrd0V3WUhLb1pJemowQ0FRWUlLb1pJemowREFRY0RRZ0FFZWVvN0xIeEpjQkJpSXd6U1ArdGc1U2t4Y2RTRDhRQytoWjFyRDRPWEF3RzFSczNVYnMvSzQrUHpENEhwN1dLOUpvMU1IcjAzczd5K2txakNydXRPT3FOc01Hb3dJZ1lKS3dZQkJBR0N4QW9DQkJVeExqTXVOaTR4TGpRdU1TNDBNVFE0TWk0eExqY3dFd1lMS3dZQkJBR0M1UndDQVFFRUJBTUNCU0F3SVFZTEt3WUJCQUdDNVJ3QkFRUUVFZ1FReTJsSUhvLzNRRG1UN0FvbkthRlVxREFNQmdOVkhSTUJBZjhFQWpBQU1BMEdDU3FHU0liM0RRRUJDd1VBQTRJQkFRQ1huUU9YMkdENEx1RmRNUng1YnJyN0l2cW40SVRadXJUR0c3dFg4K2Ewd1lwSU43aGNQRTdiNUlORDlOYWwyYkhPMm9yaC90U1JLU0Z6Qlk1ZTRjdmRhOXJBZFZmR29PalRhQ1c2Rlo1L3RhMk0ydmdFaG96NURvOGZpdW9Yd0JhMVhDcDYxSmZJbFB0eDExUFhtNXBJUzJ3M2JYSTdtWTB1SFVNR3Z4QXp0YTc0ektYTHNsYUxhU1FpYlNLaldLdDloK1NzWHk0SkdxY1ZlZk9sYVFsSmZYTDFUZ2E2d2NPMFFUdTZYcStVdzdaUE5QbnJwQnJMYXVLRGQyMDJSbE40U1A3b2hMM2Q5Ykc2VjVoVXovM091c05FQlpVbjVXM1ZtUGoxWm5GYXZrTUIzUmtSTU9hNThNWkFPUkpUNGltQVB6cnZKMHZ0djk0L3k3MUM2dFo1Il0sImRpc3BsYXlOYW1lIjoiWXViaUtleSA1In0=";
        byte[] keyMaterialBytes = Convert.FromBase64String(encodedKeyMaterial);

        // Parse the FIDO key
        var keyMaterial = KeyMaterialFido.ParseJson(keyMaterialBytes);

        // Check all fields
        Assert.IsNotNull(keyMaterial);
        Assert.AreEqual(1, keyMaterial.Version);
        Assert.AreEqual(1, keyMaterial.AttestationCertificatesRaw?.Length);
        Assert.AreEqual("YubiKey 5", keyMaterial.DisplayName);
        Assert.AreEqual("e7d092ba192fdbbb2f36552832d616126971a269", keyMaterial.AttestationCertificates[0].Thumbprint.ToLowerInvariant());
        Assert.AreEqual("cb69481e-8ff7-4039-93ec-0a2729a154a8", keyMaterial.AuthenticatorData?.AttestedCredentialData.AaGuid.ToString());
    }

    [TestMethod]
    public void FidoKeyMaterial_Parse_FIDO_Input2()
    {
        string encodedKeyMaterial = "eyJ2ZXJzaW9uIjoxLCJhdXRoRGF0YSI6Ik5XeWUxS0NUSWJscFh4NnZrWUlEOGJWZmFKMm1IN3lXR0V3VmZkcG9ESUhGQUFBRmRoTGUxMFZMN1VmVXE2cm5FL1VkWTVNQUlKVW96bENOMTFMWmFFOFF0SFhWU2JUeXltVEVNaWxpcTA0RjFtblJwaC9YcFFFQ0F5WWdBU0ZZSVAzWms5Vm5URUpONlQ4VWgxMHRPcUpmdDRicW1leVJUUzhvNkJQK2prYkNJbGdnYjFnR0dNMnM3T2dDMTNQdkZBVTJ1UDZJcVVGWWdiaGxQc3diUzRDK1FoV2hhMmh0WVdNdGMyVmpjbVYwOVE9PSIsIng1YyI6WyJNSUlDUXpDQ0FlbWdBd0lCQWdJUUhmSzFXbEhjUzJpRm85bWVhWC90RmpBS0JnZ3Foa2pPUFFRREFqQkpNUXN3Q1FZRFZRUUdFd0pWVXpFZE1Cc0dBMVVFQ2d3VVJtVnBkR2xoYmlCVVpXTm9ibTlzYjJkcFpYTXhHekFaQmdOVkJBTU1Fa1psYVhScFlXNGdSa2xFVHlCRFFTQXdNekFnRncweE9ERXlNalV3TURBd01EQmFHQTh5TURNek1USXlOREl6TlRrMU9Wb3djREVMTUFrR0ExVUVCaE1DVlZNeEhUQWJCZ05WQkFvTUZFWmxhWFJwWVc0Z1ZHVmphRzV2Ykc5bmFXVnpNU0l3SUFZRFZRUUxEQmxCZFhSb1pXNTBhV05oZEc5eUlFRjBkR1Z6ZEdGMGFXOXVNUjR3SEFZRFZRUUREQlZHVkNCQ2FXOVFZWE56SUVaSlJFOHlJREEwTnpBd1dUQVRCZ2NxaGtqT1BRSUJCZ2dxaGtqT1BRTUJCd05DQUFTNjJoSWJ5ZW5IOVdQbnpZSGVoYUJSM0M3cXN3b21aa2FQekd5VWxGUmlKSU1vM3VJVGVJbUZPRmZOY0R1T3pvcTF3Y1hYR1RtRXRFdHhGMndvOW5va280R0pNSUdHTUIwR0ExVWREZ1FXQkJTQkkxWG9MRFkxL0hKYWJhK1czMm54aHhwM1dqQWZCZ05WSFNNRUdEQVdnQlJCdC94TmRjcU8wcDhzMHhlYnpZTlJpbm5ZcVRBTUJnTlZIUk1CQWY4RUFqQUFNQk1HQ3lzR0FRUUJndVVjQWdFQkJBUURBZ1J3TUNFR0N5c0dBUVFCZ3VVY0FRRUVCQklFRUJMZTEwVkw3VWZVcTZybkUvVWRZNU13Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUk2R1NWaTEwcjY3M3VxdHNvKzJvQjZmNVM1Z0UwZmY0NHQzTmNRK1ROOU5BaUFDL1NDUCtlS3cxQm5tY1NnYnhjUXBZdVdqQlBNVkRmcWVnOHBibU9kSEt3PT0iLCJvQWpxMkNlTkEyL2d5NW14YzV0Vll4NVJ4RWFMektEMWxhMDdhZ3RmR28wPSJdLCJkaXNwbGF5TmFtZSI6IkZlaXRpYW4gQWxsLUluLVBhc3MifQ==";
        byte[] keyMaterialBytes = Convert.FromBase64String(encodedKeyMaterial);

        // Parse the FIDO key and check all fields
        var keyMaterial = KeyMaterialFido.ParseJson(keyMaterialBytes);
        Assert.AreEqual("Feitian All-In-Pass", keyMaterial.DisplayName);
        Assert.AreEqual("9e3808651ecfa53162772e9d2bc62bfe568350a9", keyMaterial.AttestationCertificates[0].Thumbprint.ToLowerInvariant());
        Assert.AreEqual("12ded745-4bed-47d4-abaa-e713f51d6393", keyMaterial.AuthenticatorData.AttestedCredentialData.AaGuid.ToString());
    }

    [TestMethod]
    public void FidoKeyMaterial_Parse_Null()
    {
        Assert.IsNull(KeyMaterialFido.ParseJson((string)null));
    }

    [TestMethod]
    public void FidoKeyMaterial_Parse_Empty_String()
    {
        Assert.IsNull(KeyMaterialFido.ParseJson(String.Empty));
    }

    [TestMethod]
    public void FidoKeyMaterial_Parse_Empty_Array()
    {
        Assert.IsNull(KeyMaterialFido.ParseJson([]));
    }
}
