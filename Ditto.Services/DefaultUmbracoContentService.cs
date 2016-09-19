using System;
using Gibe.DittoServices.ModelConverters;
using Umbraco.Core.Models;

namespace Gibe.DittoServices
{
	public class UmbracoContentService : IUmbracoContentService
	{
		private readonly IModelConverter _modelConverter;

		public UmbracoContentService(IModelConverter modelConverter)
		{
			_modelConverter = modelConverter;
		}

		public T GetUmbracoContentModel<T>(IPublishedContent currentPage) where T : class
		{
			var model = _modelConverter.ToModel<T>(currentPage);

			return model;
		}

		public object GetUmbracoContentModel(Type type, IPublishedContent currentPage)
		{
			return _modelConverter.ToModel(type, currentPage);
		}
	}
}
