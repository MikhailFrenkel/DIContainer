using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Models
{
    public class RegisteredType
    {
        public Type Dependency { get; set; }

        public object Implementation { get; set; }

        public bool IsSingleton { get; set; } = false;
    }
}
