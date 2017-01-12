using System;
using System.Text;
using System.Security.Cryptography;

namespace Gibe.DittoProcessors.Processors
{
	public class StringToMd5HashAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			if (Value == null) throw new NullReferenceException();

			var stringValue = Value as string;

			byte[] encodedValue = new UTF8Encoding().GetBytes(stringValue);

			byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedValue);

			// string representation (similar to UNIX format)
			return BitConverter.ToString(hashValue).Replace("-", string.Empty).ToLower();
		}
	}
}
