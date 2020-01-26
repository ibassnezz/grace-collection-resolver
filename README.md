# GreaceCollectionResolver
Keyed DI allows to register services with common interface independently in different places

HowTo register:
```
serviceCollection.RegisterImplementation<FooType, IFooInterface, Value2Service>(FooType.Value2, ServiceLifetime.Transient);
```
 
 HowTo use:

 Inject `CollectionGraceResolver<FooType, IFooInterface>` to any service you want

 ```
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
 ```

