using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using DIContainer.Interfaces;
using DIContainer.Models;

namespace DIContainer
{
    public class DIContainer : IDIContainer
    {
        private Stack<Type> _stack = new Stack<Type>();
        private readonly IConfiguration _conf;
        private static readonly object Sync = new object();

        public DIContainer(IConfiguration conf)
        {
            _conf = conf;
        }

        public TDependency Resolve<TDependency>() where TDependency : class
        {
            lock (Sync)
            {
                return ResolveType<TDependency>();
            }
        }

        private TDependency ResolveType<TDependency>() where TDependency : class 
        {
            var type = typeof(TDependency);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return ResolveCollection<TDependency>();
            }

            var registeredType = _conf.GetRegisteredType(type);
            if (registeredType != null)
            {
                return GetInstance<TDependency>(registeredType);
            }

            return null;
        }

        private TDependency ResolveCollection<TDependency>()
        {
            var nestedType = typeof(TDependency).GenericTypeArguments.FirstOrDefault();
            var registeredType = _conf.GetRegisteredType(nestedType);

            if (registeredType == null)
            {
                throw new Exception($"Not registered type: {nestedType?.FullName}");
            }

            var collection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(nestedType));

            var registeredTypes = _conf.GetRegisteredTypes(nestedType);
            foreach (var type in registeredTypes)
            {
                collection.Add(GetInstance(type.Dependency, type));
            }

            return (TDependency)collection;
        }

        private TDependency GetInstance<TDependency>(RegisteredType registeredType)
        {
            var type = typeof(TDependency);

            if (type.IsValueType)
            {
                return Activator.CreateInstance<TDependency>();
            }

            if (registeredType.IsSingleton)
            {
                if (registeredType.Implementation != null)
                {
                    return (TDependency) registeredType.Implementation;
                }
            }

            return (TDependency)GetInstance(type, registeredType);
        }

        private object GetInstance(Type type, RegisteredType registeredType)
        {
            if (_stack.Contains(type))
            {
                throw new Exception("circular dependency");
            }

            _stack.Push(type);

            var constructor = type.GetConstructors()
                                .OrderByDescending(x => x.GetParameters().Length)
                                .FirstOrDefault();

            var parameters = GetConstructorParameters(constructor);

            registeredType.Implementation = Activator.CreateInstance(type, parameters);

            _stack.Pop();

            return registeredType.Implementation;
        }

        private object[] GetConstructorParameters(ConstructorInfo constructor)
        {
            var parameters = new List<object>();

            foreach (var parameter in constructor.GetParameters())
            {
                var registeredType = _conf.GetRegisteredType(parameter.ParameterType);
                if (registeredType == null)
                {
                    throw new Exception($"Not registered type {parameter.Name}");
                }

                parameters.Add(GetInstance(parameter.ParameterType, registeredType));
            }

            return parameters.ToArray();
        }
    }
}
