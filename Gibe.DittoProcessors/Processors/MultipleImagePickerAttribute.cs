using System.Web.Mvc;
using Gibe.DittoProcessors.Media;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class MultipleImagePickerAttribute : DittoProcessorAttribute
	{
		private readonly IMediaService _mediaService;

		public MultipleImagePickerAttribute(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		public MultipleImagePickerAttribute()
		{
			_mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			int id = int.TryParse(Value.ToString(), out id) ? id : 0;

			return id == 0 ? null : _mediaService.GetImages(id);
		}
	}
}