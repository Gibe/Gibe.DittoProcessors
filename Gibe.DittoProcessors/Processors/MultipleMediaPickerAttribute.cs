using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.DittoProcessors.Media;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class MultipleMediaPickerAttribute : InjectableProcessorAttribute
	{
		public Func<IMediaService> MediaService => Inject<IMediaService>();
		
		public override object ProcessValue()
		{
			switch (Value)
			{
				case IEnumerable<IPublishedContent> _:
					return Value;
				case string value:
					var ids = value.Split(',').Select(int.Parse);
					return MediaService().Media(ids);
			}
			return null;
		}
	}
}