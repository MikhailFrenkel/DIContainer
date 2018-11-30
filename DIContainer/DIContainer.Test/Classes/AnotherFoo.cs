using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes
{
    internal class AnotherFoo : IFoo
    {
        public IBar Bar { get; }

        public AnotherFoo(IBar bar)
        {
            Bar = bar;
        }
    }
}
