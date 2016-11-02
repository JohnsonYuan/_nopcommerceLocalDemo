using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 我的注释 : autofac 文档
// autofac 基本用法
// var builder = new ContainerBuilder(); builder.RegisterType<ConsoleOutput>().As<IOutput>();
// container = builder.Build();  //构建容器
// using (var scope = container.BeginLifetimeScope()) {IDateWriter datawriter = container.Resolve<IDateWriter>();}

/// resolve optional
/// http://docs.autofac.org/en/latest/resolve/index.html?highlight=ResolveOptional
/// resolve keyed, named(ComponentNotRegisteredException will throw when resolve if you don't specify key or name)
/// (identify services by a string name or by an object key.)
/// Named services are simply keyed services that use a string as a key, so the techniques described in the next section apply equally to named services.
/// builder.Register<OnlineState>().Named<IDeviceState>("online"); => var r = container.ResolveNamed<IDeviceState>("online");
/// public enum DeviceState { Online, Offline } builder.RegisterType<OnlineState>().Keyed<IDeviceState>(DeviceState.Online); => var r = container.ResolveKeyed<IDeviceState>(DeviceState.Online);
/// http://docs.autofac.org/en/latest/advanced/keyed-services.html
namespace Nop.Core.Infrastructure.DependencyManagement
{
    public class ContainerManager
    {
        private readonly IContainer _container;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Conainer</param>
        public ContainerManager(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Gets a container
        /// </summary>
        public IContainer Container { get { return _container; } }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no socpe specified
                scope = Scope();
            }
            return scope.Resolve(type);
        }

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved services</returns>
        public virtual T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<IEnumerable<T>>().ToArray();
            }
            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class
        {
            return ResolveUnregistered(typeof(T), scope) as T;
        }

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveUnregistered(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no socpe specified
                scope = Scope();
            }

            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parametersInstance = new List<object>();

                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType, scope);
                        if (service == null) throw new NopException("Unknown dependency");
                        parametersInstance.Add(service);
                    }
                    Activator.CreateInstance(type, parametersInstance.ToArray());
                }
                catch (NopException)
                {

                }
            }
            throw new NopException("No constructor  was found that had all the dependencies satisfied.");
        }

        /// <summary>
        /// Try to resolve srevice
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <param name="instance">Resolved service</param>
        /// <returns>Value indicating whether service has been successfully resolved</returns>
        public virtual bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance)
        {
            if (scope == null)
            {
                //no socpe specified
                scope = Scope();
            }
            return scope.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// Check whether some service is registered (can be resolved)
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Result</returns>
        public virtual bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no socpe specified
                scope = Scope();
            }
            return scope.IsRegistered(serviceType);
        }

        /// <summary>
        /// Resolve optional
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no socpe specified
                scope = Scope();
            }
            return scope.ResolveOptional(serviceType);
        }

        /// <summary>
        /// Get current scope
        /// </summary>
        /// <returns>Scope</returns>
        public ILifetimeScope Scope()
        {
            try
            {
                // 我的注释：AutofacDependencyResolver.Current.RequestLifetimeScope
                // https://github.com/autofac/Autofac.Mvc/blob/develop/src/Autofac.Integration.Mvc/AutofacDependencyResolver.cs
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                //we can get an exception here if RequestLifetimeScope is already disposed
                //for example, requested in or after "Application_EndRequest" handler
                //but note that usually it should never happen

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }
    }
}
