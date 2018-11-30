using System.Collections.Generic;
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

        [TestMethod]
        public void TestMethod2()
        {
            var conf = new DIConfiguration();
            conf.Register<Bar>();
            conf.Register<IBar>();

            var container = new DIContainer(conf);

            var bar = container.Resolve<Bar>();
            var ibar = container.Resolve<IBar>();
        }

        [TestMethod]
        public void TestMethod3()
        {
            var conf = new DIConfiguration();
            conf.Register<Service, Service>();
            conf.Register<IBar, IBar>();

            var container = new DIContainer(conf);

            var ibar = container.Resolve<IBar>();
            var service = container.Resolve<Service>();
        }

        [TestMethod]
        public void TestMethod4()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>();
            conf.Register<IBar, AnotherBar>().AsSingleton();
            conf.Register<IFoo, Foo>().AsSingleton();
            conf.Register<IFoo, AnotherFoo>();

            var container = new DIContainer(conf);

            var bars = container.Resolve<IEnumerable<IBar>>();
            var foos = container.Resolve<ICollection<IFoo>>();
        }
    }
}
