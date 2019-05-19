using System.Threading.Tasks;
using WebApi.Model.Request;
using WebApi.Model.Response;

namespace WebApi.Database
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