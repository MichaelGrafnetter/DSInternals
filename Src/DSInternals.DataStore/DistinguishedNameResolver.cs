using System.Collections.Concurrent;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Schema;
using Microsoft.Database.Isam;

namespace DSInternals.DataStore;

/// <summary>
/// Performs translation between Distinguished Names and DN Tags.
/// </summary>
public class DistinguishedNameResolver
{
    private const string DnsZoneContainer = "MicrosoftDNS";
    private DirectorySchema _schema;
    private IsamDatabase _database;

    /// <summary>
    ///  Distinguished name cache
    /// </summary>
    private IDictionary<DNTag, DistinguishedName> _dnCache;

    public DistinguishedNameResolver(IsamDatabase database, DirectorySchema schema)
    {
        ArgumentNullException.ThrowIfNull(database);
        ArgumentNullException.ThrowIfNull(schema);

        // Initialize the thread-safe DN cache, while pre-caching the root DN as a sentinel.
        _dnCache = new ConcurrentDictionary<DNTag, DistinguishedName>();
        _dnCache.Add(DNTag.RootObject, new DistinguishedName());

        // Cache AD schema and database
        _schema = schema;
        _database = database;
    }

    /// <summary>
    /// Recursively resolves a DN Tag to a full distinguished name.
    /// </summary>
    /// <param name="dnTag">Distinguished name tag</param>
    /// <returns>Resolved DN</returns>
    /// <exception cref="DirectoryObjectNotFoundException" />
    public DistinguishedName Resolve(DNTag dnTag)
    {
        // Check the DN cache first
        DistinguishedName dnFromCache = this.TryResolveFromCache(dnTag);
        if (dnFromCache != null)
        {
            // We just return the DN from cache.
            return dnFromCache;
        }

        if (dnTag < DNTag.RootObject)
        {
            // Minimum DN Tag is 2.
            throw new ArgumentOutOfRangeException(nameof(dnTag));
        }

        using (var cursor = _database.OpenCursor(ADConstants.DataTableName))
        {
            // Set index to the Distinguished Name Tag (~primary key)
            cursor.CurrentIndex = DirectorySchema.DistinguishedNameTagIndex;

            // We will build the DN from leaf to root
            DistinguishedName result = new DistinguishedName();
            DNTag currentDNTag = dnTag;
            while (currentDNTag != DNTag.RootObject)
            {
                // Move cursor to the current object
                bool found = cursor.GotoKey(Key.Compose(currentDNTag));
                if (!found)
                {
                    throw new DirectoryObjectNotFoundException(dnTag);
                }

                // Retrieve the current object's RDN, e.g. CN=Administrator
                string? name = cursor.RetrieveColumnAsString(_schema.RelativeDistinguishedNameColumnId);
                AttributeType? rdnType = cursor.RetrieveColumnAsAttributeType(_schema.RelativeDistinguishedNameTypeColumnId).Value;
                string rdnAtt = _schema.FindAttribute(rdnType.Value).Name.ToUpperInvariant();
                var currentRDN = new DistinguishedNameComponent(rdnAtt, name);

                // Concat the current RDN with the child RDN in the resulting DN
                result.AddParent(currentRDN);

                // Identify the current object's parent
                DNTag parentDNTag = cursor.RetrieveColumnAsDNTag(_schema.ParentDistinguishedNameTagColumnId).Value;

                // Check the DN cache
                DistinguishedName parentDN = this.TryResolveFromCache(parentDNTag);
                if (parentDN != null)
                {
                    // We have found the parent object in DN cache.
                    result.AddParent(parentDN);

                    // Speed-up future DN lookups by caching root containers, all OUs, naming contexts, and DNS zones.
                    // Add the current object to the cache if any of the following conditions is met:
                    // 1) The parent object is the root DN. (DC=com)
                    // 2) The current object is an organizational unit. (OU=Employees,DC=contoso,DC=com)
                    // 3) The current object is the DNS zone container. (CN=MicrosoftDNS,CN=System,DC=contoso,DC=com)
                    // 4) The current object is a domain component, while the parent object is not. (DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com)
                    // 5) The parent object is a domain component, while the current object is not. (CN=Computers,DC=contoso,DC=com)
                    // 6) Both the current and parent objects are domain components, but the current object is not the leaf object. (...,DC=contoso,DC=com)
                    // Note: The last condition should prevent individual DNS records from being cached unnecessarily. (DC=www,DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com)
                    bool shoulCache = parentDN.Components.Count == 0 ||
                        String.Equals(rdnAtt, CommonDirectoryAttributes.OrganizationalUnitName, StringComparison.OrdinalIgnoreCase) ||
                        String.Equals(name, DnsZoneContainer, StringComparison.OrdinalIgnoreCase);
                    bool isLeaf = currentDNTag == dnTag;

                    if (!shoulCache)
                    {
                        bool isDomainComponent = String.Equals(rdnAtt, CommonDirectoryAttributes.DomainComponent, StringComparison.OrdinalIgnoreCase);
                        bool isParentDomainComponent = String.Equals(parentDN.Components[0].Name, CommonDirectoryAttributes.DomainComponent, StringComparison.OrdinalIgnoreCase);
                        shoulCache =
                            isDomainComponent && !isParentDomainComponent ||
                            !isDomainComponent && isParentDomainComponent ||
                            isDomainComponent && isParentDomainComponent && !isLeaf;
                    }

                    if (shoulCache)
                    {
                        if (isLeaf)
                        {
                            // We have resolved the entire DN, so we can cache it.
                            _dnCache[dnTag] = result;
                        }
                        else
                        {
                            // We have resolved a part of the DN, so we can cache it as well.
                            var currentDN = new DistinguishedName(currentRDN);
                            currentDN.AddParent(parentDN);
                            _dnCache[currentDNTag] = currentDN;
                        }
                    }

                    // We can stop the recursion as we have resolved the entire DN using cache.
                    break;
                }

                // Move upwards to the object's parent as we have not found it in the DN cache.
                currentDNTag = parentDNTag;
            }

            return result;
        }
    }

