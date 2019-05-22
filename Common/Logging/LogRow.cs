using System;
using Coding4fun.Common.Utils;
using JetBrains.Annotations;

namespace Coding4fun.Common.Logging
{
	[UsedImplicitly]
	internal class LogRow
	{
		internal LogRow(
			string @class,
			string method,
			ILogSerializable request,
			ActionKind actionKind,
			DateTime startTime,
			TimeSpan elapsedTime,
			string clientAddress,
			string serviceAddress,
			Exception exception = null)
		{
			Host = Environment.MachineName;
			ActionKind = actionKind.ToString();
			Exception = exception?.Message;
			StartTimeMs = startTime.ToEpoch();
			ElapsedTimeMs = (long) elapsedTime.TotalMilliseconds;
			Class = @class;
			Method = method;
			ClientAddress = clientAddress;
			ServiceAddress = serviceAddress;

			//if (request != NullEntity.Instance) Request = request;
			Request = request;
		}

		public string Host { get; }
		public string Class { get; }
		public string Method { get; }
		public long StartTimeMs { get; }
		public long ElapsedTimeMs { get; }
		public ILogSerializable Request { get; }
		public string Exception { get; }
		public string ActionKind { get; }
		public string ClientAddress { get; }
		public string ServiceAddress { get; }
	}
}