// ---------------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Microsoft">
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

    /// <summary>
    /// A IsamTransaction object represents a single save point of a transaction
    /// that is begun on a Session object.  This object is not required to
    /// begin, commit, or abort a transaction as this can be done directly
    /// on the Session object.  However, this object is very useful for making
    /// convenient code constructs involving transactions with the "using"
    /// keyword in C# like this:
    /// <code>
    ///     using( IsamTransaction t = new IsamTransaction( session ) )
    ///     {
    ///         /* do some work */
    ///
    ///         /* save the work */
    ///         t.Commit();
    ///     }
    /// </code>
    /// </summary>
    public class IsamTransaction : IDisposable
    {
        /// <summary>
        /// The session
        /// </summary>
        private readonly IsamSession isamSession;

        /// <summary>
        /// The transaction level
        /// </summary>
        private readonly long transactionLevel;

        /// <summary>
        /// The transaction level identifier
        /// </summary>
        private readonly long transactionLevelID;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamTransaction"/> class. 
        /// Begin a transaction on the given session.
        /// </summary>
        /// <param name="isamSession">
        /// The session we will use for the transaction.
        /// </param>
        /// <remarks>
        /// If this transaction is not committed before this object is disposed
        /// then the transaction will automatically be aborted.
        /// </remarks>
        public IsamTransaction(IsamSession isamSession)
        {
            lock (isamSession)
            {
                this.isamSession = isamSession;
                this.isamSession.BeginTransaction();
                this.transactionLevel = isamSession.TransactionLevel;
                this.transactionLevelID = isamSession.TransactionLevelID(isamSession.TransactionLevel);
                this.cleanup = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamTransaction"/> class. 
        /// Joins the current transaction on the given session.  If the session
        /// is not currently in a transaction then a new transaction will be
        /// begun.
        /// </summary>
        /// <param name="isamSession">
        /// The session we will use for the transaction.
        /// </param>
        /// <param name="join">
        /// If true, the current transaction on the session will be joined.  If false or if there is no current transaction then a new transaction will be begun.
        /// </param>
        /// <remarks>
        /// If an existing transaction is joined, then committing or aborting
        /// this transaction will have no effect on the joined transaction.  If
        /// a new transaction was begun then these actions will work normally.
        /// <para>
        /// If this transaction is not committed before this object is disposed
        /// then the transaction will automatically be aborted.
        /// </para>
        /// </remarks>
        public IsamTransaction(IsamSession isamSession, bool join)
        {
            lock (isamSession)
            {
                this.isamSession = isamSession;
                if (!join || isamSession.TransactionLevel == 0)
                {
                    this.isamSession.BeginTransaction();
                    this.transactionLevel = isamSession.TransactionLevel;
                    this.transactionLevelID = isamSession.TransactionLevelID(isamSession.TransactionLevel);
                    this.cleanup = true;
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the IsamTransaction class
        /// </summary>
        ~IsamTransaction()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the save point (level) for this transaction.
        /// </summary>
        public long TransactionLevel
        {
            get
            {
                this.CheckDisposed();
                return this.transactionLevel;
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
                return this.disposed || this.isamSession.Disposed || (this.cleanup && this.isamSession.TransactionLevelID(this.transactionLevel) != this.transactionLevelID);
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
        /// Commits the save point (level) represented by this transaction on
        /// this session.  All changes made to the database for this save point
        /// (level) will be kept.
        /// </summary>
        /// <remarks>
        /// Changes made to the database will become permanent if and only if
        /// those changes are committed to save point (level) zero.
        /// <para>
        /// A commit to save point (level) zero is guaranteed to be persisted
        /// to the database upon completion of this method.
        /// </para>
        /// <para>
        /// The transaction object will be disposed as a side effect of this
        /// call.
        /// </para>
        /// </remarks>
        public void Commit()
        {
            this.Commit(true);
        }

        /// <summary>
        /// Commits the save point (level) represented by this transaction on
        /// this session.  All changes made to the database for this save point
        /// (level) will be kept.
        /// </summary>
        /// <param name="durableCommit">When true, a commit to save point (level) zero is guaranteed to be persisted to the database upon completion of this method.</param>
        /// <remarks>
        /// Changes made to the database will become permanent if and only if
        /// those changes are committed to save point (level) zero.
        /// <para>
        /// A commit to save point (level) zero is guaranteed to be persisted
        /// to the database upon completion of this method only if
        /// durableCommit is true.  If durableCommit is false then the changes
        /// will only be persisted to the database if their transaction log
        /// entries happen to be written to disk before a crash or if the
        /// database is shut down cleanly.
        /// </para>
        /// <para>
        /// The transaction object will be disposed as a side effect of this
        /// call.
        /// </para>
        /// </remarks>
        public void Commit(bool durableCommit)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                if (this.cleanup)
                {
                    this.isamSession.CommitTransaction(durableCommit);
                    this.cleanup = false;
                }

                this.Dispose();
            }
        }

        /// <summary>
        /// Aborts the save point (level) represented by this transaction on
        /// this session.  All changes made to the database for this save point
        /// (level) will be discarded.
        /// </summary>
        /// <remarks>
        /// The transaction object will be disposed as a side effect of this
        /// call.
        /// </remarks>
        public void Rollback()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                this.Dispose();
            }
        }

        /// <summary>
        /// Aborts the save point (level) represented by this transaction on
        /// this session.  All changes made to the database for this save point
        /// (level) will be discarded.
        /// </summary>
        /// <remarks>
        /// The transaction object will be disposed as a side effect of this
        /// call.
        /// <para>
        /// IsamTransaction.Abort is an alias for <see cref="IsamTransaction.Rollback"/>.
        /// </para>
        /// </remarks>
        public void Abort()
        {
            this.Rollback();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Checks whether this object is disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">If the object has already been disposed.</exception>
        internal void CheckDisposed()
        {
            if (this.Disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this.isamSession)
            {
                if (!this.Disposed)
                {
                    if (this.cleanup)
                    {
                        this.isamSession.RollbackTransaction();
                        this.cleanup = false;
                    }

                    this.Disposed = true;
                }
            }
        }
    }
}
