//-----------------------------------------------------------------------
// <copyright file="IConfigSet.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Database.Isam.Config
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Merge rules for merging config sets.
    /// </summary>
    public enum MergeRules
    {
        /// <summary>
        /// Throw an exception if a merge causes a parameter to be overwritten with a different value in the destination config set.
        /// </summary>
        ThrowOnConflicts = 0,

        /// <summary>
        /// Overwrite destination config set.
        /// </summary>
        Overwrite,

        /// <summary>
        /// Keep existing values of the destination config set intact while performing the merge.
        /// </summary>
        KeepExisting,
    }

    /// <summary>
    /// Interface definition for a config set.
    /// </summary>
    public interface IConfigSet : IEnumerable<KeyValuePair<int, object>>
    {
        /// <summary>
        /// Gets a particular config parameter's value.
        /// </summary>
        /// <param name="key">The parameter to get.</param>
        /// <returns>The requested parameter's value.</returns>
        object this[int key] { get; }

        /// <summary>
        /// Gets a particular config parameter's value.
        /// </summary>
        /// <param name="key">The parameter to get.</param>
        /// <param name="value">The requested parameter's value.</param>
        /// <returns>true if the value was found, false otherwise.</returns>
        bool TryGetValue(int key, out object value);

        /// <summary>
        /// Merges two config sets into one and throws an exception if there are any conflicts.
        /// </summary>
        /// <param name="source">The MergeSource config set to user.</param>
        void Merge(IConfigSet source);

        /// <summary>
        /// Merges two config sets into one.
        /// </summary>
        /// <param name="source">The MergeSource config set to user.</param>
        /// <param name="mergeRule">The merge rule to use.</param>
        void Merge(IConfigSet source, MergeRules mergeRule);
    }

    /// <summary>
    /// Represents exceptions thrown while merging two config sets.
    /// </summary>
    public sealed class ConfigSetMergeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ConfigSetMergeException class.
        /// </summary>
        /// <param name="mergeSource">The MergeSource config set.</param>
        /// <param name="mergeDest">The destination config set.</param>
        /// <param name="message">The exception message.</param>
        public ConfigSetMergeException(IConfigSet mergeSource, IConfigSet mergeDest, string message) : this(mergeSource, mergeDest, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConfigSetMergeException class.
        /// </summary>
        /// <param name="mergeSource">The MergeSource config set.</param>
        /// <param name="mergeDest">The destination config set.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public ConfigSetMergeException(IConfigSet mergeSource, IConfigSet mergeDest, string message, Exception inner) : base(message, inner)
        {
            this.MergeSource = mergeSource;
            this.MergeDest = mergeDest;
        }

        /// <summary>
        /// Gets the MergeSource config set used during the merge operation.
        /// </summary>
        public IConfigSet MergeSource { get; private set; }

        /// <summary>
        /// Gets the destination config set used during the merge operation.
        /// </summary>
        public IConfigSet MergeDest { get; private set; }
    }
}
