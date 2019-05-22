using Coding4fun.Common.Logging;
using It = Coding4fun.WebApi.Model.Request.GetCompaniesCountRequest;

namespace Coding4fun.WebApi.Model.Request
{
	public partial class GetCompaniesCountRequest : ILogSerializable
	{
		private static readonly LogJsonSerializer SERIALIZER;

		static GetCompaniesCountRequest()
		{
			SERIALIZER = new LogJsonSerializer(config =>
				config
					.SetKeyword<It>(it => it.CompanyStatusCode)
					.SetText<It>(it => it.CompanyName)
					.SetIgnore<It>(it => it.JsonSerializer)
			);
		}

		public string RemoteIpAddress { get; set; }
		public string LocalIpAddress { get; set; }

		/// <inheritdoc />
		public LogJsonSerializer JsonSerializer { get; } = SERIALIZER;
	}
}