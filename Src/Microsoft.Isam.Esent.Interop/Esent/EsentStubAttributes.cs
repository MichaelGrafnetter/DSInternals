//-----------------------------------------------------------------------
// <copyright file="EsentStubAttributes.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//  Attribute stubs to allow compiling on CoreClr.
// </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#if !MANAGEDESENT_ON_WSA
This file should only be compiled with MANAGEDESENT_ON_WSA
#endif

    /// <summary>
    /// A fake enumeration to allow compilation on platforms that lack this enumeration.
    /// </summary>
    public enum SecurityAction
    {
        /// <summary>
        /// A fake enumeration to allow compilation on platforms that lack this enumeration.
        /// </summary>
        LinkDemand
    }

    /// <summary>
    /// A fake attribute to allow compilation on platforms that lack this attribute.
    /// </summary>
    //// The real one inherits from System.Security.Permissions.CodeAccessSecurityAttribute.
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    [AttributeUsageAttribute(
        AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor
            | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    internal sealed class SecurityPermissionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityPermissionAttribute"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        public SecurityPermissionAttribute(
            SecurityAction action)
        {
        }

        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// A fake attribute to allow compilation on platforms that lack this attribute.
    /// </summary>
    [ComVisibleAttribute(true)]
    [AttributeUsageAttribute(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    internal sealed class BestFitMappingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BestFitMappingAttribute"/> class.
        /// </summary>
        /// <param name="bestFitMapping">
        /// The best fit mapping.
        /// </param>
        public BestFitMappingAttribute(
            bool bestFitMapping)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether ThrowOnUnmappableChar.
        /// </summary>
        public bool ThrowOnUnmappableChar
        {
            get;
            set;
        }

        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// A fake attribute to allow compilation on platforms that lack this attribute.
    /// </summary>
    [ComVisibleAttribute(true)]
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true,
                             Inherited = false)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    internal sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
    {
        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// A fake attribute to allow compilation on platforms that lack this attribute.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
    [ComVisibleAttribute(true)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    internal sealed class ComVisibleAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComVisibleAttribute"/> class.
        /// </summary>
        /// <param name="comVisible">
        /// The com visible.
        /// </param>
        public ComVisibleAttribute(bool comVisible)
        {
        }

        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// Indicates that a class can be serialized. This class cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false), ComVisible(true)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    internal sealed class SerializableAttribute : Attribute
    {
        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// Indicates that a field of a serializable class should not be serialized. This class cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true), AttributeUsage(AttributeTargets.Field, Inherited = false)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here because it's a collection of trivial classes.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "These stub classes are compiled only on some platforms that do not contain the entire framework, e.g. new Windows user interface.")]
    internal sealed class NonSerializedAttribute : Attribute
    {
        /// <summary>
        /// Prints out the object's contents.
        /// </summary>
        /// <returns>A string represenetation or the object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

namespace System.Runtime.ConstrainedExecution
{
}

namespace System.Security.Cryptography
{
}

namespace System.Security.Permissions
{
}

namespace Microsoft.Win32.SafeHandles
{
}

namespace System.Runtime.ConstrainedExecution
{
    using System;
    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// The consistency model. A stub.
    /// </summary>
    internal enum Consistency
    {
        /// <summary>
        /// Might corrupt the process.
        /// </summary>
        MayCorruptProcess,

        /// <summary>
        /// Might corrupt the application domain.
        /// </summary>
        MayCorruptAppDomain,

        /// <summary>
        /// Might corrupt the instance.
        /// </summary>
        MayCorruptInstance,

        /// <summary>
        /// Will not corrupt the state.
        /// </summary>
        WillNotCorruptState,
    }

    /// <summary>
    /// The Crticial Execution Region description. A stub.
    /// </summary>
    internal enum Cer
    {
        /// <summary>
        /// No options.
        /// </summary>
        None,

        /// <summary>
        /// This might fail.
        /// </summary>
        MayFail,

        /// <summary>
        /// A successful CER.
        /// </summary>
        Success,
    }

    /// <summary>
    /// The description of the reliability contract. A stub.
    /// </summary>
    internal sealed class ReliabilityContractAttribute : Attribute
    {
        /// <summary>
        /// The consistency guarantee. A stub.
        /// </summary>
        private Consistency consistency;

        /// <summary>
        /// The critical execution region. A stub.
        /// </summary>
        private Cer cer;

        /// <summary>
        /// Initializes a new instance of the ReliabilityContractAttribute class. A stub.
        /// </summary>
        /// <param name="consistencyGuarantee">The guarantee of the consistency.</param>
        /// <param name="cer">The critical execution region description.</param>
        public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
        {
            this.consistency = consistencyGuarantee;
            this.cer = cer;
        }

        /// <summary>
        /// Gets the consistency guarantee. A stub.
        /// </summary>
        public Consistency ConsistencyGuarantee
        {
            get
            {
                return this.consistency;
            }
        }

        /// <summary>
        /// Gets the critical execution region. A stub.
        /// </summary>
        public Cer Cer
        {
            get
            {
                return this.cer;
            }
        }
    }
}
