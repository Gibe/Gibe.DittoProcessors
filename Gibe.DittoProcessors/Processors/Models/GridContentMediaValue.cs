using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors.Models
{
	public class GridContentMediaValue
	{
		public int Id { get; set; }

		[UmbracoProperty("umbracoFile")]
		public string Image { get; set; }

		public string Caption { get; set; }
	}
}