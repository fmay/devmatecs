using System;
using Crotchety.Services;
using Microsoft.Extensions.Configuration;
using Neo4j.Driver;

namespace Crotchety.Repository.Db
{
    public class DatabaseService
    {
        private static DatabaseService instance = new DatabaseService();
        public IDriver? Driver { get; private set; }
        private string databaseName = "";

        private DatabaseService() { }

        public static DatabaseService Instance
        {
            get { return instance; }
        }

        public void Init(IConfiguration configuration)
        {
            var dbConfig = configuration.GetSection("Database");
            var dbUri = dbConfig["Uri"] ?? "";
            var dbUsername = dbConfig["Username"] ?? "";
            var dbPassword = dbConfig["Password"] ?? "";
            Driver = GraphDatabase.Driver(dbUri, AuthTokens.Basic(dbUsername, dbPassword));
            databaseName = dbConfig["DatabaseName"] ?? "";
        }

        public void TestMe()
        {
            Console.WriteLine("Hello");
        }


        public async Task<List<IRecord>> ReadTx(String query)
        {
            List<IRecord> result;
            // REFACTOR : throw exception
            if (Driver == null) return new List<IRecord>();
            var session = Driver.AsyncSession(SessionConfigBuilder.ForDatabase(databaseName));
            try
            {
                result = await session.ReadTransactionAsync(async tx =>
                {
                    var data = new List<string>();
                    var reader = await tx.RunAsync(query);
                    var lst = await reader.ToListAsync();
                    return lst;
                });
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            return result;
        }

        //public List<Record> WriteTx(String query)
        //{
        //    Config config = Config.getInstance();
        //    Session session = driver.session(SessionConfig.forDatabase(config.dbUserName));
        //    return session.writeTransaction(tx-> {
        //        Result result = tx.run(query);
        //        return result.list();
        //    });
        //}

    }
}

