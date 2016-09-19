using System;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Gibe.DittoServices.ModelConverters
{
	public interface IModelConverter
	{
		T ToModel<T>(IPublishedContent content) where T : class;

		object ToModel(Type type, IPublishedContent content);

		IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes) where T : class;

		IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> content);
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

		public T ToModel<T>(IPublishedContent content) where T : class
		{
			return (T)_model;
		}

		public object ToModel(Type type, IPublishedContent content)
		{
			return _model;
		}

		public IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes) where T : class
		{
			return (IEnumerable<T>)_models;
		}

		public IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> content)
		{
			return _models;
		}
	}
}