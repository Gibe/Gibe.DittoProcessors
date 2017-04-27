using System;
using System.Collections.Generic;
using System.Linq;
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
		public static FakeModelConverter Of(
			params (Predicate<IPublishedContent>, Func<IPublishedContent, Type, object>)[] options)
			=> new FakeModelConverter((i, t) => options.First(o => o.Item1(i)).Item2(i, t));

		public static FakeModelConverter Of<T>(T @object)
			=> new FakeModelConverter((i, t) => @object);

		private readonly Func<IPublishedContent, Type, object> _converter;

		public FakeModelConverter(Func<IPublishedContent, Type, object> converter)
		{
			_converter = converter;
		}

		public T ToModel<T>(IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null)
			where T : class
			=> (T)ToModel(typeof(T), content, contexts);

		public object ToModel(Type type, IPublishedContent content, IEnumerable<DittoProcessorContext> contexts = null)
			=> _converter(content, type);

		public IEnumerable<T> ToModel<T>(IEnumerable<IPublishedContent> nodes,
			IEnumerable<DittoProcessorContext> contexts = null) where T : class
			=> nodes.Select(n => ToModel<T>(n, contexts));

		public IEnumerable<object> ToModel(Type type, IEnumerable<IPublishedContent> content,
			IEnumerable<DittoProcessorContext> contexts = null)
			=> content.Select(c => ToModel(type, c, contexts));
	}
}