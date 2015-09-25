/************************************************************************************
 * 
 *  Umbraco Data Layer
 *  MIT Licensed work
 *  ©2008 Ruben Verborgh
 * 
 ***********************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using PostgreSQLClient = Npgsql;

namespace umbraco.DataLayer.SqlHelpers.PostgreSQL
{
    /// <summary>
    /// Sql Helper for a Postgres database.
    /// </summary>
    public class PostgreSQLHelper : SqlHelper<PostgreSQLClient.NpgsqlParameter>
    {
        /// <summary>SQL parser that replaces the SQL-Server specific tokens by their Postgres equivalent.</summary>
        private PostgreSQLParser m_SqlParser = new PostgreSQLParser();

        /// <summary>Initializes a new instance of the <see cref="PostgresHelper"/> class.</summary>
        /// <param name="connectionString">The connection string.</param>
        public PostgreSQLHelper(string connectionString)
            : base(connectionString)
        {
            m_Utility = new PostgreSQLUtility(this);
        }

        /// <summary>
        /// Creates a new parameter for use with this specific implementation of ISqlHelper.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <returns>A new parameter of the correct type.</returns>
        /// <remarks>Abstract factory pattern</remarks>
        public override IParameter CreateParameter(string parameterName, object value)
        {
            return new PostgreSQLParameter(parameterName, value);
        }

        /// <summary>Converts a the command before executing.</summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>The original command text.</returns>
        protected override string ConvertCommand(string commandText)
        {
            return m_SqlParser.Parse(commandText);
        }

        /// <summary>Executes a command that returns a single value.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value of the command.</returns>
        protected override object ExecuteScalar(string commandText, PostgreSQLClient.NpgsqlParameter[] parameters)
        {
            var conn = new PostgreSQLClient.NpgsqlConnection(ConnectionString);
            object returnValue;

            try
            {
                conn.Open();
                var cmd = new PostgreSQLClient.NpgsqlCommand(commandText, conn);
                cmd.Parameters.AddRange(parameters);

                returnValue = cmd.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return returnValue;
        }

        /// <summary>Executes a command and returns the number of rows affected.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The number of rows affected by the command.
        /// </returns>
        protected override int ExecuteNonQuery(string commandText, PostgreSQLClient.NpgsqlParameter[] parameters)
        {
            var conn = new PostgreSQLClient.NpgsqlConnection(ConnectionString);
            int returnValue;

            try
            {
                conn.Open();
                var cmd = new PostgreSQLClient.NpgsqlCommand(commandText, conn);
                cmd.Parameters.AddRange(parameters);

                returnValue = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return returnValue;
        }

        /// <summary>Executes a command and returns a records reader containing the results.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// A data reader containing the results of the command.
        /// </returns>
        protected override IRecordsReader ExecuteReader(string commandText,
            PostgreSQLClient.NpgsqlParameter[] parameters)
        {
            var conn = new PostgreSQLClient.NpgsqlConnection(ConnectionString);
            PostgreSQLClient.NpgsqlDataReader returnValue;

            try
            {
                conn.Open();
                var cmd = new PostgreSQLClient.NpgsqlCommand(commandText, conn);
                cmd.Parameters.AddRange(parameters);

                returnValue = cmd.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return new PostgreSQLDataReader(returnValue);
        }

        /// <summary>Converts the scalar value to the given type.</summary>
        /// <typeparam name="T">Desired type of the value.</typeparam>
        /// <param name="scalar">A scalar returned by ExecuteScalar.</param>
        /// <returns>The scalar, converted to type T.</returns>
        protected override T ConvertScalar<T>(object scalar)
        {
            if (scalar == null)
                return default(T);

            switch (typeof(T).FullName)
            {
                case "System.Boolean": return (T)(object)((1).Equals(scalar));
                case "System.Guid": return (T)(object)(Guid.Parse(scalar.ToString()));
                default: return base.ConvertScalar<T>(scalar);
            }
        }

		/// <summary>
		/// Creates a concatenation fragment for use in an SQL query.
		/// </summary>
		/// <param name="values">The values that need to be concatenated</param>
		/// <returns>The SQL query fragment.</returns>
		/// <remarks>SQL Server uses a+b, MySql uses concat(a,b), Oracle uses a||b...</remarks>
		public override string Concat(params string[] values)
		{
			// mysql has a concat function
			return "concat(" + string.Join(",", values) + ")";
		}
	}
}
