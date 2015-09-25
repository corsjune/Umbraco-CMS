/************************************************************************************
 * 
 *  Umbraco Data Layer
 *  MIT Licensed work
 *  ©2008 Ruben Verborgh
 * 
 ***********************************************************************************/

using System;
using PostgreSQLClient = Npgsql;

namespace umbraco.DataLayer.SqlHelpers.PostgreSQL
{
    /// <summary>
    /// Class that adapts a PostgreSQLDataReader to a RecordsReaderAdapter.
    /// </summary>
    public class PostgreSQLDataReader : RecordsReaderAdapter<Npgsql.NpgsqlDataReader>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgresDataReader"/> class.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        public PostgreSQLDataReader(Npgsql.NpgsqlDataReader dataReader) : base(dataReader) { }

        #endregion

        #region RecordsReaderAdapter Members

        /// <summary>
        /// Gets a value indicating whether this instance has records.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has records; otherwise, <c>false</c>.
        /// </value>
        public override bool HasRecords
        {
            get { return DataReader.HasRows; }
        }

        #endregion
    }
}
