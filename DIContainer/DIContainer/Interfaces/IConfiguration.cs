using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using DIContainer.Models;

namespace DIContainer.Interfaces
{
    public interface IConfiguration
    {
        void Register<TImplementation>() where TImplementation : class;

        void Register<TDependency, TImplementation>()
            where TDependency : class
            where TImplementation : TDependency;

        RegisteredType GetRegisteredType(Type type);

        IEnumerable<RegisteredType> GetRegisteredTypes(Type type);
    }
}
