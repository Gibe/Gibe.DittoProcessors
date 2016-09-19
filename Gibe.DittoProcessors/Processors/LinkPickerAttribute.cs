using Gibe.DittoProcessors.Processors.Models;
using Newtonsoft.Json;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class LinkPickerAttribute : DittoProcessorAttribute
	{

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			return JsonConvert.DeserializeObject<LinkPickerModel>(Value.ToString());
		}
	}
}