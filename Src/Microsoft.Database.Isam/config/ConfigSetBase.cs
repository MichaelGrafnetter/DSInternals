//-----------------------------------------------------------------------
// <copyright file="ConfigSetBase.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Database.Isam.Config
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A delegate type that is used by the config classes to change the parameter get behaviour when the config set is alive (is associated with a container).
    /// </summary>
    /// <param name="key">The parameter id.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>True if the parameter's value was read and returned. False otherwise.</returns>
    internal delegate bool TryGetParamDelegate(int key, out object value);

    /// <summary>
    /// Base class for a config set.
    /// </summary>
    public abstract class ConfigSetBase : IConfigSet
    {
        /// <summary>
        /// Initializes a new instance of the ConfigSetBase class.
        /// </summary>
        protected ConfigSetBase()
        {
            this.ParamStore = new Dictionary<int, object>();
        }

        /// <summary>
        /// Gets or sets the dictionary containing all config parameters.
        /// </summary>
        internal Dictionary<int, object> ParamStore { get; set; }

        /// <summary>
        /// Gets or sets the delegate used by config sets to update parameters.
        /// </summary>
        internal Action<int, object> SetParamDelegate { get; set; }

        /// <summary>
        /// Gets or sets the delegate used by config sets to get parameter values.
        /// </summary>
        internal TryGetParamDelegate GetParamDelegate { get; set; }

        /// <summary>
        /// Gets a particular config parameter's value.
        /// </summary>
        /// <param name="key">The parameter to get.</param>
        /// <returns>The requested parameter's value.</returns>
        public object this[int key]
        {
            get
            {
                return this.GetParam<object>(key);
            }
        }

        /// <summary>
        /// Gets a particular config parameter's value.
        /// </summary>
        /// <param name="key">The parameter to get.</param>
        /// <param name="value">The requested parameter's value.</param>
        /// <returns>true if the value was found, false otherwise.</returns>
        public bool TryGetValue(int key, out object value)
        {
            if (this.GetParamDelegate == null)
            {
                return this.ParamStore.TryGetValue(key, out value);
            }
            else
            {
                return this.GetParamDelegate(key, out value);
            }
        }

        /// <summary>
        /// Merges two config sets into one and throws an exception if there are any conflicts.
        /// </summary>
        /// <param name="source">The source config set to user.</param>
        public void Merge(IConfigSet source)
        {
            // Merges between different types don't yield anything
            if (this.GetType() == source.GetType())
            {
                this.MergeThrowOnConflicts(source);
            }
        }

        /// <summary>
        /// Merges two config sets into one.
        /// </summary>
        /// <param name="source">The source config set to user.</param>
        /// <param name="mergeRule">The merge rule to use.</param>
        public void Merge(IConfigSet source, MergeRules mergeRule)
        {
            // Merges between different types don't yield anything
            if (this.GetType() != source.GetType())
            {
                return;
            }

            if (mergeRule == MergeRules.ThrowOnConflicts)
            {
                this.MergeThrowOnConflicts(source);
                return;
            }

            foreach (var kvp in source)
            {
                object currValue;
                if (this.TryGetValue(kvp.Key, out currValue))
                {
                    if (!currValue.Equals(kvp.Value))
                    {
                        switch (mergeRule)
                        {
                            case MergeRules.Overwrite:
                                this.SetParam(kvp.Key, kvp.Value);
                                break;

                            case MergeRules.KeepExisting:
                                break;

                            default:
                                throw new ArgumentException("Unsupported merge rule!", "mergeRule");
                        }
                    }
                }
                else
                {
                    this.SetParam(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// Returns an enumerator over the config set.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<int, object>> GetEnumerator()
        {
            return this.ParamStore.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator over the config set.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.ParamStore).GetEnumerator();
        }

        /// <summary>
        /// Gets the value from a IDictionary.TryGetValue style method, or returns the default value for the give type.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="getParamMethod">Method to call for getting the value.</param>
        /// <param name="key">The key identifying the value.</param>
        /// <returns>The value or the default value of type T.</returns>
        internal static T GetValueOrDefault<T>(TryGetParamDelegate getParamMethod, int key)
        {
            object value;
            if (getParamMethod(key, out value))
            {
                return (T)value;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Merges two config sets into one and throws an exception if there are any conflicts.
        /// </summary>
        /// <param name="source">The source config set to user.</param>
        protected void MergeThrowOnConflicts(IConfigSet source)
        {
            var mergeable = new Dictionary<int, object>();
            foreach (var kvp in source)
            {
                object currValue;
                if (this.TryGetValue(kvp.Key, out currValue))
                {
                    if (!currValue.Equals(kvp.Value))
                    {
                        throw new ConfigSetMergeException(source, this, string.Format("Conflict on param {0}. Dest = {1}, Source = {2}", kvp.Key, currValue, kvp.Value));
                    }
                }
                else
                {
                    mergeable.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (var kvp in mergeable)
            {
                this.SetParam(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Helper method to get a config parameter.
        /// </summary>
        /// <typeparam name="T">The type of the config parameter.</typeparam>
        /// <param name="key">The config parameter.</param>
        /// <returns>The config parameter, or the default value of type T if the parameter isn't specifed.</returns>
        protected T GetParam<T>(int key)
        {
            if (this.GetParamDelegate == null)
            {
                return ConfigSetBase.GetValueOrDefault<T>(this.ParamStore.TryGetValue, key);
            }
            else
            {
                return ConfigSetBase.GetValueOrDefault<T>(this.GetParamDelegate, key);
            }
        }

        /// <summary>
        /// Helper method to set a config parameter.
        /// </summary>
        /// <param name="key">The config parameter.</param>
        /// <param name="value">Value of the config parameter.</param>
        protected void SetParam(int key, object value)
        {
            if (this.SetParamDelegate == null)
            {
                this.ParamStore[key] = value;
            }
            else
            {
                this.SetParamDelegate(key, value);
            }
        }
    }
}
