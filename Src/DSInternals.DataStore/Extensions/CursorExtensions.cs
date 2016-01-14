using DSInternals.Common;
using DSInternals.Common.Data;
using Microsoft.Database.Isam;
using Microsoft.Isam.Esent.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace DSInternals.DataStore
{
    public static class CursorExtensions
    {
        private static readonly Func<byte[], byte[], bool> byteArrayComparer = (x, y) => x.SequenceEqual(y);
        // TODO: Move some of these extensions to DirectoryObject
        // TODO: Convert return value to out or template
        public static SearchFlags RetrieveColumnAsSearchFlags(this Cursor cursor, Columnid columnId)
        {
            return (SearchFlags)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        public static AttributeSyntax RetrieveColumnAsAttributeSyntax(this Cursor cursor, Columnid columnId)
        {
            return (AttributeSyntax)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        public static AttributeSystemFlags RetrieveColumnAsAttributeSystemFlags(this Cursor cursor, Columnid columnId)
        {
            return (AttributeSystemFlags)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        public static AttributeOmSyntax RetrieveColumnAsAttributeOmSyntax(this Cursor cursor, Columnid columnId)
        {
            return (AttributeOmSyntax)cursor.RetrieveColumnAsInt(columnId).GetValueOrDefault(0);
        }

        public static string RetrieveColumnAsString(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if(value != DBNull.Value)
            {
                return (string)value;
            }
            else
            {
                return null;
            }
        }
        public static AttributeMetadataCollection RetrieveColumnAsAttrMeta(this Cursor cursor, Columnid columnId)
        {
            byte[] buffer = cursor.RetrieveColumnAsByteArray(columnId);
            return new AttributeMetadataCollection(buffer);
        }

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

        public static byte[] RetrieveColumnAsByteArray(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsByteArray(columnId);
        }

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

        public static int? RetrieveColumnAsDNTag(this Cursor cursor, Columnid columnId)
        {
            return cursor.RetrieveColumnAsInt(columnId);
        }

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

        public static int? RetrieveColumnAsInt(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsInt(columnId);
        }

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

        public static uint? RetrieveColumnAsUInt(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsUInt(columnId);
        }


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

        public static long? RetrieveColumnAsLong(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsLong(columnId);
        }

        public static bool RetrieveColumnAsBoolean(this Cursor cursor, Columnid columnId)
        {
            object value = cursor.Record[columnId];
            if (value != DBNull.Value)
            {
                //TODO: Test RetrieveColumnAsBoolean
                return ((int)value != 0);
            }
            else
            {
                return false;
            }
        }

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

        public static DateTime? RetrieveColumnAsTimestamp(this Cursor cursor, Columnid columnId)
        {
            long? timestamp = cursor.RetrieveColumnAsLong(columnId);
            if (timestamp.HasValue)
            {
                return DateTime.FromFileTime(timestamp.Value);
            }
            else
            {
                return null;
            }
        }

        public static DateTime? RetrieveColumnAsGeneralizedTime(this Cursor cursor, Columnid columnId)
        {
            long? timestamp = cursor.RetrieveColumnAsLong(columnId);
            if (timestamp.HasValue)
            {
                // 0 = January 1, 1601 1:00:00 AM = Never
                return DateTime.FromFileTime(timestamp.Value * ADConstants.GeneralizedTimeCoefficient);
            }
            else
            {
                return null;
            }
        }

        public static DateTime? RetrieveColumnAsGeneralizedTime(this Cursor cursor, string columnName)
        {
            var columnId = cursor.TableDefinition.Columns[columnName].Columnid;
            return cursor.RetrieveColumnAsGeneralizedTime(columnId);
        }

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

        public static bool ClearMultiValue(this Cursor cursor, Columnid column)
        {
            // TODO: implement ClearMultiValue
            throw new NotImplementedException();
        }

        public static bool AddMultiValue(this Cursor cursor, Columnid columnId, SecurityIdentifier[] valuesToAdd)
        {
            //NOTE: Must be in transaction and record must be in editing state.
            var binarySidHistory = valuesToAdd.Select(sid => sid.GetBinaryForm(true));
            // TODO: Create method , bool[][]?
            return cursor.AddMultiValue(columnId, binarySidHistory, byteArrayComparer);
        }

        public static bool AddMultiValue<T>(this Cursor cursor, Columnid columnId, IEnumerable<T> valuesToAdd, Func<T, T, bool> equalityComparer)
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

        public static bool AddMultiValue<T>(this Cursor cursor, Columnid columnId, T newValue, Func<T, T, bool> equalityComparer)
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
                if (equalityComparer.Invoke(currentValue, newValue))
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

        public static bool SetValue<T>(this Cursor cursor, Columnid columnId, Nullable<T> newValue) where T : struct
        {
            // Note: Must be in transaction and record must be in editing state.
            ColumnAccessor record = cursor.EditRecord;
            object currentValue = record[columnId];
            bool hasChanged = !currentValue.Equals(newValue);
            if (hasChanged)
            {
                if (newValue.HasValue)
                {
                    record[columnId] = newValue.Value;
                }
                else
                {
                    // TODO: Test nulling an attribute
                    record[columnId] = DBNull.Value;
                }
            }
            return hasChanged;
        }

        public static bool SetValue<T>(this Cursor cursor, Columnid columnId, T newValue, Func<T, T, bool> equalityComparer) where T : class
        {
            ColumnAccessor record = cursor.EditRecord;
            T currentValue = (T) record[columnId];
            bool hasChanged = ! equalityComparer.Invoke(currentValue, newValue);
            if (hasChanged)
            {
                if (newValue != null)
                {
                    record[columnId] = newValue;
                }
                else
                {
                    // TODO: Test nulling an attribute
                    record[columnId] = DBNull.Value;
                }
            }
            return hasChanged;

        }

        public static bool SetValue(this Cursor cursor, Columnid columnId, byte[] newValue)
        {
            return cursor.SetValue(columnId, newValue, byteArrayComparer);
        }

        public static bool SetValue(this Cursor cursor, Columnid columnId, string newValue)
        {
            // TODO: Only one instance of byteArrayComparer
            Func<string, string, bool> stringComparer = (x, y) => String.Compare(x, y) == 0;
            return cursor.SetValue(columnId, newValue, stringComparer);
        }

        public static bool GotoToParentObject(this Cursor dataTableCursor, DirectorySchema schema)
        {
            // TODO: Check if we are really dealing with the datatable.
            // Read parent DN Tag of the current record
            int parentDNTag = dataTableCursor.RetrieveColumnAsDNTag(schema.FindColumnId(CommonDirectoryAttributes.ParentDNTag)).Value;
            // Set the index to PDNT column
            dataTableCursor.CurrentIndex = schema.FindIndexName(CommonDirectoryAttributes.ParentDNTag);
            // Position the cursor to the only matching record
            return dataTableCursor.GotoKey(Key.Compose(parentDNTag));
        }

        public static bool MoveToFirst(this Cursor cursor)
        {
            cursor.MoveBeforeFirst();
            return cursor.MoveNext();
        }

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

        public static void RestoreLocation(this Cursor cursor, Location location)
        {
            if (location != null)
            {
                cursor.Location = location;
            }
        }
    }
}
