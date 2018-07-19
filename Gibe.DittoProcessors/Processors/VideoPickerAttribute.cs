using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class VideoPickerAttribute : DittoProcessorAttribute
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public VideoPickerAttribute()
		{
			_umbracoWrapper = DependencyResolver.Current.GetService<IUmbracoWrapper>();
		}

		public VideoPickerAttribute(IUmbracoWrapper umbracoWrapper)
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

			return _umbracoWrapper.TypedMedia(id)?.Url;
		}
	}
}