using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coding4fun.WebApi.Database
{
	/// <summary>
	///     Источник справочных данных.
	/// </summary>
	public interface IDictionaryDataSource
	{
		/// <summary>
		///     Получение списка статусов.
		/// </summary>
		/// <returns>Список статусов.</returns>
		Task<List<KeyValuePair<string, string>>> GetStatusesAsync();
	}
}