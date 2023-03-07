using System;
using Crotchety.Repository.Db;

public class VersionRepository
{
	public static async Task<string> Execute()
	{
		var dbSvc = DatabaseService.Instance;
        var query = "call dbms.components() yield name, versions, edition unwind versions as version return name, version, edition;";
		var result = await dbSvc.ReadTx(query);
		return result[0]["name"] + "; " + result[0]["version"] + "; " + result[0]["edition"];
    }
}
