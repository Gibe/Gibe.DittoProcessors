using System;
using System.Linq;
using Gibe.UmbracoWrappers;

namespace Gibe.DittoProcessors.Processors
{
	public class MultiNodeTreePickerAttribute : InjectableProcessorAttribute
	{
		public Func<IUmbracoWrapper> UmbracoWrapper => Inject<IUmbracoWrapper>();

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			var ids = Convert.ToString(Value)
				.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse);

			return ids
				.Where(i => i > 0)
				.Select(UmbracoWrapper().TypedContent);
		}
	}
}
