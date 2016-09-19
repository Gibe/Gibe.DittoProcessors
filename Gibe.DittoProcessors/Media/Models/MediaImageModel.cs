using System.ComponentModel;

namespace Gibe.DittoProcessors.Media.Models
{
	public class MediaImageModel
	{
		public string Url { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		[Description("the alt text is not held with the image and will not be automatically set")]
		public string Alt { get; set; }

		[Description("the caption is not held with the image and will not be automatically set")]
		public string Caption { get; set; }
	}
}