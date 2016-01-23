// ---------------------------------------------------------------------------
// <copyright file="TempTableHandle.cs" company="Microsoft">
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

    /// <summary>
    /// A wrapper class around Temp Tables.
    /// </summary>
    internal class TempTableHandle
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        private readonly Guid guid;

        /// <summary>
        /// The name
        /// </summary>
        private readonly string name = null;

        /// <summary>
        /// The sesid
        /// </summary>
        private readonly JET_SESID sesid;

        /// <summary>
        /// The tableid
        /// </summary>
        private readonly JET_TABLEID tableid;

        /// <summary>
        /// The in insert mode
        /// </summary>
        private bool inInsertMode = false;

        /// <summary>
        /// The cursor count
        /// </summary>
        private int cursorCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TempTableHandle"/> class.
        /// </summary>
        internal TempTableHandle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TempTableHandle"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sesid">The sesid.</param>
        /// <param name="tableid">The tableid.</param>
        /// <param name="inInsertMode">if set to <c>true</c> [in insert mode].</param>
        internal TempTableHandle(string name, JET_SESID sesid, JET_TABLEID tableid, bool inInsertMode)
        {
            this.guid = Guid.NewGuid();
            this.name = name;
            this.sesid = sesid;
            this.tableid = tableid;
            this.inInsertMode = inInsertMode;
            this.cursorCount = 0;
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the <see cref="JET_SESID"/> used to open and access the table.
        /// </summary>
        public JET_SESID Sesid
        {
            get
            {
                return this.sesid;
            }
        }

        /// <summary>
        /// Gets the handle of the table.
        /// </summary>
        public JET_TABLEID Handle
        {
            get
            {
                return this.tableid;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the table handle is in insert mode.
        /// </summary>
        public bool InInsertMode
        {
            get
            {
                return this.inInsertMode;
            }

            set
            {
                this.inInsertMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the count of cursors open on this table.
        /// </summary>
        public int CursorCount
        {
            get
            {
                return this.cursorCount;
            }

            set
            {
                this.cursorCount = value;
            }
        }

        /// <summary>
        /// Gets the GUID of the table.
        /// </summary>
        internal Guid Guid
        {
            get
            {
                return this.guid;
            }
        }
    }
}
