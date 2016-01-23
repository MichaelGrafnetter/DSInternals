// ---------------------------------------------------------------------------
// <copyright file="Session.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
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

    /// <summary>
    /// A Session is the transactional context for the ISAM.  It can be used to
    /// begin, commit, or abort transactions that influence when changes made
    /// to databases are kept or discarded.
    /// <para>
    /// The session object currently also controls which databases can be
    /// accessed by the ISAM.
    /// </para>
    /// </summary>
    public class IsamSession : IDisposable
    {
        /// <summary>
        /// The instance
        /// </summary>
        private readonly IsamInstance isamInstance;

        /// <summary>
        /// The sesid
        /// </summary>
        private readonly JET_SESID sesid;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The temporary database
        /// </summary>
        private TemporaryDatabase temporaryDatabase = null;

        /// <summary>
        /// The transaction level
        /// </summary>
        private long transactionLevel = 0;

        /// <summary>
        /// The transaction level identifier
        /// </summary>
        private long[] transactionLevelID = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamSession"/> class.
        /// </summary>
        /// <param name="isamInstance">The instance.</param>
        internal IsamSession(IsamInstance isamInstance)
        {
            lock (isamInstance)
            {
                this.isamInstance = isamInstance;
                Api.JetBeginSession(isamInstance.Inst, out this.sesid, null, null);
                this.cleanup = true;
                this.transactionLevelID = new long[7]; // JET only supports 7 levels
            }
        }

        /// <summary>
        /// Finalizes an instance of the IsamSession class.
        /// </summary>
        ~IsamSession()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the instance that created this session.
        /// </summary>
        public IsamInstance IsamInstance
        {
            get
            {
                return this.isamInstance;
            }
        }

        /// <summary>
        /// Gets the ID of the session's current transaction.
        /// </summary>
        /// <remarks>
        /// The transaction ID is incremented every time the session's current
        /// transaction save point (level) reaches zero such that the session
        /// is no longer considered to be in a transaction.
        /// </remarks>
        public long TransactionID
        {
            get
            {
                this.CheckDisposed();
                return this.transactionLevelID[0];
            }
        }

        /// <summary>
        /// Gets the save point (level) of the session's current transaction.
        /// </summary>
        /// <remarks>
        /// Every time a new transaction is begun, the save point (level) of
        /// the session's current transaction is increased.  Every time a
        /// transaction is successfully committed or aborted, the save point
        /// (level) of the session's current transaction is decreased.  If the
        /// save point (level) of the session's current transaction is zero
        /// then the session is not considered to be in a transaction.
        /// However, individual operations performed using the session will
        /// still be in a transaction.
        /// </remarks>
        public long TransactionLevel
        {
            get
            {
                this.CheckDisposed();
                return this.transactionLevel;
            }
        }

        /// <summary>
        /// Gets the sesid.
        /// </summary>
        /// <value>
        /// The sesid.
        /// </value>
        internal JET_SESID Sesid
        {
            get
            {
                return this.sesid;
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
                return this.disposed || this.isamInstance.Disposed;
            }

            set
            {
                this.disposed = value;
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
        /// Creates a new database at the specified location
        /// </summary>
        /// <param name="databaseName">The file name (relative or absolute) at which the database will be created</param>
        /// <remarks>
        /// The new database will automatically be attached to the instance.
        /// See Session.AttachDatabase for more information.
        /// </remarks>
        public void CreateDatabase(string databaseName)
        {
            lock (this)
            {
                this.CheckDisposed();

                JET_DBID dbid;
                Api.JetCreateDatabase(this.Sesid, databaseName, null, out dbid, CreateDatabaseGrbit.None);
                Api.JetCloseDatabase(this.Sesid, dbid, CloseDatabaseGrbit.None);
            }
        }

        /// <summary>
        /// Attaches an existing database at the specified location
        /// </summary>
        /// <param name="databaseName">The file name (relative or absolute) at which the database will be attached</param>
        /// <remarks>
        /// Attaching a database to the instance enables that database to be
        /// opened for access.  When a database is attached, its file is
        /// opened and so must be available to be locked as required.  The
        /// file will be held open until the database is detached or until the
        /// instance is disposed.
        /// </remarks>
        public void AttachDatabase(string databaseName)
        {
            lock (this)
            {
                this.CheckDisposed();

                AttachDatabaseGrbit grbit = AttachDatabaseGrbit.None;

                if (this.isamInstance.ReadOnly)
                {
                    grbit |= AttachDatabaseGrbit.ReadOnly;
                }

                Api.JetAttachDatabase(this.Sesid, databaseName, grbit);
            }
        }

        /// <summary>
        /// Detaches an attached database at the specified location
        /// </summary>
        /// <param name="databaseName">The file name (relative or absolute) at which the database will be detached</param>
        /// <remarks>
        /// Detaching a database from the instance will close its file and
        /// will make it no longer possible to open that database.
        /// </remarks>
        public void DetachDatabase(string databaseName)
        {
            lock (this)
            {
                this.CheckDisposed();

                Api.JetDetachDatabase(this.Sesid, databaseName);
            }
        }

        /// <summary>
        /// Determines if there is a database at the specified location
        /// </summary>
        /// <param name="databaseName">The file name (relative or absolute) at which the database may exist</param>
        /// <returns>true if the database exists and is a valid database file, false otherwise</returns>
        public bool Exists(string databaseName)
        {
            lock (this)
            {
                this.CheckDisposed();

                try
                {
                    AttachDatabaseGrbit grbit = AttachDatabaseGrbit.None;

                    if (this.isamInstance.ReadOnly)
                    {
                        grbit |= AttachDatabaseGrbit.ReadOnly;
                    }

                    Api.JetAttachDatabase(this.Sesid, databaseName, grbit);

                    try
                    {
                        Api.JetDetachDatabase(this.Sesid, databaseName);
                    }
                    catch (EsentDatabaseInUseException)
                    {
                    }

                    return true;
                }
                catch (EsentFileNotFoundException)
                {
                    return false;
                }
                catch (EsentDatabaseInvalidPathException)
                {
                    return false;
                }
                catch (EsentErrorException)
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Opens the database.
        /// </summary>
        /// <param name="databaseName">The file name (relative or absolute) at which the database will be opened</param>
        /// <returns>
        /// a Database object representing the database for this session
        /// </returns>
        /// <remarks>
        /// A database must first be attached (or created) before it can be
        /// opened successfully.
        /// </remarks>
        public IsamDatabase OpenDatabase(string databaseName)
        {
            lock (this)
            {
                this.CheckDisposed();

                return new IsamDatabase(this, databaseName);
            }
        }

        /// <summary>
        /// Opens the temporary database.
        /// </summary>
        /// <returns>
        /// A TemporaryDatabase object representing the temporary database for this session
        /// </returns>
        /// <remarks>
        /// Only one temporary database is supported per instance.
        /// </remarks>
        public TemporaryDatabase OpenTemporaryDatabase()
        {
            lock (this)
            {
                this.CheckDisposed();

                if (this.temporaryDatabase == null)
                {
                    this.temporaryDatabase = new TemporaryDatabase(this);
                }

                return this.temporaryDatabase;
            }
        }

        /// <summary>
        /// Begins a new save point (level) for the current transaction on this
        /// session.  Any changes made to the database for this save point
        /// (level) may later be kept or discarded by committing or aborting the
        /// save point (level).
        /// </summary>
        /// <remarks>
        /// Currently, there is a limit to how many save points (levels) are
        /// supported by the ISAM.  Approximately seven save points (levels)
        /// are supported.  Some ISAM functions also use some of these so the
        /// effective limit will vary with circumstance.
        /// </remarks>
        public void BeginTransaction()
        {
            lock (this)
            {
                this.CheckDisposed();

                Api.JetBeginTransaction(this.Sesid);
                this.transactionLevel++;
            }
        }

        /// <summary>
        /// Commits the current save point (level) of the current transaction
        /// on this session.  All changes made to the database for this save
        /// point (level) will be kept.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is illegal to call this method when the session is not currently
        /// in a transaction.  Use Session.TransactionLevel to determine the
        /// current transaction state of a session.
        /// </para>
        /// <para>
        /// Changes made to the database will become permanent if and only if
        /// those changes are committed to save point (level) zero.
        /// </para>
        /// <para>
        /// A commit to save point (level) zero is guaranteed to be persisted
        /// to the database upon completion of this method.
        /// </para>
        /// </remarks>
        public void CommitTransaction()
        {
            this.CommitTransaction(true);
        }

        /// <summary>
        /// Commits the current save point (level) of the current transaction
        /// on this session.  All changes made to the database for this save
        /// point (level) will be kept.
        /// </summary>
        /// <param name="durableCommit">
        /// When true, a commit to save point (level) zero is guaranteed to be
        /// persisted to the database upon completion of this method.
        /// </param>
        /// <remarks>
        /// <para>
        /// It is illegal to call this method when the session is not currently
        /// in a transaction.  Use Session.TransactionLevel to determine the
        /// current transaction state of a session.
        /// </para>
        /// <para>
        /// A commit to save point (level) zero is guaranteed to be persisted
        /// to the database upon completion of this method only if
        /// durableCommit is true.  If durableCommit is false then the changes
        /// will only be persisted to the database if their transaction log
        /// entries happen to be written to disk before a crash or if the
        /// database is shut down cleanly.
        /// </para>
        /// </remarks>
        public void CommitTransaction(bool durableCommit)
        {
            lock (this)
            {
                this.CheckDisposed();

                CommitTransactionGrbit grbit = CommitTransactionGrbit.None;
                if (!durableCommit)
                {
                    grbit |= CommitTransactionGrbit.LazyFlush;
                }

                Api.JetCommitTransaction(this.Sesid, grbit);
                this.transactionLevelID[--this.transactionLevel]++;
            }
        }

        /// <summary>
        /// Aborts the current save point (level) of the current transaction on
        /// this session.  All changes made to the database for this save point
        /// (level) will be discarded.
        /// </summary>
        /// <remarks>
        /// It is illegal to call this method when the session is not currently
        /// in a transaction.  Use Session.TransactionLevel to determine the
        /// current transaction state of a session.
        /// </remarks>
        public void RollbackTransaction()
        {
            lock (this)
            {
                this.CheckDisposed();

                Api.JetRollback(this.Sesid, RollbackTransactionGrbit.None);
                this.transactionLevelID[--this.transactionLevel]++;
            }
        }

        /// <summary>
        /// Aborts the current save point (level) of the current transaction on
        /// this session.  All changes made to the database for this save point
        /// (level) will be discarded.
        /// </summary>
        /// <remarks>
        /// It is illegal to call this method when the session is not currently
        /// in a transaction.  Use Session.TransactionLevel to determine the
        /// current transaction state of a session.
        /// <para>
        /// Session.AbortTransaction is an alias for
        /// <see cref="IsamSession.RollbackTransaction"/>.
        /// </para>
        /// </remarks>
        public void AbortTransaction()
        {
            this.RollbackTransaction();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>Gets the Transaction ID at the specifed Transaction Level.
        /// </summary>
        /// <param name="level">The Transaction Level.</param>
        /// <returns>The Transaction ID at the specifed Transaction Level.</returns>
        internal long TransactionLevelID(long level)
        {
            return this.transactionLevelID[level - 1];
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
                    if (this.cleanup)
                    {
                        if (this.temporaryDatabase != null)
                        {
                            this.temporaryDatabase.Dispose();
                        }

                        Api.JetEndSession(this.sesid, EndSessionGrbit.None);
                        this.cleanup = false;
                    }

                    this.Disposed = true;
                }
            }
        }

        /// <summary>
        /// Checks whether this object is disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">If the object has already been disposed.</exception>
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