using System;

namespace Gibe.DittoProcessors.Processors
{
	public class RadioboxAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			if (Value == null) throw new NullReferenceException();

			return umbraco.library.GetPreValueAsString(Convert.ToInt16(Value));
		}
	}
}
