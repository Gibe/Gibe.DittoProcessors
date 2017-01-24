using System;
using System.Collections.Generic;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace Gibe.DittoServices.ModelConverters
{
	public interface IModelConverter
	{
		T ToModel<T>(IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null) where T : class;
		
		object ToModel(Type type, IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null);

		IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes, IEnumerable<DittoProcessorContext> contexts = null) where T : class;

		IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> content, IEnumerable<DittoProcessorContext> contexts = null);
	}

	public class FakeModelConverter : IModelConverter
	{
		private readonly IEnumerable<object> _models;
		private readonly object _model;

		public FakeModelConverter(IEnumerable<object> models)
		{
			_models = models;
		}

		public FakeModelConverter(object model)
		{
			_model = model;
		}

		public T ToModel<T>(IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null) where T : class
		{
			return (T)_model;
		}

		public object ToModel(Type type, IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null)
		{
			return _model;
		}

		public IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes, IEnumerable<DittoProcessorContext> contexts = null) where T : class
		{
			return (IEnumerable<T>)_models;
		}

		public IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> content, IEnumerable<DittoProcessorContext> contexts = null)
		{
			return _models;
		}
	}
}