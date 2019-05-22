using Coding4fun.Common.Logging;
using It = Coding4fun.WebApi.Model.Request.GetCompaniesRequest;

namespace Coding4fun.WebApi.Model.Request
{
	public partial class GetCompaniesRequest : ILogSerializable
	{
		private static readonly LogJsonSerializer SERIALIZER;

		static GetCompaniesRequest()
		{
			SERIALIZER = new LogJsonSerializer(contractResolver =>
				contractResolver
					.SetKeyword<It>(it => it.CompanyStatusCode)
					.SetText<It>(it => it.CompanyName)
					.SetIgnore<It>(it => it.JsonSerializer)
			);
		}

		/// <inheritdoc />
		public LogJsonSerializer JsonSerializer { get; } = SERIALIZER;
	}
}