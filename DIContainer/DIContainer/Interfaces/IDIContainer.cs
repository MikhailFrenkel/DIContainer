using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Interfaces
{
    public interface IDIContainer
    {
        TDependency Resolve<TDependency>() where TDependency : class;
    }
}
