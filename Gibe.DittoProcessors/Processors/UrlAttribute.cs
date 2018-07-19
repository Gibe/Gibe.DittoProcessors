using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class UrlAttribute : TestableDittoProcessorAttribute
	{

		public override object ProcessValue()
		{
			var content = Value as IPublishedContent;
			return content?.Url;
		}
	}
}
