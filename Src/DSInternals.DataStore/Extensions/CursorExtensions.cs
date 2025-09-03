﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using DSInternals.Common;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Schema;
using Microsoft.Database.Isam;
using Microsoft.Isam.Esent.Interop;

namespace DSInternals.DataStore
{
    public static class CursorExtensions
    {
        // TODO: Move some of these extensions to DirectoryObject
        // TODO: Convert return value to out or template
        /// <summary>
        /// RetrieveColumnAsSearchFlags implementation.
        /// </summary>
        public static AttributeSearchFlags RetrieveColumnAsSearchFlags(this Cursor cursor, Columnid columnId)
        {
            return (AttributeSearchFlags)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        /// <summary>
        /// RetrieveColumnAsAttributeSyntax implementation.
        /// </summary>
        public static AttributeSyntax RetrieveColumnAsAttributeSyntax(this Cursor cursor, Columnid columnId)
        {
            int? numericValue = cursor.RetrieveColumnAsInt(columnId);
            return numericValue.HasValue ? (AttributeSyntax)numericValue.Value : AttributeSyntax.Undefined;
        }

        /// <summary>
        /// RetrieveColumnAsAttributeSystemFlags implementation.
        /// </summary>
        public static AttributeSystemFlags RetrieveColumnAsAttributeSystemFlags(this Cursor cursor, Columnid columnId)
        {
            return (AttributeSystemFlags)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        /// <summary>
        /// RetrieveColumnAsAttributeOmSyntax implementation.
        /// </summary>
        public static AttributeOmSyntax RetrieveColumnAsAttributeOmSyntax(this Cursor cursor, Columnid columnId)
        {
            return (AttributeOmSyntax)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        /// <summary>
        /// RetrieveColumnAsFunctionalLevel implementation.
        /// </summary>
        public static FunctionalLevel RetrieveColumnAsFunctionalLevel(this Cursor cursor, Columnid columnId)
        {
            return (FunctionalLevel)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        /// <summary>
        /// RetrieveColumnAsString implementation.
        /// </summary>
        public static string RetrieveColumnAsString(this Cursor cursor, Columnid columnId, bool unicode = true)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                // Try to interpret the value as a Unicode string by default
                string stringValue = value as string;
                if (stringValue != null)
                {
                    return stringValue;
                }

                byte[] binaryValue = value as byte[];
                if (binaryValue != null)
                {
                    // This is likely an IA5 string (ASCII)
                    Encoding encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
                    stringValue = encoding.GetString(binaryValue);
                    return stringValue;
                }
            }

            // All other cases
            return null;
        }

        /// <summary>
        /// RetrieveColumnAsStringArray implementation.
        /// </summary>
        public static string[] RetrieveColumnAsStringArray(this Cursor cursor, Columnid columnId, bool unicode = true)
        {
            var record = cursor.Record;
            var result = new List<string>();

            int valueIndex = 0;
            while (record.SizeOf(columnId, valueIndex) > 0)
            {
                object value = cursor.Record[columnId, valueIndex];

                // Try to interpret the value as a Unicode string by default
                string stringValue = value as string;
                if (stringValue == null)
                {
                    byte[] binaryValue = value as byte[];
                    if (binaryValue != null)
                    {
                        // This is likely an IA5 string (ASCII)
                        Encoding encoding = unicode ? Encoding.Unicode : Encoding.ASCII;
                        stringValue = encoding.GetString(binaryValue);
                    }
                }

                result.Add(stringValue);
                valueIndex++;
            }

            return result.Count > 0 ? result.ToArray() : null;
        }

        /// <summary>
        /// RetrieveColumnAsSid implementation.
        /// </summary>
        public static SecurityIdentifier RetrieveColumnAsSid(this Cursor cursor, Columnid columnId)
        {
            byte[] binarySid = cursor.RetrieveColumnAsByteArray(columnId);
            if (binarySid != null)
            {
                return binarySid.ToSecurityIdentifier(true);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsByteArray implementation.
        /// </summary>
        public static byte[] RetrieveColumnAsByteArray(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                return (byte[])value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsByteArray implementation.
        /// </summary>
        public static byte[] RetrieveColumnAsByteArray(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsByteArray(columnId);
        }

        /// <summary>
        /// RetrieveColumnAsMultiByteArray implementation.
        /// </summary>
        public static byte[][] RetrieveColumnAsMultiByteArray(this Cursor cursor, Columnid columnId)
        {
            var record = cursor.Record;
            var result = new List<byte[]>();

            int valueIndex = 0;
            while (record.SizeOf(columnId, valueIndex) > 0)
            {
                object value = cursor.Record[columnId, valueIndex];
                result.Add((byte[])value);
                valueIndex++;
            }

            return result.Count > 0 ? result.ToArray() : null;
        }

        /// <summary>
        /// RetrieveColumnAsDNTag implementation.
        /// </summary>
        public static DNTag? RetrieveColumnAsDNTag(this Cursor cursor, string columnName)
        {
            return (DNTag?)cursor.RetrieveColumnAsInt(columnName);
        }

        /// <summary>
        /// RetrieveColumnAsDNTag implementation.
        /// </summary>
        public static DNTag? RetrieveColumnAsDNTag(this Cursor cursor, Columnid columnId)
        {
            return (DNTag?)cursor.RetrieveColumnAsInt(columnId);
        }

        /// <summary>
        /// RetrieveColumnAsAttributeType implementation.
        /// </summary>
        public static AttributeType? RetrieveColumnAsAttributeType(this Cursor cursor, Columnid columnId)
        {
            return (AttributeType?)unchecked((uint?)cursor.RetrieveColumnAsInt(columnId));
        }

        /// <summary>
        /// RetrieveColumnAsObjectCategory implementation.
        /// </summary>
        public static ClassType? RetrieveColumnAsObjectCategory(this Cursor cursor, Columnid columnId)
        {
            return (ClassType?)unchecked((uint?)cursor.RetrieveColumnAsInt(columnId));
        }

        /// <summary>
        /// RetrieveColumnAsInt implementation.
        /// </summary>
        public static int? RetrieveColumnAsInt(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                return (int)value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsInt implementation.
        /// </summary>
        public static int? RetrieveColumnAsInt(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsInt(columnId);
        }

        /// <summary>
        /// RetrieveColumnAsUInt implementation.
        /// </summary>
        public static uint? RetrieveColumnAsUInt(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                return (uint)value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsUInt implementation.
        /// </summary>
        public static uint? RetrieveColumnAsUInt(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsUInt(columnId);
        }


        /// <summary>
        /// RetrieveColumnAsLong implementation.
        /// </summary>
        public static long? RetrieveColumnAsLong(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                return (long)value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsLong implementation.
        /// </summary>
        public static long? RetrieveColumnAsLong(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsLong(columnId);
        }

        /// <summary>
        /// RetrieveColumnAsBoolean implementation.
        /// </summary>
        public static bool RetrieveColumnAsBoolean(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                return !(value.Equals(0));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// RetrieveColumnAsGuid implementation.
        /// </summary>
        public static Guid? RetrieveColumnAsGuid(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                return new Guid((byte[])value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsTimestamp implementation.
        /// </summary>
        public static DateTime? RetrieveColumnAsTimestamp(this Cursor cursor, Columnid columnId, bool asGeneralizedTime)
        {
            long? timestamp = cursor.RetrieveColumnAsLong(columnId);
            if (timestamp.HasValue)
            {
                return asGeneralizedTime ? timestamp.Value.FromGeneralizedTime() : DateTime.FromFileTime(timestamp.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// RetrieveColumnAsTimestamp implementation.
        /// </summary>
        public static DateTime? RetrieveColumnAsTimestamp(this Cursor cursor, string columnName, bool asGeneralizedTime)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsTimestamp(columnId, asGeneralizedTime);
        }

        /// <summary>
        /// RetrieveColumnAsDomainControllerOptions implementation.
        /// </summary>
        public static DomainControllerOptions RetrieveColumnAsDomainControllerOptions(this Cursor cursor, Columnid columnId)
        {
            int? numeric = cursor.RetrieveColumnAsInt(columnId);
            if (numeric.HasValue)
            {
                return (DomainControllerOptions)numeric.Value;
            }
            else
            {
                return DomainControllerOptions.None;
            }
        }

        /// <summary>
        /// ClearMultiValue implementation.
        /// </summary>
        public static bool ClearMultiValue(this Cursor cursor, Columnid column)
        {
            // TODO: implement ClearMultiValue
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddMultiValue implementation.
        /// </summary>
        public static bool AddMultiValue(this Cursor cursor, Columnid columnId, SecurityIdentifier[] valuesToAdd)
        {
            //NOTE: Must be in transaction and record must be in editing state.
            var binarySidHistory = valuesToAdd.Select(sid => sid.GetBinaryForm(true));
            // TODO: Create method , bool[][]?
            return cursor.AddMultiValue(columnId, binarySidHistory, HashEqualityComparer.GetInstance());
        }

        public static bool AddMultiValue<T>(this Cursor cursor, Columnid columnId, IEnumerable<T> valuesToAdd, IEqualityComparer<T> equalityComparer)
        {
            //NOTE: Must be in transaction and record must be in editing state.
            bool hasChanged = false;
            foreach (T valueToAdd in valuesToAdd)
            {
                bool valueAdded = AddMultiValue(cursor, columnId, valueToAdd, equalityComparer);
                hasChanged |= valueAdded;
            }
            return hasChanged;
        }

        public static bool AddMultiValue<T>(this Cursor cursor, Columnid columnId, T newValue, IEqualityComparer<T> equalityComparer)
        {
            // Note: Must be in transaction and record must be in editing state.
            ColumnAccessor record = cursor.EditRecord;
            int insertAtIndex = 0;
            bool doUpdate = true;
            // Move after the last newValue, if any
            // HACK: Report bad behavior of record.SizeOf(columnId, insertAtIndex): It returns 0 even if there has been a value inserted.
            object currentObject;
            while ((currentObject = record[columnId, insertAtIndex]) != DBNull.Value)
            {
                T currentValue = (T)currentObject;
                if (equalityComparer.Equals(currentValue, newValue))
                {
                    // Record already contains the newValue to be inserted, so skip the update
                    doUpdate = false;
                    break;
                }
                insertAtIndex++;
            }
            if (doUpdate)
            {
                record[columnId, insertAtIndex] = newValue;
            }
            return doUpdate;
        }

        public static bool SetValue<T>(this Cursor cursor, string columnName, Nullable<T> newValue) where T : struct
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.SetValue(columnId, newValue);
        }

        public static bool SetValue<T>(this Cursor cursor, Columnid columnId, Nullable<T> newValue) where T : struct
        {
            // Note: Must be in transaction and record must be in editing state.
            ColumnAccessor record = cursor.EditRecord;
            object currentValue = record[columnId];
            bool hasChanged =  currentValue != DBNull.Value ? !currentValue.Equals(newValue) : newValue != null;

            if (hasChanged)
            {
                if (newValue.HasValue)
                {
                    record[columnId] = newValue.Value;
                }
                else
                {
                    record[columnId] = DBNull.Value;
                }
            }
            return hasChanged;
        }

        public static bool SetValue<T>(this Cursor cursor, string columnName, T newValue, IEqualityComparer<T> equalityComparer) where T : class
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.SetValue(columnId, newValue, equalityComparer);
        }

        public static bool SetValue<T>(this Cursor cursor, Columnid columnId, T newValue, IEqualityComparer<T> equalityComparer) where T : class
        {
            // Retrieve the current value and compare it to the new one
            ColumnAccessor record = cursor.EditRecord;
            object currentRawValue = record[columnId];
            T currentValue = currentRawValue != DBNull.Value ? (T) record[columnId] : null;
            bool hasChanged = ! equalityComparer.Equals(currentValue, newValue);

            if (hasChanged)
            {
                if (newValue != null)
                {
                    record[columnId] = newValue;
                }
                else
                {
                    record[columnId] = DBNull.Value;
                }
            }
            return hasChanged;
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        public static bool SetValue(this Cursor cursor, string columnName, byte[] newValue)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.SetValue(columnId, newValue);
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        public static bool SetValue(this Cursor cursor, Columnid columnId, byte[] newValue)
        {
            return cursor.SetValue(columnId, newValue, HashEqualityComparer.GetInstance());
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        public static bool SetValue(this Cursor cursor, string columnName, string newValue)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.SetValue(columnId, newValue);
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        public static bool SetValue(this Cursor cursor, Columnid columnId, string newValue)
        {
            return cursor.SetValue(columnId, newValue, StringComparer.InvariantCulture);
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        public static bool SetValue(this Cursor cursor, string columnName, DateTime? newValue, bool asGeneralizedTime)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.SetValue(columnId, newValue, asGeneralizedTime);
        }
        
        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        public static bool SetValue(this Cursor cursor, Columnid columnId, DateTime? newValue, bool asGeneralizedTime)
        {
            long? newTimeStamp = null;

            // Convert the value if there is any
            if (newValue.HasValue)
            {
                newTimeStamp = asGeneralizedTime ? newValue.Value.ToGeneralizedTime() : newValue.Value.ToFileTime();
            }

            // Push the value to the DB
            return cursor.SetValue(columnId, newTimeStamp);
        }

        /// <summary>
        /// GotoParentObject implementation.
        /// </summary>
        public static bool GotoParentObject(this Cursor dataTableCursor, DirectorySchema schema)
        {
            // TODO: Check if we are really dealing with the datatable.
            // Read parent DN Tag of the current record
            DNTag parentDNTag = dataTableCursor.RetrieveColumnAsDNTag(schema.ParentDistinguishedNameTagColumnId).Value;

            // Set the index to DNT column
            dataTableCursor.CurrentIndex = DirectorySchema.DistinguishedNameTagIndex;

            // Position the cursor to the only matching record
            return dataTableCursor.GotoKey(Key.Compose(parentDNTag));
        }

        /// <summary>
        /// FindChildren implementation.
        /// </summary>
        public static void FindChildren(this Cursor dataTableCursor, DirectorySchema schema)
        {
            // TODO: Check if we are really dealing with the datatable.
            // Read DN Tag of the current record
            DNTag dnTag = dataTableCursor.RetrieveColumnAsDNTag(schema.DistinguishedNameTagColumnId).Value;
            
            // Set the index to PDNT column to get all children pointing to the current record
            dataTableCursor.CurrentIndex = DirectorySchema.ParentDistinguishedNameTagIndex;

            // Position the cursor before the first child (Indexed columns: PDNT_col, name)
            dataTableCursor.FindRecords(MatchCriteria.EqualTo, Key.ComposeWildcard(dnTag));
        }

        /// <summary>
        /// MoveToFirst implementation.
        /// </summary>
        public static bool MoveToFirst(this Cursor cursor)
        {
            cursor.MoveBeforeFirst();
            return cursor.MoveNext();
        }

        [Obsolete]
        /// <summary>
        /// SaveLocation implementation.
        /// </summary>
        public static Location SaveLocation(this Cursor cursor)
        {
            Location location;
            try
            {
                location = cursor.Location;
            }
            catch(EsentNoCurrentRecordException)
            {
                // The cursor is not yet positioned.
                location = null;
            }
            return location;
        }

        [Obsolete]
        /// <summary>
        /// RestoreLocation implementation.
        /// </summary>
        public static void RestoreLocation(this Cursor cursor, Location location)
        {
            if (location != null)
            {
                cursor.Location = location;
            }
        }
    }
}
