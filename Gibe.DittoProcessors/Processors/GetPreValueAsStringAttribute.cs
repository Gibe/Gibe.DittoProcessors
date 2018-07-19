using System;
using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;

namespace Gibe.DittoProcessors.Processors
{
	public class GetPreValueAsStringAttribute : InjectableProcessorAttribute
	{
		public Func<IUmbracoHelperWrapper> UmbracoHelperWrapper => Inject<IUmbracoHelperWrapper>();
		
		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return UmbracoHelperWrapper().GetPreValueAsString(id);
		}
	}
}