using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Configuration;
using Nop.Web.Infrastructure.Installation;
using System.Data.SqlClient;

namespace Nop.Web.Controllers
{
    public class InstallController : BasePublicController
    {
        #region Fields

        private readonly IInstallationLocalizationService _locService;
        private readonly NopConfig _config;

        #endregion

        #region Ctor

        public InstallController(IInstallationLocalizationService locService, NopConfig config)
        {
            this._locService = locService;
            this._config = config;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// A value indicating whether we use MARS (Multiple Active Result Sets)
        /// </summary>
        protected bool UseMars
        {
            get { return false; }
        }

        /// <summary>
        /// Checks if the specified database exists, returns true if database exists
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Returns true if the database exists.</returns>
        [NonAction]
        protected bool SqlServerDatabaseExists(string connectionString)
        {
            try
            {
                //just try to connect
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected string CreateConnectionString(bool trustedConnection,
            string serverName, string databaseName,
            string userName, string password, int timeout = 0)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = trustedConnection;
            builder.DataSource = serverName;
            builder.InitialCatalog = databaseName;
            if (!trustedConnection)
            {
                builder.UserID = userName;
                builder.Password = password;
            }
            builder.PersistSecurityInfo = false;
            if(this.UseMars)
            {
                builder.MultipleActiveResultSets = true;
            }
            if(timeout > 0)
            {
                builder.ConnectTimeout = timeout;
            }
            return builder.ConnectionString;
        }

        #endregion

        // GET: Install
        public ActionResult Index()
        {
            return Content("Hello");
        }
        // GET: Install
        [NonAction]
        public ActionResult Index2()
        {
            return Content("Hello");
        }
    }
}