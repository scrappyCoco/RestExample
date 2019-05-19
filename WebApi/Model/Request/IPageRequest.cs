namespace WebApi.Model.Request
{
	/// <summary>
	///     Paging.
	/// </summary>
	public interface IPageRequest
	{
		/// <summary>
		///     Количество компаний в результате.
		/// </summary>
		int ItemsPerPage { get; }

		/// <summary>
		///     Порядковый номер страницы.
		/// </summary>
		int PageNumber { get; set; }
	}
}