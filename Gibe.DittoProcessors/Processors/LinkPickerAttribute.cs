using System;
using System.Web.Mvc;
using Gibe.DittoProcessors.Processors.Models;
using Gibe.UmbracoWrappers;
using Newtonsoft.Json;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class LinkPickerAttribute : InjectableProcessorAttribute
	{
		public Func<IUmbracoWrapper> UmbracoWrapper => Inject<IUmbracoWrapper>();
		
		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			if (Value is LinkPickerModel)
			{
				return Value;
			}

			var link = JsonConvert.DeserializeObject<LinkPickerModel>(Value.ToString());
			if (link.Id != default(int))
			{
				var content = UmbracoWrapper().TypedContent(link.Id);
				if (content != null)
				{
					link.Url = content.Url;
				}
			}

			return link;
		}
	}
}