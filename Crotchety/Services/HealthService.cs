using System;
using Crotchety.Repository.Db;

namespace Crotchety.Services
{
	public class HealthService
	{
        public static async Task<string> Database()
        {
            return await VersionRepository.Execute();
        }
    }
}

