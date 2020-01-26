using System;
using GraceCollectionResolver.Application;
using Microsoft.Extensions.DependencyInjection;

namespace GraceCollectionResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<InjecteeService>();

            serviceCollection.RegisterImplementation<FooType, IFooInterface, Value1Service>(FooType.Value1,
                ServiceLifetime.Transient);

            //registering same interface and another implementation
            serviceCollection.RegisterImplementation<FooType, IFooInterface, Value2Service>(FooType.Value2,
                ServiceLifetime.Transient);

            var provider = serviceCollection.BuildServiceProvider();

            var injecteeService = provider.GetService<InjecteeService>();

            injecteeService.Run();
            
        }
    }

    public class Value1Service : IFooInterface
    {
        public void Execute()
        {
            Console.WriteLine(GetType());
        }
    }

    public class Value2Service : IFooInterface
    {
        public void Execute()
        {
            Console.WriteLine(GetType());
        }
    }

    public class InjecteeService
    {
        private readonly CollectionGraceResolver<FooType, IFooInterface> _graceResolver;

        public InjecteeService(CollectionGraceResolver<FooType, IFooInterface> graceResolver)
        {
            _graceResolver = graceResolver;
        }

        public void Run()
        {
            _graceResolver.Get(FooType.Value1).Execute();
            _graceResolver.Get(FooType.Value2).Execute();
        }
    }
}
