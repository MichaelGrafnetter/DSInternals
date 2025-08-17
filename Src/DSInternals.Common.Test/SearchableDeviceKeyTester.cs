using System;
using DSInternals.Common.Data;
using DSInternals.Common.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace DSInternals.Common.Test
{
    /// <summary>
    /// Contains tests for parsing the Azure AD SearchableDeviceKey attribute.
    /// </summary>
    [TestClass]
    public class SearchableDeviceKeyTester
    {
        [TestMethod]
        public void SearchableDeviceKey_Parse_FIDO_Input1()
        {
            string jsonData = @"{
                'usage':'FIDO',
                'keyIdentifier':'GshyINLMaOOwqt1LNUi0gQ==',
                'keyMaterial':'eyJ2ZXJzaW9uIjoxLCJhdXRoRGF0YSI6Ik5XeWUxS0NUSWJscFh4NnZrWUlEOGJWZmFKMm1IN3lXR0V3VmZkcG9ESUhGQUFBQUdjdHBTQjZQOTBBNWsrd0tKeW1oVktnQUVCckljaURTekdqanNLcmRTelZJdElHbEFRSURKaUFCSVZnZ29uTkNlY2EwZE5LTzlaWXBGdjlvMCtlZ0VGQ1ZTeXJ0UmN1NndrMStoT2dpV0NDNWE3MFI0ZlNFZ0ZpNWxnQTRBVVBmN1Y0Q2p5VWcvb1VWTFEzem5kZnc1YUZyYUcxaFl5MXpaV055WlhUMSIsIng1YyI6WyJNSUlDdlRDQ0FhV2dBd0lCQWdJRUdLeEd3REFOQmdrcWhraUc5dzBCQVFzRkFEQXVNU3d3S2dZRFZRUURFeU5aZFdKcFkyOGdWVEpHSUZKdmIzUWdRMEVnVTJWeWFXRnNJRFExTnpJd01EWXpNVEFnRncweE5EQTRNREV3TURBd01EQmFHQTh5TURVd01Ea3dOREF3TURBd01Gb3diakVMTUFrR0ExVUVCaE1DVTBVeEVqQVFCZ05WQkFvTUNWbDFZbWxqYnlCQlFqRWlNQ0FHQTFVRUN3d1pRWFYwYUdWdWRHbGpZWFJ2Y2lCQmRIUmxjM1JoZEdsdmJqRW5NQ1VHQTFVRUF3d2VXWFZpYVdOdklGVXlSaUJGUlNCVFpYSnBZV3dnTkRFek9UUXpORGc0TUZrd0V3WUhLb1pJemowQ0FRWUlLb1pJemowREFRY0RRZ0FFZWVvN0xIeEpjQkJpSXd6U1ArdGc1U2t4Y2RTRDhRQytoWjFyRDRPWEF3RzFSczNVYnMvSzQrUHpENEhwN1dLOUpvMU1IcjAzczd5K2txakNydXRPT3FOc01Hb3dJZ1lKS3dZQkJBR0N4QW9DQkJVeExqTXVOaTR4TGpRdU1TNDBNVFE0TWk0eExqY3dFd1lMS3dZQkJBR0M1UndDQVFFRUJBTUNCU0F3SVFZTEt3WUJCQUdDNVJ3QkFRUUVFZ1FReTJsSUhvLzNRRG1UN0FvbkthRlVxREFNQmdOVkhSTUJBZjhFQWpBQU1BMEdDU3FHU0liM0RRRUJDd1VBQTRJQkFRQ1huUU9YMkdENEx1RmRNUng1YnJyN0l2cW40SVRadXJUR0c3dFg4K2Ewd1lwSU43aGNQRTdiNUlORDlOYWwyYkhPMm9yaC90U1JLU0Z6Qlk1ZTRjdmRhOXJBZFZmR29PalRhQ1c2Rlo1L3RhMk0ydmdFaG96NURvOGZpdW9Yd0JhMVhDcDYxSmZJbFB0eDExUFhtNXBJUzJ3M2JYSTdtWTB1SFVNR3Z4QXp0YTc0ektYTHNsYUxhU1FpYlNLaldLdDloK1NzWHk0SkdxY1ZlZk9sYVFsSmZYTDFUZ2E2d2NPMFFUdTZYcStVdzdaUE5QbnJwQnJMYXVLRGQyMDJSbE40U1A3b2hMM2Q5Ykc2VjVoVXovM091c05FQlpVbjVXM1ZtUGoxWm5GYXZrTUIzUmtSTU9hNThNWkFPUkpUNGltQVB6cnZKMHZ0djk0L3k3MUM2dFo1Il0sImRpc3BsYXlOYW1lIjoiWXViaUtleSA1In0=',
                'creationTime':'2019-12-12T09:42:21.2641041Z',
                'deviceId':'00000000-0000-0000-0000-000000000000',
                'customKeyInformation':'AQEAAAAAAAAAAAAAAAAA',
                'fidoAaGuid':'cb69481e-8ff7-4039-93ec-0a2729a154a8',
                'fidoAuthenticatorVersion':null,
                'fidoAttestationCertificates':['e7d092ba192fdbbb2f36552832d616126971a269']
             }";

            // Parse the FIDO key and check all fields
            var keyCredential = KeyCredential.ParseJson(jsonData);
            Assert.AreEqual(KeyUsage.FIDO, keyCredential.Usage);
            Assert.AreEqual("YubiKey 5", keyCredential.FidoKeyMaterial.DisplayName);
            Assert.AreEqual("GshyINLMaOOwqt1LNUi0gQ==", keyCredential.Identifier);
            Assert.AreEqual(2019, keyCredential.CreationTime.Year);
            Assert.IsTrue(keyCredential.CustomKeyInfo.Flags.HasFlag(KeyFlags.Attestation));
            Assert.AreEqual("e7d092ba192fdbbb2f36552832d616126971a269", keyCredential.FidoKeyMaterial.AttestationCertificates[0].Thumbprint.ToLowerInvariant());
            Assert.AreEqual("cb69481e-8ff7-4039-93ec-0a2729a154a8", keyCredential.FidoKeyMaterial.AuthenticatorData.AttestedCredentialData.AaGuid.ToString());

            // Serialize the object again and compare with the original
            string normalized = JsonSerializer.Serialize(
                LenientJsonSerializer.DeserializeLenient<JsonElement>(jsonData),
                LenientJsonSerializer.Options);
            Assert.AreEqual(normalized, keyCredential.ToJson());
        }

        [TestMethod]
        public void SearchableDeviceKey_Parse_FIDO_Input2()
        {
            string jsonData = @"{
                'usage':'FIDO',
                'keyIdentifier':'lSjOUI3XUtloTxC0ddVJtPLKZMQyKWKrTgXWadGmH9c=',
                'keyMaterial':'eyJ2ZXJzaW9uIjoxLCJhdXRoRGF0YSI6Ik5XeWUxS0NUSWJscFh4NnZrWUlEOGJWZmFKMm1IN3lXR0V3VmZkcG9ESUhGQUFBRmRoTGUxMFZMN1VmVXE2cm5FL1VkWTVNQUlKVW96bENOMTFMWmFFOFF0SFhWU2JUeXltVEVNaWxpcTA0RjFtblJwaC9YcFFFQ0F5WWdBU0ZZSVAzWms5Vm5URUpONlQ4VWgxMHRPcUpmdDRicW1leVJUUzhvNkJQK2prYkNJbGdnYjFnR0dNMnM3T2dDMTNQdkZBVTJ1UDZJcVVGWWdiaGxQc3diUzRDK1FoV2hhMmh0WVdNdGMyVmpjbVYwOVE9PSIsIng1YyI6WyJNSUlDUXpDQ0FlbWdBd0lCQWdJUUhmSzFXbEhjUzJpRm85bWVhWC90RmpBS0JnZ3Foa2pPUFFRREFqQkpNUXN3Q1FZRFZRUUdFd0pWVXpFZE1Cc0dBMVVFQ2d3VVJtVnBkR2xoYmlCVVpXTm9ibTlzYjJkcFpYTXhHekFaQmdOVkJBTU1Fa1psYVhScFlXNGdSa2xFVHlCRFFTQXdNekFnRncweE9ERXlNalV3TURBd01EQmFHQTh5TURNek1USXlOREl6TlRrMU9Wb3djREVMTUFrR0ExVUVCaE1DVlZNeEhUQWJCZ05WQkFvTUZFWmxhWFJwWVc0Z1ZHVmphRzV2Ykc5bmFXVnpNU0l3SUFZRFZRUUxEQmxCZFhSb1pXNTBhV05oZEc5eUlFRjBkR1Z6ZEdGMGFXOXVNUjR3SEFZRFZRUUREQlZHVkNCQ2FXOVFZWE56SUVaSlJFOHlJREEwTnpBd1dUQVRCZ2NxaGtqT1BRSUJCZ2dxaGtqT1BRTUJCd05DQUFTNjJoSWJ5ZW5IOVdQbnpZSGVoYUJSM0M3cXN3b21aa2FQekd5VWxGUmlKSU1vM3VJVGVJbUZPRmZOY0R1T3pvcTF3Y1hYR1RtRXRFdHhGMndvOW5va280R0pNSUdHTUIwR0ExVWREZ1FXQkJTQkkxWG9MRFkxL0hKYWJhK1czMm54aHhwM1dqQWZCZ05WSFNNRUdEQVdnQlJCdC94TmRjcU8wcDhzMHhlYnpZTlJpbm5ZcVRBTUJnTlZIUk1CQWY4RUFqQUFNQk1HQ3lzR0FRUUJndVVjQWdFQkJBUURBZ1J3TUNFR0N5c0dBUVFCZ3VVY0FRRUVCQklFRUJMZTEwVkw3VWZVcTZybkUvVWRZNU13Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUk2R1NWaTEwcjY3M3VxdHNvKzJvQjZmNVM1Z0UwZmY0NHQzTmNRK1ROOU5BaUFDL1NDUCtlS3cxQm5tY1NnYnhjUXBZdVdqQlBNVkRmcWVnOHBibU9kSEt3PT0iLCJvQWpxMkNlTkEyL2d5NW14YzV0Vll4NVJ4RWFMektEMWxhMDdhZ3RmR28wPSJdLCJkaXNwbGF5TmFtZSI6IkZlaXRpYW4gQWxsLUluLVBhc3MifQ==',
                'creationTime':'2020-03-31T21:41:44.8757516Z',
                'deviceId':'00000000-0000-0000-0000-000000000000',
                'customKeyInformation':'AQEAAAAAAAAAAAAAAAAA',
                'fidoAaGuid':'12ded745-4bed-47d4-abaa-e713f51d6393',
                'fidoAuthenticatorVersion':null,
                'fidoAttestationCertificates':['4f94e8b002c79416507e9d9d390eca3153e1a5e8', '9e3808651ecfa53162772e9d2bc62bfe568350a9']
             }";

            // Parse the FIDO key and check all fields
            var keyCredential = KeyCredential.ParseJson(jsonData);
            Assert.AreEqual(KeyUsage.FIDO, keyCredential.Usage);
            Assert.AreEqual("Feitian All-In-Pass", keyCredential.FidoKeyMaterial.DisplayName);
            Assert.AreEqual("lSjOUI3XUtloTxC0ddVJtPLKZMQyKWKrTgXWadGmH9c=", keyCredential.Identifier);
            Assert.AreEqual(2020, keyCredential.CreationTime.Year);
            Assert.IsTrue(keyCredential.CustomKeyInfo.Flags.HasFlag(KeyFlags.Attestation));
            Assert.AreEqual("9e3808651ecfa53162772e9d2bc62bfe568350a9", keyCredential.FidoKeyMaterial.AttestationCertificates[0].Thumbprint.ToLowerInvariant());
            Assert.AreEqual("12ded745-4bed-47d4-abaa-e713f51d6393", keyCredential.FidoKeyMaterial.AuthenticatorData.AttestedCredentialData.AaGuid.ToString());
        }

        [TestMethod]
        public void SearchableDeviceKey_Serialize_FIDO_Input2()
        {
            string jsonData = @"{
                'usage':'FIDO',
                'keyIdentifier':'lSjOUI3XUtloTxC0ddVJtPLKZMQyKWKrTgXWadGmH9c=',
                'keyMaterial':'eyJ2ZXJzaW9uIjoxLCJhdXRoRGF0YSI6Ik5XeWUxS0NUSWJscFh4NnZrWUlEOGJWZmFKMm1IN3lXR0V3VmZkcG9ESUhGQUFBRmRoTGUxMFZMN1VmVXE2cm5FL1VkWTVNQUlKVW96bENOMTFMWmFFOFF0SFhWU2JUeXltVEVNaWxpcTA0RjFtblJwaC9YcFFFQ0F5WWdBU0ZZSVAzWms5Vm5URUpONlQ4VWgxMHRPcUpmdDRicW1leVJUUzhvNkJQK2prYkNJbGdnYjFnR0dNMnM3T2dDMTNQdkZBVTJ1UDZJcVVGWWdiaGxQc3diUzRDK1FoV2hhMmh0WVdNdGMyVmpjbVYwOVE9PSIsIng1YyI6WyJNSUlDUXpDQ0FlbWdBd0lCQWdJUUhmSzFXbEhjUzJpRm85bWVhWC90RmpBS0JnZ3Foa2pPUFFRREFqQkpNUXN3Q1FZRFZRUUdFd0pWVXpFZE1Cc0dBMVVFQ2d3VVJtVnBkR2xoYmlCVVpXTm9ibTlzYjJkcFpYTXhHekFaQmdOVkJBTU1Fa1psYVhScFlXNGdSa2xFVHlCRFFTQXdNekFnRncweE9ERXlNalV3TURBd01EQmFHQTh5TURNek1USXlOREl6TlRrMU9Wb3djREVMTUFrR0ExVUVCaE1DVlZNeEhUQWJCZ05WQkFvTUZFWmxhWFJwWVc0Z1ZHVmphRzV2Ykc5bmFXVnpNU0l3SUFZRFZRUUxEQmxCZFhSb1pXNTBhV05oZEc5eUlFRjBkR1Z6ZEdGMGFXOXVNUjR3SEFZRFZRUUREQlZHVkNCQ2FXOVFZWE56SUVaSlJFOHlJREEwTnpBd1dUQVRCZ2NxaGtqT1BRSUJCZ2dxaGtqT1BRTUJCd05DQUFTNjJoSWJ5ZW5IOVdQbnpZSGVoYUJSM0M3cXN3b21aa2FQekd5VWxGUmlKSU1vM3VJVGVJbUZPRmZOY0R1T3pvcTF3Y1hYR1RtRXRFdHhGMndvOW5va280R0pNSUdHTUIwR0ExVWREZ1FXQkJTQkkxWG9MRFkxL0hKYWJhK1czMm54aHhwM1dqQWZCZ05WSFNNRUdEQVdnQlJCdC94TmRjcU8wcDhzMHhlYnpZTlJpbm5ZcVRBTUJnTlZIUk1CQWY4RUFqQUFNQk1HQ3lzR0FRUUJndVVjQWdFQkJBUURBZ1J3TUNFR0N5c0dBUVFCZ3VVY0FRRUVCQklFRUJMZTEwVkw3VWZVcTZybkUvVWRZNU13Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUk2R1NWaTEwcjY3M3VxdHNvKzJvQjZmNVM1Z0UwZmY0NHQzTmNRK1ROOU5BaUFDL1NDUCtlS3cxQm5tY1NnYnhjUXBZdVdqQlBNVkRmcWVnOHBibU9kSEt3PT0iLCJvQWpxMkNlTkEyL2d5NW14YzV0Vll4NVJ4RWFMektEMWxhMDdhZ3RmR28wPSJdLCJkaXNwbGF5TmFtZSI6IkZlaXRpYW4gQWxsLUluLVBhc3MifQ==',
                'creationTime':'2020-03-31T21:41:44.8757516Z',
                'deviceId':'00000000-0000-0000-0000-000000000000',
                'customKeyInformation':'AQEAAAAAAAAAAAAAAAAA',
                'fidoAaGuid':'12ded745-4bed-47d4-abaa-e713f51d6393',
                'fidoAuthenticatorVersion':null,
                'fidoAttestationCertificates':['4f94e8b002c79416507e9d9d390eca3153e1a5e8', '9e3808651ecfa53162772e9d2bc62bfe568350a9']
             }";

            // Parse the FIDO key from the SearchableDeviceKey_Parse_FIDO_Input2 test
            var keyCredential = KeyCredential.ParseJson(jsonData);

            throw new AssertInconclusiveException("FIDO2 JSON serialization must be improved.");

            // This test fill fail for now:
            Assert.AreEqual(2, keyCredential.FidoKeyMaterial.AttestationCertificates.Count);

            // Serialize the object again and compare with the original
            string normalized = JsonSerializer.Serialize(
                LenientJsonSerializer.DeserializeLenient<JsonElement>(jsonData),
                LenientJsonSerializer.Options);
            Assert.AreEqual(normalized, keyCredential.ToJson());
        }

        [TestMethod]
        public void SearchableDeviceKey_Parse_NGC()
        {
            string jsonData = @"{
                'usage':'NGC',
                'keyIdentifier':'6eHBLoX0uOrd/hOsFVyQK8Rk2iqLTubd5nV80SMh+z4=',
                'keyMaterial':'UlNBMQAIAAADAAAAAAEAAAAAAAAAAAAAAQAByI8SMTWloiGnJjhv1A3o/n7FquciD7pKgFPcwbuzJK3NhmyCXuKMqrxW2zod4l9juNj9EPV8Y/YaZYvkNX5fTmSvmHihrAcdT6Kugej9GObFW1LH10yQnNYGP9X+te//tsNe90a8ORaDsOEq3gwRo1xyq14MxozgWbyRYA+Y+yfjdS+4cyP/054/pflLDHjSGKyvAaka0nJ5FzaTQ/YSupqVWgRX97BGk8ClpDm/zc0kRS0w3e4uEFcyrlDb8tccZ6lxVl7nJiIUuMEq/kyLn2lL7ISf5U3ter2/MEnU7T4wxMbIG7gUfAr4wh4DT3dI4IhQuhYdHe4s6a9lr4gppQ==',
                'creationTime':'2015-11-17T08:17:13.7724773Z',
                'deviceId':'cbad3c94-b480-4fa6-9187-ff1ed42c4479',
                'customKeyInformation':'AQA=',
                'fidoAaGuid':null,
                'fidoAuthenticatorVersion':null,
                'fidoAttestationCertificates':[]
            }";

            // Parse the NGC key and check all fields
            var parsedKey = KeyCredential.ParseJson(jsonData);
            Assert.AreEqual(KeyUsage.NGC, parsedKey.Usage);
            Assert.AreEqual("6eHBLoX0uOrd/hOsFVyQK8Rk2iqLTubd5nV80SMh+z4=", parsedKey.Identifier);
            Assert.AreEqual(2015, parsedKey.CreationTime.Year);
            Assert.AreEqual(KeyFlags.None, parsedKey.CustomKeyInfo.Flags);
            Assert.AreEqual(1, parsedKey.CustomKeyInfo.Version);
            Assert.AreEqual("cbad3c94-b480-4fa6-9187-ff1ed42c4479", parsedKey.DeviceId.Value.ToString().ToLowerInvariant());

            // Serialize the object again and compare with the original
            string normalized = JsonSerializer.Serialize(
                LenientJsonSerializer.DeserializeLenient<JsonElement>(jsonData),
                LenientJsonSerializer.Options);
            Assert.AreEqual(normalized, parsedKey.ToJson());

            // Re-generate the identifier and check that it matches the value in AAD.
            var generatedKey = new KeyCredential(
                parsedKey.RawKeyMaterial,
                Guid.Parse("cbad3c94-b480-4fa6-9187-ff1ed42c4479"),
                "CN=Test",
                DateTime.Parse("2015-11-17T08:17:13.7724773Z")
                );
            Assert.AreEqual(parsedKey.Identifier, generatedKey.Identifier);

            // Serialize the generated object and compare with the original
            Assert.AreEqual(normalized, generatedKey.ToJson());
        }

        [TestMethod]
        public void SearchableDeviceKey_Parse_Null()
        {
            Assert.IsNull(KeyCredential.ParseJson(null));
        }

        [TestMethod]
        public void SearchableDeviceKey_Parse_Empty()
        {
            Assert.IsNull(KeyCredential.ParseJson(String.Empty));
        }
    }
}
