using System;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace Gibe.DittoServices.ModelConverters
{
	public class DittoModelConverter : IModelConverter
	{
		public T ToModel<T>(IPublishedContent content) where T : class
		{
			return content.As<T>();
		}

		public object ToModel(Type type, IPublishedContent content)
		{
			return content.As(type);
		}

		public IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes) where T : class
		{
			return nodes.Select(node => node.As<T>()).ToList();
		}

		public IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> nodes)
		{
			return nodes.Select(node => node.As(type)).ToList();
		}
	}
}