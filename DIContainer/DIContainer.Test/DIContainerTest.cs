using System;
using System.Collections.Generic;
using System.Linq;
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
        public void IsSingleton()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();

            var container = new DIContainer(conf);

            var bar1 = container.Resolve<IBar>();
            var bar2 = container.Resolve<IBar>();

            Assert.AreEqual(bar1, bar2);
        }

        [TestMethod]
        public void IsNotSingleton()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().InstancePerRequest();

            var container = new DIContainer(conf);

            var bar1 = container.Resolve<IBar>();
            var bar2 = container.Resolve<IBar>();

            Assert.AreNotEqual(bar1, bar2);
        }

        [TestMethod]
        public void DependencyInjectionThroughConstructor()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>();
            conf.Register<IFoo, Foo>();

            var container = new DIContainer(conf);

            var foo = container.Resolve<IFoo>();

            Assert.IsNotNull(foo.Bar);
        }

        [TestMethod]
        public void AbstractClassDependency()
        {
            var conf = new DIConfiguration();
            conf.Register<BarBase, AbstrBar>();

            var container = new DIContainer(conf);

            var bar = container.Resolve<BarBase>();

            Assert.IsNotNull(bar);
        }

        [TestMethod]
        public void CyclicDependency()
        {
            var conf = new DIConfiguration();
            conf.Register<IFoo, Foo>();
            conf.Register<IBar, CycBar>();

            var container = new DIContainer(conf);

            try
            {
                var bar = container.Resolve<IBar>();
                Assert.Fail("Cannot be cyclic dependencies");
            }
            catch (Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
        }

        [TestMethod]
        public void NotRegisteredType()
        {
            var conf = new DIConfiguration();
            conf.Register<IFoo, Foo>();

            var container = new DIContainer(conf);

            try
            {
                var bar = container.Resolve<IBar>();
                Assert.Fail("Cannot create instance of not registered type");
            }
            catch (Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
        }

        [TestMethod]
        public void RegisterClassAsSelf()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>();
            conf.Register<IFoo, Foo>();
            conf.Register<Service>();

            var container = new DIContainer(conf);

            var service = container.Resolve<Service>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void RegisterInterfaceAsSelf()
        {
            try
            {
                var conf = new DIConfiguration();
                conf.Register<IBar>();

                var container = new DIContainer(conf);
            
                var bar = container.Resolve<IBar>();
                Assert.Fail("Cannot create instance of interface");
            }
            catch (Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
        }

        [TestMethod]
        public void RegisterAbstractClassAsSelf()
        {
            try
            {
                var conf = new DIConfiguration();
                conf.Register<BarBase>();

                var container = new DIContainer(conf);

                var bar = container.Resolve<BarBase>();
                Assert.Fail("Cannot create instance of abstract class");
            }
            catch (Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
        }

        [TestMethod]
        public void ResolveEnumerable()
        {
            var expected = 2;
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>();
            conf.Register<IBar, AnotherBar>();

            var container = new DIContainer(conf);

            var bars = container.Resolve<IEnumerable<IBar>>();
            Assert.AreEqual(expected, bars.Count());
        }

        [TestMethod]
        public void ResolveGenericType()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();
            conf.Register<IFoo, Foo>().InstancePerRequest();
            conf.Register<IService, Service>();
            conf.Register<IServiceGeneric<IService>, ServiceGeneric<IService>>();

            var container = new DIContainer(conf);

            var serviceGeneric = container.Resolve<IServiceGeneric<IService>>();
            Assert.IsNotNull(serviceGeneric.Service);
        }

        [TestMethod]
        public void ResolveOpenGenericType()
        {
            var conf = new DIConfiguration();
            conf.Register<IBar, Bar>().AsSingleton();
            conf.Register<IFoo, Foo>().InstancePerRequest();
            conf.Register<IService, Service>();
            conf.Register(typeof(IServiceGeneric<>), typeof(ServiceGeneric<>));

            var container = new DIContainer(conf);

            var serviceGeneric = container.Resolve<IServiceGeneric<IService>>();
            Assert.IsNotNull(serviceGeneric.Service);
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
    }
}
