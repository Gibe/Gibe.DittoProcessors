using System;
using Gibe.DittoProcessors.Processors;
using System.Text.RegularExpressions;

namespace Gibe.DittoProcessors.Processors
{
	public class StringToCssClassAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			if (Value == null) throw new NullReferenceException();

			var stringValue = Value as string;

			Regex unwantedChars = new Regex(@"[!""#$%&'()\*\+,\./:;<=>\?@\[\\\]^`{\|}~]");
			stringValue = unwantedChars.Replace(stringValue, string.Empty);

			stringValue = stringValue.Replace("  ", " ");

			return stringValue.Replace(" ", "-").ToLower();
		}
	}
}
