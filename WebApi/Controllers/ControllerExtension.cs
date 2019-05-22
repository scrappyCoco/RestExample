using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Coding4fun.Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coding4fun.WebApi.Controllers
{
	/// <summary>
	///     Декоратор контроллера.
	/// </summary>
	internal static class ControllerExtension
	{
		/// <summary>
		///     Декоратор выполнения запроса без параметров.
		/// </summary>
		/// <param name="controller">Контроллер.</param>
		/// <param name="asyncFunc">Функция, выполняющая запрос.</param>
		/// <param name="callerMemberName">Название вызывающего метода.</param>
		/// <typeparam name="TResponse">Тип ответа.</typeparam>
		/// <typeparam name="TController">Тип вызывающего контроллера.</typeparam>
		/// <returns>Null - в случае ошибки, иначе ответ.</returns>
		public static async Task<ActionResult> ExecuteAsync<TController, TResponse>(
			this TController controller,
			Func<Task<TResponse>> asyncFunc,
			[CallerMemberName] string callerMemberName = null
		)
			where TResponse : class
			where TController : ControllerBase, IMethodExecutorHolder
		{
			return await controller.ExecuteAsync(r => asyncFunc(), NullEntity.Instance, callerMemberName);
		}

		/// <summary>
		///     Декоратор выполнения запроса.
		/// </summary>
		/// <param name="controller">Контроллер.</param>
		/// <param name="asyncFunc">Функция, выполняющая запрос.</param>
		/// <param name="request">Параметры запроса.</param>
		/// <param name="callerMemberName">Название вызывающего метода.</param>
		/// <typeparam name="TRequest">Тип запроса.</typeparam>
		/// <typeparam name="TResponse">Тип ответа.</typeparam>
		/// <typeparam name="TController">Тип вызывающего контроллера.</typeparam>
		/// <returns>Null - в случае ошибки, иначе ответ.</returns>
		public static async Task<ActionResult> ExecuteAsync<TController, TRequest, TResponse>(
			this TController controller,
			Func<TRequest, Task<TResponse>> asyncFunc, TRequest request,
			[CallerMemberName] string callerMemberName = null
		)
			where TController : ControllerBase, IMethodExecutorHolder
			where TResponse : class
			where TRequest : ILogSerializable
		{
			controller.MethodExecutor.SetConnectionInfo(controller.HttpContext.Connection);
			TResponse response = await controller.MethodExecutor.ExecuteAsync(asyncFunc, request,
				controller.GetType().Name, callerMemberName);

			if (response != null) return new OkObjectResult(response);

			return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
		}
	}
}