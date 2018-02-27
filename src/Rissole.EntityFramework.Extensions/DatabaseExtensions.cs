using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Rissole.EntityFramework.Extensions
{
    public static class DatabaseExtensions
    {
        public static void EnsureMigrate(this DatabaseFacade databaseFacade)
        {
            databaseFacade.CreateDatabase();
            databaseFacade.CreateHistoryTable();
            databaseFacade.Migrate();
        }

        public static void CreateDatabase(this DatabaseFacade databaseFacade)
        {
            using (var dbConnection = databaseFacade.GetDbConnection())
            {
                if (string.IsNullOrEmpty(dbConnection.Database))
                    throw new NoNullAllowedException("database must be defined in connection");

                var database = dbConnection.Database;

                dbConnection.ClearDatabase();
                dbConnection.Open();

                string script = $"{ScriptConstants.CreateDatabaseIfNotExist} {database}";
                dbConnection.ExecuteNonQuery(script);
                dbConnection.ChangeDatabase(database);
            }
        }

        public static void CreateHistoryTable(this DatabaseFacade databaseFacade)
        {
            using (var dbConnection = databaseFacade.GetDbConnection())
            {
                dbConnection.Open();

                string script = $"{ScriptConstants.CreateTableIfNotExist} {ScriptConstants.MigrationsHistoryTable}";
                dbConnection.ExecuteNonQuery(script);
            }
        }

        private static void ClearDatabase(this IDbConnection dbConnection)
        {
            var connectionString = string.Join(";", dbConnection.ConnectionString.Split(';')
                .Select(x => x.Split('=').Select(y => y.Trim()).ToList())
                .Where(x => !x[0].Equals("database", StringComparison.CurrentCultureIgnoreCase))
                .Select(x => $"{x[0]}={x[1]}"));

            dbConnection.ConnectionString = connectionString;
        }

        private static void ExecuteNonQuery(this IDbConnection connection, string script)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}
