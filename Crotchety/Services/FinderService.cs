using System;
using Crotchety.Repository.Db.Finder;
using Crotchety.Domain.Finder;
using Newtonsoft.Json;
using FluentValidation;
namespace Crotchety.Services
{
	public class FinderService
	{
		public FinderService()
		{
		}

        public static async Task<string> RunQuery(string body)
        {
            var settings = new JsonSerializerSettings() { MissingMemberHandling= MissingMemberHandling.Error };
            FinderQuery? query = JsonConvert.DeserializeObject<FinderQuery>(body, settings);
            if (query == null)
                throw new ApplicationException("Missing body");
            FinderQueryValidator validator = new FinderQueryValidator();
            var sanitised = validator.Validate(query);
            if (!sanitised.IsValid)
            {
                var msg = "";
                var errors = sanitised.Errors.ToArray();
                foreach (FluentValidation.Results.ValidationFailure item in errors) {
                    msg += item.ErrorMessage + "\n";
                }
                throw new ApplicationException(msg);
            }
            var response = await FinderRepository.Execute(query);
            return JsonConvert.SerializeObject(response);
        }
    }
}

