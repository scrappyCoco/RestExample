using System.IO;
using Newtonsoft.Json;

namespace Common.Utils
{
	public static class JsonSerializerExtension
	{
		public static string SerializeToString(this JsonSerializer jsonSerializer, object obj)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				jsonSerializer.Serialize(stringWriter, obj);
				return stringWriter.ToString();
			}
		}
	}
}