using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;

namespace Gibe.DittoProcessors.Processors
{
	public class GetPreValueAsStringAttribute : TestableDittoProcessorAttribute
	{
		private readonly IUmbracoHelperWrapper _umbracoHelperWrapper;

		public GetPreValueAsStringAttribute(IUmbracoHelperWrapper umbracoHelperWrapper)
		{
			_umbracoHelperWrapper = umbracoHelperWrapper;
		}

		public GetPreValueAsStringAttribute()
		{
			_umbracoHelperWrapper = DependencyResolver.Current.GetService<IUmbracoHelperWrapper>();
		}

		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return _umbracoHelperWrapper.GetPreValueAsString(id);
		}
	}
}