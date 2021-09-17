using System;
using Autofac;
using UdemyCourseExample;
using Xunit;

namespace SingletonPattern
{
    public class Program
    {
        [Fact]
        public void TestThatSingletonIsOnlyOneInstance()
        {
            SingletonExample instance1 = SingletonExample.Instance;
            SingletonExample instance2 = SingletonExample.Instance;
            Assert.Equal(instance1, instance2);
            Assert.Equal(1, SingletonExample.InstanceCount);
        }

        [Fact]
        public void SingletonTotalPopulationTest()
        {
            SingletonRecordFinder recordFinder = new ();
            string[] names = new[] {"Seoul", "Manila"};
            int totalPopulation = recordFinder.GetTotalPopulation(names);
            Assert.Equal(17500000 + 14750000, totalPopulation);
        }

        [Fact]
        public void DependencyInjectionSingletonTest()
        {
            ContainerBuilder containerBuilder = new ();
            containerBuilder.RegisterType<DummyDatabase>().As<IDatabase>().SingleInstance();
            containerBuilder.RegisterType<DependencyInjectionSingeltonRecordFinder>();
            using IContainer container = containerBuilder.Build();
            DependencyInjectionSingeltonRecordFinder recordFinder = container.Resolve<DependencyInjectionSingeltonRecordFinder>();
            string[] names = new[] {"city1", "city2"};
            int totalPopulation = recordFinder.GetTotalPopulation(names);
            Assert.Equal(12, totalPopulation);
        }
    }
}