    /// <summary>
    /// Translates a distinguished name to a its DN Tag.
    /// </summary>
    /// <param name="dn">Distinguished name to translate</param>
    /// <returns>DN Tag</returns>
    /// <exception cref="DirectoryObjectNotFoundException" />
    public DNTag Resolve(string dn)
    {
        var parsed = new DistinguishedName(dn);
        return this.Resolve(parsed);
    }

    /// <summary>
    /// Translates a distinguished name to a its DN Tag.
    /// </summary>
    /// <param name="dn">Distinguished name to translate</param>
    /// <returns>DN Tag</returns>
    /// <exception cref="DirectoryObjectNotFoundException"></exception>
    public DNTag Resolve(DistinguishedName dn)
    {
        if (dn == null || dn.Components.Count == 0)
        {
            throw new ArgumentException("Empty distinguished name provided.", nameof(dn));
        }

        using (var cursor = _database.OpenCursor(ADConstants.DataTableName))
        {
            // Get the PDNT_index
            cursor.CurrentIndex = DirectorySchema.ParentDistinguishedNameTagIndex;

            // Start at the root object
            DNTag currentDNTag = DNTag.RootObject;
            foreach (var component in dn.Components.Reverse())
            {
                // Indexed columns: PDNT_col, name
                bool found = cursor.GotoKey(Key.Compose(currentDNTag, component.Value));
                if (!found)
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Test AttrTyp
                AttributeType? foundRdnAttId = cursor.RetrieveColumnAsAttributeType(_schema.RelativeDistinguishedNameTypeColumnId).Value;
                string foundRdnAttName = _schema.FindAttribute(foundRdnAttId.Value).Name;

                // Compare the found isRDN attribute with the requested one. Case insensitive.
                if (!string.Equals(component.Name, foundRdnAttName, StringComparison.OrdinalIgnoreCase))
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Move to the found object
                currentDNTag = cursor.RetrieveColumnAsDNTag(_schema.DistinguishedNameTagColumnId).Value;
            }

            return currentDNTag;
        }
    }

    /// <summary>
    /// Resolves a DN Tag to a full distinguished name using the cache.
    /// </summary>
    /// <param name="dnTag">DN Tag</param>
    /// <returns>Distinguished name</returns>
    private DistinguishedName? TryResolveFromCache(DNTag dnTag)
    {
        bool found = _dnCache.TryGetValue(dnTag, out DistinguishedName dnFromCache);
        return found ? dnFromCache : null;
    }
}
