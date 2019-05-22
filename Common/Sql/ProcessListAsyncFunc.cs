using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Coding4fun.Common.Sql
{
	public delegate Task<List<TResponse>> ProcessListAsyncFunc<TResponse>(SqlDataReader reader);
}