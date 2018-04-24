using Umbraco.Core;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Processors
{
	public class CanonicalUrlAttribute : TestableDittoProcessorAttribute
	{
		public override object ProcessValue() => Context.Content.DocumentTypeAlias.InvariantEquals("home")
			? Context.Content.UrlAbsolute().Replace(Context.Content.Url, "/")
			: Context.Content.UrlAbsolute();
	}
}
