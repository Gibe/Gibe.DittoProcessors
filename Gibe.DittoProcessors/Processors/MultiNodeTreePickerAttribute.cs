using System;
using System.Linq;
using Gibe.UmbracoWrappers;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class MultiNodeTreePickerAttribute : DittoProcessorAttribute
	{
		[Inject]
		public IUmbracoWrapper UmbracoWrapper { private get; set; }
		
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
				.Select(UmbracoWrapper.TypedContent);
		}
	}
}
