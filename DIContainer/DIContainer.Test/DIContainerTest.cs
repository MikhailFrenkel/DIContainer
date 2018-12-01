using System.Collections.Generic;
using DIContainer.Extensions;
using DIContainer.Test.Classes;
using DIContainer.Test.Classes.Base;
using DIContainer.Test.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DIContainer.Test
{
    //TODO: check generic

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

        [TestMethod]
        public void TestMethod5()
        {
            var conf = new DIConfiguration();
            conf.Register<BarBase, AbstrBar>().AsSingleton();
            conf.Register<FooBase, AbstrFoo>();

            var container = new DIContainer(conf);

            var bar = container.Resolve<BarBase>();
            var foo = container.Resolve<FooBase>();

            var fl = foo.BarBase.Equals(bar);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var conf = new DIConfiguration();
            conf.Register<IFoo, Foo>();
            conf.Register<IBar, CycBar>();

            var container = new DIContainer(conf);

            var bar = container.Resolve<IBar>();
        }

        [TestMethod]
        public void TestMethod7()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();
            conf.Register<IFoo, Foo>().InstancePerRequest();
            conf.Register<IService, Service>();
            conf.Register<IServiceGeneric<IService>, ServiceGeneric<IService>>();

            var container = new DIContainer(conf);

            var serviceGeneric = container.Resolve<IServiceGeneric<IService>>();
        }

        [TestMethod]
        public void TestMethod8()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();
            conf.Register<IFoo, Foo>().InstancePerRequest();
            conf.Register<IService, Service>();
            conf.Register(typeof(IServiceGeneric<>), typeof(ServiceGeneric<>));

            var container = new DIContainer(conf);

            var serviceGeneric = container.Resolve<IServiceGeneric<IService>>();
        }

        [TestMethod]
        public void WhyThisWork()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();
            conf.Register<IFoo, Foo>().InstancePerRequest();
            conf.Register<IService, Service>();
            conf.Register(typeof(IServiceGeneric<>), typeof(ServiceGeneric<>));

            var container = new DIContainer(conf);

            var serviceGeneric = container.Resolve<IServiceGeneric<IService>>();

            //При получении конструктора возвращает конструктор класса Service?
            var serviceGeneric2 = container.Resolve<IServiceGeneric<ServiceBase>>();
        }

        [TestMethod]
        public void TestMethod9()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>();
            conf.Register<IBar, AnotherBar>();
            conf.Register<IFoo, Foo>();
            conf.Register<IFoo, AnotherFoo>();

            var container = new DIContainer(conf);

            var fooCollection = container.Resolve<ICollection<IFoo>>();
        }
    }
}
