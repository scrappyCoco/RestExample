namespace Coding4fun.WebApi.Model.Request
{
	/// <summary>
	///     Расширения для <c>IPageRequest</c>.
	/// </summary>
	public static class PageRequestExtension
	{
		/// <summary>
		///     Получение смещения от начала.
		/// </summary>
		public static int GetOffset(this IPageRequest pageRequest)
		{
			return (pageRequest.PageNumber - 1) * pageRequest.ItemsPerPage;
		}
	}
}