//-----------------------------------------------------------------------
// <copyright file="jet_errcat.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    /// <summary>
    /// The error category. The hierarchy is as follows:
    /// <para>
    /// JET_errcatError
    ///   |
    ///   |-- JET_errcatOperation
    ///   |    |-- JET_errcatFatal
    ///   |    |-- JET_errcatIO               //      bad IO issues, may or may not be transient.
    ///   |    |-- JET_errcatResource
    ///   |         |-- JET_errcatMemory      //      out of memory (all variants)
    ///   |         |-- JET_errcatQuota
    ///   |         |-- JET_errcatDisk        //      out of disk space (all variants)
    ///   |-- JET_errcatData
    ///   |     |-- JET_errcatCorruption
    ///   |     |-- JET_errcatInconsistent    //      typically caused by user Mishandling
    ///   |     |-- JET_errcatFragmentation
    ///   |-- JET_errcatApi
    ///         |-- JET_errcatUsage
    ///         |-- JET_errcatState
    ///         |-- JET_errcatObsolete
    /// </para>
    /// </summary>
    public enum JET_ERRCAT
    {
        /// <summary>
        /// Unknown category.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A generic category.
        /// </summary>
        Error,

        /// <summary>
        /// Errors that can usually happen any time due to uncontrollable 
        /// conditions.  Frequently temporary, but not always.
        /// <para>
        /// Recovery: Probably retry, or eventually inform the operator.
        /// </para>
        /// </summary>
        Operation,

        /// <summary>
        /// This sort error happens only when ESE encounters an error condition
        /// so grave, that we can not continue on in a safe (often transactional)
        /// way, and rather than corrupt data we throw errors of this category.
        /// <para>
        /// Recovery: Restart the instance or process. If the problem persists
        /// inform the operator.
        /// </para>
        /// </summary>
        Fatal,

        /// <summary>
        /// O errors come from the OS, and are out of ESE's control, this sort
        /// of error is possibly temporary, possibly not.
        /// <para>
        /// Recovery: Retry.  If not resolved, ask operator about disk issue.
        /// </para>
        /// </summary>
        IO,

        /// <summary>
        /// This is a category that indicates one of many potential out-of-resource
        /// conditions.
        /// </summary>
        Resource,

        /// <summary>
        /// Classic out of memory condition.
        /// <para>
        /// Recovery: Wait a while and retry, free up memory, or quit.
        /// </para>
        /// </summary>
        Memory,

        /// <summary>
        /// Certain "specialty" resources are in pools of a certain size, making
        /// it easier to detect leaks of these resources.
        /// <para>
        /// Recovery: Bug fix, generally the application should Assert() on these
        /// conditions so as to detect these issues during development.  However,
        /// in retail code, the best to hope for is to treat like Memory.
        /// </para>
        /// </summary>
        Quota,

        /// <summary>
        /// Out of disk conditions.
        /// <para>
        /// Recovery: Can retry later in the hope more space is available, or 
        /// ask the operator to free some disk space.
        /// </para>
        /// </summary>
        Disk,

        /// <summary>
        /// A data-related error.
        /// </summary>
        Data,

        /// <summary>
        /// My hard drive ate my homework. Classic corruption issues, frequently
        /// permanent without corrective action.
        /// <para>
        /// Recovery: Restore from backup, perhaps the ese utilities repair 
        /// operation (which only salvages what data is left / lossy) .Also
        /// in the case of recovery(JetInit) perhaps recovery can be performed
        /// by allowing data loss.
        /// </para>
        /// </summary>
        Corruption,

        /// <summary>
        /// This is similar to Corruption in that the database and/or log files
        /// are in a state that is inconsistent and unreconcilable with each 
        /// other. Often this is caused by application/administrator mishandling.
        /// <para>
        /// Recovery: Restore from backup, perhaps the ese utilities repair 
        /// operation (which only salvages what data is left / lossy). Also
        /// in the case of recovery(JetInit) perhaps recovery can be performed
        /// by allowing data loss.
        /// </para>
        /// </summary>
        Inconsistent,

        /// <summary>
        /// This is a class of errors where some persisted internal resource ran
        /// out.
        /// <para>
        /// Recovery: For database errors, offline defragmentation will rectify
        /// the problem, for the log files _first_ recover all attached databases
        /// to a clean shutdown, and then delete all the log files and checkpoint.
        /// </para>
        /// </summary>
        Fragmentation,

        /// <summary>
        /// A container for <see cref="Usage"/> and <see cref="State"/>.
        /// </summary>
        Api,

        /// <summary>
        /// Classic usage error, this means the client code did not pass correct
        /// arguments to the JET API.  This error will likely not go away with
        /// retry.
        /// <para>
        /// Recovery: Generally speaking client code should Assert() this class
        /// of errors is not returned, so issues can be caught during development.
        /// In retail, the app will probably have little option but to return
        /// the issue up to the operator.
        /// </para>
        /// </summary>
        Usage,

        /// <summary>
        /// This is the classification for different signals the API could return
        /// describe the state of the database, a classic case is JET_errRecordNotFound
        /// which can be returned by JetSeek() when the record you asked for
        /// was not found.
        /// <para>
        /// Recovery: Not really relevant, depends greatly on the API.
        /// </para>
        /// </summary>
        State,

        /// <summary>
        /// The error is recognized as a valid error, but is not expected to be
        /// returned by this version of the API.
        /// </summary>
        Obsolete,

        /// <summary>
        /// The maximum value for the enum. This should not be used.
        /// </summary>
        Max,
    }
}
