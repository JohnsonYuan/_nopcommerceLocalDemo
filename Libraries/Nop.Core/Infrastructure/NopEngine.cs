using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure.DependencyManagement;

namespace Nop.Core.Infrastructure
{
    /// <summary>
    /// Engine
    /// </summary>
    public class NopEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Utilitiess

        /// <summary>
        /// Run startup tasks
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterDependencies(NopConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            this._containerManager = new ContainerManager(container);

            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.

            //dependencies
            var typeFinder = new WebAppTypeFinder();
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<NopConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            var drInstances = new List<IDependencyRegister>();
            foreach (var startUpTaskType in drTypes)
                drInstances.Add((IDependencyRegister)Activator.CreateInstance(startUpTaskType));
            drInstances.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var dependencyRegister in drInstances)
                dependencyRegister.Register(builder, typeFinder, config);
            builder.Update(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize(NopConfig config)
        {
            //register dependencies
            RegisterDependencies(config);

            //startup tasks
            RunStartupTasks();
        }

        public object Resolve(Type type)
        {
            return _containerManager.Resolve(type);
        }

        public T Resolve<T>() where T : class
        {
            return _containerManager.Resolve<T>();
        }

        public T[] ResolveAll<T>()
        {
            return _containerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Container manager
        /// </summary>
        public ContainerManager ContainerManager
        {
            get
            {
                return _containerManager;
            }
        }

        #endregion
    }
}
