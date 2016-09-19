using System.Linq;

namespace Gibe.DittoProcessors.Processors
{
	public class TagsAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			return string.IsNullOrEmpty(Value?.ToString()) ? Enumerable.Empty<string>() : Value.ToString().Split(',');
		}
	}
}