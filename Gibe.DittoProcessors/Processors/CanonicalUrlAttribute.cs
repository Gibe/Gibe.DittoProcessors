using Umbraco.Core;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Processors
{
	public class CanonicalUrlAttribute : TestableDittoProcessorAttribute
	{
		private string _homeDocTypeAlias;
	
		public CanonicalUrlAttribute(string homeDocTypeAlias = "home") 
		{
			_homeDocTypeAlias = homeDocTypeAlias;
		}
		
		public override object ProcessValue() => Context.Content.DocumentTypeAlias.InvariantEquals(_homeDocTypeAlias)
			? Context.Content.UrlAbsolute().Replace(Context.Content.Url, "/")
			: Context.Content.UrlAbsolute();
	}
}
