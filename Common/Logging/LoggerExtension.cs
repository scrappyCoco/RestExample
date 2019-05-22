using System.IO;
using Microsoft.Extensions.Logging;

namespace Coding4fun.Common.Logging
{
	internal static class LoggerExtension
	{
		public static void Write(this ILogger logger, LogRow logRow)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				logRow.Request.JsonSerializer.Serialize(stringWriter, logRow);
				string logRowJson = stringWriter.ToString();

				logger.Log(LogLevel.Information, logRowJson);
			}
		}
	}
}