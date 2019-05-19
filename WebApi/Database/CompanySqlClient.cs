using System.Threading.Tasks;
using Common.Sql;
using Microsoft.Extensions.Configuration;
using WebApi.Model.Request;
using WebApi.Model.Response;

namespace WebApi.Database
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