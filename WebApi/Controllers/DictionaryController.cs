using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WebApi.Database;

namespace WebApi.Controllers
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
				.SetLogger(logger, ActionKind.Exception);
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