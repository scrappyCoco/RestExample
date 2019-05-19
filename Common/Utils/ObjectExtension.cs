using Common.Log;
using Newtonsoft.Json;

namespace Common.Utils
{
	public static class ObjectExtension
	{
		private static readonly JsonSerializer SERIALIZER = JsonSerializer.Create();

		private static string ToJson(this object it)
		{
			switch (it)
			{
				case null:
					return "null";
				case ILogSerializable logSerializable:
					return logSerializable.JsonSerializer.SerializeToString(it);
				default:
					return SERIALIZER.SerializeToString(it);
			}
		}

		public static string GetObjectHash(this object it)
		{
			return it.GetType().FullName + it.ToJson().ToLowerInvariant().GetHashCode();
		}
	}
}