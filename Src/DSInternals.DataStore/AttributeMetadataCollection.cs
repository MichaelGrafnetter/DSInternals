using System.Text;
using DSInternals.Common;
using DSInternals.Common.Schema;

namespace DSInternals.DataStore;

public class AttributeMetadataCollection
{
    private const int GuidSize = 16;
    private const int EntrySize = 3 * sizeof(long) + 2 * sizeof(int) + GuidSize;
    private const int HeaderSize = 2 * sizeof(long); // Structure: | Unknown | Number of Entries | Entries |
    private const long DefaultUnknownValue = 1;

    public long Unknown
    {
        get;
        private set;
    }

    public int Count
    {
        get
        {
            return this.InnerList?.Count ?? 0;
        }
    }

    public IList<AttributeType> Attributes
    {
        get
        {
            return this.InnerList?.Keys;
        }
    }

    /// <summary>
    /// Holds a list of attribute metadata sorted by attribute ID.
    /// </summary>
    protected SortedList<AttributeType, AttributeMetadata> InnerList
    {
        get;
        private set;
    }

    public AttributeMetadataCollection() : this(null) { }

    public AttributeMetadataCollection(byte[] buffer)
    {
        if (buffer == null)
        {
            // Initialize an empty collection
            this.Unknown = DefaultUnknownValue;
            this.InnerList = new SortedList<AttributeType, AttributeMetadata>();
            return;
        }

        Validator.AssertMinLength(buffer, HeaderSize, nameof(buffer));

        using (Stream stream = new MemoryStream(buffer))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Read structure and validate header
                this.Unknown = reader.ReadInt64();
                long numEntries = reader.ReadInt64();
                long expectedBufferSize = CalculateBinarySize(numEntries);
                Validator.AssertLength(buffer, expectedBufferSize, nameof(buffer));

                // Read all entries
                this.InnerList = new SortedList<AttributeType, AttributeMetadata>((int)numEntries);
                for (int i = 1; i <= numEntries; i++)
                {
                    AttributeType attributeId = (AttributeType)reader.ReadUInt32();
                    int version = reader.ReadInt32();
                    long timestamp = reader.ReadInt64();
                    Guid originatingDSA = new Guid(reader.ReadBytes(16));
                    long originatingUSN = reader.ReadInt64();
                    long localUSN = reader.ReadInt64();
                    var entry = new AttributeMetadata(version, timestamp, originatingDSA, originatingUSN, localUSN);
                    try
                    {
                        this.InnerList.Add(attributeId, entry);
                    }
                    catch (ArgumentException)
                    {
                        // An element with the same key already exists.
                        // We will simply ignore duplicate values and thus remove them on save.
                    }
                }
            }
        }
    }

    public void Update(AttributeType attributeId, Guid invocationId, DateTime time, long usn)
    {
        this.InnerList.TryGetValue(attributeId, out AttributeMetadata entry);

        if (entry != null)
        {
            // This attribute is already contained in the list, so we just update it
            entry.Update(invocationId, time, usn);
        }
        else
        {
            // This is a newly added attribute
            entry = new AttributeMetadata(invocationId, time, usn);
            this.InnerList.Add(attributeId, entry);
        }
    }

    public byte[] ToByteArray()
    {
        byte[] buffer = new byte[CalculateBinarySize(this.Count)];
        using (MemoryStream stream = new MemoryStream(buffer))
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(this.Unknown);

                // Important: Write Count as 64-bit and not 32-bit:
                writer.Write((long)this.Count);

                foreach (var entry in this.InnerList)
                {
                    writer.Write((uint)entry.Key);
                    writer.Write(entry.Value.Version);
                    writer.Write(entry.Value.LastOriginatingChangeTimestamp);
                    writer.Write(entry.Value.LastOriginatingInvocationId.ToByteArray());
                    writer.Write(entry.Value.OriginatingChangeUsn);
                    writer.Write(entry.Value.LocalChangeUsn);
                }
            }
        }
        return buffer;
    }

    public override string ToString()
    {
        var text = new StringBuilder();
        foreach (var entry in InnerList)
        {
            text.AppendFormat("AttId: {0}, ", entry.Key);
            text.AppendLine(entry.Value.ToString());
        }
        return text.ToString();
    }

    private static long CalculateBinarySize(long numEntries)
    {
        // Unknown Value + Entry Count + Entries
        return 2 * sizeof(long) + numEntries * EntrySize;
    }
}
