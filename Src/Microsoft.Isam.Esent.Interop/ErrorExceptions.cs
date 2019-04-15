//-----------------------------------------------------------------------
// <copyright file="ErrorExceptions.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

// Auto generated

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    // The basic exception hierarchy ...
    //
    // EsentErrorException
    //    |
    //    |-- EsentOperationException
    //    |     |-- EsentFatalException
    //    |     |-- EsentIOException               // bad IO issues, may or may not be transient.
    //    |     |-- EsentResourceException
    //    |           |-- EsentMemoryException    // out of memory (all variants)
    //    |           |-- EsentQuotaException    
    //    |           |-- EsentDiskException    // out of disk space (all variants)
    //    |-- EsentDataException
    //    |     |-- EsentCorruptionException
    //    |     |-- EsentInconsistentException
    //    |     |-- EsentFragmentationException
    //    |-- EsentApiException
    //          |-- EsentUsageException
    //          |-- EsentStateException
    //          |-- EsentObsoleteException

    /// <summary>
    /// Base class for Operation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentOperationException : EsentErrorException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOperationException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentOperationException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOperationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentOperationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Data exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentDataException : EsentErrorException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDataException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentDataException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDataException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentDataException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Api exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentApiException : EsentErrorException
    {
        /// <summary>
        /// Initializes a new instance of the EsentApiException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentApiException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentApiException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentApiException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Fatal exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentFatalException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFatalException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentFatalException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFatalException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentFatalException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for IO exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentIOException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIOException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentIOException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIOException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentIOException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Resource exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentResourceException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentResourceException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentResourceException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentResourceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentResourceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Memory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentMemoryException : EsentResourceException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMemoryException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentMemoryException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMemoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentMemoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Quota exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentQuotaException : EsentResourceException
    {
        /// <summary>
        /// Initializes a new instance of the EsentQuotaException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentQuotaException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentQuotaException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentQuotaException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Disk exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentDiskException : EsentResourceException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDiskException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentDiskException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDiskException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentDiskException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Corruption exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentCorruptionException : EsentDataException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCorruptionException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentCorruptionException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCorruptionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentCorruptionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Inconsistent exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentInconsistentException : EsentDataException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInconsistentException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentInconsistentException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInconsistentException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentInconsistentException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Fragmentation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentFragmentationException : EsentDataException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFragmentationException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentFragmentationException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFragmentationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentFragmentationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Usage exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentUsageException : EsentApiException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUsageException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentUsageException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUsageException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentUsageException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for State exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentStateException : EsentApiException
    {
        /// <summary>
        /// Initializes a new instance of the EsentStateException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentStateException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentStateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentStateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for Obsolete exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public abstract class EsentObsoleteException : EsentApiException
    {
        /// <summary>
        /// Initializes a new instance of the EsentObsoleteException class.
        /// </summary>
        /// <param name="description">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        protected EsentObsoleteException(string description, JET_err err) :
            base(description, err)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentObsoleteException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentObsoleteException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RfsFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRfsFailureException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRfsFailureException class.
        /// </summary>
        public EsentRfsFailureException() :
            base("Resource Failure Simulator failure", JET_err.RfsFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRfsFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRfsFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RfsNotArmed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRfsNotArmedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRfsNotArmedException class.
        /// </summary>
        public EsentRfsNotArmedException() :
            base("Resource Failure Simulator not initialized", JET_err.RfsNotArmed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRfsNotArmedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRfsNotArmedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileClose exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileCloseException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileCloseException class.
        /// </summary>
        public EsentFileCloseException() :
            base("Could not close file", JET_err.FileClose)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileCloseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileCloseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfThreads exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfThreadsException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfThreadsException class.
        /// </summary>
        public EsentOutOfThreadsException() :
            base("Could not start thread", JET_err.OutOfThreads)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfThreadsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfThreadsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyIO exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyIOException : EsentResourceException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyIOException class.
        /// </summary>
        public EsentTooManyIOException() :
            base("System busy due to too many IOs", JET_err.TooManyIO)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyIOException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyIOException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TaskDropped exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTaskDroppedException : EsentResourceException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTaskDroppedException class.
        /// </summary>
        public EsentTaskDroppedException() :
            base("A requested async task could not be executed", JET_err.TaskDropped)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTaskDroppedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTaskDroppedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InternalError exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInternalErrorException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInternalErrorException class.
        /// </summary>
        public EsentInternalErrorException() :
            base("Fatal internal error", JET_err.InternalError)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInternalErrorException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInternalErrorException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DisabledFunctionality exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDisabledFunctionalityException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDisabledFunctionalityException class.
        /// </summary>
        public EsentDisabledFunctionalityException() :
            base("You are running MinESE, that does not have all features compiled in.  This functionality is only supported in a full version of ESE.", JET_err.DisabledFunctionality)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDisabledFunctionalityException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDisabledFunctionalityException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UnloadableOSFunctionality exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUnloadableOSFunctionalityException : EsentFatalException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUnloadableOSFunctionalityException class.
        /// </summary>
        public EsentUnloadableOSFunctionalityException() :
            base("The desired OS functionality could not be located and loaded / linked.", JET_err.UnloadableOSFunctionality)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUnloadableOSFunctionalityException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUnloadableOSFunctionalityException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseBufferDependenciesCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseBufferDependenciesCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseBufferDependenciesCorruptedException class.
        /// </summary>
        public EsentDatabaseBufferDependenciesCorruptedException() :
            base("Buffer dependencies improperly set. Recovery failure", JET_err.DatabaseBufferDependenciesCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseBufferDependenciesCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseBufferDependenciesCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PreviousVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPreviousVersionException : EsentErrorException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPreviousVersionException class.
        /// </summary>
        public EsentPreviousVersionException() :
            base("Version already existed. Recovery failure", JET_err.PreviousVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPreviousVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPreviousVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PageBoundary exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPageBoundaryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPageBoundaryException class.
        /// </summary>
        public EsentPageBoundaryException() :
            base("Reached Page Boundary", JET_err.PageBoundary)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPageBoundaryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPageBoundaryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.KeyBoundary exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentKeyBoundaryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentKeyBoundaryException class.
        /// </summary>
        public EsentKeyBoundaryException() :
            base("Reached Key Boundary", JET_err.KeyBoundary)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentKeyBoundaryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentKeyBoundaryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadPageLink exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadPageLinkException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadPageLinkException class.
        /// </summary>
        public EsentBadPageLinkException() :
            base("Database corrupted", JET_err.BadPageLink)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadPageLinkException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadPageLinkException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadBookmark exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadBookmarkException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadBookmarkException class.
        /// </summary>
        public EsentBadBookmarkException() :
            base("Bookmark has no corresponding address in database", JET_err.BadBookmark)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadBookmarkException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadBookmarkException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NTSystemCallFailed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNTSystemCallFailedException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNTSystemCallFailedException class.
        /// </summary>
        public EsentNTSystemCallFailedException() :
            base("A call to the operating system failed", JET_err.NTSystemCallFailed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNTSystemCallFailedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNTSystemCallFailedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadParentPageLink exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadParentPageLinkException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadParentPageLinkException class.
        /// </summary>
        public EsentBadParentPageLinkException() :
            base("Database corrupted", JET_err.BadParentPageLink)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadParentPageLinkException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadParentPageLinkException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SPAvailExtCacheOutOfSync exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSPAvailExtCacheOutOfSyncException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSPAvailExtCacheOutOfSyncException class.
        /// </summary>
        public EsentSPAvailExtCacheOutOfSyncException() :
            base("AvailExt cache doesn't match btree", JET_err.SPAvailExtCacheOutOfSync)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSPAvailExtCacheOutOfSyncException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSPAvailExtCacheOutOfSyncException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SPAvailExtCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSPAvailExtCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSPAvailExtCorruptedException class.
        /// </summary>
        public EsentSPAvailExtCorruptedException() :
            base("AvailExt space tree is corrupt", JET_err.SPAvailExtCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSPAvailExtCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSPAvailExtCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SPAvailExtCacheOutOfMemory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSPAvailExtCacheOutOfMemoryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSPAvailExtCacheOutOfMemoryException class.
        /// </summary>
        public EsentSPAvailExtCacheOutOfMemoryException() :
            base("Out of memory allocating an AvailExt cache node", JET_err.SPAvailExtCacheOutOfMemory)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSPAvailExtCacheOutOfMemoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSPAvailExtCacheOutOfMemoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SPOwnExtCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSPOwnExtCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSPOwnExtCorruptedException class.
        /// </summary>
        public EsentSPOwnExtCorruptedException() :
            base("OwnExt space tree is corrupt", JET_err.SPOwnExtCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSPOwnExtCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSPOwnExtCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DbTimeCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDbTimeCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDbTimeCorruptedException class.
        /// </summary>
        public EsentDbTimeCorruptedException() :
            base("Dbtime on current page is greater than global database dbtime", JET_err.DbTimeCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDbTimeCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDbTimeCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.KeyTruncated exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentKeyTruncatedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentKeyTruncatedException class.
        /// </summary>
        public EsentKeyTruncatedException() :
            base("key truncated on index that disallows key truncation", JET_err.KeyTruncated)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentKeyTruncatedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentKeyTruncatedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseLeakInSpace exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseLeakInSpaceException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseLeakInSpaceException class.
        /// </summary>
        public EsentDatabaseLeakInSpaceException() :
            base("Some database pages have become unreachable even from the avail tree, only an offline defragmentation can return the lost space.", JET_err.DatabaseLeakInSpace)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseLeakInSpaceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseLeakInSpaceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadEmptyPage exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadEmptyPageException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadEmptyPageException class.
        /// </summary>
        public EsentBadEmptyPageException() :
            base("Database corrupted. Searching an unexpectedly empty page.", JET_err.BadEmptyPage)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadEmptyPageException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadEmptyPageException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadLineCount exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadLineCountException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadLineCountException class.
        /// </summary>
        public EsentBadLineCountException() :
            base("Number of lines on the page is too few compared to the line being operated on", JET_err.BadLineCount)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadLineCountException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadLineCountException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.KeyTooBig exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentKeyTooBigException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentKeyTooBigException class.
        /// </summary>
        public EsentKeyTooBigException() :
            base("Key is too large", JET_err.KeyTooBig)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentKeyTooBigException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentKeyTooBigException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotSeparateIntrinsicLV exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotSeparateIntrinsicLVException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotSeparateIntrinsicLVException class.
        /// </summary>
        public EsentCannotSeparateIntrinsicLVException() :
            base("illegal attempt to separate an LV which must be intrinsic", JET_err.CannotSeparateIntrinsicLV)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotSeparateIntrinsicLVException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotSeparateIntrinsicLVException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SeparatedLongValue exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSeparatedLongValueException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSeparatedLongValueException class.
        /// </summary>
        public EsentSeparatedLongValueException() :
            base("Operation not supported on separated long-value", JET_err.SeparatedLongValue)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSeparatedLongValueException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSeparatedLongValueException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MustBeSeparateLongValue exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMustBeSeparateLongValueException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMustBeSeparateLongValueException class.
        /// </summary>
        public EsentMustBeSeparateLongValueException() :
            base("Can only preread long value columns that can be separate, e.g. not size constrained so that they are fixed or variable columns", JET_err.MustBeSeparateLongValue)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMustBeSeparateLongValueException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMustBeSeparateLongValueException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidPreread exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidPrereadException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidPrereadException class.
        /// </summary>
        public EsentInvalidPrereadException() :
            base("Cannot preread long values when current index secondary", JET_err.InvalidPreread)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidPrereadException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidPrereadException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidColumnReference exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidColumnReferenceException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidColumnReferenceException class.
        /// </summary>
        public EsentInvalidColumnReferenceException() :
            base("Column reference is invalid", JET_err.InvalidColumnReference)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidColumnReferenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidColumnReferenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.StaleColumnReference exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentStaleColumnReferenceException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentStaleColumnReferenceException class.
        /// </summary>
        public EsentStaleColumnReferenceException() :
            base("Column reference is stale", JET_err.StaleColumnReference)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentStaleColumnReferenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentStaleColumnReferenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CompressionIntegrityCheckFailed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCompressionIntegrityCheckFailedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCompressionIntegrityCheckFailedException class.
        /// </summary>
        public EsentCompressionIntegrityCheckFailedException() :
            base("A compression integrity check failed. Decompressing data failed the integrity checksum indicating a data corruption in the compress/decompress pipeline.", JET_err.CompressionIntegrityCheckFailed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCompressionIntegrityCheckFailedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCompressionIntegrityCheckFailedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLoggedOperation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLoggedOperationException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLoggedOperationException class.
        /// </summary>
        public EsentInvalidLoggedOperationException() :
            base("Logged operation cannot be redone", JET_err.InvalidLoggedOperation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLoggedOperationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLoggedOperationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogFileCorrupt exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogFileCorruptException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogFileCorruptException class.
        /// </summary>
        public EsentLogFileCorruptException() :
            base("Log file is corrupt", JET_err.LogFileCorrupt)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogFileCorruptException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogFileCorruptException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NoBackupDirectory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNoBackupDirectoryException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNoBackupDirectoryException class.
        /// </summary>
        public EsentNoBackupDirectoryException() :
            base("No backup directory given", JET_err.NoBackupDirectory)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNoBackupDirectoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNoBackupDirectoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BackupDirectoryNotEmpty exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBackupDirectoryNotEmptyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBackupDirectoryNotEmptyException class.
        /// </summary>
        public EsentBackupDirectoryNotEmptyException() :
            base("The backup directory is not emtpy", JET_err.BackupDirectoryNotEmpty)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBackupDirectoryNotEmptyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBackupDirectoryNotEmptyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BackupInProgress exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBackupInProgressException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBackupInProgressException class.
        /// </summary>
        public EsentBackupInProgressException() :
            base("Backup is active already", JET_err.BackupInProgress)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBackupInProgressException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBackupInProgressException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RestoreInProgress exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRestoreInProgressException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRestoreInProgressException class.
        /// </summary>
        public EsentRestoreInProgressException() :
            base("Restore in progress", JET_err.RestoreInProgress)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRestoreInProgressException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRestoreInProgressException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingPreviousLogFile exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingPreviousLogFileException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingPreviousLogFileException class.
        /// </summary>
        public EsentMissingPreviousLogFileException() :
            base("Missing the log file for check point", JET_err.MissingPreviousLogFile)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingPreviousLogFileException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingPreviousLogFileException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogWriteFail exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogWriteFailException : EsentIOException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogWriteFailException class.
        /// </summary>
        public EsentLogWriteFailException() :
            base("Failure writing to log file", JET_err.LogWriteFail)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogWriteFailException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogWriteFailException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogDisabledDueToRecoveryFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogDisabledDueToRecoveryFailureException : EsentFatalException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogDisabledDueToRecoveryFailureException class.
        /// </summary>
        public EsentLogDisabledDueToRecoveryFailureException() :
            base("Try to log something after recovery faild", JET_err.LogDisabledDueToRecoveryFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogDisabledDueToRecoveryFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogDisabledDueToRecoveryFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotLogDuringRecoveryRedo exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotLogDuringRecoveryRedoException : EsentErrorException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotLogDuringRecoveryRedoException class.
        /// </summary>
        public EsentCannotLogDuringRecoveryRedoException() :
            base("Try to log something during recovery redo", JET_err.CannotLogDuringRecoveryRedo)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotLogDuringRecoveryRedoException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotLogDuringRecoveryRedoException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogGenerationMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogGenerationMismatchException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogGenerationMismatchException class.
        /// </summary>
        public EsentLogGenerationMismatchException() :
            base("Name of logfile does not match internal generation number", JET_err.LogGenerationMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogGenerationMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogGenerationMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadLogVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadLogVersionException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadLogVersionException class.
        /// </summary>
        public EsentBadLogVersionException() :
            base("Version of log file is not compatible with Jet version", JET_err.BadLogVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadLogVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadLogVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLogSequence exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLogSequenceException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLogSequenceException class.
        /// </summary>
        public EsentInvalidLogSequenceException() :
            base("Timestamp in next log does not match expected", JET_err.InvalidLogSequence)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLogSequenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLogSequenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LoggingDisabled exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLoggingDisabledException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLoggingDisabledException class.
        /// </summary>
        public EsentLoggingDisabledException() :
            base("Log is not active", JET_err.LoggingDisabled)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLoggingDisabledException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLoggingDisabledException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogBufferTooSmall exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogBufferTooSmallException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogBufferTooSmallException class.
        /// </summary>
        public EsentLogBufferTooSmallException() :
            base("Log buffer is too small for recovery", JET_err.LogBufferTooSmall)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogBufferTooSmallException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogBufferTooSmallException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogSequenceEnd exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogSequenceEndException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogSequenceEndException class.
        /// </summary>
        public EsentLogSequenceEndException() :
            base("Maximum log file number exceeded", JET_err.LogSequenceEnd)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogSequenceEndException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogSequenceEndException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NoBackup exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNoBackupException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNoBackupException class.
        /// </summary>
        public EsentNoBackupException() :
            base("No backup in progress", JET_err.NoBackup)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNoBackupException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNoBackupException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidBackupSequence exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidBackupSequenceException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBackupSequenceException class.
        /// </summary>
        public EsentInvalidBackupSequenceException() :
            base("Backup call out of sequence", JET_err.InvalidBackupSequence)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBackupSequenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidBackupSequenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BackupNotAllowedYet exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBackupNotAllowedYetException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBackupNotAllowedYetException class.
        /// </summary>
        public EsentBackupNotAllowedYetException() :
            base("Cannot do backup now", JET_err.BackupNotAllowedYet)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBackupNotAllowedYetException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBackupNotAllowedYetException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DeleteBackupFileFail exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDeleteBackupFileFailException : EsentIOException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDeleteBackupFileFailException class.
        /// </summary>
        public EsentDeleteBackupFileFailException() :
            base("Could not delete backup file", JET_err.DeleteBackupFileFail)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDeleteBackupFileFailException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDeleteBackupFileFailException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MakeBackupDirectoryFail exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMakeBackupDirectoryFailException : EsentIOException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMakeBackupDirectoryFailException class.
        /// </summary>
        public EsentMakeBackupDirectoryFailException() :
            base("Could not make backup temp directory", JET_err.MakeBackupDirectoryFail)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMakeBackupDirectoryFailException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMakeBackupDirectoryFailException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidBackup exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidBackupException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBackupException class.
        /// </summary>
        public EsentInvalidBackupException() :
            base("Cannot perform incremental backup when circular logging enabled", JET_err.InvalidBackup)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBackupException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidBackupException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecoveredWithErrors exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecoveredWithErrorsException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecoveredWithErrorsException class.
        /// </summary>
        public EsentRecoveredWithErrorsException() :
            base("Restored with errors", JET_err.RecoveredWithErrors)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecoveredWithErrorsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecoveredWithErrorsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingLogFile exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingLogFileException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingLogFileException class.
        /// </summary>
        public EsentMissingLogFileException() :
            base("Current log file missing", JET_err.MissingLogFile)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingLogFileException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingLogFileException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogDiskFull exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogDiskFullException : EsentDiskException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogDiskFullException class.
        /// </summary>
        public EsentLogDiskFullException() :
            base("Log disk full", JET_err.LogDiskFull)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogDiskFullException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogDiskFullException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadLogSignature exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadLogSignatureException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadLogSignatureException class.
        /// </summary>
        public EsentBadLogSignatureException() :
            base("Bad signature for a log file", JET_err.BadLogSignature)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadLogSignatureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadLogSignatureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadDbSignature exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadDbSignatureException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadDbSignatureException class.
        /// </summary>
        public EsentBadDbSignatureException() :
            base("Bad signature for a db file", JET_err.BadDbSignature)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadDbSignatureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadDbSignatureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadCheckpointSignature exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadCheckpointSignatureException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadCheckpointSignatureException class.
        /// </summary>
        public EsentBadCheckpointSignatureException() :
            base("Bad signature for a checkpoint file", JET_err.BadCheckpointSignature)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadCheckpointSignatureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadCheckpointSignatureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CheckpointCorrupt exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCheckpointCorruptException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCheckpointCorruptException class.
        /// </summary>
        public EsentCheckpointCorruptException() :
            base("Checkpoint file not found or corrupt", JET_err.CheckpointCorrupt)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCheckpointCorruptException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCheckpointCorruptException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingPatchPage exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingPatchPageException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingPatchPageException class.
        /// </summary>
        public EsentMissingPatchPageException() :
            base("Patch file page not found during recovery", JET_err.MissingPatchPage)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingPatchPageException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingPatchPageException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadPatchPage exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadPatchPageException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadPatchPageException class.
        /// </summary>
        public EsentBadPatchPageException() :
            base("Patch file page is not valid", JET_err.BadPatchPage)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadPatchPageException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadPatchPageException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RedoAbruptEnded exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRedoAbruptEndedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRedoAbruptEndedException class.
        /// </summary>
        public EsentRedoAbruptEndedException() :
            base("Redo abruptly ended due to sudden failure in reading logs from log file", JET_err.RedoAbruptEnded)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRedoAbruptEndedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRedoAbruptEndedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PatchFileMissing exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPatchFileMissingException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPatchFileMissingException class.
        /// </summary>
        public EsentPatchFileMissingException() :
            base("Hard restore detected that patch file is missing from backup set", JET_err.PatchFileMissing)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPatchFileMissingException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPatchFileMissingException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseLogSetMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseLogSetMismatchException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseLogSetMismatchException class.
        /// </summary>
        public EsentDatabaseLogSetMismatchException() :
            base("Database does not belong with the current set of log files", JET_err.DatabaseLogSetMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseLogSetMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseLogSetMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseStreamingFileMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseStreamingFileMismatchException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseStreamingFileMismatchException class.
        /// </summary>
        public EsentDatabaseStreamingFileMismatchException() :
            base("Database and streaming file do not match each other", JET_err.DatabaseStreamingFileMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseStreamingFileMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseStreamingFileMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogFileSizeMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogFileSizeMismatchException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogFileSizeMismatchException class.
        /// </summary>
        public EsentLogFileSizeMismatchException() :
            base("actual log file size does not match JET_paramLogFileSize", JET_err.LogFileSizeMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogFileSizeMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogFileSizeMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CheckpointFileNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCheckpointFileNotFoundException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCheckpointFileNotFoundException class.
        /// </summary>
        public EsentCheckpointFileNotFoundException() :
            base("Could not locate checkpoint file", JET_err.CheckpointFileNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCheckpointFileNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCheckpointFileNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RequiredLogFilesMissing exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRequiredLogFilesMissingException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRequiredLogFilesMissingException class.
        /// </summary>
        public EsentRequiredLogFilesMissingException() :
            base("The required log files for recovery is missing.", JET_err.RequiredLogFilesMissing)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRequiredLogFilesMissingException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRequiredLogFilesMissingException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SoftRecoveryOnBackupDatabase exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSoftRecoveryOnBackupDatabaseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSoftRecoveryOnBackupDatabaseException class.
        /// </summary>
        public EsentSoftRecoveryOnBackupDatabaseException() :
            base("Soft recovery is intended on a backup database. Restore should be used instead", JET_err.SoftRecoveryOnBackupDatabase)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSoftRecoveryOnBackupDatabaseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSoftRecoveryOnBackupDatabaseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogFileSizeMismatchDatabasesConsistent exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogFileSizeMismatchDatabasesConsistentException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogFileSizeMismatchDatabasesConsistentException class.
        /// </summary>
        public EsentLogFileSizeMismatchDatabasesConsistentException() :
            base("databases have been recovered, but the log file size used during recovery does not match JET_paramLogFileSize", JET_err.LogFileSizeMismatchDatabasesConsistent)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogFileSizeMismatchDatabasesConsistentException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogFileSizeMismatchDatabasesConsistentException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogSectorSizeMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogSectorSizeMismatchException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogSectorSizeMismatchException class.
        /// </summary>
        public EsentLogSectorSizeMismatchException() :
            base("the log file sector size does not match the current volume's sector size", JET_err.LogSectorSizeMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogSectorSizeMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogSectorSizeMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogSectorSizeMismatchDatabasesConsistent exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogSectorSizeMismatchDatabasesConsistentException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogSectorSizeMismatchDatabasesConsistentException class.
        /// </summary>
        public EsentLogSectorSizeMismatchDatabasesConsistentException() :
            base("databases have been recovered, but the log file sector size (used during recovery) does not match the current volume's sector size", JET_err.LogSectorSizeMismatchDatabasesConsistent)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogSectorSizeMismatchDatabasesConsistentException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogSectorSizeMismatchDatabasesConsistentException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogSequenceEndDatabasesConsistent exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogSequenceEndDatabasesConsistentException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogSequenceEndDatabasesConsistentException class.
        /// </summary>
        public EsentLogSequenceEndDatabasesConsistentException() :
            base("databases have been recovered, but all possible log generations in the current sequence are used; delete all log files and the checkpoint file and backup the databases before continuing", JET_err.LogSequenceEndDatabasesConsistent)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogSequenceEndDatabasesConsistentException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogSequenceEndDatabasesConsistentException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.StreamingDataNotLogged exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentStreamingDataNotLoggedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentStreamingDataNotLoggedException class.
        /// </summary>
        public EsentStreamingDataNotLoggedException() :
            base("Illegal attempt to replay a streaming file operation where the data wasn't logged. Probably caused by an attempt to roll-forward with circular logging enabled", JET_err.StreamingDataNotLogged)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentStreamingDataNotLoggedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentStreamingDataNotLoggedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseDirtyShutdown exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseDirtyShutdownException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseDirtyShutdownException class.
        /// </summary>
        public EsentDatabaseDirtyShutdownException() :
            base("Database was not shutdown cleanly. Recovery must first be run to properly complete database operations for the previous shutdown.", JET_err.DatabaseDirtyShutdown)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseDirtyShutdownException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseDirtyShutdownException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ConsistentTimeMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentConsistentTimeMismatchException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentConsistentTimeMismatchException class.
        /// </summary>
        public EsentConsistentTimeMismatchException() :
            base("Database last consistent time unmatched", JET_err.ConsistentTimeMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentConsistentTimeMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentConsistentTimeMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabasePatchFileMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabasePatchFileMismatchException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabasePatchFileMismatchException class.
        /// </summary>
        public EsentDatabasePatchFileMismatchException() :
            base("Patch file is not generated from this backup", JET_err.DatabasePatchFileMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabasePatchFileMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabasePatchFileMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EndingRestoreLogTooLow exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEndingRestoreLogTooLowException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEndingRestoreLogTooLowException class.
        /// </summary>
        public EsentEndingRestoreLogTooLowException() :
            base("The starting log number too low for the restore", JET_err.EndingRestoreLogTooLow)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEndingRestoreLogTooLowException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEndingRestoreLogTooLowException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.StartingRestoreLogTooHigh exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentStartingRestoreLogTooHighException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentStartingRestoreLogTooHighException class.
        /// </summary>
        public EsentStartingRestoreLogTooHighException() :
            base("The starting log number too high for the restore", JET_err.StartingRestoreLogTooHigh)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentStartingRestoreLogTooHighException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentStartingRestoreLogTooHighException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.GivenLogFileHasBadSignature exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentGivenLogFileHasBadSignatureException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentGivenLogFileHasBadSignatureException class.
        /// </summary>
        public EsentGivenLogFileHasBadSignatureException() :
            base("Restore log file has bad signature", JET_err.GivenLogFileHasBadSignature)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentGivenLogFileHasBadSignatureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentGivenLogFileHasBadSignatureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.GivenLogFileIsNotContiguous exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentGivenLogFileIsNotContiguousException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentGivenLogFileIsNotContiguousException class.
        /// </summary>
        public EsentGivenLogFileIsNotContiguousException() :
            base("Restore log file is not contiguous", JET_err.GivenLogFileIsNotContiguous)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentGivenLogFileIsNotContiguousException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentGivenLogFileIsNotContiguousException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingRestoreLogFiles exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingRestoreLogFilesException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingRestoreLogFilesException class.
        /// </summary>
        public EsentMissingRestoreLogFilesException() :
            base("Some restore log files are missing", JET_err.MissingRestoreLogFiles)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingRestoreLogFilesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingRestoreLogFilesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingFullBackup exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingFullBackupException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingFullBackupException class.
        /// </summary>
        public EsentMissingFullBackupException() :
            base("The database missed a previous full backup before incremental backup", JET_err.MissingFullBackup)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingFullBackupException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingFullBackupException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadBackupDatabaseSize exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadBackupDatabaseSizeException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadBackupDatabaseSizeException class.
        /// </summary>
        public EsentBadBackupDatabaseSizeException() :
            base("The backup database size is not in 4k", JET_err.BadBackupDatabaseSize)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadBackupDatabaseSizeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadBackupDatabaseSizeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseAlreadyUpgraded exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseAlreadyUpgradedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseAlreadyUpgradedException class.
        /// </summary>
        public EsentDatabaseAlreadyUpgradedException() :
            base("Attempted to upgrade a database that is already current", JET_err.DatabaseAlreadyUpgraded)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseAlreadyUpgradedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseAlreadyUpgradedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseIncompleteUpgrade exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseIncompleteUpgradeException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseIncompleteUpgradeException class.
        /// </summary>
        public EsentDatabaseIncompleteUpgradeException() :
            base("Attempted to use a database which was only partially converted to the current format -- must restore from backup", JET_err.DatabaseIncompleteUpgrade)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseIncompleteUpgradeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseIncompleteUpgradeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingCurrentLogFiles exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingCurrentLogFilesException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingCurrentLogFilesException class.
        /// </summary>
        public EsentMissingCurrentLogFilesException() :
            base("Some current log files are missing for continuous restore", JET_err.MissingCurrentLogFiles)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingCurrentLogFilesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingCurrentLogFilesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DbTimeTooOld exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDbTimeTooOldException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDbTimeTooOldException class.
        /// </summary>
        public EsentDbTimeTooOldException() :
            base("dbtime on page smaller than dbtimeBefore in record", JET_err.DbTimeTooOld)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDbTimeTooOldException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDbTimeTooOldException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DbTimeTooNew exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDbTimeTooNewException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDbTimeTooNewException class.
        /// </summary>
        public EsentDbTimeTooNewException() :
            base("dbtime on page in advance of the dbtimeBefore in record", JET_err.DbTimeTooNew)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDbTimeTooNewException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDbTimeTooNewException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MissingFileToBackup exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMissingFileToBackupException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMissingFileToBackupException class.
        /// </summary>
        public EsentMissingFileToBackupException() :
            base("Some log or patch files are missing during backup", JET_err.MissingFileToBackup)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMissingFileToBackupException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMissingFileToBackupException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogTornWriteDuringHardRestore exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogTornWriteDuringHardRestoreException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogTornWriteDuringHardRestoreException class.
        /// </summary>
        public EsentLogTornWriteDuringHardRestoreException() :
            base("torn-write was detected in a backup set during hard restore", JET_err.LogTornWriteDuringHardRestore)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogTornWriteDuringHardRestoreException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogTornWriteDuringHardRestoreException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogTornWriteDuringHardRecovery exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogTornWriteDuringHardRecoveryException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogTornWriteDuringHardRecoveryException class.
        /// </summary>
        public EsentLogTornWriteDuringHardRecoveryException() :
            base("torn-write was detected during hard recovery (log was not part of a backup set)", JET_err.LogTornWriteDuringHardRecovery)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogTornWriteDuringHardRecoveryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogTornWriteDuringHardRecoveryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogCorruptDuringHardRestore exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogCorruptDuringHardRestoreException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogCorruptDuringHardRestoreException class.
        /// </summary>
        public EsentLogCorruptDuringHardRestoreException() :
            base("corruption was detected in a backup set during hard restore", JET_err.LogCorruptDuringHardRestore)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogCorruptDuringHardRestoreException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogCorruptDuringHardRestoreException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogCorruptDuringHardRecovery exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogCorruptDuringHardRecoveryException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogCorruptDuringHardRecoveryException class.
        /// </summary>
        public EsentLogCorruptDuringHardRecoveryException() :
            base("corruption was detected during hard recovery (log was not part of a backup set)", JET_err.LogCorruptDuringHardRecovery)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogCorruptDuringHardRecoveryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogCorruptDuringHardRecoveryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MustDisableLoggingForDbUpgrade exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMustDisableLoggingForDbUpgradeException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMustDisableLoggingForDbUpgradeException class.
        /// </summary>
        public EsentMustDisableLoggingForDbUpgradeException() :
            base("Cannot have logging enabled while attempting to upgrade db", JET_err.MustDisableLoggingForDbUpgrade)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMustDisableLoggingForDbUpgradeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMustDisableLoggingForDbUpgradeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadRestoreTargetInstance exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadRestoreTargetInstanceException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadRestoreTargetInstanceException class.
        /// </summary>
        public EsentBadRestoreTargetInstanceException() :
            base("TargetInstance specified for restore is not found or log files don't match", JET_err.BadRestoreTargetInstance)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadRestoreTargetInstanceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadRestoreTargetInstanceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecoveredWithoutUndo exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecoveredWithoutUndoException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecoveredWithoutUndoException class.
        /// </summary>
        public EsentRecoveredWithoutUndoException() :
            base("Soft recovery successfully replayed all operations, but the Undo phase of recovery was skipped", JET_err.RecoveredWithoutUndo)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecoveredWithoutUndoException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecoveredWithoutUndoException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabasesNotFromSameSnapshot exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabasesNotFromSameSnapshotException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabasesNotFromSameSnapshotException class.
        /// </summary>
        public EsentDatabasesNotFromSameSnapshotException() :
            base("Databases to be restored are not from the same shadow copy backup", JET_err.DatabasesNotFromSameSnapshot)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabasesNotFromSameSnapshotException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabasesNotFromSameSnapshotException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SoftRecoveryOnSnapshot exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSoftRecoveryOnSnapshotException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSoftRecoveryOnSnapshotException class.
        /// </summary>
        public EsentSoftRecoveryOnSnapshotException() :
            base("Soft recovery on a database from a shadow copy backup set", JET_err.SoftRecoveryOnSnapshot)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSoftRecoveryOnSnapshotException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSoftRecoveryOnSnapshotException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CommittedLogFilesMissing exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCommittedLogFilesMissingException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCommittedLogFilesMissingException class.
        /// </summary>
        public EsentCommittedLogFilesMissingException() :
            base("One or more logs that were committed to this database, are missing.  These log files are required to maintain durable ACID semantics, but not required to maintain consistency if the JET_bitReplayIgnoreLostLogs bit is specified during recovery.", JET_err.CommittedLogFilesMissing)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCommittedLogFilesMissingException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCommittedLogFilesMissingException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SectorSizeNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSectorSizeNotSupportedException : EsentFatalException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSectorSizeNotSupportedException class.
        /// </summary>
        public EsentSectorSizeNotSupportedException() :
            base("The physical sector size reported by the disk subsystem, is unsupported by ESE for a specific file type.", JET_err.SectorSizeNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSectorSizeNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSectorSizeNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecoveredWithoutUndoDatabasesConsistent exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecoveredWithoutUndoDatabasesConsistentException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecoveredWithoutUndoDatabasesConsistentException class.
        /// </summary>
        public EsentRecoveredWithoutUndoDatabasesConsistentException() :
            base("Soft recovery successfully replayed all operations and intended to skip the Undo phase of recovery, but the Undo phase was not required", JET_err.RecoveredWithoutUndoDatabasesConsistent)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecoveredWithoutUndoDatabasesConsistentException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecoveredWithoutUndoDatabasesConsistentException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CommittedLogFileCorrupt exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCommittedLogFileCorruptException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCommittedLogFileCorruptException class.
        /// </summary>
        public EsentCommittedLogFileCorruptException() :
            base("One or more logs were found to be corrupt during recovery.  These log files are required to maintain durable ACID semantics, but not required to maintain consistency if the JET_bitIgnoreLostLogs bit and JET_paramDeleteOutOfRangeLogs is specified during recovery.", JET_err.CommittedLogFileCorrupt)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCommittedLogFileCorruptException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCommittedLogFileCorruptException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogSequenceChecksumMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogSequenceChecksumMismatchException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogSequenceChecksumMismatchException class.
        /// </summary>
        public EsentLogSequenceChecksumMismatchException() :
            base("The previous log's accumulated segment checksum doesn't match the next log", JET_err.LogSequenceChecksumMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogSequenceChecksumMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogSequenceChecksumMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PageInitializedMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPageInitializedMismatchException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPageInitializedMismatchException class.
        /// </summary>
        public EsentPageInitializedMismatchException() :
            base("Database divergence mismatch. Page was uninitialized on remote node, but initialized on local node.", JET_err.PageInitializedMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPageInitializedMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPageInitializedMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UnicodeTranslationBufferTooSmall exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUnicodeTranslationBufferTooSmallException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeTranslationBufferTooSmallException class.
        /// </summary>
        public EsentUnicodeTranslationBufferTooSmallException() :
            base("Unicode translation buffer too small", JET_err.UnicodeTranslationBufferTooSmall)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeTranslationBufferTooSmallException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUnicodeTranslationBufferTooSmallException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UnicodeTranslationFail exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUnicodeTranslationFailException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeTranslationFailException class.
        /// </summary>
        public EsentUnicodeTranslationFailException() :
            base("Unicode normalization failed", JET_err.UnicodeTranslationFail)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeTranslationFailException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUnicodeTranslationFailException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UnicodeNormalizationNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUnicodeNormalizationNotSupportedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeNormalizationNotSupportedException class.
        /// </summary>
        public EsentUnicodeNormalizationNotSupportedException() :
            base("OS does not provide support for Unicode normalisation (and no normalisation callback was specified)", JET_err.UnicodeNormalizationNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeNormalizationNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUnicodeNormalizationNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UnicodeLanguageValidationFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUnicodeLanguageValidationFailureException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeLanguageValidationFailureException class.
        /// </summary>
        public EsentUnicodeLanguageValidationFailureException() :
            base("Can not validate the language", JET_err.UnicodeLanguageValidationFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUnicodeLanguageValidationFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUnicodeLanguageValidationFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ExistingLogFileHasBadSignature exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentExistingLogFileHasBadSignatureException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentExistingLogFileHasBadSignatureException class.
        /// </summary>
        public EsentExistingLogFileHasBadSignatureException() :
            base("Existing log file has bad signature", JET_err.ExistingLogFileHasBadSignature)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentExistingLogFileHasBadSignatureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentExistingLogFileHasBadSignatureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ExistingLogFileIsNotContiguous exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentExistingLogFileIsNotContiguousException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentExistingLogFileIsNotContiguousException class.
        /// </summary>
        public EsentExistingLogFileIsNotContiguousException() :
            base("Existing log file is not contiguous", JET_err.ExistingLogFileIsNotContiguous)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentExistingLogFileIsNotContiguousException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentExistingLogFileIsNotContiguousException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogReadVerifyFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogReadVerifyFailureException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogReadVerifyFailureException class.
        /// </summary>
        public EsentLogReadVerifyFailureException() :
            base("Checksum error in log file during backup", JET_err.LogReadVerifyFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogReadVerifyFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogReadVerifyFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CheckpointDepthTooDeep exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCheckpointDepthTooDeepException : EsentQuotaException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCheckpointDepthTooDeepException class.
        /// </summary>
        public EsentCheckpointDepthTooDeepException() :
            base("too many outstanding generations between checkpoint and current generation", JET_err.CheckpointDepthTooDeep)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCheckpointDepthTooDeepException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCheckpointDepthTooDeepException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RestoreOfNonBackupDatabase exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRestoreOfNonBackupDatabaseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRestoreOfNonBackupDatabaseException class.
        /// </summary>
        public EsentRestoreOfNonBackupDatabaseException() :
            base("hard recovery attempted on a database that wasn't a backup database", JET_err.RestoreOfNonBackupDatabase)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRestoreOfNonBackupDatabaseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRestoreOfNonBackupDatabaseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogFileNotCopied exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogFileNotCopiedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogFileNotCopiedException class.
        /// </summary>
        public EsentLogFileNotCopiedException() :
            base("log truncation attempted but not all required logs were copied", JET_err.LogFileNotCopied)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogFileNotCopiedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogFileNotCopiedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SurrogateBackupInProgress exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSurrogateBackupInProgressException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSurrogateBackupInProgressException class.
        /// </summary>
        public EsentSurrogateBackupInProgressException() :
            base("A surrogate backup is in progress.", JET_err.SurrogateBackupInProgress)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSurrogateBackupInProgressException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSurrogateBackupInProgressException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TransactionTooLong exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTransactionTooLongException : EsentQuotaException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTransactionTooLongException class.
        /// </summary>
        public EsentTransactionTooLongException() :
            base("Too many outstanding generations between JetBeginTransaction and current generation.", JET_err.TransactionTooLong)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTransactionTooLongException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTransactionTooLongException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EngineFormatVersionNoLongerSupportedTooLow exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEngineFormatVersionNoLongerSupportedTooLowException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionNoLongerSupportedTooLowException class.
        /// </summary>
        public EsentEngineFormatVersionNoLongerSupportedTooLowException() :
            base("The specified JET_ENGINEFORMATVERSION value is too low to be supported by this version of ESE.", JET_err.EngineFormatVersionNoLongerSupportedTooLow)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionNoLongerSupportedTooLowException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEngineFormatVersionNoLongerSupportedTooLowException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EngineFormatVersionNotYetImplementedTooHigh exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEngineFormatVersionNotYetImplementedTooHighException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionNotYetImplementedTooHighException class.
        /// </summary>
        public EsentEngineFormatVersionNotYetImplementedTooHighException() :
            base("The specified JET_ENGINEFORMATVERSION value is too high, higher than this version of ESE knows about.", JET_err.EngineFormatVersionNotYetImplementedTooHigh)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionNotYetImplementedTooHighException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEngineFormatVersionNotYetImplementedTooHighException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EngineFormatVersionParamTooLowForRequestedFeature exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEngineFormatVersionParamTooLowForRequestedFeatureException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionParamTooLowForRequestedFeatureException class.
        /// </summary>
        public EsentEngineFormatVersionParamTooLowForRequestedFeatureException() :
            base("Thrown by a format feature (not at JetSetSystemParameter) if the client requests a feature that requires a version higher than that set for the JET_paramEngineFormatVersion.", JET_err.EngineFormatVersionParamTooLowForRequestedFeature)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionParamTooLowForRequestedFeatureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEngineFormatVersionParamTooLowForRequestedFeatureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EngineFormatVersionSpecifiedTooLowForLogVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEngineFormatVersionSpecifiedTooLowForLogVersionException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionSpecifiedTooLowForLogVersionException class.
        /// </summary>
        public EsentEngineFormatVersionSpecifiedTooLowForLogVersionException() :
            base("The specified JET_ENGINEFORMATVERSION is set too low for this log stream, the log files have already been upgraded to a higher version.  A higher JET_ENGINEFORMATVERSION value must be set in the param.", JET_err.EngineFormatVersionSpecifiedTooLowForLogVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionSpecifiedTooLowForLogVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEngineFormatVersionSpecifiedTooLowForLogVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EngineFormatVersionSpecifiedTooLowForDatabaseVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEngineFormatVersionSpecifiedTooLowForDatabaseVersionException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionSpecifiedTooLowForDatabaseVersionException class.
        /// </summary>
        public EsentEngineFormatVersionSpecifiedTooLowForDatabaseVersionException() :
            base("The specified JET_ENGINEFORMATVERSION is set too low for this database file, the database file has already been upgraded to a higher version.  A higher JET_ENGINEFORMATVERSION value must be set in the param.", JET_err.EngineFormatVersionSpecifiedTooLowForDatabaseVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEngineFormatVersionSpecifiedTooLowForDatabaseVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEngineFormatVersionSpecifiedTooLowForDatabaseVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BackupAbortByServer exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBackupAbortByServerException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBackupAbortByServerException class.
        /// </summary>
        public EsentBackupAbortByServerException() :
            base("Backup was aborted by server by calling JetTerm with JET_bitTermStopBackup or by calling JetStopBackup", JET_err.BackupAbortByServer)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBackupAbortByServerException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBackupAbortByServerException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidGrbit exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidGrbitException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidGrbitException class.
        /// </summary>
        public EsentInvalidGrbitException() :
            base("Invalid flags parameter", JET_err.InvalidGrbit)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidGrbitException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidGrbitException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TermInProgress exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTermInProgressException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTermInProgressException class.
        /// </summary>
        public EsentTermInProgressException() :
            base("Termination in progress", JET_err.TermInProgress)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTermInProgressException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTermInProgressException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FeatureNotAvailable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFeatureNotAvailableException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFeatureNotAvailableException class.
        /// </summary>
        public EsentFeatureNotAvailableException() :
            base("API not supported", JET_err.FeatureNotAvailable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFeatureNotAvailableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFeatureNotAvailableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidName exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidNameException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidNameException class.
        /// </summary>
        public EsentInvalidNameException() :
            base("Invalid name", JET_err.InvalidName)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidNameException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidNameException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidParameter exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidParameterException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidParameterException class.
        /// </summary>
        public EsentInvalidParameterException() :
            base("Invalid API parameter", JET_err.InvalidParameter)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidParameterException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidParameterException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseFileReadOnly exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseFileReadOnlyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseFileReadOnlyException class.
        /// </summary>
        public EsentDatabaseFileReadOnlyException() :
            base("Tried to attach a read-only database file for read/write operations", JET_err.DatabaseFileReadOnly)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseFileReadOnlyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseFileReadOnlyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidDatabaseId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidDatabaseIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidDatabaseIdException class.
        /// </summary>
        public EsentInvalidDatabaseIdException() :
            base("Invalid database id", JET_err.InvalidDatabaseId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidDatabaseIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidDatabaseIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfMemory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfMemoryException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfMemoryException class.
        /// </summary>
        public EsentOutOfMemoryException() :
            base("Out of Memory", JET_err.OutOfMemory)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfMemoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfMemoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfDatabaseSpace exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfDatabaseSpaceException : EsentQuotaException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfDatabaseSpaceException class.
        /// </summary>
        public EsentOutOfDatabaseSpaceException() :
            base("Maximum database size reached", JET_err.OutOfDatabaseSpace)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfDatabaseSpaceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfDatabaseSpaceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfCursors exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfCursorsException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfCursorsException class.
        /// </summary>
        public EsentOutOfCursorsException() :
            base("Out of table cursors", JET_err.OutOfCursors)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfCursorsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfCursorsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfBuffers exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfBuffersException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfBuffersException class.
        /// </summary>
        public EsentOutOfBuffersException() :
            base("Out of database page buffers", JET_err.OutOfBuffers)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfBuffersException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfBuffersException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyIndexes exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyIndexesException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyIndexesException class.
        /// </summary>
        public EsentTooManyIndexesException() :
            base("Too many indexes", JET_err.TooManyIndexes)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyIndexesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyIndexesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyKeys exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyKeysException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyKeysException class.
        /// </summary>
        public EsentTooManyKeysException() :
            base("Too many columns in an index", JET_err.TooManyKeys)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyKeysException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyKeysException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordDeleted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordDeletedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordDeletedException class.
        /// </summary>
        public EsentRecordDeletedException() :
            base("Record has been deleted", JET_err.RecordDeleted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordDeletedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordDeletedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ReadVerifyFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentReadVerifyFailureException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentReadVerifyFailureException class.
        /// </summary>
        public EsentReadVerifyFailureException() :
            base("Checksum error on a database page", JET_err.ReadVerifyFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentReadVerifyFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentReadVerifyFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PageNotInitialized exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPageNotInitializedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPageNotInitializedException class.
        /// </summary>
        public EsentPageNotInitializedException() :
            base("Blank database page", JET_err.PageNotInitialized)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPageNotInitializedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPageNotInitializedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfFileHandles exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfFileHandlesException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfFileHandlesException class.
        /// </summary>
        public EsentOutOfFileHandlesException() :
            base("Out of file handles", JET_err.OutOfFileHandles)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfFileHandlesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfFileHandlesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DiskReadVerificationFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDiskReadVerificationFailureException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDiskReadVerificationFailureException class.
        /// </summary>
        public EsentDiskReadVerificationFailureException() :
            base("The OS returned ERROR_CRC from file IO", JET_err.DiskReadVerificationFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDiskReadVerificationFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDiskReadVerificationFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DiskIO exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDiskIOException : EsentIOException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDiskIOException class.
        /// </summary>
        public EsentDiskIOException() :
            base("Disk IO error", JET_err.DiskIO)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDiskIOException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDiskIOException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidPath exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidPathException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidPathException class.
        /// </summary>
        public EsentInvalidPathException() :
            base("Invalid file path", JET_err.InvalidPath)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidPathException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidPathException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidSystemPath exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidSystemPathException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSystemPathException class.
        /// </summary>
        public EsentInvalidSystemPathException() :
            base("Invalid system path", JET_err.InvalidSystemPath)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSystemPathException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidSystemPathException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLogDirectory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLogDirectoryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLogDirectoryException class.
        /// </summary>
        public EsentInvalidLogDirectoryException() :
            base("Invalid log directory", JET_err.InvalidLogDirectory)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLogDirectoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLogDirectoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordTooBig exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordTooBigException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordTooBigException class.
        /// </summary>
        public EsentRecordTooBigException() :
            base("Record larger than maximum size", JET_err.RecordTooBig)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordTooBigException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordTooBigException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyOpenDatabases exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyOpenDatabasesException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenDatabasesException class.
        /// </summary>
        public EsentTooManyOpenDatabasesException() :
            base("Too many open databases", JET_err.TooManyOpenDatabases)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenDatabasesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyOpenDatabasesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidDatabase exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidDatabaseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidDatabaseException class.
        /// </summary>
        public EsentInvalidDatabaseException() :
            base("Not a database file", JET_err.InvalidDatabase)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidDatabaseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidDatabaseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NotInitialized exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNotInitializedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNotInitializedException class.
        /// </summary>
        public EsentNotInitializedException() :
            base("Database engine not initialized", JET_err.NotInitialized)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNotInitializedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNotInitializedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.AlreadyInitialized exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentAlreadyInitializedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentAlreadyInitializedException class.
        /// </summary>
        public EsentAlreadyInitializedException() :
            base("Database engine already initialized", JET_err.AlreadyInitialized)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentAlreadyInitializedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentAlreadyInitializedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InitInProgress exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInitInProgressException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInitInProgressException class.
        /// </summary>
        public EsentInitInProgressException() :
            base("Database engine is being initialized", JET_err.InitInProgress)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInitInProgressException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInitInProgressException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileAccessDenied exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileAccessDeniedException : EsentIOException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileAccessDeniedException class.
        /// </summary>
        public EsentFileAccessDeniedException() :
            base("Cannot access file, the file is locked or in use", JET_err.FileAccessDenied)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileAccessDeniedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileAccessDeniedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.QueryNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentQueryNotSupportedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentQueryNotSupportedException class.
        /// </summary>
        public EsentQueryNotSupportedException() :
            base("Query support unavailable", JET_err.QueryNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentQueryNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentQueryNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SQLLinkNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSQLLinkNotSupportedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSQLLinkNotSupportedException class.
        /// </summary>
        public EsentSQLLinkNotSupportedException() :
            base("SQL Link support unavailable", JET_err.SQLLinkNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSQLLinkNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSQLLinkNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BufferTooSmall exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBufferTooSmallException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBufferTooSmallException class.
        /// </summary>
        public EsentBufferTooSmallException() :
            base("Buffer is too small", JET_err.BufferTooSmall)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBufferTooSmallException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBufferTooSmallException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyColumns exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyColumnsException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyColumnsException class.
        /// </summary>
        public EsentTooManyColumnsException() :
            base("Too many columns defined", JET_err.TooManyColumns)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyColumnsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyColumnsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ContainerNotEmpty exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentContainerNotEmptyException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentContainerNotEmptyException class.
        /// </summary>
        public EsentContainerNotEmptyException() :
            base("Container is not empty", JET_err.ContainerNotEmpty)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentContainerNotEmptyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentContainerNotEmptyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidFilename exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidFilenameException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidFilenameException class.
        /// </summary>
        public EsentInvalidFilenameException() :
            base("Filename is invalid", JET_err.InvalidFilename)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidFilenameException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidFilenameException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidBookmark exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidBookmarkException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBookmarkException class.
        /// </summary>
        public EsentInvalidBookmarkException() :
            base("Invalid bookmark", JET_err.InvalidBookmark)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBookmarkException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidBookmarkException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnInUseException class.
        /// </summary>
        public EsentColumnInUseException() :
            base("Column used in an index", JET_err.ColumnInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidBufferSize exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidBufferSizeException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBufferSizeException class.
        /// </summary>
        public EsentInvalidBufferSizeException() :
            base("Data buffer doesn't match column size", JET_err.InvalidBufferSize)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidBufferSizeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidBufferSizeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnNotUpdatable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnNotUpdatableException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnNotUpdatableException class.
        /// </summary>
        public EsentColumnNotUpdatableException() :
            base("Cannot set column value", JET_err.ColumnNotUpdatable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnNotUpdatableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnNotUpdatableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexInUseException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexInUseException class.
        /// </summary>
        public EsentIndexInUseException() :
            base("Index is in use", JET_err.IndexInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LinkNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLinkNotSupportedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLinkNotSupportedException class.
        /// </summary>
        public EsentLinkNotSupportedException() :
            base("Link support unavailable", JET_err.LinkNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLinkNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLinkNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NullKeyDisallowed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNullKeyDisallowedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNullKeyDisallowedException class.
        /// </summary>
        public EsentNullKeyDisallowedException() :
            base("Null keys are disallowed on index", JET_err.NullKeyDisallowed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNullKeyDisallowedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNullKeyDisallowedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NotInTransaction exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNotInTransactionException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNotInTransactionException class.
        /// </summary>
        public EsentNotInTransactionException() :
            base("Operation must be within a transaction", JET_err.NotInTransaction)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNotInTransactionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNotInTransactionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MustRollback exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMustRollbackException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMustRollbackException class.
        /// </summary>
        public EsentMustRollbackException() :
            base("Transaction must rollback because failure of unversioned update", JET_err.MustRollback)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMustRollbackException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMustRollbackException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyActiveUsers exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyActiveUsersException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyActiveUsersException class.
        /// </summary>
        public EsentTooManyActiveUsersException() :
            base("Too many active database users", JET_err.TooManyActiveUsers)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyActiveUsersException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyActiveUsersException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidCountry exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidCountryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCountryException class.
        /// </summary>
        public EsentInvalidCountryException() :
            base("Invalid or unknown country/region code", JET_err.InvalidCountry)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCountryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidCountryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLanguageId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLanguageIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLanguageIdException class.
        /// </summary>
        public EsentInvalidLanguageIdException() :
            base("Invalid or unknown language id", JET_err.InvalidLanguageId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLanguageIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLanguageIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidCodePage exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidCodePageException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCodePageException class.
        /// </summary>
        public EsentInvalidCodePageException() :
            base("Invalid or unknown code page", JET_err.InvalidCodePage)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCodePageException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidCodePageException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLCMapStringFlags exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLCMapStringFlagsException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLCMapStringFlagsException class.
        /// </summary>
        public EsentInvalidLCMapStringFlagsException() :
            base("Invalid flags for LCMapString()", JET_err.InvalidLCMapStringFlags)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLCMapStringFlagsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLCMapStringFlagsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.VersionStoreEntryTooBig exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentVersionStoreEntryTooBigException : EsentErrorException
    {
        /// <summary>
        /// Initializes a new instance of the EsentVersionStoreEntryTooBigException class.
        /// </summary>
        public EsentVersionStoreEntryTooBigException() :
            base("Attempted to create a version store entry (RCE) larger than a version bucket", JET_err.VersionStoreEntryTooBig)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentVersionStoreEntryTooBigException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentVersionStoreEntryTooBigException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.VersionStoreOutOfMemoryAndCleanupTimedOut exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentVersionStoreOutOfMemoryAndCleanupTimedOutException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentVersionStoreOutOfMemoryAndCleanupTimedOutException class.
        /// </summary>
        public EsentVersionStoreOutOfMemoryAndCleanupTimedOutException() :
            base("Version store out of memory (and cleanup attempt failed to complete)", JET_err.VersionStoreOutOfMemoryAndCleanupTimedOut)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentVersionStoreOutOfMemoryAndCleanupTimedOutException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentVersionStoreOutOfMemoryAndCleanupTimedOutException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.VersionStoreOutOfMemory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentVersionStoreOutOfMemoryException : EsentQuotaException
    {
        /// <summary>
        /// Initializes a new instance of the EsentVersionStoreOutOfMemoryException class.
        /// </summary>
        public EsentVersionStoreOutOfMemoryException() :
            base("Version store out of memory (cleanup already attempted)", JET_err.VersionStoreOutOfMemory)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentVersionStoreOutOfMemoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentVersionStoreOutOfMemoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CurrencyStackOutOfMemory exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCurrencyStackOutOfMemoryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCurrencyStackOutOfMemoryException class.
        /// </summary>
        public EsentCurrencyStackOutOfMemoryException() :
            base("UNUSED: lCSRPerfFUCB * g_lCursorsMax exceeded (XJET only)", JET_err.CurrencyStackOutOfMemory)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCurrencyStackOutOfMemoryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCurrencyStackOutOfMemoryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotIndex exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotIndexException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotIndexException class.
        /// </summary>
        public EsentCannotIndexException() :
            base("Cannot index escrow column", JET_err.CannotIndex)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotIndexException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotIndexException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordNotDeleted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordNotDeletedException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordNotDeletedException class.
        /// </summary>
        public EsentRecordNotDeletedException() :
            base("Record has not been deleted", JET_err.RecordNotDeleted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordNotDeletedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordNotDeletedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyMempoolEntries exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyMempoolEntriesException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyMempoolEntriesException class.
        /// </summary>
        public EsentTooManyMempoolEntriesException() :
            base("Too many mempool entries requested", JET_err.TooManyMempoolEntries)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyMempoolEntriesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyMempoolEntriesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfObjectIDs exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfObjectIDsException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfObjectIDsException class.
        /// </summary>
        public EsentOutOfObjectIDsException() :
            base("Out of btree ObjectIDs (perform offline defrag to reclaim freed/unused ObjectIds)", JET_err.OutOfObjectIDs)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfObjectIDsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfObjectIDsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfLongValueIDs exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfLongValueIDsException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfLongValueIDsException class.
        /// </summary>
        public EsentOutOfLongValueIDsException() :
            base("Long-value ID counter has reached maximum value. (perform offline defrag to reclaim free/unused LongValueIDs)", JET_err.OutOfLongValueIDs)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfLongValueIDsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfLongValueIDsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfAutoincrementValues exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfAutoincrementValuesException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfAutoincrementValuesException class.
        /// </summary>
        public EsentOutOfAutoincrementValuesException() :
            base("Auto-increment counter has reached maximum value (offline defrag WILL NOT be able to reclaim free/unused Auto-increment values).", JET_err.OutOfAutoincrementValues)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfAutoincrementValuesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfAutoincrementValuesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfDbtimeValues exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfDbtimeValuesException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfDbtimeValuesException class.
        /// </summary>
        public EsentOutOfDbtimeValuesException() :
            base("Dbtime counter has reached maximum value (perform offline defrag to reclaim free/unused Dbtime values)", JET_err.OutOfDbtimeValues)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfDbtimeValuesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfDbtimeValuesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfSequentialIndexValues exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfSequentialIndexValuesException : EsentFragmentationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfSequentialIndexValuesException class.
        /// </summary>
        public EsentOutOfSequentialIndexValuesException() :
            base("Sequential index counter has reached maximum value (perform offline defrag to reclaim free/unused SequentialIndex values)", JET_err.OutOfSequentialIndexValues)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfSequentialIndexValuesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfSequentialIndexValuesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RunningInOneInstanceMode exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRunningInOneInstanceModeException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRunningInOneInstanceModeException class.
        /// </summary>
        public EsentRunningInOneInstanceModeException() :
            base("Multi-instance call with single-instance mode enabled", JET_err.RunningInOneInstanceMode)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRunningInOneInstanceModeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRunningInOneInstanceModeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RunningInMultiInstanceMode exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRunningInMultiInstanceModeException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRunningInMultiInstanceModeException class.
        /// </summary>
        public EsentRunningInMultiInstanceModeException() :
            base("Single-instance call with multi-instance mode enabled", JET_err.RunningInMultiInstanceMode)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRunningInMultiInstanceModeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRunningInMultiInstanceModeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SystemParamsAlreadySet exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSystemParamsAlreadySetException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSystemParamsAlreadySetException class.
        /// </summary>
        public EsentSystemParamsAlreadySetException() :
            base("Global system parameters have already been set", JET_err.SystemParamsAlreadySet)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSystemParamsAlreadySetException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSystemParamsAlreadySetException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SystemPathInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSystemPathInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSystemPathInUseException class.
        /// </summary>
        public EsentSystemPathInUseException() :
            base("System path already used by another database instance", JET_err.SystemPathInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSystemPathInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSystemPathInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogFilePathInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogFilePathInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogFilePathInUseException class.
        /// </summary>
        public EsentLogFilePathInUseException() :
            base("Logfile path already used by another database instance", JET_err.LogFilePathInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogFilePathInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogFilePathInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TempPathInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTempPathInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTempPathInUseException class.
        /// </summary>
        public EsentTempPathInUseException() :
            base("Temp path already used by another database instance", JET_err.TempPathInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTempPathInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTempPathInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InstanceNameInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInstanceNameInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInstanceNameInUseException class.
        /// </summary>
        public EsentInstanceNameInUseException() :
            base("Instance Name already in use", JET_err.InstanceNameInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInstanceNameInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInstanceNameInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SystemParameterConflict exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSystemParameterConflictException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSystemParameterConflictException class.
        /// </summary>
        public EsentSystemParameterConflictException() :
            base("Global system parameters have already been set, but to a conflicting or disagreeable state to the specified values.", JET_err.SystemParameterConflict)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSystemParameterConflictException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSystemParameterConflictException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InstanceUnavailable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInstanceUnavailableException : EsentFatalException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInstanceUnavailableException class.
        /// </summary>
        public EsentInstanceUnavailableException() :
            base("This instance cannot be used because it encountered a fatal error", JET_err.InstanceUnavailable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInstanceUnavailableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInstanceUnavailableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseUnavailable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseUnavailableException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseUnavailableException class.
        /// </summary>
        public EsentDatabaseUnavailableException() :
            base("This database cannot be used because it encountered a fatal error", JET_err.DatabaseUnavailable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseUnavailableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseUnavailableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InstanceUnavailableDueToFatalLogDiskFull exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInstanceUnavailableDueToFatalLogDiskFullException : EsentFatalException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInstanceUnavailableDueToFatalLogDiskFullException class.
        /// </summary>
        public EsentInstanceUnavailableDueToFatalLogDiskFullException() :
            base("This instance cannot be used because it encountered a log-disk-full error performing an operation (likely transaction rollback) that could not tolerate failure", JET_err.InstanceUnavailableDueToFatalLogDiskFull)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInstanceUnavailableDueToFatalLogDiskFullException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInstanceUnavailableDueToFatalLogDiskFullException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidSesparamId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidSesparamIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSesparamIdException class.
        /// </summary>
        public EsentInvalidSesparamIdException() :
            base("This JET_sesparam* identifier is not known to the ESE engine.", JET_err.InvalidSesparamId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSesparamIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidSesparamIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OutOfSessions exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOutOfSessionsException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOutOfSessionsException class.
        /// </summary>
        public EsentOutOfSessionsException() :
            base("Out of sessions", JET_err.OutOfSessions)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOutOfSessionsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOutOfSessionsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.WriteConflict exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentWriteConflictException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentWriteConflictException class.
        /// </summary>
        public EsentWriteConflictException() :
            base("Write lock failed due to outstanding write lock", JET_err.WriteConflict)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentWriteConflictException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentWriteConflictException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TransTooDeep exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTransTooDeepException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTransTooDeepException class.
        /// </summary>
        public EsentTransTooDeepException() :
            base("Transactions nested too deeply", JET_err.TransTooDeep)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTransTooDeepException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTransTooDeepException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidSesid exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidSesidException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSesidException class.
        /// </summary>
        public EsentInvalidSesidException() :
            base("Invalid session handle", JET_err.InvalidSesid)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSesidException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidSesidException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.WriteConflictPrimaryIndex exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentWriteConflictPrimaryIndexException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentWriteConflictPrimaryIndexException class.
        /// </summary>
        public EsentWriteConflictPrimaryIndexException() :
            base("Update attempted on uncommitted primary index", JET_err.WriteConflictPrimaryIndex)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentWriteConflictPrimaryIndexException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentWriteConflictPrimaryIndexException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InTransaction exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInTransactionException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInTransactionException class.
        /// </summary>
        public EsentInTransactionException() :
            base("Operation not allowed within a transaction", JET_err.InTransaction)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInTransactionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInTransactionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RollbackRequired exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRollbackRequiredException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRollbackRequiredException class.
        /// </summary>
        public EsentRollbackRequiredException() :
            base("Must rollback current transaction -- cannot commit or begin a new one", JET_err.RollbackRequired)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRollbackRequiredException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRollbackRequiredException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TransReadOnly exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTransReadOnlyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTransReadOnlyException class.
        /// </summary>
        public EsentTransReadOnlyException() :
            base("Read-only transaction tried to modify the database", JET_err.TransReadOnly)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTransReadOnlyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTransReadOnlyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SessionWriteConflict exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSessionWriteConflictException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSessionWriteConflictException class.
        /// </summary>
        public EsentSessionWriteConflictException() :
            base("Attempt to replace the same record by two diffrerent cursors in the same session", JET_err.SessionWriteConflict)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSessionWriteConflictException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSessionWriteConflictException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordTooBigForBackwardCompatibility exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordTooBigForBackwardCompatibilityException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordTooBigForBackwardCompatibilityException class.
        /// </summary>
        public EsentRecordTooBigForBackwardCompatibilityException() :
            base("record would be too big if represented in a database format from a previous version of Jet", JET_err.RecordTooBigForBackwardCompatibility)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordTooBigForBackwardCompatibilityException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordTooBigForBackwardCompatibilityException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotMaterializeForwardOnlySort exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotMaterializeForwardOnlySortException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotMaterializeForwardOnlySortException class.
        /// </summary>
        public EsentCannotMaterializeForwardOnlySortException() :
            base("The temp table could not be created due to parameters that conflict with JET_bitTTForwardOnly", JET_err.CannotMaterializeForwardOnlySort)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotMaterializeForwardOnlySortException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotMaterializeForwardOnlySortException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SesidTableIdMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSesidTableIdMismatchException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSesidTableIdMismatchException class.
        /// </summary>
        public EsentSesidTableIdMismatchException() :
            base("This session handle can't be used with this table id", JET_err.SesidTableIdMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSesidTableIdMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSesidTableIdMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidInstance exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidInstanceException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidInstanceException class.
        /// </summary>
        public EsentInvalidInstanceException() :
            base("Invalid instance handle", JET_err.InvalidInstance)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidInstanceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidInstanceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DirtyShutdown exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDirtyShutdownException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDirtyShutdownException class.
        /// </summary>
        public EsentDirtyShutdownException() :
            base("The instance was shutdown successfully but all the attached databases were left in a dirty state by request via JET_bitTermDirty", JET_err.DirtyShutdown)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDirtyShutdownException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDirtyShutdownException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ReadPgnoVerifyFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentReadPgnoVerifyFailureException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentReadPgnoVerifyFailureException class.
        /// </summary>
        public EsentReadPgnoVerifyFailureException() :
            base("The database page read from disk had the wrong page number.", JET_err.ReadPgnoVerifyFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentReadPgnoVerifyFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentReadPgnoVerifyFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ReadLostFlushVerifyFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentReadLostFlushVerifyFailureException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentReadLostFlushVerifyFailureException class.
        /// </summary>
        public EsentReadLostFlushVerifyFailureException() :
            base("The database page read from disk had a previous write not represented on the page.", JET_err.ReadLostFlushVerifyFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentReadLostFlushVerifyFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentReadLostFlushVerifyFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileSystemCorruption exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileSystemCorruptionException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileSystemCorruptionException class.
        /// </summary>
        public EsentFileSystemCorruptionException() :
            base("File system operation failed with an error indicating the file system is corrupt.", JET_err.FileSystemCorruption)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileSystemCorruptionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileSystemCorruptionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecoveryVerifyFailure exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecoveryVerifyFailureException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecoveryVerifyFailureException class.
        /// </summary>
        public EsentRecoveryVerifyFailureException() :
            base("One or more database pages read from disk during recovery do not match the expected state.", JET_err.RecoveryVerifyFailure)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecoveryVerifyFailureException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecoveryVerifyFailureException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FilteredMoveNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFilteredMoveNotSupportedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFilteredMoveNotSupportedException class.
        /// </summary>
        public EsentFilteredMoveNotSupportedException() :
            base("Attempted to provide a filter to JetSetCursorFilter() in an unsupported scenario.", JET_err.FilteredMoveNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFilteredMoveNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFilteredMoveNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MustCommitDistributedTransactionToLevel0 exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMustCommitDistributedTransactionToLevel0Exception : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMustCommitDistributedTransactionToLevel0Exception class.
        /// </summary>
        public EsentMustCommitDistributedTransactionToLevel0Exception() :
            base("Attempted to PrepareToCommit a distributed transaction to non-zero level", JET_err.MustCommitDistributedTransactionToLevel0)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMustCommitDistributedTransactionToLevel0Exception class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMustCommitDistributedTransactionToLevel0Exception(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DistributedTransactionAlreadyPreparedToCommit exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDistributedTransactionAlreadyPreparedToCommitException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDistributedTransactionAlreadyPreparedToCommitException class.
        /// </summary>
        public EsentDistributedTransactionAlreadyPreparedToCommitException() :
            base("Attempted a write-operation after a distributed transaction has called PrepareToCommit", JET_err.DistributedTransactionAlreadyPreparedToCommit)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDistributedTransactionAlreadyPreparedToCommitException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDistributedTransactionAlreadyPreparedToCommitException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NotInDistributedTransaction exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNotInDistributedTransactionException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNotInDistributedTransactionException class.
        /// </summary>
        public EsentNotInDistributedTransactionException() :
            base("Attempted to PrepareToCommit a non-distributed transaction", JET_err.NotInDistributedTransaction)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNotInDistributedTransactionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNotInDistributedTransactionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DistributedTransactionNotYetPreparedToCommit exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDistributedTransactionNotYetPreparedToCommitException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDistributedTransactionNotYetPreparedToCommitException class.
        /// </summary>
        public EsentDistributedTransactionNotYetPreparedToCommitException() :
            base("Attempted to commit a distributed transaction, but PrepareToCommit has not yet been called", JET_err.DistributedTransactionNotYetPreparedToCommit)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDistributedTransactionNotYetPreparedToCommitException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDistributedTransactionNotYetPreparedToCommitException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotNestDistributedTransactions exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotNestDistributedTransactionsException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotNestDistributedTransactionsException class.
        /// </summary>
        public EsentCannotNestDistributedTransactionsException() :
            base("Attempted to begin a distributed transaction when not at level 0", JET_err.CannotNestDistributedTransactions)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotNestDistributedTransactionsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotNestDistributedTransactionsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DTCMissingCallback exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDTCMissingCallbackException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDTCMissingCallbackException class.
        /// </summary>
        public EsentDTCMissingCallbackException() :
            base("Attempted to begin a distributed transaction but no callback for DTC coordination was specified on initialisation", JET_err.DTCMissingCallback)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDTCMissingCallbackException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDTCMissingCallbackException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DTCMissingCallbackOnRecovery exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDTCMissingCallbackOnRecoveryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDTCMissingCallbackOnRecoveryException class.
        /// </summary>
        public EsentDTCMissingCallbackOnRecoveryException() :
            base("Attempted to recover a distributed transaction but no callback for DTC coordination was specified on initialisation", JET_err.DTCMissingCallbackOnRecovery)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDTCMissingCallbackOnRecoveryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDTCMissingCallbackOnRecoveryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DTCCallbackUnexpectedError exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDTCCallbackUnexpectedErrorException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDTCCallbackUnexpectedErrorException class.
        /// </summary>
        public EsentDTCCallbackUnexpectedErrorException() :
            base("Unexpected error code returned from DTC callback", JET_err.DTCCallbackUnexpectedError)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDTCCallbackUnexpectedErrorException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDTCCallbackUnexpectedErrorException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseDuplicateException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseDuplicateException class.
        /// </summary>
        public EsentDatabaseDuplicateException() :
            base("Database already exists", JET_err.DatabaseDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInUseException class.
        /// </summary>
        public EsentDatabaseInUseException() :
            base("Database in use", JET_err.DatabaseInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseNotFoundException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseNotFoundException class.
        /// </summary>
        public EsentDatabaseNotFoundException() :
            base("No such database", JET_err.DatabaseNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseInvalidName exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseInvalidNameException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidNameException class.
        /// </summary>
        public EsentDatabaseInvalidNameException() :
            base("Invalid database name", JET_err.DatabaseInvalidName)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidNameException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseInvalidNameException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseInvalidPages exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseInvalidPagesException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidPagesException class.
        /// </summary>
        public EsentDatabaseInvalidPagesException() :
            base("Invalid number of pages", JET_err.DatabaseInvalidPages)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidPagesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseInvalidPagesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseCorruptedException class.
        /// </summary>
        public EsentDatabaseCorruptedException() :
            base("Non database file or corrupted db", JET_err.DatabaseCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseLocked exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseLockedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseLockedException class.
        /// </summary>
        public EsentDatabaseLockedException() :
            base("Database exclusively locked", JET_err.DatabaseLocked)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseLockedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseLockedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotDisableVersioning exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotDisableVersioningException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotDisableVersioningException class.
        /// </summary>
        public EsentCannotDisableVersioningException() :
            base("Cannot disable versioning for this database", JET_err.CannotDisableVersioning)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotDisableVersioningException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotDisableVersioningException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidDatabaseVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidDatabaseVersionException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidDatabaseVersionException class.
        /// </summary>
        public EsentInvalidDatabaseVersionException() :
            base("Database engine is incompatible with database", JET_err.InvalidDatabaseVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidDatabaseVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidDatabaseVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.Database200Format exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabase200FormatException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabase200FormatException class.
        /// </summary>
        public EsentDatabase200FormatException() :
            base("The database is in an older (200) format", JET_err.Database200Format)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabase200FormatException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabase200FormatException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.Database400Format exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabase400FormatException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabase400FormatException class.
        /// </summary>
        public EsentDatabase400FormatException() :
            base("The database is in an older (400) format", JET_err.Database400Format)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabase400FormatException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabase400FormatException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.Database500Format exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabase500FormatException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabase500FormatException class.
        /// </summary>
        public EsentDatabase500FormatException() :
            base("The database is in an older (500) format", JET_err.Database500Format)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabase500FormatException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabase500FormatException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PageSizeMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPageSizeMismatchException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPageSizeMismatchException class.
        /// </summary>
        public EsentPageSizeMismatchException() :
            base("The database page size does not match the engine", JET_err.PageSizeMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPageSizeMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPageSizeMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyInstances exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyInstancesException : EsentQuotaException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyInstancesException class.
        /// </summary>
        public EsentTooManyInstancesException() :
            base("Cannot start any more database instances", JET_err.TooManyInstances)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyInstancesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyInstancesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseSharingViolation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseSharingViolationException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseSharingViolationException class.
        /// </summary>
        public EsentDatabaseSharingViolationException() :
            base("A different database instance is using this database", JET_err.DatabaseSharingViolation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseSharingViolationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseSharingViolationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.AttachedDatabaseMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentAttachedDatabaseMismatchException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentAttachedDatabaseMismatchException class.
        /// </summary>
        public EsentAttachedDatabaseMismatchException() :
            base("An outstanding database attachment has been detected at the start or end of recovery, but database is missing or does not match attachment info", JET_err.AttachedDatabaseMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentAttachedDatabaseMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentAttachedDatabaseMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseInvalidPath exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseInvalidPathException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidPathException class.
        /// </summary>
        public EsentDatabaseInvalidPathException() :
            base("Specified path to database file is illegal", JET_err.DatabaseInvalidPath)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidPathException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseInvalidPathException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseIdInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseIdInUseException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseIdInUseException class.
        /// </summary>
        public EsentDatabaseIdInUseException() :
            base("A database is being assigned an id already in use", JET_err.DatabaseIdInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseIdInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseIdInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ForceDetachNotAllowed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentForceDetachNotAllowedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentForceDetachNotAllowedException class.
        /// </summary>
        public EsentForceDetachNotAllowedException() :
            base("Force Detach allowed only after normal detach errored out", JET_err.ForceDetachNotAllowed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentForceDetachNotAllowedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentForceDetachNotAllowedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CatalogCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCatalogCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCatalogCorruptedException class.
        /// </summary>
        public EsentCatalogCorruptedException() :
            base("Corruption detected in catalog", JET_err.CatalogCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCatalogCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCatalogCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PartiallyAttachedDB exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPartiallyAttachedDBException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPartiallyAttachedDBException class.
        /// </summary>
        public EsentPartiallyAttachedDBException() :
            base("Database is partially attached. Cannot complete attach operation", JET_err.PartiallyAttachedDB)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPartiallyAttachedDBException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPartiallyAttachedDBException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseSignInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseSignInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseSignInUseException class.
        /// </summary>
        public EsentDatabaseSignInUseException() :
            base("Database with same signature in use", JET_err.DatabaseSignInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseSignInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseSignInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseCorruptedNoRepair exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseCorruptedNoRepairException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseCorruptedNoRepairException class.
        /// </summary>
        public EsentDatabaseCorruptedNoRepairException() :
            base("Corrupted db but repair not allowed", JET_err.DatabaseCorruptedNoRepair)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseCorruptedNoRepairException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseCorruptedNoRepairException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidCreateDbVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidCreateDbVersionException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCreateDbVersionException class.
        /// </summary>
        public EsentInvalidCreateDbVersionException() :
            base("recovery tried to replay a database creation, but the database was originally created with an incompatible (likely older) version of the database engine", JET_err.InvalidCreateDbVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCreateDbVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidCreateDbVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseIncompleteIncrementalReseed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseIncompleteIncrementalReseedException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseIncompleteIncrementalReseedException class.
        /// </summary>
        public EsentDatabaseIncompleteIncrementalReseedException() :
            base("The database cannot be attached because it is currently being rebuilt as part of an incremental reseed.", JET_err.DatabaseIncompleteIncrementalReseed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseIncompleteIncrementalReseedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseIncompleteIncrementalReseedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseInvalidIncrementalReseed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseInvalidIncrementalReseedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidIncrementalReseedException class.
        /// </summary>
        public EsentDatabaseInvalidIncrementalReseedException() :
            base("The database is not a valid state to perform an incremental reseed.", JET_err.DatabaseInvalidIncrementalReseed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseInvalidIncrementalReseedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseInvalidIncrementalReseedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseFailedIncrementalReseed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseFailedIncrementalReseedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseFailedIncrementalReseedException class.
        /// </summary>
        public EsentDatabaseFailedIncrementalReseedException() :
            base("The incremental reseed being performed on the specified database cannot be completed due to a fatal error.  A full reseed is required to recover this database.", JET_err.DatabaseFailedIncrementalReseed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseFailedIncrementalReseedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseFailedIncrementalReseedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NoAttachmentsFailedIncrementalReseed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNoAttachmentsFailedIncrementalReseedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNoAttachmentsFailedIncrementalReseedException class.
        /// </summary>
        public EsentNoAttachmentsFailedIncrementalReseedException() :
            base("The incremental reseed being performed on the specified database cannot be completed because the min required log contains no attachment info.  A full reseed is required to recover this database.", JET_err.NoAttachmentsFailedIncrementalReseed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNoAttachmentsFailedIncrementalReseedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNoAttachmentsFailedIncrementalReseedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseNotReady exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseNotReadyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseNotReadyException class.
        /// </summary>
        public EsentDatabaseNotReadyException() :
            base("Recovery on this database has not yet completed enough to permit access.", JET_err.DatabaseNotReady)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseNotReadyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseNotReadyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseAttachedForRecovery exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseAttachedForRecoveryException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseAttachedForRecoveryException class.
        /// </summary>
        public EsentDatabaseAttachedForRecoveryException() :
            base("Database is attached but only for recovery.  It must be explicitly attached before it can be opened. ", JET_err.DatabaseAttachedForRecovery)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseAttachedForRecoveryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseAttachedForRecoveryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TransactionsNotReadyDuringRecovery exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTransactionsNotReadyDuringRecoveryException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTransactionsNotReadyDuringRecoveryException class.
        /// </summary>
        public EsentTransactionsNotReadyDuringRecoveryException() :
            base("Recovery has not seen any Begin0/Commit0 records and so does not know what trxBegin0 to assign to this transaction", JET_err.TransactionsNotReadyDuringRecovery)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTransactionsNotReadyDuringRecoveryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTransactionsNotReadyDuringRecoveryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TableLocked exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTableLockedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTableLockedException class.
        /// </summary>
        public EsentTableLockedException() :
            base("Table is exclusively locked", JET_err.TableLocked)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTableLockedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTableLockedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TableDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTableDuplicateException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTableDuplicateException class.
        /// </summary>
        public EsentTableDuplicateException() :
            base("Table already exists", JET_err.TableDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTableDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTableDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TableInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTableInUseException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTableInUseException class.
        /// </summary>
        public EsentTableInUseException() :
            base("Table is in use, cannot lock", JET_err.TableInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTableInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTableInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ObjectNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentObjectNotFoundException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentObjectNotFoundException class.
        /// </summary>
        public EsentObjectNotFoundException() :
            base("No such table or object", JET_err.ObjectNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentObjectNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentObjectNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DensityInvalid exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDensityInvalidException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDensityInvalidException class.
        /// </summary>
        public EsentDensityInvalidException() :
            base("Bad file/index density", JET_err.DensityInvalid)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDensityInvalidException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDensityInvalidException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TableNotEmpty exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTableNotEmptyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTableNotEmptyException class.
        /// </summary>
        public EsentTableNotEmptyException() :
            base("Table is not empty", JET_err.TableNotEmpty)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTableNotEmptyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTableNotEmptyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidTableId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidTableIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidTableIdException class.
        /// </summary>
        public EsentInvalidTableIdException() :
            base("Invalid table id", JET_err.InvalidTableId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidTableIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidTableIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyOpenTables exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyOpenTablesException : EsentQuotaException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenTablesException class.
        /// </summary>
        public EsentTooManyOpenTablesException() :
            base("Cannot open any more tables (cleanup already attempted)", JET_err.TooManyOpenTables)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenTablesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyOpenTablesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IllegalOperation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIllegalOperationException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIllegalOperationException class.
        /// </summary>
        public EsentIllegalOperationException() :
            base("Oper. not supported on table", JET_err.IllegalOperation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIllegalOperationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIllegalOperationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyOpenTablesAndCleanupTimedOut exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyOpenTablesAndCleanupTimedOutException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenTablesAndCleanupTimedOutException class.
        /// </summary>
        public EsentTooManyOpenTablesAndCleanupTimedOutException() :
            base("Cannot open any more tables (cleanup attempt failed to complete)", JET_err.TooManyOpenTablesAndCleanupTimedOut)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenTablesAndCleanupTimedOutException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyOpenTablesAndCleanupTimedOutException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ObjectDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentObjectDuplicateException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentObjectDuplicateException class.
        /// </summary>
        public EsentObjectDuplicateException() :
            base("Table or object name in use", JET_err.ObjectDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentObjectDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentObjectDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidObject exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidObjectException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidObjectException class.
        /// </summary>
        public EsentInvalidObjectException() :
            base("Object is invalid for operation", JET_err.InvalidObject)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidObjectException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidObjectException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotDeleteTempTable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotDeleteTempTableException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotDeleteTempTableException class.
        /// </summary>
        public EsentCannotDeleteTempTableException() :
            base("Use CloseTable instead of DeleteTable to delete temp table", JET_err.CannotDeleteTempTable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotDeleteTempTableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotDeleteTempTableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotDeleteSystemTable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotDeleteSystemTableException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotDeleteSystemTableException class.
        /// </summary>
        public EsentCannotDeleteSystemTableException() :
            base("Illegal attempt to delete a system table", JET_err.CannotDeleteSystemTable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotDeleteSystemTableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotDeleteSystemTableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotDeleteTemplateTable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotDeleteTemplateTableException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotDeleteTemplateTableException class.
        /// </summary>
        public EsentCannotDeleteTemplateTableException() :
            base("Illegal attempt to delete a template table", JET_err.CannotDeleteTemplateTable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotDeleteTemplateTableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotDeleteTemplateTableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ExclusiveTableLockRequired exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentExclusiveTableLockRequiredException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentExclusiveTableLockRequiredException class.
        /// </summary>
        public EsentExclusiveTableLockRequiredException() :
            base("Must have exclusive lock on table.", JET_err.ExclusiveTableLockRequired)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentExclusiveTableLockRequiredException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentExclusiveTableLockRequiredException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FixedDDL exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFixedDDLException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFixedDDLException class.
        /// </summary>
        public EsentFixedDDLException() :
            base("DDL operations prohibited on this table", JET_err.FixedDDL)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFixedDDLException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFixedDDLException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FixedInheritedDDL exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFixedInheritedDDLException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFixedInheritedDDLException class.
        /// </summary>
        public EsentFixedInheritedDDLException() :
            base("On a derived table, DDL operations are prohibited on inherited portion of DDL", JET_err.FixedInheritedDDL)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFixedInheritedDDLException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFixedInheritedDDLException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotNestDDL exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotNestDDLException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotNestDDLException class.
        /// </summary>
        public EsentCannotNestDDLException() :
            base("Nesting of hierarchical DDL is not currently supported.", JET_err.CannotNestDDL)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotNestDDLException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotNestDDLException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DDLNotInheritable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDDLNotInheritableException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDDLNotInheritableException class.
        /// </summary>
        public EsentDDLNotInheritableException() :
            base("Tried to inherit DDL from a table not marked as a template table.", JET_err.DDLNotInheritable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDDLNotInheritableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDDLNotInheritableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidSettings exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidSettingsException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSettingsException class.
        /// </summary>
        public EsentInvalidSettingsException() :
            base("System parameters were set improperly", JET_err.InvalidSettings)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidSettingsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidSettingsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ClientRequestToStopJetService exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentClientRequestToStopJetServiceException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentClientRequestToStopJetServiceException class.
        /// </summary>
        public EsentClientRequestToStopJetServiceException() :
            base("Client has requested stop service", JET_err.ClientRequestToStopJetService)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentClientRequestToStopJetServiceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentClientRequestToStopJetServiceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotAddFixedVarColumnToDerivedTable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotAddFixedVarColumnToDerivedTableException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotAddFixedVarColumnToDerivedTableException class.
        /// </summary>
        public EsentCannotAddFixedVarColumnToDerivedTableException() :
            base("Template table was created with NoFixedVarColumnsInDerivedTables", JET_err.CannotAddFixedVarColumnToDerivedTable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotAddFixedVarColumnToDerivedTableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotAddFixedVarColumnToDerivedTableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexCantBuild exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexCantBuildException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexCantBuildException class.
        /// </summary>
        public EsentIndexCantBuildException() :
            base("Index build failed", JET_err.IndexCantBuild)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexCantBuildException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexCantBuildException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexHasPrimary exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexHasPrimaryException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexHasPrimaryException class.
        /// </summary>
        public EsentIndexHasPrimaryException() :
            base("Primary index already defined", JET_err.IndexHasPrimary)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexHasPrimaryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexHasPrimaryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexDuplicateException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexDuplicateException class.
        /// </summary>
        public EsentIndexDuplicateException() :
            base("Index is already defined", JET_err.IndexDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexNotFoundException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexNotFoundException class.
        /// </summary>
        public EsentIndexNotFoundException() :
            base("No such index", JET_err.IndexNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexMustStay exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexMustStayException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexMustStayException class.
        /// </summary>
        public EsentIndexMustStayException() :
            base("Cannot delete clustered index", JET_err.IndexMustStay)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexMustStayException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexMustStayException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexInvalidDef exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexInvalidDefException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexInvalidDefException class.
        /// </summary>
        public EsentIndexInvalidDefException() :
            base("Illegal index definition", JET_err.IndexInvalidDef)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexInvalidDefException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexInvalidDefException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidCreateIndex exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidCreateIndexException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCreateIndexException class.
        /// </summary>
        public EsentInvalidCreateIndexException() :
            base("Invalid create index description", JET_err.InvalidCreateIndex)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidCreateIndexException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidCreateIndexException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyOpenIndexes exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyOpenIndexesException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenIndexesException class.
        /// </summary>
        public EsentTooManyOpenIndexesException() :
            base("Out of index description blocks", JET_err.TooManyOpenIndexes)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyOpenIndexesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyOpenIndexesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MultiValuedIndexViolation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMultiValuedIndexViolationException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedIndexViolationException class.
        /// </summary>
        public EsentMultiValuedIndexViolationException() :
            base("Non-unique inter-record index keys generated for a multivalued index", JET_err.MultiValuedIndexViolation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedIndexViolationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMultiValuedIndexViolationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexBuildCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexBuildCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexBuildCorruptedException class.
        /// </summary>
        public EsentIndexBuildCorruptedException() :
            base("Failed to build a secondary index that properly reflects primary index", JET_err.IndexBuildCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexBuildCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexBuildCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PrimaryIndexCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPrimaryIndexCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPrimaryIndexCorruptedException class.
        /// </summary>
        public EsentPrimaryIndexCorruptedException() :
            base("Primary index is corrupt. The database must be defragmented or the table deleted.", JET_err.PrimaryIndexCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPrimaryIndexCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPrimaryIndexCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SecondaryIndexCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSecondaryIndexCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSecondaryIndexCorruptedException class.
        /// </summary>
        public EsentSecondaryIndexCorruptedException() :
            base("Secondary index is corrupt. The database must be defragmented or the affected index must be deleted. If the corrupt index is over Unicode text, a likely cause is a sort-order change.", JET_err.SecondaryIndexCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSecondaryIndexCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSecondaryIndexCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidIndexId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidIndexIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidIndexIdException class.
        /// </summary>
        public EsentInvalidIndexIdException() :
            base("Illegal index id", JET_err.InvalidIndexId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidIndexIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidIndexIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesSecondaryIndexOnly exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesSecondaryIndexOnlyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesSecondaryIndexOnlyException class.
        /// </summary>
        public EsentIndexTuplesSecondaryIndexOnlyException() :
            base("tuple index can only be on a secondary index", JET_err.IndexTuplesSecondaryIndexOnly)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesSecondaryIndexOnlyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesSecondaryIndexOnlyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesTooManyColumns exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesTooManyColumnsException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesTooManyColumnsException class.
        /// </summary>
        public EsentIndexTuplesTooManyColumnsException() :
            base("tuple index may only have eleven columns in the index", JET_err.IndexTuplesTooManyColumns)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesTooManyColumnsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesTooManyColumnsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesNonUniqueOnly exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesNonUniqueOnlyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesNonUniqueOnlyException class.
        /// </summary>
        public EsentIndexTuplesNonUniqueOnlyException() :
            base("tuple index must be a non-unique index", JET_err.IndexTuplesNonUniqueOnly)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesNonUniqueOnlyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesNonUniqueOnlyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesTextBinaryColumnsOnly exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesTextBinaryColumnsOnlyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesTextBinaryColumnsOnlyException class.
        /// </summary>
        public EsentIndexTuplesTextBinaryColumnsOnlyException() :
            base("tuple index must be on a text/binary column", JET_err.IndexTuplesTextBinaryColumnsOnly)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesTextBinaryColumnsOnlyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesTextBinaryColumnsOnlyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesVarSegMacNotAllowed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesVarSegMacNotAllowedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesVarSegMacNotAllowedException class.
        /// </summary>
        public EsentIndexTuplesVarSegMacNotAllowedException() :
            base("tuple index does not allow setting cbVarSegMac", JET_err.IndexTuplesVarSegMacNotAllowed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesVarSegMacNotAllowedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesVarSegMacNotAllowedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesInvalidLimits exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesInvalidLimitsException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesInvalidLimitsException class.
        /// </summary>
        public EsentIndexTuplesInvalidLimitsException() :
            base("invalid min/max tuple length or max characters to index specified", JET_err.IndexTuplesInvalidLimits)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesInvalidLimitsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesInvalidLimitsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesCannotRetrieveFromIndex exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesCannotRetrieveFromIndexException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesCannotRetrieveFromIndexException class.
        /// </summary>
        public EsentIndexTuplesCannotRetrieveFromIndexException() :
            base("cannot call RetrieveColumn() with RetrieveFromIndex on a tuple index", JET_err.IndexTuplesCannotRetrieveFromIndex)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesCannotRetrieveFromIndexException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesCannotRetrieveFromIndexException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.IndexTuplesKeyTooSmall exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentIndexTuplesKeyTooSmallException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesKeyTooSmallException class.
        /// </summary>
        public EsentIndexTuplesKeyTooSmallException() :
            base("specified key does not meet minimum tuple length", JET_err.IndexTuplesKeyTooSmall)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentIndexTuplesKeyTooSmallException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentIndexTuplesKeyTooSmallException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLVChunkSize exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLVChunkSizeException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLVChunkSizeException class.
        /// </summary>
        public EsentInvalidLVChunkSizeException() :
            base("Specified LV chunk size is not supported", JET_err.InvalidLVChunkSize)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLVChunkSizeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLVChunkSizeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnCannotBeEncrypted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnCannotBeEncryptedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnCannotBeEncryptedException class.
        /// </summary>
        public EsentColumnCannotBeEncryptedException() :
            base("Only JET_coltypLongText and JET_coltypLongBinary columns without default values can be encrypted", JET_err.ColumnCannotBeEncrypted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnCannotBeEncryptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnCannotBeEncryptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotIndexOnEncryptedColumn exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotIndexOnEncryptedColumnException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotIndexOnEncryptedColumnException class.
        /// </summary>
        public EsentCannotIndexOnEncryptedColumnException() :
            base("Cannot index encrypted column", JET_err.CannotIndexOnEncryptedColumn)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotIndexOnEncryptedColumnException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotIndexOnEncryptedColumnException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnLong exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnLongException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnLongException class.
        /// </summary>
        public EsentColumnLongException() :
            base("Column value is long", JET_err.ColumnLong)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnLongException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnLongException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnNoChunk exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnNoChunkException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnNoChunkException class.
        /// </summary>
        public EsentColumnNoChunkException() :
            base("No such chunk in long value", JET_err.ColumnNoChunk)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnNoChunkException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnNoChunkException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnDoesNotFit exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnDoesNotFitException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnDoesNotFitException class.
        /// </summary>
        public EsentColumnDoesNotFitException() :
            base("Field will not fit in record", JET_err.ColumnDoesNotFit)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnDoesNotFitException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnDoesNotFitException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NullInvalid exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNullInvalidException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNullInvalidException class.
        /// </summary>
        public EsentNullInvalidException() :
            base("Null not valid", JET_err.NullInvalid)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNullInvalidException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNullInvalidException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnIndexed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnIndexedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnIndexedException class.
        /// </summary>
        public EsentColumnIndexedException() :
            base("Column indexed, cannot delete", JET_err.ColumnIndexed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnIndexedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnIndexedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnTooBig exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnTooBigException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnTooBigException class.
        /// </summary>
        public EsentColumnTooBigException() :
            base("Field length is greater than maximum", JET_err.ColumnTooBig)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnTooBigException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnTooBigException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnNotFoundException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnNotFoundException class.
        /// </summary>
        public EsentColumnNotFoundException() :
            base("No such column", JET_err.ColumnNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnDuplicateException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnDuplicateException class.
        /// </summary>
        public EsentColumnDuplicateException() :
            base("Field is already defined", JET_err.ColumnDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MultiValuedColumnMustBeTagged exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMultiValuedColumnMustBeTaggedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedColumnMustBeTaggedException class.
        /// </summary>
        public EsentMultiValuedColumnMustBeTaggedException() :
            base("Attempted to create a multi-valued column, but column was not Tagged", JET_err.MultiValuedColumnMustBeTagged)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedColumnMustBeTaggedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMultiValuedColumnMustBeTaggedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnRedundant exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnRedundantException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnRedundantException class.
        /// </summary>
        public EsentColumnRedundantException() :
            base("Second autoincrement or version column", JET_err.ColumnRedundant)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnRedundantException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnRedundantException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidColumnType exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidColumnTypeException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidColumnTypeException class.
        /// </summary>
        public EsentInvalidColumnTypeException() :
            base("Invalid column data type", JET_err.InvalidColumnType)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidColumnTypeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidColumnTypeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TaggedNotNULL exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTaggedNotNULLException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTaggedNotNULLException class.
        /// </summary>
        public EsentTaggedNotNULLException() :
            base("No non-NULL tagged columns", JET_err.TaggedNotNULL)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTaggedNotNULLException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTaggedNotNULLException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NoCurrentIndex exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNoCurrentIndexException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNoCurrentIndexException class.
        /// </summary>
        public EsentNoCurrentIndexException() :
            base("Invalid w/o a current index", JET_err.NoCurrentIndex)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNoCurrentIndexException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNoCurrentIndexException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.KeyIsMade exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentKeyIsMadeException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentKeyIsMadeException class.
        /// </summary>
        public EsentKeyIsMadeException() :
            base("The key is completely made", JET_err.KeyIsMade)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentKeyIsMadeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentKeyIsMadeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadColumnId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadColumnIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadColumnIdException class.
        /// </summary>
        public EsentBadColumnIdException() :
            base("Column Id Incorrect", JET_err.BadColumnId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadColumnIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadColumnIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.BadItagSequence exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentBadItagSequenceException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentBadItagSequenceException class.
        /// </summary>
        public EsentBadItagSequenceException() :
            base("Bad itagSequence for tagged column", JET_err.BadItagSequence)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentBadItagSequenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentBadItagSequenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnInRelationship exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnInRelationshipException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnInRelationshipException class.
        /// </summary>
        public EsentColumnInRelationshipException() :
            base("Cannot delete, column participates in relationship", JET_err.ColumnInRelationship)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnInRelationshipException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnInRelationshipException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CannotBeTagged exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCannotBeTaggedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCannotBeTaggedException class.
        /// </summary>
        public EsentCannotBeTaggedException() :
            base("AutoIncrement and Version cannot be tagged", JET_err.CannotBeTagged)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCannotBeTaggedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCannotBeTaggedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DefaultValueTooBig exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDefaultValueTooBigException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDefaultValueTooBigException class.
        /// </summary>
        public EsentDefaultValueTooBigException() :
            base("Default value exceeds maximum size", JET_err.DefaultValueTooBig)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDefaultValueTooBigException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDefaultValueTooBigException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MultiValuedDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMultiValuedDuplicateException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedDuplicateException class.
        /// </summary>
        public EsentMultiValuedDuplicateException() :
            base("Duplicate detected on a unique multi-valued column", JET_err.MultiValuedDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMultiValuedDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LVCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLVCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLVCorruptedException class.
        /// </summary>
        public EsentLVCorruptedException() :
            base("Corruption encountered in long-value tree", JET_err.LVCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLVCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLVCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.MultiValuedDuplicateAfterTruncation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentMultiValuedDuplicateAfterTruncationException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedDuplicateAfterTruncationException class.
        /// </summary>
        public EsentMultiValuedDuplicateAfterTruncationException() :
            base("Duplicate detected on a unique multi-valued column after data was normalized, and normalizing truncated the data before comparison", JET_err.MultiValuedDuplicateAfterTruncation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentMultiValuedDuplicateAfterTruncationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentMultiValuedDuplicateAfterTruncationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DerivedColumnCorruption exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDerivedColumnCorruptionException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDerivedColumnCorruptionException class.
        /// </summary>
        public EsentDerivedColumnCorruptionException() :
            base("Invalid column in derived table", JET_err.DerivedColumnCorruption)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDerivedColumnCorruptionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDerivedColumnCorruptionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidPlaceholderColumn exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidPlaceholderColumnException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidPlaceholderColumnException class.
        /// </summary>
        public EsentInvalidPlaceholderColumnException() :
            base("Tried to convert column to a primary index placeholder, but column doesn't meet necessary criteria", JET_err.InvalidPlaceholderColumn)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidPlaceholderColumnException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidPlaceholderColumnException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnCannotBeCompressed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnCannotBeCompressedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnCannotBeCompressedException class.
        /// </summary>
        public EsentColumnCannotBeCompressedException() :
            base("Only JET_coltypLongText and JET_coltypLongBinary columns can be compressed", JET_err.ColumnCannotBeCompressed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnCannotBeCompressedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnCannotBeCompressedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.ColumnNoEncryptionKey exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentColumnNoEncryptionKeyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentColumnNoEncryptionKeyException class.
        /// </summary>
        public EsentColumnNoEncryptionKeyException() :
            base("Cannot retrieve/set encrypted column without an encryption key", JET_err.ColumnNoEncryptionKey)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentColumnNoEncryptionKeyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentColumnNoEncryptionKeyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordNotFoundException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordNotFoundException class.
        /// </summary>
        public EsentRecordNotFoundException() :
            base("The key was not found", JET_err.RecordNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordNoCopy exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordNoCopyException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordNoCopyException class.
        /// </summary>
        public EsentRecordNoCopyException() :
            base("No working buffer", JET_err.RecordNoCopy)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordNoCopyException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordNoCopyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.NoCurrentRecord exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentNoCurrentRecordException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentNoCurrentRecordException class.
        /// </summary>
        public EsentNoCurrentRecordException() :
            base("Currently not on a record", JET_err.NoCurrentRecord)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentNoCurrentRecordException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentNoCurrentRecordException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordPrimaryChanged exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordPrimaryChangedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordPrimaryChangedException class.
        /// </summary>
        public EsentRecordPrimaryChangedException() :
            base("Primary key may not change", JET_err.RecordPrimaryChanged)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordPrimaryChangedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordPrimaryChangedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.KeyDuplicate exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentKeyDuplicateException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentKeyDuplicateException class.
        /// </summary>
        public EsentKeyDuplicateException() :
            base("Illegal duplicate key", JET_err.KeyDuplicate)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentKeyDuplicateException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentKeyDuplicateException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.AlreadyPrepared exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentAlreadyPreparedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentAlreadyPreparedException class.
        /// </summary>
        public EsentAlreadyPreparedException() :
            base("Attempted to update record when record update was already in progress", JET_err.AlreadyPrepared)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentAlreadyPreparedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentAlreadyPreparedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.KeyNotMade exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentKeyNotMadeException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentKeyNotMadeException class.
        /// </summary>
        public EsentKeyNotMadeException() :
            base("No call to JetMakeKey", JET_err.KeyNotMade)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentKeyNotMadeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentKeyNotMadeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UpdateNotPrepared exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUpdateNotPreparedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUpdateNotPreparedException class.
        /// </summary>
        public EsentUpdateNotPreparedException() :
            base("No call to JetPrepareUpdate", JET_err.UpdateNotPrepared)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUpdateNotPreparedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUpdateNotPreparedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DataHasChanged exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDataHasChangedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDataHasChangedException class.
        /// </summary>
        public EsentDataHasChangedException() :
            base("Data has changed, operation aborted", JET_err.DataHasChanged)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDataHasChangedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDataHasChangedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LanguageNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLanguageNotSupportedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLanguageNotSupportedException class.
        /// </summary>
        public EsentLanguageNotSupportedException() :
            base("Windows installation does not support language", JET_err.LanguageNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLanguageNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLanguageNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DecompressionFailed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDecompressionFailedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDecompressionFailedException class.
        /// </summary>
        public EsentDecompressionFailedException() :
            base("Internal error: data could not be decompressed", JET_err.DecompressionFailed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDecompressionFailedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDecompressionFailedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.UpdateMustVersion exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentUpdateMustVersionException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentUpdateMustVersionException class.
        /// </summary>
        public EsentUpdateMustVersionException() :
            base("No version updates only for uncommitted tables", JET_err.UpdateMustVersion)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentUpdateMustVersionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentUpdateMustVersionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DecryptionFailed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDecryptionFailedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDecryptionFailedException class.
        /// </summary>
        public EsentDecryptionFailedException() :
            base("Data could not be decrypted", JET_err.DecryptionFailed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDecryptionFailedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDecryptionFailedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EncryptionBadItag exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEncryptionBadItagException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEncryptionBadItagException class.
        /// </summary>
        public EsentEncryptionBadItagException() :
            base("Cannot encrypt tagged columns with itag>1", JET_err.EncryptionBadItag)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEncryptionBadItagException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEncryptionBadItagException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManySorts exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManySortsException : EsentMemoryException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManySortsException class.
        /// </summary>
        public EsentTooManySortsException() :
            base("Too many sort processes", JET_err.TooManySorts)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManySortsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManySortsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidOnSort exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidOnSortException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidOnSortException class.
        /// </summary>
        public EsentInvalidOnSortException() :
            base("Invalid operation on Sort", JET_err.InvalidOnSort)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidOnSortException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidOnSortException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TempFileOpenError exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTempFileOpenErrorException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTempFileOpenErrorException class.
        /// </summary>
        public EsentTempFileOpenErrorException() :
            base("Temp file could not be opened", JET_err.TempFileOpenError)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTempFileOpenErrorException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTempFileOpenErrorException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyAttachedDatabases exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyAttachedDatabasesException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyAttachedDatabasesException class.
        /// </summary>
        public EsentTooManyAttachedDatabasesException() :
            base("Too many open databases", JET_err.TooManyAttachedDatabases)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyAttachedDatabasesException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyAttachedDatabasesException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DiskFull exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDiskFullException : EsentDiskException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDiskFullException class.
        /// </summary>
        public EsentDiskFullException() :
            base("No space left on disk", JET_err.DiskFull)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDiskFullException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDiskFullException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.PermissionDenied exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentPermissionDeniedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentPermissionDeniedException class.
        /// </summary>
        public EsentPermissionDeniedException() :
            base("Permission denied", JET_err.PermissionDenied)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentPermissionDeniedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentPermissionDeniedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileNotFoundException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileNotFoundException class.
        /// </summary>
        public EsentFileNotFoundException() :
            base("File not found", JET_err.FileNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileInvalidType exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileInvalidTypeException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileInvalidTypeException class.
        /// </summary>
        public EsentFileInvalidTypeException() :
            base("Invalid file type", JET_err.FileInvalidType)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileInvalidTypeException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileInvalidTypeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileAlreadyExists exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileAlreadyExistsException : EsentInconsistentException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileAlreadyExistsException class.
        /// </summary>
        public EsentFileAlreadyExistsException() :
            base("File already exists", JET_err.FileAlreadyExists)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileAlreadyExistsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileAlreadyExistsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.AfterInitialization exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentAfterInitializationException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentAfterInitializationException class.
        /// </summary>
        public EsentAfterInitializationException() :
            base("Cannot Restore after init.", JET_err.AfterInitialization)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentAfterInitializationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentAfterInitializationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LogCorrupted exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLogCorruptedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLogCorruptedException class.
        /// </summary>
        public EsentLogCorruptedException() :
            base("Logs could not be interpreted", JET_err.LogCorrupted)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLogCorruptedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLogCorruptedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidOperation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidOperationException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidOperationException class.
        /// </summary>
        public EsentInvalidOperationException() :
            base("Invalid operation", JET_err.InvalidOperation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidOperationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidOperationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.AccessDenied exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentAccessDeniedException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentAccessDeniedException class.
        /// </summary>
        public EsentAccessDeniedException() :
            base("Access denied", JET_err.AccessDenied)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentAccessDeniedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentAccessDeniedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManySplits exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManySplitsException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManySplitsException class.
        /// </summary>
        public EsentTooManySplitsException() :
            base("Infinite split", JET_err.TooManySplits)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManySplitsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManySplitsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SessionSharingViolation exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSessionSharingViolationException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSessionSharingViolationException class.
        /// </summary>
        public EsentSessionSharingViolationException() :
            base("Multiple threads are using the same session", JET_err.SessionSharingViolation)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSessionSharingViolationException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSessionSharingViolationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.EntryPointNotFound exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentEntryPointNotFoundException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentEntryPointNotFoundException class.
        /// </summary>
        public EsentEntryPointNotFoundException() :
            base("An entry point in a DLL we require could not be found", JET_err.EntryPointNotFound)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentEntryPointNotFoundException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentEntryPointNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SessionContextAlreadySet exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSessionContextAlreadySetException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSessionContextAlreadySetException class.
        /// </summary>
        public EsentSessionContextAlreadySetException() :
            base("Specified session already has a session context set", JET_err.SessionContextAlreadySet)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSessionContextAlreadySetException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSessionContextAlreadySetException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SessionContextNotSetByThisThread exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSessionContextNotSetByThisThreadException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSessionContextNotSetByThisThreadException class.
        /// </summary>
        public EsentSessionContextNotSetByThisThreadException() :
            base("Tried to reset session context, but current thread did not orignally set the session context", JET_err.SessionContextNotSetByThisThread)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSessionContextNotSetByThisThreadException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSessionContextNotSetByThisThreadException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SessionInUse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSessionInUseException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSessionInUseException class.
        /// </summary>
        public EsentSessionInUseException() :
            base("Tried to terminate session in use", JET_err.SessionInUse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSessionInUseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSessionInUseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RecordFormatConversionFailed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRecordFormatConversionFailedException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRecordFormatConversionFailedException class.
        /// </summary>
        public EsentRecordFormatConversionFailedException() :
            base("Internal error during dynamic record format conversion", JET_err.RecordFormatConversionFailed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRecordFormatConversionFailedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRecordFormatConversionFailedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OneDatabasePerSession exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOneDatabasePerSessionException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOneDatabasePerSessionException class.
        /// </summary>
        public EsentOneDatabasePerSessionException() :
            base("Just one open user database per session is allowed (JET_paramOneDatabasePerSession)", JET_err.OneDatabasePerSession)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOneDatabasePerSessionException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOneDatabasePerSessionException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.RollbackError exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentRollbackErrorException : EsentFatalException
    {
        /// <summary>
        /// Initializes a new instance of the EsentRollbackErrorException class.
        /// </summary>
        public EsentRollbackErrorException() :
            base("error during rollback", JET_err.RollbackError)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentRollbackErrorException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentRollbackErrorException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FlushMapVersionUnsupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFlushMapVersionUnsupportedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFlushMapVersionUnsupportedException class.
        /// </summary>
        public EsentFlushMapVersionUnsupportedException() :
            base("The version of the persisted flush map is not supported by this version of the engine.", JET_err.FlushMapVersionUnsupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFlushMapVersionUnsupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFlushMapVersionUnsupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FlushMapDatabaseMismatch exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFlushMapDatabaseMismatchException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFlushMapDatabaseMismatchException class.
        /// </summary>
        public EsentFlushMapDatabaseMismatchException() :
            base("The persisted flush map and the database do not match.", JET_err.FlushMapDatabaseMismatch)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFlushMapDatabaseMismatchException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFlushMapDatabaseMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FlushMapUnrecoverable exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFlushMapUnrecoverableException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFlushMapUnrecoverableException class.
        /// </summary>
        public EsentFlushMapUnrecoverableException() :
            base("The persisted flush map cannot be reconstructed.", JET_err.FlushMapUnrecoverable)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFlushMapUnrecoverableException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFlushMapUnrecoverableException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.DatabaseAlreadyRunningMaintenance exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentDatabaseAlreadyRunningMaintenanceException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseAlreadyRunningMaintenanceException class.
        /// </summary>
        public EsentDatabaseAlreadyRunningMaintenanceException() :
            base("The operation did not complete successfully because the database is already running maintenance on specified database", JET_err.DatabaseAlreadyRunningMaintenance)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentDatabaseAlreadyRunningMaintenanceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentDatabaseAlreadyRunningMaintenanceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CallbackFailed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCallbackFailedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCallbackFailedException class.
        /// </summary>
        public EsentCallbackFailedException() :
            base("A callback failed", JET_err.CallbackFailed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCallbackFailedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCallbackFailedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.CallbackNotResolved exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentCallbackNotResolvedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentCallbackNotResolvedException class.
        /// </summary>
        public EsentCallbackNotResolvedException() :
            base("A callback function could not be found", JET_err.CallbackNotResolved)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentCallbackNotResolvedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentCallbackNotResolvedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.SpaceHintsInvalid exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentSpaceHintsInvalidException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentSpaceHintsInvalidException class.
        /// </summary>
        public EsentSpaceHintsInvalidException() :
            base("An element of the JET space hints structure was not correct or actionable.", JET_err.SpaceHintsInvalid)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentSpaceHintsInvalidException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentSpaceHintsInvalidException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OSSnapshotInvalidSequence exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOSSnapshotInvalidSequenceException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotInvalidSequenceException class.
        /// </summary>
        public EsentOSSnapshotInvalidSequenceException() :
            base("OS Shadow copy API used in an invalid sequence", JET_err.OSSnapshotInvalidSequence)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotInvalidSequenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOSSnapshotInvalidSequenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OSSnapshotTimeOut exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOSSnapshotTimeOutException : EsentOperationException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotTimeOutException class.
        /// </summary>
        public EsentOSSnapshotTimeOutException() :
            base("OS Shadow copy ended with time-out", JET_err.OSSnapshotTimeOut)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotTimeOutException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOSSnapshotTimeOutException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OSSnapshotNotAllowed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOSSnapshotNotAllowedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotNotAllowedException class.
        /// </summary>
        public EsentOSSnapshotNotAllowedException() :
            base("OS Shadow copy not allowed (backup or recovery in progress)", JET_err.OSSnapshotNotAllowed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotNotAllowedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOSSnapshotNotAllowedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.OSSnapshotInvalidSnapId exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentOSSnapshotInvalidSnapIdException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotInvalidSnapIdException class.
        /// </summary>
        public EsentOSSnapshotInvalidSnapIdException() :
            base("invalid JET_OSSNAPID", JET_err.OSSnapshotInvalidSnapId)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentOSSnapshotInvalidSnapIdException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentOSSnapshotInvalidSnapIdException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TooManyTestInjections exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTooManyTestInjectionsException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTooManyTestInjectionsException class.
        /// </summary>
        public EsentTooManyTestInjectionsException() :
            base("Internal test injection limit hit", JET_err.TooManyTestInjections)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTooManyTestInjectionsException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTooManyTestInjectionsException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.TestInjectionNotSupported exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentTestInjectionNotSupportedException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentTestInjectionNotSupportedException class.
        /// </summary>
        public EsentTestInjectionNotSupportedException() :
            base("Test injection not supported", JET_err.TestInjectionNotSupported)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentTestInjectionNotSupportedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentTestInjectionNotSupportedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.InvalidLogDataSequence exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentInvalidLogDataSequenceException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLogDataSequenceException class.
        /// </summary>
        public EsentInvalidLogDataSequenceException() :
            base("Some how the log data provided got out of sequence with the current state of the instance", JET_err.InvalidLogDataSequence)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentInvalidLogDataSequenceException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentInvalidLogDataSequenceException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LSCallbackNotSpecified exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLSCallbackNotSpecifiedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLSCallbackNotSpecifiedException class.
        /// </summary>
        public EsentLSCallbackNotSpecifiedException() :
            base("Attempted to use Local Storage without a callback function being specified", JET_err.LSCallbackNotSpecified)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLSCallbackNotSpecifiedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLSCallbackNotSpecifiedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LSAlreadySet exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLSAlreadySetException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLSAlreadySetException class.
        /// </summary>
        public EsentLSAlreadySetException() :
            base("Attempted to set Local Storage for an object which already had it set", JET_err.LSAlreadySet)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLSAlreadySetException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLSAlreadySetException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.LSNotSet exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentLSNotSetException : EsentStateException
    {
        /// <summary>
        /// Initializes a new instance of the EsentLSNotSetException class.
        /// </summary>
        public EsentLSNotSetException() :
            base("Attempted to retrieve Local Storage from an object which didn't have it set", JET_err.LSNotSet)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentLSNotSetException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentLSNotSetException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileIOSparse exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileIOSparseException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileIOSparseException class.
        /// </summary>
        public EsentFileIOSparseException() :
            base("an I/O was issued to a location that was sparse", JET_err.FileIOSparse)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileIOSparseException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileIOSparseException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileIOBeyondEOF exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileIOBeyondEOFException : EsentCorruptionException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileIOBeyondEOFException class.
        /// </summary>
        public EsentFileIOBeyondEOFException() :
            base("a read was issued to a location beyond EOF (writes will expand the file)", JET_err.FileIOBeyondEOF)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileIOBeyondEOFException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileIOBeyondEOFException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileIOAbort exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileIOAbortException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileIOAbortException class.
        /// </summary>
        public EsentFileIOAbortException() :
            base("instructs the JET_ABORTRETRYFAILCALLBACK caller to abort the specified I/O", JET_err.FileIOAbort)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileIOAbortException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileIOAbortException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileIORetry exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileIORetryException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileIORetryException class.
        /// </summary>
        public EsentFileIORetryException() :
            base("instructs the JET_ABORTRETRYFAILCALLBACK caller to retry the specified I/O", JET_err.FileIORetry)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileIORetryException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileIORetryException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileIOFail exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileIOFailException : EsentObsoleteException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileIOFailException class.
        /// </summary>
        public EsentFileIOFailException() :
            base("instructs the JET_ABORTRETRYFAILCALLBACK caller to fail the specified I/O", JET_err.FileIOFail)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileIOFailException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileIOFailException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Base class for JET_err.FileCompressed exceptions.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Auto-generated code.")]
    [Serializable]
    public sealed class EsentFileCompressedException : EsentUsageException
    {
        /// <summary>
        /// Initializes a new instance of the EsentFileCompressedException class.
        /// </summary>
        public EsentFileCompressedException() :
            base("read/write access is not supported on compressed files", JET_err.FileCompressed)
        {
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>
        /// Initializes a new instance of the EsentFileCompressedException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        private EsentFileCompressedException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
#endif
    }

    /// <summary>
    /// Method to generate an EsentErrorException from an error code.
    /// </summary>
    internal static class EsentExceptionHelper
    {
        /// <summary>
        /// Create an EsentErrorException from an error code.
        /// </summary>
        /// <param name="err">The error code.</param>
        /// <returns>An EsentErrorException for the error code.</returns>
        public static EsentErrorException JetErrToException(JET_err err)
        {
        switch (err)
            {
            case JET_err.RfsFailure:
                return new EsentRfsFailureException();
            case JET_err.RfsNotArmed:
                return new EsentRfsNotArmedException();
            case JET_err.FileClose:
                return new EsentFileCloseException();
            case JET_err.OutOfThreads:
                return new EsentOutOfThreadsException();
            case JET_err.TooManyIO:
                return new EsentTooManyIOException();
            case JET_err.TaskDropped:
                return new EsentTaskDroppedException();
            case JET_err.InternalError:
                return new EsentInternalErrorException();
            case JET_err.DisabledFunctionality:
                return new EsentDisabledFunctionalityException();
            case JET_err.UnloadableOSFunctionality:
                return new EsentUnloadableOSFunctionalityException();
            case JET_err.DatabaseBufferDependenciesCorrupted:
                return new EsentDatabaseBufferDependenciesCorruptedException();
            case JET_err.PreviousVersion:
                return new EsentPreviousVersionException();
            case JET_err.PageBoundary:
                return new EsentPageBoundaryException();
            case JET_err.KeyBoundary:
                return new EsentKeyBoundaryException();
            case JET_err.BadPageLink:
                return new EsentBadPageLinkException();
            case JET_err.BadBookmark:
                return new EsentBadBookmarkException();
            case JET_err.NTSystemCallFailed:
                return new EsentNTSystemCallFailedException();
            case JET_err.BadParentPageLink:
                return new EsentBadParentPageLinkException();
            case JET_err.SPAvailExtCacheOutOfSync:
                return new EsentSPAvailExtCacheOutOfSyncException();
            case JET_err.SPAvailExtCorrupted:
                return new EsentSPAvailExtCorruptedException();
            case JET_err.SPAvailExtCacheOutOfMemory:
                return new EsentSPAvailExtCacheOutOfMemoryException();
            case JET_err.SPOwnExtCorrupted:
                return new EsentSPOwnExtCorruptedException();
            case JET_err.DbTimeCorrupted:
                return new EsentDbTimeCorruptedException();
            case JET_err.KeyTruncated:
                return new EsentKeyTruncatedException();
            case JET_err.DatabaseLeakInSpace:
                return new EsentDatabaseLeakInSpaceException();
            case JET_err.BadEmptyPage:
                return new EsentBadEmptyPageException();
            case JET_err.BadLineCount:
                return new EsentBadLineCountException();
            case JET_err.KeyTooBig:
                return new EsentKeyTooBigException();
            case JET_err.CannotSeparateIntrinsicLV:
                return new EsentCannotSeparateIntrinsicLVException();
            case JET_err.SeparatedLongValue:
                return new EsentSeparatedLongValueException();
            case JET_err.MustBeSeparateLongValue:
                return new EsentMustBeSeparateLongValueException();
            case JET_err.InvalidPreread:
                return new EsentInvalidPrereadException();
            case JET_err.InvalidColumnReference:
                return new EsentInvalidColumnReferenceException();
            case JET_err.StaleColumnReference:
                return new EsentStaleColumnReferenceException();
            case JET_err.CompressionIntegrityCheckFailed:
                return new EsentCompressionIntegrityCheckFailedException();
            case JET_err.InvalidLoggedOperation:
                return new EsentInvalidLoggedOperationException();
            case JET_err.LogFileCorrupt:
                return new EsentLogFileCorruptException();
            case JET_err.NoBackupDirectory:
                return new EsentNoBackupDirectoryException();
            case JET_err.BackupDirectoryNotEmpty:
                return new EsentBackupDirectoryNotEmptyException();
            case JET_err.BackupInProgress:
                return new EsentBackupInProgressException();
            case JET_err.RestoreInProgress:
                return new EsentRestoreInProgressException();
            case JET_err.MissingPreviousLogFile:
                return new EsentMissingPreviousLogFileException();
            case JET_err.LogWriteFail:
                return new EsentLogWriteFailException();
            case JET_err.LogDisabledDueToRecoveryFailure:
                return new EsentLogDisabledDueToRecoveryFailureException();
            case JET_err.CannotLogDuringRecoveryRedo:
                return new EsentCannotLogDuringRecoveryRedoException();
            case JET_err.LogGenerationMismatch:
                return new EsentLogGenerationMismatchException();
            case JET_err.BadLogVersion:
                return new EsentBadLogVersionException();
            case JET_err.InvalidLogSequence:
                return new EsentInvalidLogSequenceException();
            case JET_err.LoggingDisabled:
                return new EsentLoggingDisabledException();
            case JET_err.LogBufferTooSmall:
                return new EsentLogBufferTooSmallException();
            case JET_err.LogSequenceEnd:
                return new EsentLogSequenceEndException();
            case JET_err.NoBackup:
                return new EsentNoBackupException();
            case JET_err.InvalidBackupSequence:
                return new EsentInvalidBackupSequenceException();
            case JET_err.BackupNotAllowedYet:
                return new EsentBackupNotAllowedYetException();
            case JET_err.DeleteBackupFileFail:
                return new EsentDeleteBackupFileFailException();
            case JET_err.MakeBackupDirectoryFail:
                return new EsentMakeBackupDirectoryFailException();
            case JET_err.InvalidBackup:
                return new EsentInvalidBackupException();
            case JET_err.RecoveredWithErrors:
                return new EsentRecoveredWithErrorsException();
            case JET_err.MissingLogFile:
                return new EsentMissingLogFileException();
            case JET_err.LogDiskFull:
                return new EsentLogDiskFullException();
            case JET_err.BadLogSignature:
                return new EsentBadLogSignatureException();
            case JET_err.BadDbSignature:
                return new EsentBadDbSignatureException();
            case JET_err.BadCheckpointSignature:
                return new EsentBadCheckpointSignatureException();
            case JET_err.CheckpointCorrupt:
                return new EsentCheckpointCorruptException();
            case JET_err.MissingPatchPage:
                return new EsentMissingPatchPageException();
            case JET_err.BadPatchPage:
                return new EsentBadPatchPageException();
            case JET_err.RedoAbruptEnded:
                return new EsentRedoAbruptEndedException();
            case JET_err.PatchFileMissing:
                return new EsentPatchFileMissingException();
            case JET_err.DatabaseLogSetMismatch:
                return new EsentDatabaseLogSetMismatchException();
            case JET_err.DatabaseStreamingFileMismatch:
                return new EsentDatabaseStreamingFileMismatchException();
            case JET_err.LogFileSizeMismatch:
                return new EsentLogFileSizeMismatchException();
            case JET_err.CheckpointFileNotFound:
                return new EsentCheckpointFileNotFoundException();
            case JET_err.RequiredLogFilesMissing:
                return new EsentRequiredLogFilesMissingException();
            case JET_err.SoftRecoveryOnBackupDatabase:
                return new EsentSoftRecoveryOnBackupDatabaseException();
            case JET_err.LogFileSizeMismatchDatabasesConsistent:
                return new EsentLogFileSizeMismatchDatabasesConsistentException();
            case JET_err.LogSectorSizeMismatch:
                return new EsentLogSectorSizeMismatchException();
            case JET_err.LogSectorSizeMismatchDatabasesConsistent:
                return new EsentLogSectorSizeMismatchDatabasesConsistentException();
            case JET_err.LogSequenceEndDatabasesConsistent:
                return new EsentLogSequenceEndDatabasesConsistentException();
            case JET_err.StreamingDataNotLogged:
                return new EsentStreamingDataNotLoggedException();
            case JET_err.DatabaseDirtyShutdown:
                return new EsentDatabaseDirtyShutdownException();
            case JET_err.ConsistentTimeMismatch:
                return new EsentConsistentTimeMismatchException();
            case JET_err.DatabasePatchFileMismatch:
                return new EsentDatabasePatchFileMismatchException();
            case JET_err.EndingRestoreLogTooLow:
                return new EsentEndingRestoreLogTooLowException();
            case JET_err.StartingRestoreLogTooHigh:
                return new EsentStartingRestoreLogTooHighException();
            case JET_err.GivenLogFileHasBadSignature:
                return new EsentGivenLogFileHasBadSignatureException();
            case JET_err.GivenLogFileIsNotContiguous:
                return new EsentGivenLogFileIsNotContiguousException();
            case JET_err.MissingRestoreLogFiles:
                return new EsentMissingRestoreLogFilesException();
            case JET_err.MissingFullBackup:
                return new EsentMissingFullBackupException();
            case JET_err.BadBackupDatabaseSize:
                return new EsentBadBackupDatabaseSizeException();
            case JET_err.DatabaseAlreadyUpgraded:
                return new EsentDatabaseAlreadyUpgradedException();
            case JET_err.DatabaseIncompleteUpgrade:
                return new EsentDatabaseIncompleteUpgradeException();
            case JET_err.MissingCurrentLogFiles:
                return new EsentMissingCurrentLogFilesException();
            case JET_err.DbTimeTooOld:
                return new EsentDbTimeTooOldException();
            case JET_err.DbTimeTooNew:
                return new EsentDbTimeTooNewException();
            case JET_err.MissingFileToBackup:
                return new EsentMissingFileToBackupException();
            case JET_err.LogTornWriteDuringHardRestore:
                return new EsentLogTornWriteDuringHardRestoreException();
            case JET_err.LogTornWriteDuringHardRecovery:
                return new EsentLogTornWriteDuringHardRecoveryException();
            case JET_err.LogCorruptDuringHardRestore:
                return new EsentLogCorruptDuringHardRestoreException();
            case JET_err.LogCorruptDuringHardRecovery:
                return new EsentLogCorruptDuringHardRecoveryException();
            case JET_err.MustDisableLoggingForDbUpgrade:
                return new EsentMustDisableLoggingForDbUpgradeException();
            case JET_err.BadRestoreTargetInstance:
                return new EsentBadRestoreTargetInstanceException();
            case JET_err.RecoveredWithoutUndo:
                return new EsentRecoveredWithoutUndoException();
            case JET_err.DatabasesNotFromSameSnapshot:
                return new EsentDatabasesNotFromSameSnapshotException();
            case JET_err.SoftRecoveryOnSnapshot:
                return new EsentSoftRecoveryOnSnapshotException();
            case JET_err.CommittedLogFilesMissing:
                return new EsentCommittedLogFilesMissingException();
            case JET_err.SectorSizeNotSupported:
                return new EsentSectorSizeNotSupportedException();
            case JET_err.RecoveredWithoutUndoDatabasesConsistent:
                return new EsentRecoveredWithoutUndoDatabasesConsistentException();
            case JET_err.CommittedLogFileCorrupt:
                return new EsentCommittedLogFileCorruptException();
            case JET_err.LogSequenceChecksumMismatch:
                return new EsentLogSequenceChecksumMismatchException();
            case JET_err.PageInitializedMismatch:
                return new EsentPageInitializedMismatchException();
            case JET_err.UnicodeTranslationBufferTooSmall:
                return new EsentUnicodeTranslationBufferTooSmallException();
            case JET_err.UnicodeTranslationFail:
                return new EsentUnicodeTranslationFailException();
            case JET_err.UnicodeNormalizationNotSupported:
                return new EsentUnicodeNormalizationNotSupportedException();
            case JET_err.UnicodeLanguageValidationFailure:
                return new EsentUnicodeLanguageValidationFailureException();
            case JET_err.ExistingLogFileHasBadSignature:
                return new EsentExistingLogFileHasBadSignatureException();
            case JET_err.ExistingLogFileIsNotContiguous:
                return new EsentExistingLogFileIsNotContiguousException();
            case JET_err.LogReadVerifyFailure:
                return new EsentLogReadVerifyFailureException();
            case JET_err.CheckpointDepthTooDeep:
                return new EsentCheckpointDepthTooDeepException();
            case JET_err.RestoreOfNonBackupDatabase:
                return new EsentRestoreOfNonBackupDatabaseException();
            case JET_err.LogFileNotCopied:
                return new EsentLogFileNotCopiedException();
            case JET_err.SurrogateBackupInProgress:
                return new EsentSurrogateBackupInProgressException();
            case JET_err.TransactionTooLong:
                return new EsentTransactionTooLongException();
            case JET_err.EngineFormatVersionNoLongerSupportedTooLow:
                return new EsentEngineFormatVersionNoLongerSupportedTooLowException();
            case JET_err.EngineFormatVersionNotYetImplementedTooHigh:
                return new EsentEngineFormatVersionNotYetImplementedTooHighException();
            case JET_err.EngineFormatVersionParamTooLowForRequestedFeature:
                return new EsentEngineFormatVersionParamTooLowForRequestedFeatureException();
            case JET_err.EngineFormatVersionSpecifiedTooLowForLogVersion:
                return new EsentEngineFormatVersionSpecifiedTooLowForLogVersionException();
            case JET_err.EngineFormatVersionSpecifiedTooLowForDatabaseVersion:
                return new EsentEngineFormatVersionSpecifiedTooLowForDatabaseVersionException();
            case JET_err.BackupAbortByServer:
                return new EsentBackupAbortByServerException();
            case JET_err.InvalidGrbit:
                return new EsentInvalidGrbitException();
            case JET_err.TermInProgress:
                return new EsentTermInProgressException();
            case JET_err.FeatureNotAvailable:
                return new EsentFeatureNotAvailableException();
            case JET_err.InvalidName:
                return new EsentInvalidNameException();
            case JET_err.InvalidParameter:
                return new EsentInvalidParameterException();
            case JET_err.DatabaseFileReadOnly:
                return new EsentDatabaseFileReadOnlyException();
            case JET_err.InvalidDatabaseId:
                return new EsentInvalidDatabaseIdException();
            case JET_err.OutOfMemory:
                return new EsentOutOfMemoryException();
            case JET_err.OutOfDatabaseSpace:
                return new EsentOutOfDatabaseSpaceException();
            case JET_err.OutOfCursors:
                return new EsentOutOfCursorsException();
            case JET_err.OutOfBuffers:
                return new EsentOutOfBuffersException();
            case JET_err.TooManyIndexes:
                return new EsentTooManyIndexesException();
            case JET_err.TooManyKeys:
                return new EsentTooManyKeysException();
            case JET_err.RecordDeleted:
                return new EsentRecordDeletedException();
            case JET_err.ReadVerifyFailure:
                return new EsentReadVerifyFailureException();
            case JET_err.PageNotInitialized:
                return new EsentPageNotInitializedException();
            case JET_err.OutOfFileHandles:
                return new EsentOutOfFileHandlesException();
            case JET_err.DiskReadVerificationFailure:
                return new EsentDiskReadVerificationFailureException();
            case JET_err.DiskIO:
                return new EsentDiskIOException();
            case JET_err.InvalidPath:
                return new EsentInvalidPathException();
            case JET_err.InvalidSystemPath:
                return new EsentInvalidSystemPathException();
            case JET_err.InvalidLogDirectory:
                return new EsentInvalidLogDirectoryException();
            case JET_err.RecordTooBig:
                return new EsentRecordTooBigException();
            case JET_err.TooManyOpenDatabases:
                return new EsentTooManyOpenDatabasesException();
            case JET_err.InvalidDatabase:
                return new EsentInvalidDatabaseException();
            case JET_err.NotInitialized:
                return new EsentNotInitializedException();
            case JET_err.AlreadyInitialized:
                return new EsentAlreadyInitializedException();
            case JET_err.InitInProgress:
                return new EsentInitInProgressException();
            case JET_err.FileAccessDenied:
                return new EsentFileAccessDeniedException();
            case JET_err.QueryNotSupported:
                return new EsentQueryNotSupportedException();
            case JET_err.SQLLinkNotSupported:
                return new EsentSQLLinkNotSupportedException();
            case JET_err.BufferTooSmall:
                return new EsentBufferTooSmallException();
            case JET_err.TooManyColumns:
                return new EsentTooManyColumnsException();
            case JET_err.ContainerNotEmpty:
                return new EsentContainerNotEmptyException();
            case JET_err.InvalidFilename:
                return new EsentInvalidFilenameException();
            case JET_err.InvalidBookmark:
                return new EsentInvalidBookmarkException();
            case JET_err.ColumnInUse:
                return new EsentColumnInUseException();
            case JET_err.InvalidBufferSize:
                return new EsentInvalidBufferSizeException();
            case JET_err.ColumnNotUpdatable:
                return new EsentColumnNotUpdatableException();
            case JET_err.IndexInUse:
                return new EsentIndexInUseException();
            case JET_err.LinkNotSupported:
                return new EsentLinkNotSupportedException();
            case JET_err.NullKeyDisallowed:
                return new EsentNullKeyDisallowedException();
            case JET_err.NotInTransaction:
                return new EsentNotInTransactionException();
            case JET_err.MustRollback:
                return new EsentMustRollbackException();
            case JET_err.TooManyActiveUsers:
                return new EsentTooManyActiveUsersException();
            case JET_err.InvalidCountry:
                return new EsentInvalidCountryException();
            case JET_err.InvalidLanguageId:
                return new EsentInvalidLanguageIdException();
            case JET_err.InvalidCodePage:
                return new EsentInvalidCodePageException();
            case JET_err.InvalidLCMapStringFlags:
                return new EsentInvalidLCMapStringFlagsException();
            case JET_err.VersionStoreEntryTooBig:
                return new EsentVersionStoreEntryTooBigException();
            case JET_err.VersionStoreOutOfMemoryAndCleanupTimedOut:
                return new EsentVersionStoreOutOfMemoryAndCleanupTimedOutException();
            case JET_err.VersionStoreOutOfMemory:
                return new EsentVersionStoreOutOfMemoryException();
            case JET_err.CurrencyStackOutOfMemory:
                return new EsentCurrencyStackOutOfMemoryException();
            case JET_err.CannotIndex:
                return new EsentCannotIndexException();
            case JET_err.RecordNotDeleted:
                return new EsentRecordNotDeletedException();
            case JET_err.TooManyMempoolEntries:
                return new EsentTooManyMempoolEntriesException();
            case JET_err.OutOfObjectIDs:
                return new EsentOutOfObjectIDsException();
            case JET_err.OutOfLongValueIDs:
                return new EsentOutOfLongValueIDsException();
            case JET_err.OutOfAutoincrementValues:
                return new EsentOutOfAutoincrementValuesException();
            case JET_err.OutOfDbtimeValues:
                return new EsentOutOfDbtimeValuesException();
            case JET_err.OutOfSequentialIndexValues:
                return new EsentOutOfSequentialIndexValuesException();
            case JET_err.RunningInOneInstanceMode:
                return new EsentRunningInOneInstanceModeException();
            case JET_err.RunningInMultiInstanceMode:
                return new EsentRunningInMultiInstanceModeException();
            case JET_err.SystemParamsAlreadySet:
                return new EsentSystemParamsAlreadySetException();
            case JET_err.SystemPathInUse:
                return new EsentSystemPathInUseException();
            case JET_err.LogFilePathInUse:
                return new EsentLogFilePathInUseException();
            case JET_err.TempPathInUse:
                return new EsentTempPathInUseException();
            case JET_err.InstanceNameInUse:
                return new EsentInstanceNameInUseException();
            case JET_err.SystemParameterConflict:
                return new EsentSystemParameterConflictException();
            case JET_err.InstanceUnavailable:
                return new EsentInstanceUnavailableException();
            case JET_err.DatabaseUnavailable:
                return new EsentDatabaseUnavailableException();
            case JET_err.InstanceUnavailableDueToFatalLogDiskFull:
                return new EsentInstanceUnavailableDueToFatalLogDiskFullException();
            case JET_err.InvalidSesparamId:
                return new EsentInvalidSesparamIdException();
            case JET_err.OutOfSessions:
                return new EsentOutOfSessionsException();
            case JET_err.WriteConflict:
                return new EsentWriteConflictException();
            case JET_err.TransTooDeep:
                return new EsentTransTooDeepException();
            case JET_err.InvalidSesid:
                return new EsentInvalidSesidException();
            case JET_err.WriteConflictPrimaryIndex:
                return new EsentWriteConflictPrimaryIndexException();
            case JET_err.InTransaction:
                return new EsentInTransactionException();
            case JET_err.RollbackRequired:
                return new EsentRollbackRequiredException();
            case JET_err.TransReadOnly:
                return new EsentTransReadOnlyException();
            case JET_err.SessionWriteConflict:
                return new EsentSessionWriteConflictException();
            case JET_err.RecordTooBigForBackwardCompatibility:
                return new EsentRecordTooBigForBackwardCompatibilityException();
            case JET_err.CannotMaterializeForwardOnlySort:
                return new EsentCannotMaterializeForwardOnlySortException();
            case JET_err.SesidTableIdMismatch:
                return new EsentSesidTableIdMismatchException();
            case JET_err.InvalidInstance:
                return new EsentInvalidInstanceException();
            case JET_err.DirtyShutdown:
                return new EsentDirtyShutdownException();
            case JET_err.ReadPgnoVerifyFailure:
                return new EsentReadPgnoVerifyFailureException();
            case JET_err.ReadLostFlushVerifyFailure:
                return new EsentReadLostFlushVerifyFailureException();
            case JET_err.FileSystemCorruption:
                return new EsentFileSystemCorruptionException();
            case JET_err.RecoveryVerifyFailure:
                return new EsentRecoveryVerifyFailureException();
            case JET_err.FilteredMoveNotSupported:
                return new EsentFilteredMoveNotSupportedException();
            case JET_err.MustCommitDistributedTransactionToLevel0:
                return new EsentMustCommitDistributedTransactionToLevel0Exception();
            case JET_err.DistributedTransactionAlreadyPreparedToCommit:
                return new EsentDistributedTransactionAlreadyPreparedToCommitException();
            case JET_err.NotInDistributedTransaction:
                return new EsentNotInDistributedTransactionException();
            case JET_err.DistributedTransactionNotYetPreparedToCommit:
                return new EsentDistributedTransactionNotYetPreparedToCommitException();
            case JET_err.CannotNestDistributedTransactions:
                return new EsentCannotNestDistributedTransactionsException();
            case JET_err.DTCMissingCallback:
                return new EsentDTCMissingCallbackException();
            case JET_err.DTCMissingCallbackOnRecovery:
                return new EsentDTCMissingCallbackOnRecoveryException();
            case JET_err.DTCCallbackUnexpectedError:
                return new EsentDTCCallbackUnexpectedErrorException();
            case JET_err.DatabaseDuplicate:
                return new EsentDatabaseDuplicateException();
            case JET_err.DatabaseInUse:
                return new EsentDatabaseInUseException();
            case JET_err.DatabaseNotFound:
                return new EsentDatabaseNotFoundException();
            case JET_err.DatabaseInvalidName:
                return new EsentDatabaseInvalidNameException();
            case JET_err.DatabaseInvalidPages:
                return new EsentDatabaseInvalidPagesException();
            case JET_err.DatabaseCorrupted:
                return new EsentDatabaseCorruptedException();
            case JET_err.DatabaseLocked:
                return new EsentDatabaseLockedException();
            case JET_err.CannotDisableVersioning:
                return new EsentCannotDisableVersioningException();
            case JET_err.InvalidDatabaseVersion:
                return new EsentInvalidDatabaseVersionException();
            case JET_err.Database200Format:
                return new EsentDatabase200FormatException();
            case JET_err.Database400Format:
                return new EsentDatabase400FormatException();
            case JET_err.Database500Format:
                return new EsentDatabase500FormatException();
            case JET_err.PageSizeMismatch:
                return new EsentPageSizeMismatchException();
            case JET_err.TooManyInstances:
                return new EsentTooManyInstancesException();
            case JET_err.DatabaseSharingViolation:
                return new EsentDatabaseSharingViolationException();
            case JET_err.AttachedDatabaseMismatch:
                return new EsentAttachedDatabaseMismatchException();
            case JET_err.DatabaseInvalidPath:
                return new EsentDatabaseInvalidPathException();
            case JET_err.DatabaseIdInUse:
                return new EsentDatabaseIdInUseException();
            case JET_err.ForceDetachNotAllowed:
                return new EsentForceDetachNotAllowedException();
            case JET_err.CatalogCorrupted:
                return new EsentCatalogCorruptedException();
            case JET_err.PartiallyAttachedDB:
                return new EsentPartiallyAttachedDBException();
            case JET_err.DatabaseSignInUse:
                return new EsentDatabaseSignInUseException();
            case JET_err.DatabaseCorruptedNoRepair:
                return new EsentDatabaseCorruptedNoRepairException();
            case JET_err.InvalidCreateDbVersion:
                return new EsentInvalidCreateDbVersionException();
            case JET_err.DatabaseIncompleteIncrementalReseed:
                return new EsentDatabaseIncompleteIncrementalReseedException();
            case JET_err.DatabaseInvalidIncrementalReseed:
                return new EsentDatabaseInvalidIncrementalReseedException();
            case JET_err.DatabaseFailedIncrementalReseed:
                return new EsentDatabaseFailedIncrementalReseedException();
            case JET_err.NoAttachmentsFailedIncrementalReseed:
                return new EsentNoAttachmentsFailedIncrementalReseedException();
            case JET_err.DatabaseNotReady:
                return new EsentDatabaseNotReadyException();
            case JET_err.DatabaseAttachedForRecovery:
                return new EsentDatabaseAttachedForRecoveryException();
            case JET_err.TransactionsNotReadyDuringRecovery:
                return new EsentTransactionsNotReadyDuringRecoveryException();
            case JET_err.TableLocked:
                return new EsentTableLockedException();
            case JET_err.TableDuplicate:
                return new EsentTableDuplicateException();
            case JET_err.TableInUse:
                return new EsentTableInUseException();
            case JET_err.ObjectNotFound:
                return new EsentObjectNotFoundException();
            case JET_err.DensityInvalid:
                return new EsentDensityInvalidException();
            case JET_err.TableNotEmpty:
                return new EsentTableNotEmptyException();
            case JET_err.InvalidTableId:
                return new EsentInvalidTableIdException();
            case JET_err.TooManyOpenTables:
                return new EsentTooManyOpenTablesException();
            case JET_err.IllegalOperation:
                return new EsentIllegalOperationException();
            case JET_err.TooManyOpenTablesAndCleanupTimedOut:
                return new EsentTooManyOpenTablesAndCleanupTimedOutException();
            case JET_err.ObjectDuplicate:
                return new EsentObjectDuplicateException();
            case JET_err.InvalidObject:
                return new EsentInvalidObjectException();
            case JET_err.CannotDeleteTempTable:
                return new EsentCannotDeleteTempTableException();
            case JET_err.CannotDeleteSystemTable:
                return new EsentCannotDeleteSystemTableException();
            case JET_err.CannotDeleteTemplateTable:
                return new EsentCannotDeleteTemplateTableException();
            case JET_err.ExclusiveTableLockRequired:
                return new EsentExclusiveTableLockRequiredException();
            case JET_err.FixedDDL:
                return new EsentFixedDDLException();
            case JET_err.FixedInheritedDDL:
                return new EsentFixedInheritedDDLException();
            case JET_err.CannotNestDDL:
                return new EsentCannotNestDDLException();
            case JET_err.DDLNotInheritable:
                return new EsentDDLNotInheritableException();
            case JET_err.InvalidSettings:
                return new EsentInvalidSettingsException();
            case JET_err.ClientRequestToStopJetService:
                return new EsentClientRequestToStopJetServiceException();
            case JET_err.CannotAddFixedVarColumnToDerivedTable:
                return new EsentCannotAddFixedVarColumnToDerivedTableException();
            case JET_err.IndexCantBuild:
                return new EsentIndexCantBuildException();
            case JET_err.IndexHasPrimary:
                return new EsentIndexHasPrimaryException();
            case JET_err.IndexDuplicate:
                return new EsentIndexDuplicateException();
            case JET_err.IndexNotFound:
                return new EsentIndexNotFoundException();
            case JET_err.IndexMustStay:
                return new EsentIndexMustStayException();
            case JET_err.IndexInvalidDef:
                return new EsentIndexInvalidDefException();
            case JET_err.InvalidCreateIndex:
                return new EsentInvalidCreateIndexException();
            case JET_err.TooManyOpenIndexes:
                return new EsentTooManyOpenIndexesException();
            case JET_err.MultiValuedIndexViolation:
                return new EsentMultiValuedIndexViolationException();
            case JET_err.IndexBuildCorrupted:
                return new EsentIndexBuildCorruptedException();
            case JET_err.PrimaryIndexCorrupted:
                return new EsentPrimaryIndexCorruptedException();
            case JET_err.SecondaryIndexCorrupted:
                return new EsentSecondaryIndexCorruptedException();
            case JET_err.InvalidIndexId:
                return new EsentInvalidIndexIdException();
            case JET_err.IndexTuplesSecondaryIndexOnly:
                return new EsentIndexTuplesSecondaryIndexOnlyException();
            case JET_err.IndexTuplesTooManyColumns:
                return new EsentIndexTuplesTooManyColumnsException();
            case JET_err.IndexTuplesNonUniqueOnly:
                return new EsentIndexTuplesNonUniqueOnlyException();
            case JET_err.IndexTuplesTextBinaryColumnsOnly:
                return new EsentIndexTuplesTextBinaryColumnsOnlyException();
            case JET_err.IndexTuplesVarSegMacNotAllowed:
                return new EsentIndexTuplesVarSegMacNotAllowedException();
            case JET_err.IndexTuplesInvalidLimits:
                return new EsentIndexTuplesInvalidLimitsException();
            case JET_err.IndexTuplesCannotRetrieveFromIndex:
                return new EsentIndexTuplesCannotRetrieveFromIndexException();
            case JET_err.IndexTuplesKeyTooSmall:
                return new EsentIndexTuplesKeyTooSmallException();
            case JET_err.InvalidLVChunkSize:
                return new EsentInvalidLVChunkSizeException();
            case JET_err.ColumnCannotBeEncrypted:
                return new EsentColumnCannotBeEncryptedException();
            case JET_err.CannotIndexOnEncryptedColumn:
                return new EsentCannotIndexOnEncryptedColumnException();
            case JET_err.ColumnLong:
                return new EsentColumnLongException();
            case JET_err.ColumnNoChunk:
                return new EsentColumnNoChunkException();
            case JET_err.ColumnDoesNotFit:
                return new EsentColumnDoesNotFitException();
            case JET_err.NullInvalid:
                return new EsentNullInvalidException();
            case JET_err.ColumnIndexed:
                return new EsentColumnIndexedException();
            case JET_err.ColumnTooBig:
                return new EsentColumnTooBigException();
            case JET_err.ColumnNotFound:
                return new EsentColumnNotFoundException();
            case JET_err.ColumnDuplicate:
                return new EsentColumnDuplicateException();
            case JET_err.MultiValuedColumnMustBeTagged:
                return new EsentMultiValuedColumnMustBeTaggedException();
            case JET_err.ColumnRedundant:
                return new EsentColumnRedundantException();
            case JET_err.InvalidColumnType:
                return new EsentInvalidColumnTypeException();
            case JET_err.TaggedNotNULL:
                return new EsentTaggedNotNULLException();
            case JET_err.NoCurrentIndex:
                return new EsentNoCurrentIndexException();
            case JET_err.KeyIsMade:
                return new EsentKeyIsMadeException();
            case JET_err.BadColumnId:
                return new EsentBadColumnIdException();
            case JET_err.BadItagSequence:
                return new EsentBadItagSequenceException();
            case JET_err.ColumnInRelationship:
                return new EsentColumnInRelationshipException();
            case JET_err.CannotBeTagged:
                return new EsentCannotBeTaggedException();
            case JET_err.DefaultValueTooBig:
                return new EsentDefaultValueTooBigException();
            case JET_err.MultiValuedDuplicate:
                return new EsentMultiValuedDuplicateException();
            case JET_err.LVCorrupted:
                return new EsentLVCorruptedException();
            case JET_err.MultiValuedDuplicateAfterTruncation:
                return new EsentMultiValuedDuplicateAfterTruncationException();
            case JET_err.DerivedColumnCorruption:
                return new EsentDerivedColumnCorruptionException();
            case JET_err.InvalidPlaceholderColumn:
                return new EsentInvalidPlaceholderColumnException();
            case JET_err.ColumnCannotBeCompressed:
                return new EsentColumnCannotBeCompressedException();
            case JET_err.ColumnNoEncryptionKey:
                return new EsentColumnNoEncryptionKeyException();
            case JET_err.RecordNotFound:
                return new EsentRecordNotFoundException();
            case JET_err.RecordNoCopy:
                return new EsentRecordNoCopyException();
            case JET_err.NoCurrentRecord:
                return new EsentNoCurrentRecordException();
            case JET_err.RecordPrimaryChanged:
                return new EsentRecordPrimaryChangedException();
            case JET_err.KeyDuplicate:
                return new EsentKeyDuplicateException();
            case JET_err.AlreadyPrepared:
                return new EsentAlreadyPreparedException();
            case JET_err.KeyNotMade:
                return new EsentKeyNotMadeException();
            case JET_err.UpdateNotPrepared:
                return new EsentUpdateNotPreparedException();
            case JET_err.DataHasChanged:
                return new EsentDataHasChangedException();
            case JET_err.LanguageNotSupported:
                return new EsentLanguageNotSupportedException();
            case JET_err.DecompressionFailed:
                return new EsentDecompressionFailedException();
            case JET_err.UpdateMustVersion:
                return new EsentUpdateMustVersionException();
            case JET_err.DecryptionFailed:
                return new EsentDecryptionFailedException();
            case JET_err.EncryptionBadItag:
                return new EsentEncryptionBadItagException();
            case JET_err.TooManySorts:
                return new EsentTooManySortsException();
            case JET_err.InvalidOnSort:
                return new EsentInvalidOnSortException();
            case JET_err.TempFileOpenError:
                return new EsentTempFileOpenErrorException();
            case JET_err.TooManyAttachedDatabases:
                return new EsentTooManyAttachedDatabasesException();
            case JET_err.DiskFull:
                return new EsentDiskFullException();
            case JET_err.PermissionDenied:
                return new EsentPermissionDeniedException();
            case JET_err.FileNotFound:
                return new EsentFileNotFoundException();
            case JET_err.FileInvalidType:
                return new EsentFileInvalidTypeException();
            case JET_err.FileAlreadyExists:
                return new EsentFileAlreadyExistsException();
            case JET_err.AfterInitialization:
                return new EsentAfterInitializationException();
            case JET_err.LogCorrupted:
                return new EsentLogCorruptedException();
            case JET_err.InvalidOperation:
                return new EsentInvalidOperationException();
            case JET_err.AccessDenied:
                return new EsentAccessDeniedException();
            case JET_err.TooManySplits:
                return new EsentTooManySplitsException();
            case JET_err.SessionSharingViolation:
                return new EsentSessionSharingViolationException();
            case JET_err.EntryPointNotFound:
                return new EsentEntryPointNotFoundException();
            case JET_err.SessionContextAlreadySet:
                return new EsentSessionContextAlreadySetException();
            case JET_err.SessionContextNotSetByThisThread:
                return new EsentSessionContextNotSetByThisThreadException();
            case JET_err.SessionInUse:
                return new EsentSessionInUseException();
            case JET_err.RecordFormatConversionFailed:
                return new EsentRecordFormatConversionFailedException();
            case JET_err.OneDatabasePerSession:
                return new EsentOneDatabasePerSessionException();
            case JET_err.RollbackError:
                return new EsentRollbackErrorException();
            case JET_err.FlushMapVersionUnsupported:
                return new EsentFlushMapVersionUnsupportedException();
            case JET_err.FlushMapDatabaseMismatch:
                return new EsentFlushMapDatabaseMismatchException();
            case JET_err.FlushMapUnrecoverable:
                return new EsentFlushMapUnrecoverableException();
            case JET_err.DatabaseAlreadyRunningMaintenance:
                return new EsentDatabaseAlreadyRunningMaintenanceException();
            case JET_err.CallbackFailed:
                return new EsentCallbackFailedException();
            case JET_err.CallbackNotResolved:
                return new EsentCallbackNotResolvedException();
            case JET_err.SpaceHintsInvalid:
                return new EsentSpaceHintsInvalidException();
            case JET_err.OSSnapshotInvalidSequence:
                return new EsentOSSnapshotInvalidSequenceException();
            case JET_err.OSSnapshotTimeOut:
                return new EsentOSSnapshotTimeOutException();
            case JET_err.OSSnapshotNotAllowed:
                return new EsentOSSnapshotNotAllowedException();
            case JET_err.OSSnapshotInvalidSnapId:
                return new EsentOSSnapshotInvalidSnapIdException();
            case JET_err.TooManyTestInjections:
                return new EsentTooManyTestInjectionsException();
            case JET_err.TestInjectionNotSupported:
                return new EsentTestInjectionNotSupportedException();
            case JET_err.InvalidLogDataSequence:
                return new EsentInvalidLogDataSequenceException();
            case JET_err.LSCallbackNotSpecified:
                return new EsentLSCallbackNotSpecifiedException();
            case JET_err.LSAlreadySet:
                return new EsentLSAlreadySetException();
            case JET_err.LSNotSet:
                return new EsentLSNotSetException();
            case JET_err.FileIOSparse:
                return new EsentFileIOSparseException();
            case JET_err.FileIOBeyondEOF:
                return new EsentFileIOBeyondEOFException();
            case JET_err.FileIOAbort:
                return new EsentFileIOAbortException();
            case JET_err.FileIORetry:
                return new EsentFileIORetryException();
            case JET_err.FileIOFail:
                return new EsentFileIOFailException();
            case JET_err.FileCompressed:
                return new EsentFileCompressedException();
            default:
                // This could be a new error introduced in a newer version of Esent. Try to look up the description.
                IntPtr errNum = new IntPtr((int)err);
                string description;
                int wrn = Api.Impl.JetGetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, JET_param.ErrorToString, ref errNum, out description, 1024);
                err = (JET_err)errNum.ToInt32();
                if ((int)JET_wrn.Success != wrn)
                {
                    description = "Unknown error";
                }
                
                return new EsentErrorException(description, err);
            }
        }
    }
}
