using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.DittoProcessors.Processors
{
	public class ParentAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			return Context.Content.Parent;
		}
	}
}
