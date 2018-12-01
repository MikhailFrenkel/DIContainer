using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes
{
    internal class ServiceGeneric<T> : IServiceGeneric<T> where T : IService
    {
        public IService Service { get; }

        public ServiceGeneric(IService service)
        {
            Service = service;
        }
    }
}
