// ---------------------------------------------------------------------------
// <copyright file="converter.cs" company="Microsoft">
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
    using System.Globalization;
    using System.Text;
    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Vista;

    /// <summary>
    /// Convert an array of Bytes to/from a CLR object, given the type of the underlying column
    /// </summary>
    /// <remarks>
    /// The Interop layer operates with Byte arrays. The Converter class provides methods to convert
    /// an array of bytes retrieved from the Interop layer into a CLR object and to convert a CLR
    /// object into an array of bytes.
    /// </remarks>
    internal class Converter
    {
        /// <summary>
        /// Given a column type and an object, convert the object to an array of bytes
        /// </summary>
        /// <remarks>
        /// This method uses System.Convert and System.BitConverter to do automatic type conversion (e.g. String -> Boolean)
        /// </remarks>
        /// <param name="type">The type of the database column that this object is to be stored in</param>
        /// <param name="isASCII">For text columns, is the column an ASCII column. Ignored for other types</param>
        /// <param name="o">The object to be converted</param>
        /// <returns>An array of bytes representing the object</returns>
        /// <exception cref="InvalidCastException">This conversion is not supported</exception>
        /// <exception cref="ArgumentOutOfRangeException">Unknown column type</exception>
        public static byte[] BytesFromObject(JET_coltyp type, bool isASCII, object o)
        {
            if (o is DBNull)
            {
                return null;
            }

            if (null == o)
            {
                return null;
            }

            switch (type)
            {
                case JET_coltyp.Nil:
                    throw new ArgumentOutOfRangeException("type", "Nil is not valid");
                case JET_coltyp.Bit:
                    return BitConverter.GetBytes(Convert.ToBoolean(o));
                case JET_coltyp.UnsignedByte:
                    byte[] bytes = new byte[1];
                    bytes[0] = (byte)o;
                    return bytes;
                case JET_coltyp.Short:
                    return BitConverter.GetBytes(Convert.ToInt16(o));
                case JET_coltyp.Long:
                    return BitConverter.GetBytes(Convert.ToInt32(o));
                case VistaColtyp.LongLong:
                    return BitConverter.GetBytes(Convert.ToInt64(o));
                case JET_coltyp.Currency:
                    return BitConverter.GetBytes(Convert.ToInt64(o));
                case JET_coltyp.IEEESingle:
                    return BitConverter.GetBytes(Convert.ToSingle(o));
                case JET_coltyp.IEEEDouble:
                    return BitConverter.GetBytes(Convert.ToDouble(o));
                case VistaColtyp.UnsignedShort:
                    return BitConverter.GetBytes(Convert.ToUInt16(o));
                case VistaColtyp.UnsignedLong:
                    return BitConverter.GetBytes(Convert.ToUInt32(o));
                case JET_coltyp.Binary:
                    return (byte[])o;
                case JET_coltyp.LongBinary:
                    return (byte[])o;
                case JET_coltyp.DateTime:
                    // Internally DateTime is stored as a double, mapping to a COLEDateTime
                    DateTime datetime = Convert.ToDateTime(o);
                    double oleDateTime = datetime.ToOADate();
                    return BitConverter.GetBytes(oleDateTime);
                case JET_coltyp.Text:
                    if (isASCII)
                    {
                        return Encoding.ASCII.GetBytes(o.ToString());
                    }
                    else
                    {
                        return Encoding.Unicode.GetBytes(o.ToString());
                    }

                case JET_coltyp.LongText:
                    if (isASCII)
                    {
                        return Encoding.ASCII.GetBytes(o.ToString());
                    }
                    else
                    {
                        return Encoding.Unicode.GetBytes(o.ToString());
                    }

                case VistaColtyp.GUID:
                    if (o is string)
                    {
                        Guid guid = new Guid((string)o);
                        return guid.ToByteArray();
                    }
                    else if (o is byte[])
                    {
                        Guid guid = new Guid((byte[])o);
                        return guid.ToByteArray();
                    }
                    else
                    {
                        // if o isn't a Guid, this will throw an exception
                        return ((Guid)o).ToByteArray();
                    }

                default:
                    throw new ArgumentOutOfRangeException("type", "unknown type");
            }
        }

        /// <summary>
        /// Given a column type and a Byte array, convert the bytes into a CLR object
        /// </summary>
        /// <remarks>
        /// This method uses System.BitConverter to do the conversion
        /// </remarks>
        /// <param name="type">The type of the database column that this bytes were retrieved from</param>
        /// <param name="isASCII">For text columns, is the column an ASCII column. Ignored for other types</param>
        /// <param name="value">The bytes to be converted</param>
        /// <returns>An Object constructed from the bytes or DBNull for a null value</returns>
        /// <exception cref="ArgumentOutOfRangeException">Unknown column type or incorrect Byte array size</exception>
        public static object ObjectFromBytes(JET_coltyp type, bool isASCII, byte[] value)
        {
            if (null == value)
            {
                return DBNull.Value;
            }

            switch (type)
            {
                case JET_coltyp.Nil:
                    throw new ArgumentOutOfRangeException("type", "Nil is not valid");
                case JET_coltyp.Bit:
                    return BitConverter.ToBoolean(value, 0);
                case JET_coltyp.UnsignedByte:
                    return value[0];
                case JET_coltyp.Short:
                    return BitConverter.ToInt16(value, 0);
                case JET_coltyp.Long:
                    return BitConverter.ToInt32(value, 0);
                case VistaColtyp.LongLong:
                    return BitConverter.ToInt64(value, 0);
                case JET_coltyp.Currency:
                    return BitConverter.ToInt64(value, 0);
                case JET_coltyp.IEEESingle:
                    return BitConverter.ToSingle(value, 0);
                case JET_coltyp.IEEEDouble:
                    return BitConverter.ToDouble(value, 0);
                case VistaColtyp.UnsignedShort:
                    return BitConverter.ToUInt16(value, 0);
                case VistaColtyp.UnsignedLong:
                    return BitConverter.ToUInt32(value, 0);
                case JET_coltyp.Binary:
                    return value;
                case JET_coltyp.LongBinary:
                    return value;
                case JET_coltyp.Text:
                    if (isASCII)
                    {
                        return new string(Encoding.ASCII.GetChars(value));
                    }
                    else
                    {
                        return new string(Encoding.Unicode.GetChars(value));
                    }

                case JET_coltyp.LongText:
                    if (isASCII)
                    {
                        return new string(Encoding.ASCII.GetChars(value));
                    }
                    else
                    {
                        return new string(Encoding.Unicode.GetChars(value));
                    }

                case VistaColtyp.GUID:
                    return new Guid(value);
                case JET_coltyp.DateTime:
                    // Internally DateTime is stored as a double, mapping to a COLEDateTime
                    return DateTime.FromOADate(BitConverter.ToDouble(value, 0));
                default:
                    throw new ArgumentOutOfRangeException("type", "unknown type");
            }
        }

        /// <summary>
        /// Converts from <see cref="CompareOptions"/> to <see cref="UnicodeIndexFlags"/>.
        /// </summary>
        /// <param name="options">The value to convert.</param>
        /// <returns>A <see cref="UnicodeIndexFlags"/> value equivalent to <paramref name="options"/>.</returns>
        /// <exception cref="System.ArgumentException">CompareOptions.Ordinal is not supported;compareOptions</exception>
        internal static UnicodeIndexFlags UnicodeFlagsFromCompareOptions(CompareOptions options)
        {
            UnicodeIndexFlags unicodeflags = UnicodeIndexFlags.None;
            if (IsCompareOptionSet(options, CompareOptions.IgnoreCase))
            {
                unicodeflags |= UnicodeIndexFlags.CaseInsensitive;
            }

            if (IsCompareOptionSet(options, CompareOptions.IgnoreKanaType))
            {
                unicodeflags |= UnicodeIndexFlags.KanaInsensitive;
            }

            if (IsCompareOptionSet(options, CompareOptions.IgnoreNonSpace))
            {
                unicodeflags |= UnicodeIndexFlags.AccentInsensitive;
            }

            if (IsCompareOptionSet(options, CompareOptions.IgnoreSymbols))
            {
                unicodeflags |= UnicodeIndexFlags.IgnoreSymbols;
            }

            if (IsCompareOptionSet(options, CompareOptions.IgnoreWidth))
            {
                unicodeflags |= UnicodeIndexFlags.WidthInsensitive;
            }

            if (IsCompareOptionSet(options, CompareOptions.StringSort))
            {
                unicodeflags |= UnicodeIndexFlags.StringSort;
            }

            if (IsCompareOptionSet(options, CompareOptions.Ordinal))
            {
                throw new ArgumentException("CompareOptions.Ordinal is not supported", "compareOptions");
            }

            return unicodeflags;
        }

        /// <summary>
        /// Converts unicodeIndexFlags to a ulong, suitable for dwMapFlags.
        /// </summary>
        /// <param name="unicodeIndexFlags">A UnicodeIndexFlags value.</param>
        /// <returns>A value suitable for calling by LCMapString.</returns>
        internal static uint MapFlagsFromUnicodeIndexFlags(UnicodeIndexFlags unicodeIndexFlags)
        {
            return (uint)unicodeIndexFlags;
        }

        /// <summary>
        /// Converts <see cref="ColumnFlags"/> to a <see cref="ColumndefGrbit"/>, suitable for ESE function calls.
        /// </summary>
        /// <param name="columnFlags">A <see cref="ColumnFlags"/> value.</param>
        /// <returns>A ColumndefGrbit suitable for function calls.</returns>
        internal static ColumndefGrbit ColumndefGrbitFromColumnFlags(ColumnFlags columnFlags)
        {
            return (ColumndefGrbit)columnFlags;
        }

        /// <summary>
        /// Determines whether all of the the specified options in <paramref name="optionsMask"/>
        /// are set in <paramref name="optionsToTest"/>.
        /// </summary>
        /// <param name="optionsToTest">The value to test.</param>
        /// <param name="optionsMask">The optionsMask.</param>
        /// <returns>
        /// Whether all of the the specified options in <paramref name="optionsMask"/>
        /// </returns>
        private static bool IsCompareOptionSet(CompareOptions optionsToTest, CompareOptions optionsMask)
        {
            if ((optionsToTest & optionsMask) == optionsMask)
            {
                return true;
            }

            return false;
        }
    }
}
