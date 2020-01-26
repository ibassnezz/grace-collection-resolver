using System.Collections.Generic;
using System.Linq;

namespace GraceCollectionResolver.Application
{
    public class CollectionGraceResolver<TKey, TImplementation>
        where TImplementation : class
    {
        private readonly IEnumerable<KeyImplementationContainer<TKey, TImplementation>> _objects;

        public CollectionGraceResolver(IEnumerable<KeyImplementationContainer<TKey, TImplementation>> objects) => _objects = objects;

        public TImplementation Get(TKey key) => _objects.FirstOrDefault(o => o.Key.Equals(key))?.Implementation;
    }
}