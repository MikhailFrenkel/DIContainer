using System;
using System.Collections.Generic;
using System.Text;
using DIContainer.Interfaces;

namespace DIContainer
{
    public class DIContainer : IDIContainer
    {
        private readonly IConfiguration _conf;

        public DIContainer(IConfiguration conf)
        {
            _conf = conf;
        }

        public TDependency Resolve<TDependency>() where TDependency : class
        {
            throw new NotImplementedException();
        }
    }
}
