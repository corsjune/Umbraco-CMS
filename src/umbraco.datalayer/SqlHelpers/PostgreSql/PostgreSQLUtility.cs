/************************************************************************************
 * 
 *  Umbraco Data Layer
 *  MIT Licensed work
 *  ©2008 Ruben Verborgh
 * 
 ***********************************************************************************/

using System;
using umbraco.DataLayer.Utility;
using umbraco.DataLayer.Utility.Installer;
using PostgreSQLClient = Npgsql;

namespace umbraco.DataLayer.SqlHelpers.PostgreSQL
{
    /// <summary>
    /// Utility for an PostgreSQL data source.
    /// </summary>
    [Obsolete("The legacy installers are no longer used and will be removed from the codebase in the future")]
    public class PostgreSQLUtility : DefaultUtility<PostgreSQLHelper>
    {
        #region Public Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLUtility"/> class.
        /// </summary>
        /// <param name="sqlHelper">The SQL helper.</param>
        public PostgreSQLUtility(PostgreSQLHelper sqlHelper)
            : base(sqlHelper)
        { }

        #endregion

        #region DefaultUtility Members

        /// <summary>
        /// Creates an installer.
        /// </summary>
        /// <returns>The PostgresInstaller installer.</returns>
        [Obsolete("The legacy installers are no longer used and will be removed from the codebase in the future")]
        public override IInstallerUtility CreateInstaller()
        {
            return new PostgreSQLInstaller(SqlHelper);
        }

        #endregion
    }
}
