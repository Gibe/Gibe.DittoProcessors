using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;

namespace Gibe.DittoProcessors.Processors
{
	public class ContentPickerToUrlAttribute : TestableDittoProcessorAttribute
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
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}
			
			return _umbracoWrapper.TypedContent(id)?.Url;
		}
	}
}