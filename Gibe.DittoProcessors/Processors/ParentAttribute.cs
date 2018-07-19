using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.DittoProcessors.Processors
{
	public class CurrentContentParentAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			return Context.Content.Parent;
		}
	}
}
