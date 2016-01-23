//-----------------------------------------------------------------------
// <copyright file="EsentErrorException.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for ESENT error exceptions.
    /// </summary>
    [Serializable]
    public class EsentErrorException : EsentException
    {
        /// <summary>
        /// The error code that was encountered.
        /// </summary>
        private JET_err errorCode;

        /// <summary>
        /// Initializes a new instance of the EsentErrorException class.
        /// </summary>
        /// <param name="message">The description of the error.</param>
        /// <param name="err">The error code of the exception.</param>
        internal EsentErrorException(string message, JET_err err) : base(message)
        {
            this.errorCode = err;
        }

        /// <summary>
        /// Initializes a new instance of the EsentErrorException class. This constructor
        /// is used to deserialize a serialized exception.
        /// </summary>
        /// <param name="info">The data needed to deserialize the object.</param>
        /// <param name="context">The deserialization context.</param>
        protected EsentErrorException(SerializationInfo info, StreamingContext context) :
                base(info, context)
        {
            this.errorCode = (JET_err)info.GetInt32("errorCode");
        }

        /// <summary>
        /// Gets the underlying Esent error for this exception.
        /// </summary>
        public JET_err Error
        {
            get
            {
                return this.errorCode;
            }
        }

#if !MANAGEDESENT_ON_CORECLR // Serialization does not work in Core CLR.
        /// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic). </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null)
            {
                info.AddValue("errorCode", this.errorCode, typeof(int));
            }
        }
#endif
    }
}
