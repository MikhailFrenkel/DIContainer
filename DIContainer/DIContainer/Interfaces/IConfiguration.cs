using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Interfaces
{
    public interface IConfiguration
    {
        void Register<TImplementation>() where TImplementation : class;

        void Register<TDependency, TImplementation>()
            where TDependency : class
            where TImplementation : TDependency;
    }
}
