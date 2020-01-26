namespace GraceCollectionResolver.Application
{
    public class KeyImplementationContainer<TKey, TImplementation>
    {
        public KeyImplementationContainer(TKey key, TImplementation implementation)
        {
            Key = key;
            Implementation = implementation;
        }

        public TKey Key { get; }

        public TImplementation Implementation { get; }
    }
}