using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Models
{
    public class RegisteredType
    {
        public Type Dependency { get; set; }

        public Type Implementation { get; set; }

        public object Instance { get; set; }

        public bool IsSingleton { get; set; } = false;
    }
}
