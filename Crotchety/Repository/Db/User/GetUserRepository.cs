using System;
using Crotchety.Domain;
using Crotchety.Core;
using Neo4j.Driver;

namespace Crotchety.Repository.Db.User
{
	public class GetUserRepository
	{
		public GetUserRepository()
		{
		}

		public static async Task<Crotchety.Domain.User> Execute(string id)
		{
            var dbSvc = DatabaseService.Instance;
            string query = "MATCH (user:User {uid: '" + id + "'})" +
                    "OPTIONAL MATCH (user:User)-[:SKILL]->(skills)" +
                    "OPTIONAL MATCH (user:User)-[:PROFILE]->(profile)" +
                    "RETURN user, profile, collect(distinct skills) as skills";
            var result = await dbSvc.ReadTx(query);
            var user = new Domain.User();

            // Process User
            var userProperties = result[0]["user"].As<INode>().Properties;
            if (userProperties != null)
            {
                user.uid = userProperties["uid"].As<string>();
                user.hasAvatar = userProperties["hasAvatar"].As<string>() == "true";
                user.hasAvatar = userProperties["hasBanner"].As<string>() == "true";
                Enum.TryParse(userProperties["userType"].As<string>(), out user.userType);
            }

            // Process Profile
            var profileProperties = result[0]["profile"].As<INode>().Properties;
            if (profileProperties != null)
            {
                user.profile.contact_email = profileProperties["contact_email"].As<string>();
                user.profile.contact_public_email = profileProperties["contact_public_email"].As<string>();
                user.profile.contact_first_name = profileProperties["contact_first_name"].As<string>();
                user.profile.contact_last_name = profileProperties["contact_last_name"].As<string>();
            }


            // Skills
            List<object> skills = result[0]["skills"].As<List<object>>();
            var skillList = new List<Skill>();
            foreach (var skill in skills)
            {
                var skillProperties = skill.As<INode>();
                if (skillProperties != null)
                {
                    Skill skillItem = new Skill();
                    skillItem.topLevelId = skillProperties["topLevelId"].As<string>();
                    skillItem.skillId = skillProperties["skillId"].As<string>();
                    skillItem.level = skillProperties["level"].As<int>();
                    skillList.Add(skillItem);
                }
            }
            user.skills = skillList.ToArray();
            return user;
        }


    }
}

