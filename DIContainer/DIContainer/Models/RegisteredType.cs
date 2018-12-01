using System;

namespace DIContainer.Models
{
    public class RegisteredType
    {
        internal Type Dependency { get; set; }

        internal Type Implementation { get; set; }

        internal object Instance { get; set; }

        internal bool IsSingleton { get; set; } = false;
    }
}
