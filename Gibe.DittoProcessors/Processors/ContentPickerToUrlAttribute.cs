using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class ContentPickerToUrlAttribute : ContentPickerAttribute
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public ContentPickerToUrlAttribute()
		{
			_umbracoWrapper = DependencyResolver.Current.GetService<IUmbracoWrapper>();
		}

		public ContentPickerToUrlAttribute(IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}

		public override object ProcessValue()
		{
			var content = base.ProcessValue();
			if (content is IPublishedContent publishedContent)
			{
				return publishedContent.Url;
			}

			return null;
		}
	}
}