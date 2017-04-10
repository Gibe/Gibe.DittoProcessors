using System;
using Gibe.UmbracoWrappers;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class AncestorAttribute : InjectableProcessorAttribute
	{
		public Func<IUmbracoWrapper> UmbracoWrapper { get; set; } = Inject<IUmbracoWrapper>();

		private readonly int? _maxDepth;
		private readonly string _docTypeAlias;

		public AncestorAttribute()
		{
		}

		public AncestorAttribute(int? maxDepth)
		{
			_maxDepth = maxDepth;
		}

		public AncestorAttribute(string docTypeAlias)
		{
			_docTypeAlias = docTypeAlias;
		}

		public override object ProcessValue()
		{
			if (!string.IsNullOrEmpty(_docTypeAlias))
			{
				return UmbracoWrapper().AncestorOrSelf(Context.Content, _docTypeAlias);
			}
			else if (_maxDepth.HasValue)
			{
				return UmbracoWrapper().AncestorOrSelf(Context.Content, _maxDepth.Value);
			}
			return UmbracoWrapper().AncestorOrSelf(Context.Content);
		}
	}
}
