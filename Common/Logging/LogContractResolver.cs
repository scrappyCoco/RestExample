using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Coding4fun.Common.Logging
{
	public class LogContractResolver : DefaultContractResolver
	{
		private readonly Dictionary<MemberInfo, ElasticSearchKind> _typeMappings =
			new Dictionary<MemberInfo, ElasticSearchKind>();

		internal LogContractResolver()
		{
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);

			if (!_typeMappings.TryGetValue(member, out ElasticSearchKind elasticSearchType)) return jsonProperty;

			if (elasticSearchType == ElasticSearchKind.Ignore)
				jsonProperty.Ignored = true;
			else
				jsonProperty.PropertyName += $"_{elasticSearchType.ToString().ToLower()}";

			return jsonProperty;
		}

		public LogContractResolver SetText<T>(Expression<Func<T, object>> property)
		{
			return Set(property, ElasticSearchKind.Text);
		}

		public LogContractResolver SetIgnore<T>(Expression<Func<T, object>> property)
		{
			return Set(property, ElasticSearchKind.Ignore);
		}

		public LogContractResolver SetKeyword<T>(Expression<Func<T, object>> property)
		{
			return Set(property, ElasticSearchKind.Keyword);
		}

		private LogContractResolver Set<T>(Expression<Func<T, object>> property,
			ElasticSearchKind elasticSearchKind)
		{
			MemberExpression memberExpression = (MemberExpression) property.Body;
			MemberInfo targetMemberInfo = memberExpression.Member;
			if (!_typeMappings.ContainsKey(targetMemberInfo))
				_typeMappings.Add(targetMemberInfo, elasticSearchKind);
			else
				_typeMappings[targetMemberInfo] = elasticSearchKind;

			return this;
		}
	}
}