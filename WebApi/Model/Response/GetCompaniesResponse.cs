using System.Collections.Generic;

namespace WebApi.Model.Response
{
	/// <summary>
	///     Список компаний.
	/// </summary>
	public class GetCompaniesResponse
	{
		/// <summary>
		///     Общее количество компаний.
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		///     Компании.
		/// </summary>
		public ICollection<Company> Companies { get; set; }
	}
}