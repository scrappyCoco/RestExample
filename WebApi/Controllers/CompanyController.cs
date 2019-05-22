using System.Threading.Tasks;
using Coding4fun.Common;
using Coding4fun.WebApi.Database;
using Coding4fun.WebApi.Model.Request;
using Coding4fun.WebApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Coding4fun.WebApi.Controllers
{
	/// <summary>
	///     Компании.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase, IMethodExecutorHolder
	{
		private readonly ICompanyDataSource _companyDataSource;

		/// <inheritdoc />
		public CompanyController(
			ICompanyDataSource companyDataSource,
			ILogger<CompanyController> logger,
			IMemoryCache memoryCache)
		{
			_companyDataSource = companyDataSource;
			MethodExecutor = new MethodExecutor()
				.SetLogger(logger)
				.SetCache(memoryCache);
		}

		public MethodExecutor MethodExecutor { get; }

		/// <summary>
		///     Список компаний.
		/// </summary>
		/// <param name="page">Номер страницы.</param>
		/// <param name="name">Часть названия компании.</param>
		/// <param name="status">Статус.</param>
		[HttpGet(nameof(GetCompanies))]
		[ActionName(nameof(GetCompanies))]
		[ProducesResponseType(typeof(GetCompaniesResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCompanies(int? page, string name, string status)
		{
			GetCompaniesRequest getCompaniesRequest = new GetCompaniesRequest
			{
				PageNumber = page ?? 1,
				CompanyStatusCode = status,
				CompanyName = name
			};

			return await this.ExecuteAsync(_companyDataSource.GetCompaniesAsync, getCompaniesRequest);
		}
	}
}