using DIContainer.Test.Interfaces;

namespace DIContainer.Test.Classes
{
    internal class ServiceGeneric : IServiceGeneric<IService>
    {
        public IService Service { get; }

        public ServiceGeneric(IService service)
        {
            Service = service;
        }
    }
}
