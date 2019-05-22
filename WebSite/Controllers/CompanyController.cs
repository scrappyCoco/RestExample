using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Coding4fun.WebSite.Api;
using Coding4fun.WebSite.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Coding4fun.WebSite.Controllers
{
	public class CompanyController : Controller
	{
		private readonly string _apiUrl;

		public CompanyController(IConfiguration configuration)
		{
			_apiUrl = configuration.GetValue<string>("ApiUrl");
		}

		[HttpGet]
		public async Task<IActionResult> List(int? page, string name, string status)
		{
			HttpClient httpClient = new HttpClient();
			Client client = new Client(_apiUrl, httpClient);
			ICollection<KeyValuePairOfStringAndString> statuses = await client.GetStatusesAsync();
			GetCompaniesResponse companiesResponse = await client.GetCompaniesAsync(page, name, status);

			CompanyViewModel model = new CompanyViewModel
			{
				Statuses = statuses,
				Companies = companiesResponse.Companies,
				CurrentCompanyName = name,
				CurrentStatusCode = status,
				PagingInfo = new PagingInfo
				{
					CurrentPage = page ?? 1,
					// ReSharper disable once PossibleInvalidOperationException
					TotalItems = companiesResponse.Total.Value,
					ItemsPerPage = 30
				}
			};

			return View(model);
		}
	}
}