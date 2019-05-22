using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Coding4fun.Common.Sql
{
	public class SqlClient
	{
		private readonly string _connectionString;

		public SqlClient(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<TObject> GetFromJsonAsync<TObject>(SqlCommand command)
			where TObject : class
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				command.Connection = connection;

				StringBuilder stringBuilder = new StringBuilder();

				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (reader.Read()) stringBuilder.Append(reader[0]);
				}

				connection.Close();

				if (stringBuilder.Length == 0) return null;

				using (StringReader stringReader = new StringReader(stringBuilder.ToString()))
				using (JsonTextReader jsonTextReader = new JsonTextReader(stringReader))
				{
					JsonSerializer jsonSerializer = JsonSerializer.Create();
					return jsonSerializer.Deserialize<TObject>(jsonTextReader);
				}
			}
		}

		public async Task<object> GetObjectAsync(SqlCommand command)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				command.Connection = connection;
				connection.Open();
				object resultObject = await command.ExecuteScalarAsync();
				connection.Close();

				return resultObject;
			}
		}

		public async Task<List<TResponse>> GetListAsync<TResponse>(SqlCommand command,
			ProcessListAsyncFunc<TResponse> sqlReaderProcess)
		{
			List<TResponse> entityList;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				command.Connection = connection;
				connection.Open();

				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					entityList = await sqlReaderProcess(reader);
				}

				connection.Close();
			}

			return entityList;
		}
	}
}