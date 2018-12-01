using System;
using System.Collections.Generic;
using System.Linq;
using DIContainer.Interfaces;
using DIContainer.Models;

namespace DIContainer
{
    public class DIConfiguration : IConfiguration
    {
        private readonly Dictionary<Type, ICollection<RegisteredType>> _container = 
            new Dictionary<Type, ICollection<RegisteredType>>();

        public RegisteredType Register(Type dependency, Type implementation)
        {
            return RegisterType(dependency, implementation);
        }

        public RegisteredType Register<TImplementation>() where TImplementation : class
        {
            return RegisterType(typeof(TImplementation), typeof(TImplementation));
        }

        public RegisteredType Register<TDependency, TImplementation>()
            where TDependency : class 
            where TImplementation : TDependency
        {
            return RegisterType(typeof(TDependency), typeof(TImplementation));
        }

        public RegisteredType GetRegisteredType(Type type)
        {
            return _container.TryGetValue(type, out var registeredTypes)
                ? registeredTypes.FirstOrDefault()
                : null;
        }

        public IEnumerable<RegisteredType> GetRegisteredTypes(Type type)
        {
            return _container.TryGetValue(type, out var registeredTypes) ? registeredTypes : null;
        }

        private RegisteredType RegisterType(Type dependencyType, Type implementationType)
        {
            CheckImplementationType(implementationType);

            var registerType = new RegisteredType()
            {
                Dependency = dependencyType,
                Implementation = implementationType
            };

            if (_container.TryGetValue(dependencyType, out var registeredTypes))
            {
                if (registeredTypes.All(x => x.Implementation != implementationType))
                {
                    registeredTypes.Add(registerType);
                }
            }
            else
            {
                _container.Add(dependencyType, new List<RegisteredType>()
                {
                    registerType
                });
            }

            return registerType;
        }

        private void CheckImplementationType(Type type)
        {
            if (type.IsInterface)
            {
                throw new Exception($"Type {type.FullName} cannot be an interface");
            }

            if (type.IsAbstract)
            {
                throw new Exception($"Type {type.FullName} cannot be an abstract");
            }
        }
    }
}
