// ---------------------------------------------------------------------------
// <copyright file="TempTableHandleCollection.cs" company="Microsoft">
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
    using System.Collections;
    using System.Globalization;

    /// <summary>
    /// A class used to store the table handles in this database.
    /// </summary>
    internal class TempTableHandleCollection : DictionaryBase, IEnumerable
    {
        /// <summary>
        /// The is key unique identifier
        /// </summary>
        private readonly bool isKeyGuid = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TempTableHandleCollection"/> class.
        /// </summary>
        /// <param name="useGuid">if set to <c>true</c> [use unique identifier].</param>
        internal TempTableHandleCollection(bool useGuid)
        {
            this.isKeyGuid = useGuid;
        }

        /// <summary>
        /// Gets a value indicating whether [is key unique identifier].
        /// </summary>
        /// <value>
        /// <c>true</c> if [is key unique identifier]; otherwise, <c>false</c>.
        /// </value>
        public bool IsKeyGuid
        {
            get
            {
                return this.isKeyGuid;
            }
        }

        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        public ICollection Names
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TempTableHandle"/> for the specified table name
        /// </summary>
        /// <value>
        /// The <see cref="TempTableHandle"/>.
        /// </value>
        /// <param name="tableName">Name of the table to get or set.</param>
        /// <returns>A <see cref="TempTableHandle"/> for the specified table name.</returns>
        public TempTableHandle this[string tableName]
        {
            get
            {
                return (TempTableHandle)this.Dictionary[tableName.ToLower(CultureInfo.InvariantCulture)];
            }

            set
            {
                this.Dictionary[tableName.ToLower(CultureInfo.InvariantCulture)] = value;
            }
        }

        /// <summary>
        /// Fetches an enumerator containing all the table handles in this database.
        /// </summary>
        /// <returns>An enumerator containing all the table handles in this database.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public new TempTableHandleEnumerator GetEnumerator()
        {
            return new TempTableHandleEnumerator(this.Dictionary.GetEnumerator());
        }

        /// <summary>
        /// Fetches an enumerator containing all the tables in this database
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <remarks>
        /// This is the standard version that will work with other CLR
        /// languages.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        /// <summary>
        /// Adds the specified temporary table handle.
        /// </summary>
        /// <param name="tempTableHandle">The temporary table handle.</param>
        public void Add(TempTableHandle tempTableHandle)
        {
            if (this.isKeyGuid)
            {
                this.Dictionary.Add(tempTableHandle.Guid.ToString().ToLower(CultureInfo.InvariantCulture), tempTableHandle);
            }
            else
            {
                this.Dictionary.Add(tempTableHandle.Name.ToLower(CultureInfo.InvariantCulture), tempTableHandle);
            }
        }

        /// <summary>
        /// Determines if the database contains a table with the given name.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>Whether the database contains a table with the given name.</returns>
        public bool Contains(string tableName)
        {
            return this.Dictionary.Contains(tableName.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Removes the specified table name.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public void Remove(string tableName)
        {
            this.Dictionary.Remove(tableName.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Performs additional custom processes when validating the element with the specified key and value.
        /// </summary>
        /// <param name="key">The key of the element to validate.</param>
        /// <param name="value">The value of the element to validate.</param>
        /// <exception cref="System.ArgumentException">
        /// key must be of type System.String;key
        /// or
        /// value must be of type TempTableHandle;value
        /// or
        /// key must match value.Guid.ToString();key
        /// or
        /// key must match value.Name;key
        /// </exception>
        protected override void OnValidate(object key, object value)
        {
            if (!(key is string))
            {
                throw new ArgumentException("key must be of type System.String", "key");
            }

            if (!(value is TempTableHandle))
            {
                throw new ArgumentException("value must be of type TempTableHandle", "value");
            }

            if (this.isKeyGuid)
            {
                if (((string)key).ToLower(CultureInfo.InvariantCulture) != ((TempTableHandle)value).Guid.ToString().ToLower(CultureInfo.InvariantCulture))
                {
                    throw new ArgumentException("key must match value.Guid.ToString()", "key");
                }
            }
            else
            {
                if (((string)key).ToLower(CultureInfo.InvariantCulture) != ((TempTableHandle)value).Name.ToLower(CultureInfo.InvariantCulture))
                {
                    throw new ArgumentException("key must match value.Name", "key");
                }
            }
        }
    }
}
