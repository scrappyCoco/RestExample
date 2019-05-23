namespace Coding4fun.WebApi.Model.Request
{
	/// <summary>
	///     Запрос на получение количества компаний.
	/// </summary>
	public partial class GetCompaniesCountRequest : ICompanyFilter
	{
		/// <summary>
		///     Инициализация запроса.
		/// </summary>
		/// <param name="companyFilter">Фильтр компаний.</param>
		public GetCompaniesCountRequest(ICompanyFilter companyFilter)
		{
			CompanyName = companyFilter.CompanyName;
			CompanyStatusCode = companyFilter.CompanyStatusCode;
		}

		/// <inheritdoc />
		public string CompanyName { get; set; }

		/// <inheritdoc />
		public string CompanyStatusCode { get; set; }
	}
}