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

        public void Register<TImplementation>() where TImplementation : class
        {
            RegisterType(typeof(TImplementation), typeof(TImplementation).FullName);
        }

        public void Register<TDependency, TImplementation>()
            where TDependency : class 
            where TImplementation : TDependency
        {
            RegisterType(typeof(TDependency), typeof(TImplementation).FullName);
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

        private void RegisterType(Type type, string fullName)
        {
            var registerType = new RegisteredType()
            {
                Dependency = type,
                FullName = fullName
            };

            if (_container.TryGetValue(type, out var registeredTypes))
            {
                if (registeredTypes.All(x => x.FullName != registerType.FullName))
                {
                    registeredTypes.Add(registerType);
                }
            }
            else
            {
                _container.Add(type, new List<RegisteredType>()
                {
                    registerType
                });
            }
        }
    }
}
