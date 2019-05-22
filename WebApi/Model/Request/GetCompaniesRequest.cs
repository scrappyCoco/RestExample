using JetBrains.Annotations;

namespace Coding4fun.WebApi.Model.Request
{
	/// <summary>
	///     Запрос на получение списка компаний.
	/// </summary>
	[PublicAPI]
	public partial class GetCompaniesRequest : IPageRequest, ICompanyFilter
	{
		/// <inheritdoc />
		public string CompanyName { get; set; }

		/// <inheritdoc />
		public string CompanyStatusCode { get; set; }

		/// <inheritdoc />
		public int ItemsPerPage { get; } = 30;

		/// <inheritdoc />
		public int PageNumber { get; set; }
	}
}