using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Extension;
using Unity.Lifetime;
using Unity.Registration;
using Unity.Resolution;
using WpfPrismUtilsTest.Dummies;

namespace WpfPrismUtilsTest {
    class DummyUnityContainer : IUnityContainer {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type type, string name, object instance, LifetimeManager lifetime)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type, string name, params ResolverOverride[] resolverOverrides)
        {
            if (type.Name.Equals("TestUserControl"))
            {
                return new TestUserControl();
            } else if (type.Name.Equals("TestWindow"))
            {
                return new TestWindow();
            }
            return null;
        }

        public object BuildUp(Type type, object existing, string name, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer AddExtension(UnityContainerExtension extension)
        {
            throw new NotImplementedException();
        }

        public object Configure(Type configurationInterface)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer CreateChildContainer()
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(Type type, string name)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer Parent { get; }
        public IEnumerable<IContainerRegistration> Registrations { get; }
    }
}
