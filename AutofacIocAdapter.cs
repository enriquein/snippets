using Autofac;
using Autofac.Integration.Mvc;
using Funq;
using ServiceStack.Configuration;
using System.Web;

namespace Csmu.Api.IocAdapters
{
    public class AutofacIocAdapter : IContainerAdapter
    {
        private readonly IContainer _autofacRootContainer;
        private readonly Container _funqContainer;

        public AutofacIocAdapter(IContainer autofacRootContainer, Container funqContainer)
        {
            // Register a RequestLifetimeScopeProvider (from Autofac.Integration.Mvc) with Funq
            var lifetimeScopeProvider = new RequestLifetimeScopeProvider(autofacRootContainer);
            funqContainer.Register<ILifetimeScopeProvider>(x => lifetimeScopeProvider);
            // Store the autofac application (root) container, and the funq container for later use
            _autofacRootContainer = autofacRootContainer;
            _funqContainer = funqContainer;
        }

        private ILifetimeScope ActiveScope
        {
            get
            {
                return HttpContext.Current == null
                            ? _autofacRootContainer
                            : _funqContainer.Resolve<ILifetimeScopeProvider>().GetLifetimeScope(x => new ContainerBuilder());
            }
        }

        public T Resolve<T>()
        {
            return ActiveScope.Resolve<T>();
        }

        public T TryResolve<T>()
        {
            T result;
            if (ActiveScope.TryResolve(out result))
            {
                return result;
            }
            return default(T);
        }
    }
}

/*
Then on ServiceStack Configure method:

            public override void Configure(Container container)
            {
                // Autofac config
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(typeof (Some).Assembly);

                // Hook Autofac into Funq
                IContainerAdapter adapter = new AutofacIocAdapter(builder.Build(), container);
                container.Adapter = adapter;
            }
*/