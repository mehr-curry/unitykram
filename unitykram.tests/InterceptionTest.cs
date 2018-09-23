using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.PolicyInjection.Pipeline;

namespace unitykram.tests
{
    [TestClass]
    public class InterceptionTest
    {
        [TestMethod]
        public void SimpleInterception()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<IDependee, Dependee>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingAspect>(),
                new InjectionProperty(nameof(IDependee.Dependency)));
            container.RegisterType<IDependency, Dependency>();

            var dependee = container.Resolve<IDependee>();
            Assert.IsNotNull(dependee);
            Assert.IsNotNull(dependee.Dependency);
        }
    }

    public class LoggingAspect : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Assert.AreEqual($"get_{nameof(IDependee.Dependency)}", input.MethodBase.Name);
            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute { get; } = true;
    }
}