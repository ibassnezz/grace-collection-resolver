using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GraceCollectionResolver.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterImplementation<TKey, TInterface, TImplementation>(
            this IServiceCollection service,
            TKey key,
            ServiceLifetime lifetime)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var implemented = new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime);
            service.Add(implemented);

            var wrapped = new ServiceDescriptor(
                typeof(KeyImplementationContainer<TKey, TInterface>),
                provider => new KeyImplementationContainer<TKey, TInterface>(key, provider.GetService<TImplementation>()),
                lifetime);
            service.Add(wrapped);

            var collection = new ServiceDescriptor(
                typeof(CollectionGraceResolver<TKey, TInterface>),
                provider => new CollectionGraceResolver<TKey, TInterface>(provider.GetServices<KeyImplementationContainer<TKey, TInterface>>()),
                lifetime);
            service.TryAdd(collection);

            return service;
        }
    }
}