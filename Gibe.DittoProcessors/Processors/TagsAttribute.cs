using System.Collections.Generic;
using System.Linq;

namespace Gibe.DittoProcessors.Processors
{
	public class TagsAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
		  if (Value is IEnumerable<string>)
		  {
		    return Value;
		  }
			return string.IsNullOrEmpty(Value?.ToString()) ? Enumerable.Empty<string>() : Value.ToString().Split(',');
		}
	}
}