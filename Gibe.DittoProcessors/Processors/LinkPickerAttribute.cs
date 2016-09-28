using Gibe.DittoProcessors.Processors.Models;
using Gibe.UmbracoWrappers;
using Newtonsoft.Json;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class LinkPickerAttribute : DittoProcessorAttribute
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public LinkPickerAttribute(IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			var link = JsonConvert.DeserializeObject<LinkPickerModel>(Value.ToString());

			if (link.Id != default(int))
			{
				link.Url = _umbracoWrapper.TypedContent(link.Id).Url;
			}

			return link;
		}
	}
}