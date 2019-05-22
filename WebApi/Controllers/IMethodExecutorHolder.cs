using Coding4fun.Common;

namespace Coding4fun.WebApi.Controllers
{
	public interface IMethodExecutorHolder
	{
		MethodExecutor MethodExecutor { get; }
	}
}