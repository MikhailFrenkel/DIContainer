using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes
{
    internal class Service : IService
    {
        public IFoo Foo { get; }
        public IBar Bar { get; }

        public Service(IFoo foo, IBar bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }
}
