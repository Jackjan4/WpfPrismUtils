using System;
using System.Collections.Generic;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace WpfPrismUtilsTest.Dummies {
    internal class DummyUnityContainer : IUnityContainer {
        public void Dispose() {
            throw new NotImplementedException();
        }


        public object Resolve(Type type, string name, params ResolverOverride[] resolverOverrides) {
            if (type.Name.Equals("TestUserControl")) {
                return new TestUserControl();
            }

            return type.Name.Equals("TestWindow") ? new TestWindow() : null;
        }

        public object BuildUp(Type type, object existing, string name, params ResolverOverride[] resolverOverrides) {
            throw new NotImplementedException();
        }


        public IUnityContainer CreateChildContainer() {
            throw new NotImplementedException();
        }

        public bool IsRegistered(Type type, string name) {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type registeredType, Type mappedToType, string name, ITypeLifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterInstance(Type type, string name, object instance, IInstanceLifetimeManager lifetimeManager) {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterFactory(Type type, string name, Func<IUnityContainer, Type, string, object> factory, IFactoryLifetimeManager lifetimeManager) {
            throw new NotImplementedException();
        }

        public IUnityContainer Parent { get; }
        public IEnumerable<IContainerRegistration> Registrations { get; }
    }
}
