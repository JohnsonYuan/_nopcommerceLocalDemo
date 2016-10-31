using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Nop.Core;
using System.Data.Common;
using System.Linq;

namespace Nop.Data
{
    /// <summary>
    /// Objec context
    /// </summary>
    public class NopObjectContext : DbContext, IDbContext
    {
        #region Ctor

        public NopObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Execute store procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if(p.Direction == System.Data.ParameterDirection.InputOutput || p.Direction == System.Data.ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public int ExecuteSqlCommand(string sql, bool dbNotEnsureTransaction = false, int? timeout = default(int?), params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
        /// </summary>
        public bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }

            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
        /// </summary>
        public bool AutoDetecChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }

            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        #endregion
    }
}
