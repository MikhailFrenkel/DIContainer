using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using DIContainer.Models;

namespace DIContainer.Interfaces
{
    public interface IConfiguration
    {
        RegisteredType Register(Type dependency, Type implementation);

        RegisteredType Register<TImplementation>() where TImplementation : class;

        RegisteredType Register<TDependency, TImplementation>()
            where TDependency : class
            where TImplementation : TDependency;

        RegisteredType GetRegisteredType(Type type);

        IEnumerable<RegisteredType> GetRegisteredTypes(Type type);
    }
}
