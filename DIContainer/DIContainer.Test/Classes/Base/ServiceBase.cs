using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes.Base
{
    internal abstract class ServiceBase : IService
    {
        public IBar Bar { get; }
        public IFoo Foo { get; }
    }
}
