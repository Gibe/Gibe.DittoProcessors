using System;

namespace Gibe.DittoProcessors.Processors
{
	public class StringIsNotEmptyAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			if (Value == null) throw new NullReferenceException();

			var stringValue = Value as string;

			return !String.IsNullOrEmpty(stringValue);
		}
	}
}
