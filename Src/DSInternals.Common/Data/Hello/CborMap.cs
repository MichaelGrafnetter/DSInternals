using System.Collections;
using System.Formats.Cbor;
using System.Text.Json;
using DSInternals.Common.Data.Fido;

namespace DSInternals.Common.Data;

/// <summary>
/// Represents a CBOR (Concise Binary Object Representation) map data structure that stores key-value pairs.
/// </summary>
/// <remarks>
/// This class provides functionality to parse CBOR map data from bytes and access values using different key types
/// including COSE (CBOR Object Signing and Encryption) parameters and string keys. The map does not support
/// indefinite-length maps as per the current implementation.
/// </remarks>
public sealed class CborMap
{
    private Hashtable _items;

    /// <summary>
    /// Initializes a new instance of the <see cref="CborMap"/> class.
    /// </summary>
    /// <param name="items">The key-value pairs to initialize the map with.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="items"/> is null.</exception>
    public CborMap(Hashtable items)
    {
        _items = items ?? throw new ArgumentNullException(nameof(items));
    }

    /// <summary>
    /// Gets the value associated with the specified COSE key parameter.
    /// </summary>
    /// <param name="keyParameter">The COSE key parameter to look up.</param>
    /// <returns>The value associated with the specified key parameter, or null if not found.</returns>
    public object? this[COSE.KeyCommonParameter keyParameter] => _items[(uint)keyParameter];

    /// <summary>
    /// Gets the value associated with the specified COSE key type parameter.
    /// </summary>
    /// <param name="keyTypeParameter">The COSE key type parameter to look up.</param>
    /// <returns>The value associated with the specified key type parameter, or null if not found.</returns>
    public object? this[COSE.KeyTypeParameter keyTypeParameter] => _items[(int)keyTypeParameter];

    /// <summary>
    /// Gets the value associated with the specified string key.
    /// </summary>
    /// <param name="key">The string key to look up.</param>
    /// <returns>The value associated with the specified string key, or null if not found.</returns>
    public object? this[string key] => _items[key];

    /// <summary>
    /// Gets the value associated with the specified string key.
    /// </summary>
    /// <param name="cborData">The CBOR data to parse.</param>
    /// <returns>A tuple containing the parsed CBOR map and the number of bytes read.</returns>
    /// <exception cref="ArgumentException">Thrown when the CBOR data is invalid.</exception>
    public static (CborMap map, int bytesRead) Parse(ReadOnlyMemory<byte> cborData)
    {
        var reader = new CborReader(cborData);
        int? numItems = reader.ReadStartMap();

        if (!numItems.HasValue)
        {
            throw new ArgumentException("Indefinite-length maps are not supported.", nameof(cborData));
        }

        var items = new Hashtable(numItems.Value);

        for (int i = 0; i < numItems; i++)
        {
            object key = ReadValue(reader);
            object value = ReadValue(reader);
            items.Add(key, value);
        }

        reader.ReadEndMap();

        return (new CborMap(items), cborData.Length - reader.BytesRemaining);
    }

    /// <summary>
    /// Converts the CBOR map to its JSON representation.
    /// </summary>
    /// <returns>The JSON representation of the CBOR map.</returns>
    public string ToJson()
    {
        // Convert the data to JSON
        return JsonSerializer.SerializeToElement<Hashtable>(_items).ToString();
    }

    /// <summary>
    /// Reads a value from the CBOR reader based on its state.
    /// </summary>
    /// <param name="reader">The CBOR reader to read from.</param>
    /// <returns>The read value.</returns>
    /// <exception cref="ArgumentException">Thrown when the CBOR data is invalid.</exception>
    private static object ReadValue(CborReader reader)
    {
        CborReaderState state = reader.PeekState();

        return state switch
        {
            CborReaderState.TextString => reader.ReadTextString(),
            CborReaderState.Boolean => reader.ReadBoolean(),
            CborReaderState.ByteString => reader.ReadByteString(),
            CborReaderState.UnsignedInteger => reader.ReadUInt32(),
            CborReaderState.NegativeInteger => reader.ReadInt32(),
            _ => throw new ArgumentException($"Unexpected state {state}.")
        };
    }

}
