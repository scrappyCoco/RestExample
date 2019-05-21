using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Common
{
	public class MethodExecutor
	{
		private ConnectionInfo _connectionInfo;
		private ILogger _logger;
		private HashSet<ActionKind> _logKinds;
		private IMemoryCache _memoryCache;

		public MethodExecutor SetLogger(ILogger logger, params ActionKind[] logKinds)
		{
			_logger = logger;
			_logKinds = new HashSet<ActionKind>(logKinds);
			return this;
		}

		public MethodExecutor SetCache(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
			return this;
		}

		public MethodExecutor SetConnectionInfo(ConnectionInfo connectionInfo)
		{
			_connectionInfo = connectionInfo;
			return this;
		}

		public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(
			Func<TRequest, Task<TResponse>> funcAsync,
			TRequest request,
			string className,
			string methodName)
			where TResponse : class
			where TRequest : ILogSerializable
		{
			Stopwatch stopwatch = _logger == null ? null : Stopwatch.StartNew();
			DateTime startTime = DateTime.Now;

			void WriteLog(ActionKind actionKind, Exception exception = null)
			{
				if (stopwatch == null) return;
				stopwatch.Stop();
				if (_logKinds.Any() && !_logKinds.Contains(actionKind)) return;
				
				LogRow logRow = new LogRow(
					className,
					methodName,
					request,
					actionKind,
					startTime,
					stopwatch.Elapsed,
					$"{_connectionInfo.RemoteIpAddress}:{_connectionInfo.RemotePort}",
					$"{_connectionInfo.LocalIpAddress}:{_connectionInfo.LocalPort}",
					exception);

				_logger.Write(logRow);
			}

			try
			{
				TResponse response;

				string requestHash = $"{className}_{methodName}_{request.GetObjectHash()}";

				if (_memoryCache != null)
				{
					response = _memoryCache.Get<TResponse>(requestHash);
					if (response != null)
					{
						WriteLog(ActionKind.Cache);

						return response;
					}
				}

				response = await funcAsync(request);
				_memoryCache?.Set(requestHash, response);

				WriteLog(ActionKind.Execute);

				return response;
			}
			catch (Exception exception)
			{
				WriteLog(ActionKind.Exception, exception);
			}

			return null;
		}
	}
}