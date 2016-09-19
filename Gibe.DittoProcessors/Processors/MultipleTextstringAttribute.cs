using System.Linq;

namespace Gibe.DittoProcessors.Processors
{
	public class MultipleTextstringAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			return string.IsNullOrEmpty(Value?.ToString()) 
				? null 
				: ((string[])Value).Where(x => !string.IsNullOrEmpty(x));
		}
	}
}