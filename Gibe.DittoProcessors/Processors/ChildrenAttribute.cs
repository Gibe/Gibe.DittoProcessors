using System.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Processors
{
	public class ChildrenAttribute : DittoProcessorAttribute
	{
		private readonly string _docTypeAlias;
		
		protected ChildrenAttribute(string docTypeAlias)
		{
			_docTypeAlias = docTypeAlias;
		}

		public override object ProcessValue()
		{
			return Context.Content
				.Children()
				.Where(d => d.DocumentTypeAlias == _docTypeAlias)
				.ToList();
		}
		
	}
}