namespace Coding4fun.Common.Logging
{
	/// <summary>
	///     Сущность, которая должна логироваться.
	/// </summary>
	public interface ILogSerializable
	{
		/// <summary>
		///     Serializer для логов.
		/// </summary>
		LogJsonSerializer JsonSerializer { get; }
	}
}