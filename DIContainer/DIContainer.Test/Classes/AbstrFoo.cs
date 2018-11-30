using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
