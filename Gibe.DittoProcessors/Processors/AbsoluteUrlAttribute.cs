using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Processors
{
	public class AbsoluteUrlAttribute : TestableDittoProcessorAttribute
	{

		public override object ProcessValue()
		{
			var content = Value as IPublishedContent;
			return content.UrlAbsolute();
		}
	}
}
