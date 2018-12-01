using DIContainer.Test.Classes.Base;

namespace DIContainer.Test.Classes
{
    internal class AbstrFoo : FooBase
    {
        public AbstrFoo(BarBase barBase)
        {
            BarBase = barBase;
        }
    }
}
