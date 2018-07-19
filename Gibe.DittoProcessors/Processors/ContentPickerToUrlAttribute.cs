using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class ContentPickerToUrlAttribute : ContentPickerAttribute
	{
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