using System;
using Neo4j.Driver;
using Crotchety.Repository.Db;

namespace Crotchety.Services
{
	public class ConfigService
	{
        public string DbUri { get; private set; } = "";
        public string DbUsername { get; private set; } = "";
        public string DbPassword { get; private set; } = "";
        public string DbDatabaseName { get; private set; } = "";

        private static ConfigService instance = new ConfigService();

        private ConfigService() {
        }


        public static ConfigService Instance
        {            
            get { return instance; }
        }

        public void LoadConfig(IConfiguration configuration)
        {
            var dbConfig = configuration.GetSection("Database");
            DbUri = dbConfig["Uri"] ?? "";
            DbUsername = dbConfig["Username"] ?? "";
            DbPassword = dbConfig["Password"] ?? "";
            DbDatabaseName = dbConfig["DatabaseName"] ?? "";
        }


    }
}

