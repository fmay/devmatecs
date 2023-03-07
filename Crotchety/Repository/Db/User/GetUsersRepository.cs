using System;
using Crotchety.Domain;
using Neo4j.Driver;


namespace Crotchety.Repository.Db.User
{
	public class GetUsersRepository
	{
		public GetUsersRepository()
		{
		}

        public static async Task<List<Crotchety.Domain.User>> Execute()
		{
            var dbSvc = DatabaseService.Instance;
            var query = "MATCH (u:User) return u";
            var records = await dbSvc.ReadTx(query);
            var userList = new List<Crotchety.Domain.User>();
            foreach (var record in records)
            {
                var item = record["u"].As<INode>().Properties;
                var user = new Crotchety.Domain.User();
                if (item != null)
                {
                    user.uid = item["uid"].As<string>();
                    user.hasAvatar = item["hasAvatar"].As<string>() == "true";
                    user.hasAvatar = item["hasBanner"].As<string>() == "true";
                    Enum.TryParse(item["userType"].As<string>(), out user.userType);
                }
                userList.Add(user);
            }
            return userList;
        }

    }
}

