using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Coding4fun.Common.Utils;
using Coding4fun.WebApi.Model;
using Coding4fun.WebApi.Model.Request;
using Coding4fun.WebApi.Model.Response;

namespace Coding4fun.WebApi.Database
{
	internal partial class CompanyDataSqlClient
	{
		private async Task<GetCompaniesCountResponse> GetCompaniesCountAsync(GetCompaniesCountRequest companyFilter)
		{
			return await GetCompaniesCountAsyncImpl(companyFilter);
		}

		private async Task<GetCompaniesCountResponse> GetCompaniesCountAsyncImpl(ICompanyFilter companyFilter)
		{
			using (SqlCommand command = new SqlCommand("CompanyService.GetCompaniesCount"))
			{
				command.CommandType = CommandType.StoredProcedure;
				SetFilterParameters(command, companyFilter);
				GetCompaniesCountResponse companiesCountResponse = new GetCompaniesCountResponse
				{
					Count = (int) await _sqlClient.GetObjectAsync(command)
				};

				return companiesCountResponse;
			}
		}

		private async Task<GetCompaniesResponse> GetCompaniesAsyncImpl(GetCompaniesRequest getCompaniesRequest)
		{
			using (SqlCommand command = new SqlCommand("CompanyService.GetCompaniesJson"))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@Offset", getCompaniesRequest.GetOffset());
				command.Parameters.AddWithValue("@Count", getCompaniesRequest.ItemsPerPage);
				SetFilterParameters(command, getCompaniesRequest);

				GetCompaniesCountRequest getCompaniesCountRequest = new GetCompaniesCountRequest(getCompaniesRequest);
				Task<ICollection<Company>> companiesTask = _sqlClient.GetFromJsonAsync<ICollection<Company>>(command);
				Task<GetCompaniesCountResponse> countTask = GetCompaniesCountAsync(getCompaniesCountRequest);

				Task[] tasks = {companiesTask, countTask};
				foreach (Task task in tasks)
					if (task.Status == TaskStatus.Created)
						task.Start();

				await Task.WhenAll(tasks);

				GetCompaniesResponse getCompaniesResponse = new GetCompaniesResponse
				{
					Companies = companiesTask.Result,
					Total = countTask.Result.Count
				};

				return getCompaniesResponse;
			}
		}

		private void SetFilterParameters(SqlCommand command, ICompanyFilter companyFilter)
		{
			command.Parameters.AddWithValue("@CompanyName", companyFilter.CompanyName?.Trim() ?? (object) DBNull.Value);
			command.Parameters.AddWithValue("@StatusCode",
				companyFilter.CompanyStatusCode?.TryParseShort() ?? (object) DBNull.Value);
		}
	}
}