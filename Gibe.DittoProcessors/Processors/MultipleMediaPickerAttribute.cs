using System;
using System.Linq;
using System.Web.Mvc;
using Gibe.DittoProcessors.Media;

namespace Gibe.DittoProcessors.Processors
{
	public class MultipleMediaPickerAttribute : InjectableProcessorAttribute
	{
		public Func<IMediaService> MediaService => Inject<IMediaService>();
		
		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			var ids = (Value as string).Split(',').Select(int.Parse);
			return MediaService().Media(ids);
		}
	}
}