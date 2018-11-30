using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes
{
    internal class Foo : IFoo
    {
        public IBar Bar { get; }

        public Foo(IBar bar)
        {
            Bar = bar;
        }
    }
}
