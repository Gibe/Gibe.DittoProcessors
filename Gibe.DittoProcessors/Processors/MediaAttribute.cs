using System;
using Gibe.DittoProcessors.Media;

namespace Gibe.DittoProcessors.Processors
{
	public class MediaAttribute : InjectableProcessorAttribute
	{
		public Func<IMediaService> MediaService => Inject<IMediaService>();
	
		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}
			return MediaService().Media(id);
		}
	}
}