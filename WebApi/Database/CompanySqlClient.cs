using System.Threading.Tasks;
using Coding4fun.Common.Sql;
using Coding4fun.WebApi.Model.Request;
using Coding4fun.WebApi.Model.Response;
using Microsoft.Extensions.Configuration;

namespace Coding4fun.WebApi.Database
{
	internal partial class CompanyDataSqlClient : ICompanyDataSource
	{
		private readonly SqlClient _sqlClient;

		public CompanyDataSqlClient(IConfiguration configuration)
		{
			_sqlClient = new SqlClient(configuration.GetConnectionString(DatabaseName.CompanyGbr.ToString()));
		}

		/// <inheritdoc />
		public async Task<GetCompaniesResponse> GetCompaniesAsync(GetCompaniesRequest getCompaniesRequest)
		{
			return await GetCompaniesAsyncImpl(getCompaniesRequest);
		}
	}
}