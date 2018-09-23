using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Injection;

namespace unitykram.tests
{
    [TestClass]
    public class CompositionTest
    {
        [TestMethod]
        public void SimpleComposition()
        {
            var container = new UnityContainer();
            container.RegisterType<IDependee, Dependee>(new InjectionProperty(nameof(IDependee.Dependency)));
            container.RegisterType<IDependency, Dependency>();
            var dependee =  container.Resolve<IDependee>();
            
            Assert.IsNotNull(dependee);
            Assert.IsNotNull(dependee.Dependency);

        }

        [TestMethod]
        public void ConfigurationComposition()
        {
            var mapping = new ExeConfigurationFileMap(){ExeConfigFilename = "unity.config"};
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(mapping, ConfigurationUserLevel.None);
            var container = new UnityContainer();

            if (!(configuration.GetSection("unity") is UnityConfigurationSection section))
            {
                Assert.Fail("section named unity is not of type UnityConfigurationSection");
                return;
            }
            
            container.LoadConfiguration(section);
            
            var dependee =  container.Resolve<IDependee>();
            
            Assert.IsNotNull(dependee);
            Assert.IsNotNull(dependee.Dependency);
        }
    }
}