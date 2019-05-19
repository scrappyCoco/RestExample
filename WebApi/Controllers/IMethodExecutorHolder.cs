using Common;

namespace WebApi.Controllers
{
	public interface IMethodExecutorHolder
	{
		MethodExecutor MethodExecutor { get; }
	}
}