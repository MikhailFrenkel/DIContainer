using DIContainer.Extensions;
using DIContainer.Test.Classes;
using DIContainer.Test.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DIContainer.Test
{
    [TestClass]
    public class DIContainerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();
            conf.Register<IFoo, Foo>().InstancePerRequest();
            conf.Register<IService, Service>();

            var container = new DIContainer(conf);

            var bar = container.Resolve<IBar>();
            var foo = container.Resolve<IFoo>();
            var service = container.Resolve<IService>();

            var fl = foo.Bar.Equals(bar);
            var fl1 = service.Foo.Equals(foo);
            var fl2 = service.Bar.Equals(bar);
        }
    }
}
