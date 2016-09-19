using Gibe.UmbracoWrappers;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class VideoPickerAttribute : DittoProcessorAttribute
	{
		public IUmbracoWrapper UmbracoWrapper { get; set; }
		
		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return UmbracoWrapper.TypedMedia(id)?.Url;
		}
	}
}