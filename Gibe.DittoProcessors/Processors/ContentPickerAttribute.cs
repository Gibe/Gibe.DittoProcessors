using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class ContentPickerAttribute : TestableDittoProcessorAttribute
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public ContentPickerAttribute()
		{
			_umbracoWrapper = DependencyResolver.Current.GetService<IUmbracoWrapper>();
		}

		public ContentPickerAttribute(IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}
		
		public override object ProcessValue()
		{
			if (Value is IPublishedContent)
			{
				return Value;
			}

			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return _umbracoWrapper.TypedContent(id);
		}
	}
}
