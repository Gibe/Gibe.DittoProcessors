using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors.Models
{
	public class MetaModel
	{
		[UmbracoProperty("metaTitle")]
		public string Title { get; set; }

		[UmbracoProperty("metaDescription")]
		public string Description { get; set; }

		[UmbracoProperty("metaKeywords")]
		public string Keywords { get; set; }

		[UmbracoProperty("metaNoIndex")]
		public bool RobotsNoindexFollow { get; set; }

		[DittoIgnore]
		public string RelPrevUrl { get; set; }

		[DittoIgnore]
		public string RelNextUrl { get; set; }

		[UmbracoProperty("Url")]
		public string CanonicalUrl { get; set; }

		public bool ShowInSitemap { get; set; }
	}
}
