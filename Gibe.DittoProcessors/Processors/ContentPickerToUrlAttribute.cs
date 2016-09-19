using Gibe.UmbracoWrappers;
using Ninject;

namespace Gibe.DittoProcessors.Processors
{
	public class ContentPickerToUrlAttribute : TestableDittoProcessorAttribute
	{
		[Inject]
		public IUmbracoWrapper UmbracoWrapper { private get; set; }

		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}
			
			return UmbracoWrapper.TypedContent(id)?.Url;
		}
	}
}