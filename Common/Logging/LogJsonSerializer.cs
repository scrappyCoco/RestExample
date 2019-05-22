using System;
using Newtonsoft.Json;

namespace Coding4fun.Common.Logging
{
	public sealed class LogJsonSerializer : JsonSerializer
	{
		public LogJsonSerializer(Action<LogContractResolver> configureAction = null)
		{
			LogContractResolver logContractResolver = new LogContractResolver();

			logContractResolver
				.SetKeyword<LogRow>(l => l.Host)
				.SetKeyword<LogRow>(l => l.Class)
				.SetKeyword<LogRow>(l => l.Method)
				.SetKeyword<LogRow>(l => l.ActionKind)
				.SetKeyword<LogRow>(l => l.ServiceAddress)
				.SetKeyword<LogRow>(l => l.ClientAddress)
				.SetText<LogRow>(l => l.Exception);

			configureAction?.Invoke(logContractResolver);

			ContractResolver = logContractResolver;
		}
	}
}