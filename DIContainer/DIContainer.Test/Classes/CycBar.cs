using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes
{
    internal class CycBar : IBar
    {
        private readonly IFoo _foo;

        public CycBar(IFoo foo)
        {
            _foo = foo;
        }
    }
}
