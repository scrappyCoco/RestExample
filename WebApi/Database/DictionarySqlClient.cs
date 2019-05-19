using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Common.Sql;
using Microsoft.Extensions.Configuration;

namespace WebApi.Database
{
	internal class DictionarySqlClient : IDictionaryDataSource
	{
		private readonly SqlClient _sqlClient;

		public DictionarySqlClient(IConfiguration configuration)
		{
			_sqlClient = new SqlClient(configuration.GetConnectionString(DatabaseName.CompanyGbr.ToString()));
		}

		public async Task<List<KeyValuePair<string, string>>> GetStatusesAsync()
		{
			using (SqlCommand command = new SqlCommand("SELECT [Key], Value FROM DictionaryService.GetStatuses();"))
			{
				return await _sqlClient.GetListAsync(command, SqlReaderProcessor.ProcessKeyValueListAsync);
			}
		}
	}
}