using System.Collections.Generic;
using System.Threading.Tasks;
using Coding4fun.Common;
using Coding4fun.Common.Logging;
using Coding4fun.WebApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Coding4fun.WebApi.Controllers
{
	/// <summary>
	///     Справочники.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class DictionaryController : ControllerBase, IMethodExecutorHolder
	{
		private readonly IDictionaryDataSource _dictionaryDataSource;

		/// <inheritdoc />
		public DictionaryController(
			IDictionaryDataSource dictionaryDataSource,
			IMemoryCache memoryCache,
			ILogger<DictionaryController> logger)
		{
			_dictionaryDataSource = dictionaryDataSource;
			MethodExecutor = new MethodExecutor()
				.SetCache(memoryCache)
				.SetLogger(logger, ActionKind.Exception, ActionKind.Execute);
		}

		public MethodExecutor MethodExecutor { get; }

		/// <summary>
		///     Список статусов.
		/// </summary>
		[HttpGet(nameof(GetStatuses))]
		[ActionName(nameof(GetStatuses))]
		[ProducesResponseType(typeof(List<KeyValuePair<string, string>>), StatusCodes.Status200OK)]
		public async Task<ActionResult> GetStatuses()
		{
			return await this.ExecuteAsync(_dictionaryDataSource.GetStatusesAsync);
		}
	}
}