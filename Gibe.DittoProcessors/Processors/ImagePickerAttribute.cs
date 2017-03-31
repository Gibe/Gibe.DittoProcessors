using System;
using Gibe.DittoProcessors.Media;
using Gibe.DittoProcessors.Media.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class ImagePickerAttribute : InjectableProcessorAttribute
	{
		public Func<IMediaService> MediaService => Inject<IMediaService>();
		private readonly Type _type;

		public ImagePickerAttribute(Type type = null)
		{
			_type = type ?? typeof(MediaImageModel);
		}

		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return Convert.ChangeType(MediaService().GetImage(_type, id), _type);
		}
	}
}