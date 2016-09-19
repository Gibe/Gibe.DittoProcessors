using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gibe.DittoProcessors.Processors.Models;
using Gibe.UmbracoWrappers;
using Newtonsoft.Json;
using Ninject;
using Our.Umbraco.Ditto;
using Umbraco.Core.Logging;

namespace Gibe.DittoProcessors.Processors
{
	public class RelatedLinksAttribute : DittoProcessorAttribute
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public RelatedLinksAttribute()
		{
			_umbracoWrapper = DependencyResolver.Current.GetService<IUmbracoWrapper>();
		}

		public RelatedLinksAttribute(IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}
		
			var relatedLinks = JsonConvert.DeserializeObject<IEnumerable<RelatedLink>>(Value.ToString());
			return relatedLinks.Select(ConvertToRelatedLinkModel).Where(m=>m.Link != null).ToList();
		}

		private RelatedLinkModel ConvertToRelatedLinkModel(RelatedLink link)
		{
			var url = link.IsInternal && link.Internal.HasValue ?
				_umbracoWrapper.TypedContent(link.Internal.Value)?.Url 
				: link.Link;

			if (url == null)
			{
				LogHelper.Warn<RelatedLinksAttribute>($"Related link with caption {link.Caption} has invalid URL");
			}
			return new RelatedLinkModel
			{
				Caption = link.Caption,
				Link = url,
				NewWindow = link.NewWindow
			};
		}
	}

	internal class RelatedLink
	{
		public string Caption { get; set; }
		public string Link { get; set; }
		public bool NewWindow { get; set; }
		public bool IsInternal { get; set; }
		public int? Internal { get; set; }
	}
}