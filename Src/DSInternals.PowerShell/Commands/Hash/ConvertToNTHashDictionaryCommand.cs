namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Interop;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management.Automation;

    [Cmdlet(VerbsData.ConvertTo, "NTHashDictionary")]
    [OutputType(new Type[] { typeof(IDictionary<byte[], string>) })]
    public class ConvertToNTHashDictionaryCommand : PSCmdlet
    {
        #region Parameters

        // HACK: This parameter is not called assword to pass the PSScriptAnalyzer tests. The purpose of this command is to calculate the hashes of password lists stored in text files, so there is no need to protect them by SecureStrings.
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0
        )]
        [Alias("Password")]
        [ValidateNotNullOrEmpty]
        public string[] Input
        {
            get;
            set;
        }

        #endregion Parameters

        #region Fields

        private IDictionary<byte[], string> hashDictionary;
        
        #endregion Fields

        #region Cmdlet Overrides
        protected override void BeginProcessing()
        {
            this.hashDictionary = new Dictionary<byte[], string>(HashEqualityComparer.GetInstance());
        }

        protected override void ProcessRecord()
        {
            foreach(string password in this.Input)
            {
                if(string.IsNullOrEmpty(password))
                {
                    // Skip empty lines from the input.
                    continue;
                }
                try
                {
                    byte[] hash = NTHash.ComputeHash(password);
                    if(!this.hashDictionary.ContainsKey(hash))
                    {
                        // Do not try to add duplicate hashes, because the Add method would throw an ArgumentException.
                        this.hashDictionary.Add(hash, password);
                    }
                }
                catch (ArgumentException ex)
                {
                    ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.InvalidArgument, password);
                    this.WriteError(error);
                }
                catch (Win32Exception ex)
                {
                    ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
                    ErrorRecord error = new ErrorRecord(ex, "Error2", category, password);
                    this.WriteError(error);
                }
                catch (Exception ex)
                {
                    ErrorRecord error = new ErrorRecord(ex, "Error3", ErrorCategory.NotSpecified, password);
                    this.WriteError(error);
                }
            }
        }
        protected override void EndProcessing()
        {
            this.WriteObject(this.hashDictionary, false);
        }
        #endregion Cmdlet Overrides
    }
}