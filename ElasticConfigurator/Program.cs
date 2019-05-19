using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace ElasticConfigurator
{
	internal static class Program
	{
		private const string INDEX_TEMPLATE_NAME = "my_rest_log_template";
		private const string HOST = "http://elasticsearch:9200";

		private static async Task Main()
		{
			await CreateIndexTemplate();
		}

		private static async Task CreateIndexTemplate()
		{
			Console.WriteLine("Get ElasticSearch client");
			ElasticClient elasticClient = GetClient();

			Console.WriteLine("Check for index template exists");
			IExistsResponse existsResponse;
			do
			{
				existsResponse = await elasticClient.IndexTemplateExistsAsync(INDEX_TEMPLATE_NAME);
				if (existsResponse.Exists) return;

				await Task.Delay(1_000);
			} while (!existsResponse.IsValid);


			Console.WriteLine("Put index template");
			IPutIndexTemplateRequest indexTemplateRequest = new PutIndexTemplateRequest(INDEX_TEMPLATE_NAME);
			indexTemplateRequest.IndexPatterns = new[] {"my_rest_log*"};
			indexTemplateRequest.Settings = new IndexSettings(new Dictionary<string, object>
			{
				["number_of_shards"] = 10,
				["number_of_replicas"] = 0
			});
			indexTemplateRequest.Mappings = new TypeMapping();
			indexTemplateRequest.Mappings.DynamicTemplates = new DynamicTemplateContainer();
			indexTemplateRequest.Mappings.DynamicTemplates.Add("nested_template", new DynamicTemplate
			{
				MatchMappingType = "object",
				Match = "*_nested",
				Mapping = new NestedProperty()
			});
			indexTemplateRequest.Mappings.DynamicTemplates.Add("object_template", new DynamicTemplate
			{
				MatchMappingType = "object",
				Match = "*_object",
				Mapping = new ObjectProperty()
			});
			indexTemplateRequest.Mappings.DynamicTemplates.Add("text_template", new DynamicTemplate
			{
				MatchMappingType = "string",
				Match = "*_text",
				Mapping = new TextProperty()
			});
			indexTemplateRequest.Mappings.DynamicTemplates.Add("keyword_template", new DynamicTemplate
			{
				MatchMappingType = "string",
				Match = "*_keyword",
				Mapping = new KeywordProperty()
			});

			await elasticClient.PutIndexTemplateAsync(indexTemplateRequest);
		}

		private static ElasticClient GetClient()
		{
			IConnectionPool connectionPool = new SingleNodeConnectionPool(new Uri(HOST));
			IConnectionSettingsValues connectionSettingsValues = new ConnectionSettings(connectionPool);
			return new ElasticClient(connectionSettingsValues);
		}
	}
}