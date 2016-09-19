using System;
using Umbraco.Core.Models;

namespace Gibe.DittoServices
{
	public interface IUmbracoContentService
	{
		T GetUmbracoContentModel<T>(IPublishedContent currentPage) where T : class;
		object GetUmbracoContentModel(Type type, IPublishedContent currentPage);
	}
}
