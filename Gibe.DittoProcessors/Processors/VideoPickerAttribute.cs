using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class VideoPickerAttribute : MediaAttribute
	{
		public override object ProcessValue()
		{
			var media = base.ProcessValue() as IPublishedContent;
			return media?.Url;
		}
	}
}