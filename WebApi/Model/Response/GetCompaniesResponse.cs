using System.Collections.Generic;
using JetBrains.Annotations;

namespace Coding4fun.WebApi.Model.Response
{
	/// <summary>
	///     Список компаний.
	/// </summary>
	[PublicAPI]
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