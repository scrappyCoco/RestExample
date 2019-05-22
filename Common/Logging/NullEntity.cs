namespace Coding4fun.Common.Logging
{
	public class NullEntity : ILogSerializable
	{
		private static readonly LogJsonSerializer SERIALIZER;

		static NullEntity()
		{
			SERIALIZER = new LogJsonSerializer(config => config.SetIgnore<NullEntity>(s => s.JsonSerializer));
			Instance = new NullEntity();
		}

		public static NullEntity Instance { get; }

		public LogJsonSerializer JsonSerializer { get; } = SERIALIZER;
	}
}