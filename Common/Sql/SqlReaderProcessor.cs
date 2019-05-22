using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Coding4fun.Common.Sql
{
	public static class SqlReaderProcessor
	{
		public static async Task<List<KeyValuePair<string, string>>> ProcessKeyValueListAsync(SqlDataReader reader)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

			while (await reader.ReadAsync())
			{
				string key = reader["Key"].ToString();
				string value = reader["Value"] == DBNull.Value ? null : reader["Value"].ToString();
				KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>(key, value);
				list.Add(keyValue);
			}

			return list;
		}
	}
}