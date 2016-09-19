using Gibe.DittoProcessors.Media;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class ImagePickerAttribute : DittoProcessorAttribute
	{
		[Inject]
		public IMediaService MediaService { private get; set; }
		
		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return MediaService.GetImage(id);
		}
	}
}