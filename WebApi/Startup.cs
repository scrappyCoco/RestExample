using System;
using System.IO;
using System.Reflection;
using Coding4fun.WebApi.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Coding4fun.WebApi
{
	internal class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			Environment = env;
		}

		private IHostingEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMemoryCache(options =>
			{
				options.ExpirationScanFrequency = TimeSpan.FromMinutes(30d);
				options.CompactionPercentage = 0.30;
			});

			services.AddLogging(builder =>
			{
				string configFileName = $"log4net.{Environment.EnvironmentName}.config";

				builder
					.ClearProviders()
					.AddLog4Net(configFileName);
			});

			services.AddSingleton<ICompanyDataSource, CompanyDataSqlClient>();
			services.AddSingleton<IDictionaryDataSource, DictionarySqlClient>();

			services.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.Formatting = Formatting.Indented)
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSwaggerGen(swaggerGenOptions =>
			{
				swaggerGenOptions.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});

				string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				swaggerGenOptions.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			app.UseSwagger();
			//app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}