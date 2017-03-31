using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Media.Models
{
	internal interface IMediaImageModel
	{
		string Url { get; set; }
		int Width { get; set; }
		int Height { get; set; }
	}

	public class MediaImageModel : IMediaImageModel
	{
		[UmbracoProperty("url")]
		public string Url { get; set; }

		[UmbracoProperty("umbracoWidth")]
		public int Width { get; set; }

		[UmbracoProperty("umbracoHeight")]
		public int Height { get; set; }
	}

	public class GridMediaImageModel : MediaImageModel
	{
		[DittoIgnore]
		public string Caption { get; set; }
	}
}