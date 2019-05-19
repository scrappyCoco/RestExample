using JetBrains.Annotations;

namespace WebApi.Model
{
	/// <summary>
	///     Компания.
	/// </summary>
	[PublicAPI]
	public class Company
	{
		/// <summary>
		///     Наименование.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Регистрационный номер.
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		///     Статус компании.
		/// </summary>
		public string Status { get; set; }
	}
}