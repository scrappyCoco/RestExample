namespace WebApi.Model.Request
{
	/// <summary>
	///     Фильтры компании.
	/// </summary>
	public interface ICompanyFilter
	{
		/// <summary>
		///     Наименование компании.
		/// </summary>
		string CompanyName { get; set; }

		/// <summary>
		///     Код статуса компании.
		/// </summary>
		string CompanyStatusCode { get; set; }
	}
}