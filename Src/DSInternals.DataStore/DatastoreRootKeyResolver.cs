﻿using System;
using System.Collections.Generic;
using DSInternals.Common.Data;
using Microsoft.Database.Isam;

namespace DSInternals.DataStore
{
    public sealed class DatastoreRootKeyResolver : IKdsRootKeyResolver
    {
        private DirectoryContext _context;
        private readonly bool _featureSupported;

        public DatastoreRootKeyResolver(DirectoryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // The schema must contain the ms-Kds-Prov-RootKey class.
            bool inSchema = context.Schema.ContainsClass(CommonDirectoryClasses.KdsRootKey);

            // None of the KDS root key attributes are present on RODCs
            bool writableDC = !context.DomainController.IsReadOnly;

            _featureSupported = inSchema && writableDC;

            if (_featureSupported)
            {
                _context = context;
            }
        }

        public IEnumerable<KdsRootKey> GetKdsRootKeys()
        {
            if (!_featureSupported)
            {
                yield break;
            }

            using (var cursor = _context.OpenDataTable())
            {
                // Query: (&(objectCategory=ms-Kds-Prov-RootKey)(isDeleted=false))
                string objectCategoryIndex = _context.Schema.FindIndexName(CommonDirectoryAttributes.ObjectCategory);
                cursor.CurrentIndex = objectCategoryIndex;
                int rootKeyClassId = _context.Schema.FindClassId(CommonDirectoryClasses.KdsRootKey);
                cursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(rootKeyClassId));

                while (cursor.MoveNext())
                {
                    var rootKeyObject = new DatastoreObject(cursor, _context);

                    if (rootKeyObject.IsDeleted)
                    {
                        // TODO: Support KDS root keys in recycle bin (not yet tombstoned) by unmangling theis CNs (IDs)
                        continue;
                    }

                    yield return new KdsRootKey(rootKeyObject);
                }
            }
        }

        public KdsRootKey? GetKdsRootKey(Guid id)
        {
            if (!_featureSupported)
            {
                return null;
            }

            int rootKeyClassId = _context.Schema.FindClassId(CommonDirectoryClasses.KdsRootKey);

            using (var cursor = _context.OpenDataTable())
            {
                // Query: (&(cn=<id>)(objectCategory=ms-Kds-Prov-RootKey)(isDeleted=false))
                var commonNameIndex = _context.Schema.FindIndexName(CommonDirectoryAttributes.CommonName);
                cursor.CurrentIndex = commonNameIndex;
                cursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(id.ToString()));

                while (cursor.MoveNext())
                {
                    var candidateObject = new DatastoreObject(cursor, _context);

                    if (candidateObject.IsDeleted)
                    {
                        // TODO: Support KDS root keys in recycle bin (not yet tombstoned) by unmangling theis CNs (IDs)
                        continue;
                    }

                    candidateObject.ReadAttribute(CommonDirectoryAttributes.ObjectCategory, out int? objectCagegory);

                    if (objectCagegory == rootKeyClassId)
                    {
                        // This is the KDS root key we are looking for.
                        return new KdsRootKey(candidateObject);
                    }
                }
            }

            // We have not found any object matching the criteria.
            return null;
        }

        public KdsRootKey GetKdsRootKey(DateTime effectiveTime)
        {
            throw new NotSupportedException("Direct search by effective time is not supported in DB. Use the caching resolver instead.");
        }

        public bool SupportsLookupAll => true;

        public bool SupportsLookupByEffectiveTime => false;
    }
}
