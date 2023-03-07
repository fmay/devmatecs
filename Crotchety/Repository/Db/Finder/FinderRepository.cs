using System;
using Crotchety.Domain.Finder;
namespace Crotchety.Repository.Db.Finder
{
	public class FinderRepository
	{
        public static async Task<FinderResponse> Execute(FinderQuery query)
        {
            var dbSvc = DatabaseService.Instance;
            var x = "call dbms.components() yield name, versions, edition unwind versions as version return name, version, edition;";
            var result = await dbSvc.ReadTx(x);
            return new FinderResponse();
        }
	}
}

