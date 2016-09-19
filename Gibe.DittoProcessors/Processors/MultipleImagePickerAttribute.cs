using Gibe.DittoProcessors.Media;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class MultipleImagePickerAttribute : DittoProcessorAttribute
	{
		[Inject]
		public IMediaService MediaService { private get; set; }

		
		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			int id = int.TryParse(Value.ToString(), out id) ? id : 0;

			return id == 0 ? null : MediaService.GetImages(id);
		}
	}
}