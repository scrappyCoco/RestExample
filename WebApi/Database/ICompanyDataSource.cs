using System.Threading.Tasks;
using Coding4fun.WebApi.Model.Request;
using Coding4fun.WebApi.Model.Response;

namespace Coding4fun.WebApi.Database
{
	/// <summary>
	///     Источник данных по компаниям.
	/// </summary>
	public interface ICompanyDataSource
	{
		/// <summary>
		///     Получение списка компаний.
		/// </summary>
		/// <param name="getCompaniesRequest">Параметры фильтрации компании.</param>
		/// <returns>Список компаний.</returns>
		Task<GetCompaniesResponse> GetCompaniesAsync(GetCompaniesRequest getCompaniesRequest);
	}
}