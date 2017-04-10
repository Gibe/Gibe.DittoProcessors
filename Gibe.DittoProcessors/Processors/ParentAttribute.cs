using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.DittoProcessors.Processors
{
	public class ParentAttribute : TestableDittoProcessorAttribute
	{
		private readonly uint _parentDepth;

		public ParentAttribute(uint parentDepth = 1)
		{
			_parentDepth = parentDepth;
		}

		public override object ProcessValue()
		{
			var content = Context.Content;
			for (var i = 0; i < _parentDepth; i++)
			{
				content = content.Parent;
			}

			return content;
		}
	}
}
