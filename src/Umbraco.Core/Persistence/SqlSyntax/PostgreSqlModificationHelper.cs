using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Core.Persistence.SqlSyntax
{
    class PostgreSqlModificationHelper
    {

        public static string intToBoolOperatorSQL
        {
            get
            {
                return @"
                CREATE OR REPLACE FUNCTION inttobool(integer, boolean) RETURNS boolean
                AS $$
                   SELECT CASE WHEN $1 = 0 and NOT $2 OR($1 <> 0 and $2) THEN true ELSE false END
                $$
                LANGUAGE sql;

                CREATE OR REPLACE FUNCTION inttobool(boolean, integer) RETURNS boolean
                AS $$
                   SELECT inttobool($2, $1);
                $$
                LANGUAGE sql;

                CREATE OR REPLACE FUNCTION notinttobool(boolean, integer) RETURNS boolean
                AS
                $$
                  SELECT NOT inttobool($2,$1);
                $$
                LANGUAGE sql;

                CREATE OR REPLACE FUNCTION notinttobool(integer, boolean) RETURNS boolean
                AS $$
	                SELECT NOT inttobool($1,$2);
                $$
                LANGUAGE sql;

		        DROP OPERATOR  IF EXISTS   = (integer,boolean);
		        DROP OPERATOR  IF EXISTS  <> (integer,boolean);
	 	        DROP OPERATOR  IF EXISTS   = (boolean,integer);
		        DROP OPERATOR  IF EXISTS  <> (boolean,integer);

                CREATE OPERATOR = (
                PROCEDURE = inttobool,
                LEFTARG = boolean,
                RIGHTARG = integer,
                COMMUTATOR = =,
                NEGATOR = <>
                );

                CREATE OPERATOR <> (
                PROCEDURE = notinttobool,
                LEFTARG = integer,
                RIGHTARG = boolean,
                COMMUTATOR = <>,
                NEGATOR = =
                );

                CREATE OPERATOR = (
                PROCEDURE = inttobool,
                LEFTARG = integer,
                RIGHTARG = boolean,
                COMMUTATOR = =,
                NEGATOR = <>
                );

                CREATE OPERATOR <> (
                PROCEDURE = notinttobool,
                LEFTARG = boolean,
                RIGHTARG = integer,
                COMMUTATOR = <>,
                NEGATOR = =
                );
";
                //update pg_cast set castcontext = 'i' where oid in (
                //select c.oid
                //from pg_cast c
                //inner join pg_type src on src.oid = c.castsource
                //inner join pg_type tgt on tgt.oid = c.casttarget
                //where src.typname like 'int%' and tgt.typname like 'bool%'
                //)

                
            }
        }

    }
}
