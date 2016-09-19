using System.Web.Mvc;
using Gibe.DittoProcessors.Media;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class ImagePickerAttribute : DittoProcessorAttribute
	{
		private readonly IMediaService _mediaService;

		public ImagePickerAttribute(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		public ImagePickerAttribute()
		{
			_mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return _mediaService.GetImage(id);
		}
	}
}