using System;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace Gibe.DittoServices.ModelConverters
{
	public class DittoModelConverter : IModelConverter
	{
		public T ToModel<T>(IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null) where T : class
		{
			return content.As<T>(processorContexts: contexts);
		}

		public object ToModel(Type type, IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null)
		{
			return content.As(type, processorContexts: contexts);
		}

		public IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes, IEnumerable<DittoProcessorContext> contexts = null) where T : class
		{
			return nodes.Select(node => node.As<T>(processorContexts: contexts)).ToList();
		}

		public IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> nodes, IEnumerable<DittoProcessorContext> contexts = null)
		{
			return nodes.Select(node => node.As(type, processorContexts: contexts)).ToList();
		}
	}
}