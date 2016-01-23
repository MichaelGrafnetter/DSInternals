// ---------------------------------------------------------------------------
// <copyright file="Instance.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using System;

    using Microsoft.Isam.Esent.Interop;

    using Miei = Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// An Instance represents the unit of recoverability for the ISAM.  It is
    /// used to manage the set of files that comprise the transaction logs and
    /// databases used by the ISAM.
    /// </summary>
    public class IsamInstance : IDisposable
    {
        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// The instance
        /// </summary>
        private JET_INSTANCE instance;

        /// <summary>
        /// The system parameters
        /// </summary>
        private IsamSystemParameters isamSystemParameters;

        /// <summary>
        /// The cleanup instance
        /// </summary>
        private bool cleanupInstance = false;

        /// <summary>
        /// The instance initialized
        /// </summary>
        private bool instanceInitialized = false;

        /// <summary>
        /// The cleanup temporary tables
        /// </summary>
        private bool cleanupTempTables = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The temporary table handle collection
        /// </summary>
        private TempTableHandleCollection tempTableHandleCollection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamInstance"/> class. 
        /// </summary>
        /// <param name="workingDirectory">
        /// The directory (relative or absolute) that will contain all the files managed by the ISAM.  The path must have a trailing backslash.
        /// </param>
        public IsamInstance(string workingDirectory)
        {
            this.Create(workingDirectory, workingDirectory, workingDirectory, "ese", string.Empty, false, 8192);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamInstance"/> class. 
        /// </summary>
        /// <param name="workingDirectory">
        /// The directory (relative or absolute) that will contain all the files managed by the ISAM.  The path must have a trailing backslash.
        /// </param>
        /// <param name="readOnly">
        /// Set to true when this instance will only be used to access read only databases
        /// </param>
        public IsamInstance(string workingDirectory, bool readOnly)
        {
            // if this is a RO instance then we will create a unique temp db
            // name so that it is easy to use the same workingDirectory for
            // all instances that wish to use the same db
            string tempDb = workingDirectory + (readOnly ? Guid.NewGuid().ToString() + ".tmp" : string.Empty);

            this.Create(workingDirectory, workingDirectory, tempDb, "ese", string.Empty, readOnly, 8192);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamInstance"/> class. 
        /// </summary>
        /// <param name="checkpointFileDirectoryPath">
        /// The directory (relative or absolute) that will contain the checkpoint file managed by the ISAM.  The path must have a trailing backslash.
        /// </param>
        /// <param name="logfileDirectoryPath">
        /// The directory (relative or absolute) that will contain the transaction log files managed by the ISAM.  The path must have a trailing backslash.
        /// </param>
        /// <param name="temporaryDatabaseFileDirectoryPath">
        /// The file or directory (relative or absolute) that will contain the temporary database managed by the ISAM.  If the path has a trailing backslash, it will be assumed to be a directory and a custom name for the temporary database will be created.  If the path does not have a trailing backslash, then it will be assumed to be a file and the temporary database will be stored in that file.
        /// </param>
        /// <param name="baseName">
        /// A three character prefix that will be used to name ISAM files.  This prefix can be used to make the ISAM's file names unique so that they may share directories with other instances.
        /// </param>
        /// <param name="eventSource">
        /// A short name that will be used to identify this instance when the ISAM emits diagnostic data.
        /// </param>
        public IsamInstance(
            string checkpointFileDirectoryPath,
            string logfileDirectoryPath,
            string temporaryDatabaseFileDirectoryPath,
            string baseName,
            string eventSource)
        {
            this.Create(
                checkpointFileDirectoryPath,
                logfileDirectoryPath,
                temporaryDatabaseFileDirectoryPath,
                baseName,
                eventSource,
                false,
                8192);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamInstance"/> class. 
        /// </summary>
        /// <param name="checkpointFileDirectoryPath">
        /// The directory (relative or absolute) that will contain the checkpoint file managed by the ISAM.  The path must have a trailing backslash.
        /// </param>
        /// <param name="logfileDirectoryPath">
        /// The directory (relative or absolute) that will contain the transaction log files managed by the ISAM.  The path must have a trailing backslash.
        /// </param>
        /// <param name="temporaryDatabaseFileDirectoryPath">
        /// The file or directory (relative or absolute) that will contain the temporary database managed by the ISAM.  If the path has a trailing backslash, it will be assumed to be a directory and a custom name for the temporary database will be created.  If the path does not have a trailing backslash, then it will be assumed to be a file and the temporary database will be stored in that file.
        /// </param>
        /// <param name="baseName">
        /// A three character prefix that will be used to name ISAM files.  This prefix can be used to make the ISAM's file names unique so that they may share directories with other instances.
        /// </param>
        /// <param name="eventSource">
        /// A short name that will be used to identify this instance when the ISAM emits diagnostic data.
        /// </param>
        /// <param name="readOnly">
        /// Set to true when this instance will only be used to access read only databases
        /// </param>
        /// <param name="pageSize">
        /// Set to the page size that will be used by all databases managed by the instance.
        /// </param>
        public IsamInstance(
            string checkpointFileDirectoryPath,
            string logfileDirectoryPath,
            string temporaryDatabaseFileDirectoryPath,
            string baseName,
            string eventSource,
            bool readOnly,
            int pageSize)
        {
            this.Create(
                checkpointFileDirectoryPath,
                logfileDirectoryPath,
                temporaryDatabaseFileDirectoryPath,
                baseName,
                eventSource,
                readOnly,
                pageSize);
        }

        /// <summary>
        /// Finalizes an instance of the IsamInstance class
        /// </summary>
        ~IsamInstance()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a <see cref="SystemParameters"/> object that provides access to a boatload of parameters that can be used to
        /// tweak various aspects of the ISAM's behavior or performance for
        /// this instance.
        /// </summary>
        public IsamSystemParameters IsamSystemParameters
        {
            get
            {
                this.CheckDisposed();
                return this.isamSystemParameters;
            }
        }

        /// <summary>
        /// Gets the inst.
        /// </summary>
        /// <value>
        /// The inst.
        /// </value>
        internal JET_INSTANCE Inst
        {
            get
            {
                return this.instance;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        internal bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disposed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disposed]; otherwise, <c>false</c>.
        /// </value>
        internal bool Disposed
        {
            get
            {
                return this.disposed;
            }

            set
            {
                this.disposed = value;
            }
        }

        /// <summary>
        /// Gets the temporary table handles.
        /// </summary>
        /// <value>
        /// The temporary table handles.
        /// </value>
        internal TempTableHandleCollection TempTableHandles
        {
            get
            {
                this.CheckDisposed();
                return this.tempTableHandleCollection;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                this.Dispose(true);
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Creates a session
        /// </summary>
        /// <returns>a session associated with this instance</returns>
        public IsamSession CreateSession()
        {
            lock (this)
            {
                this.CheckDisposed();

                if (!this.instanceInitialized)
                {
                    try
                    {
                        // FUTURE-2015/06/21-BrettSh - Someone should explain to me, why we have JetInit in Database.cs, in here
                        // and in PersistentDictionary.cs.  These different APIs are supposed to be consolidating.  Anyways, either
                        // someone will DatabaseConfig enable this and then this should consume DatabaseRecoveryFlags from there, or
                        // this should be deprecated and we'll just use Database.cs going forward (which I think is plan).
                        Api.JetInit(ref this.instance);
                    }
                    catch (EsentErrorException)
                    {
                        // if JETInit throws then we must not call JETTerm
                        // and we must not use the instance anymore
                        this.cleanupInstance = false;
                        this.instance = new JET_INSTANCE();
                        throw;
                    }

                    this.instanceInitialized = true;
                }

                return new IsamSession(this);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (!this.Disposed)
                {
                    if (this.cleanupTempTables)
                    {
                        // dispose all open temp tables for the instance before
                        // it is shutdown
                        //
                        // NOTE:  this is a hack to work around problems in ESE/ESENT
                        // that we cannot fix because we must work downlevel
                        foreach (TempTableHandle tempTableHandle in this.TempTableHandles)
                        {
                            Api.JetCloseTable(tempTableHandle.Sesid, tempTableHandle.Handle);
                        }

                        this.cleanupTempTables = false;
                    }

                    if (this.cleanupInstance)
                    {
                        // FUTURE-2015/06/21-BrettSh - Similar issue to above date, should consume DatabaseStopFlags.
                        Api.JetTerm2(this.instance, TermGrbit.Complete);
                        this.cleanupInstance = false;
                        this.instanceInitialized = false;
                    }

                    this.Disposed = true;
                }
            }
        }

        /// <summary>
        /// Creates the specified checkpoint file directory path.
        /// </summary>
        /// <param name="checkpointFileDirectoryPath">The checkpoint file directory path.</param>
        /// <param name="logfileDirectoryPath">The logfile directory path.</param>
        /// <param name="temporaryDatabaseFileDirectoryPath">The temporary database file directory path.</param>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="eventSource">The event source.</param>
        /// <param name="isReadOnly">if set to <c>true</c> [is read only].</param>
        /// <param name="pageSize">Size of the page.</param>
        private void Create(
            string checkpointFileDirectoryPath,
            string logfileDirectoryPath,
            string temporaryDatabaseFileDirectoryPath,
            string baseName,
            string eventSource,
            bool isReadOnly,
            int pageSize)
        {
            lock (this)
            {
                try
                {
                    Miei.SystemParameters.Configuration = 0;
                    Miei.SystemParameters.DatabasePageSize = pageSize;
                }
                catch (EsentAlreadyInitializedException)
                {
                }

                this.readOnly = isReadOnly;
                Api.JetCreateInstance(out this.instance, eventSource);
                this.cleanupInstance = true;
                this.instanceInitialized = false;
                this.isamSystemParameters = new IsamSystemParameters(this);
                this.tempTableHandleCollection = new TempTableHandleCollection(true);
                this.cleanupTempTables = true;

                this.isamSystemParameters.CreatePathIfNotExist = true;
                this.isamSystemParameters.PageTempDBMin = 14;
                this.isamSystemParameters.LogFileSize = 64;
                this.isamSystemParameters.CircularLog = true;
                this.isamSystemParameters.SystemPath = checkpointFileDirectoryPath;
                this.isamSystemParameters.LogFilePath = logfileDirectoryPath;
                this.isamSystemParameters.TempPath = temporaryDatabaseFileDirectoryPath;
                this.isamSystemParameters.BaseName = baseName;
                this.isamSystemParameters.EventSource = eventSource;
                this.isamSystemParameters.Recovery = this.readOnly ? "off" : "on";
            }
        }

        /// <summary>
        /// Checks the disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// Thrown when the object is already disposed.
        /// </exception>
        private void CheckDisposed()
        {
            lock (this)
            {
                if (this.Disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
            }
        }
    }
}
