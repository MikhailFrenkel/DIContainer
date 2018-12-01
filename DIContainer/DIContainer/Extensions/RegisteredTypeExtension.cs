using DIContainer.Models;

namespace DIContainer.Extensions
{
    public static class RegisteredTypeExtension
    {
        public static RegisteredType AsSingleton(this RegisteredType registeredType)
        {
            registeredType.IsSingleton = true;
            return registeredType;
        }
        
        public static RegisteredType InstancePerRequest(this RegisteredType registeredType)
        {
            registeredType.IsSingleton = false;
            return registeredType;
        }
    }
}
