﻿using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Principal;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    /// <summary>
    /// Adds DPAPI NG support for OtherKeyAttribute.
    /// </summary>
    partial struct OtherKeyAttribute
    {
        private const string ProtectionKeyDescriptorOid = "1.3.6.1.4.1.311.74.1";
        private const string SidProtectorOid = "1.3.6.1.4.1.311.74.1.1";
        private const string AndCombinerOid = "1.3.6.1.4.1.311.74.1.4";
        private const string SddlProtectorOid = "1.3.6.1.4.1.311.74.1.5";
        private const string WebCredentialsProtectorOid = "1.3.6.1.4.1.311.74.1.7";
        private const string LocalProtectorOid = "1.3.6.1.4.1.311.74.1.8";
        private const string CertificateProtectorOid = "1.3.6.1.4.1.311.74.1.11";
        private const string ExpectedSidName = "SID";
        public SecurityIdentifier? SidProtector
        {
            get
            {
                if (this.AttributeId != ProtectionKeyDescriptorOid || this.AttributeValue == null)
                {
                    // Unsupported key attribute OID or missing key attribute
                    return null;
                }

                /*
                https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-dnsp/093dba13-15c8-4848-b278-38afc5c0f5ed

                microsoft OBJECT IDENTIFIER ::= { iso(1) identified-organization(3) dod(6) internet(1) private(4) enterprise(1) 311 }
                msKeyProtection OBJECT IDENTIFIER := { microsoft 74 }
                protectionInfo OBJECT IDENTIFIER ::= { msKeyProtection 1 }
                sidProtected OBJECT IDENTIFIER ::= { protectionInfo 1 }
                sidName UTF8 STRING ::= "SID"
                ProtectionKeyAttribute ::= SEQUENCE {
                    protectionInfo OBJECT IDENTIFIER,
                    SEQUENCE SIZE (1) {
                        sidProtected OBJECT IDENTIFIER,
                        SEQUENCE SIZE (1) {
                            SEQUENCE SIZE (1) {
                                SEQUENCE SIZE (1) {
                                    sidName UTF8 STRING,
                                    sidString UTF8 STRING
                                }
                            }
                        }
                    }
                }
                */

                var protectorReader = new AsnReader(this.AttributeValue.Value, AsnEncodingRules.DER);
                var protector = protectorReader.ReadSequence();
                var protectorType = protector.ReadObjectIdentifier();

                if (protectorType != SidProtectorOid)
                {
                    // Not a SID protector
                    return null;
                }
                
                // TODO: What is the meaning of the 3 nested sequences?
                var sidProtector = protector.ReadSequence().ReadSequence().ReadSequence();
                string sidName = sidProtector.ReadCharacterString(UniversalTagNumber.UTF8String);

                if (sidName != "SID")
                {
                    throw new CryptographicException($"Unexpected SID name: {sidName}. Expected: {ExpectedSidName}.");
                }

                string sidString = sidProtector.ReadCharacterString(UniversalTagNumber.UTF8String);
                protectorReader.ThrowIfNotEmpty();
                return new SecurityIdentifier(sidString);
            }
        }
    }
}
