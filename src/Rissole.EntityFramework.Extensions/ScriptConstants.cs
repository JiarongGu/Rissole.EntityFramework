using System;
using System.Collections.Generic;
using System.Text;

namespace Rissole.EntityFramework.Extensions
{
    internal static class ScriptConstants
    {
        public const string MigrationsHistoryTable = "__EFMigrationsHistory (MigrationId nvarchar(150) NOT NULL, ProductVersion nvarchar(32) NOT NULL, PRIMARY KEY(MigrationId))";
        public const string CreateDatabaseIfNotExist = "CREATE DATABASE IF NOT EXISTS";
        public const string CreateTableIfNotExist = "CREATE TABLE IF NOT EXISTS";
    }
}
