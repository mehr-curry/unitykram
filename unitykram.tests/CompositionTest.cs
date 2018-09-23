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
    }
}