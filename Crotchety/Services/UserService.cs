using System;
using Crotchety.Domain;
using Crotchety.Repository.Db.User;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Crotchety.Services
{
    public class UserService
    {
        public static async Task<string> GetUsers()
        {
            var result = await GetUsersRepository.Execute();
            return Newtonsoft.Json.JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        public static async Task<string> GetUser(string id)
        {
            var result = await GetUserRepository.Execute(id);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}

