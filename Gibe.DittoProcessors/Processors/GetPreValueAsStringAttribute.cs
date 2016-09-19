using Gibe.UmbracoWrappers;
using Ninject;

namespace Gibe.DittoProcessors.Processors
{
	public class GetPreValueAsStringAttribute : TestableDittoProcessorAttribute
	{
		[Inject]
		public IUmbracoHelperWrapper UmbracoHelperWrapper { private get; set; }
		
		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return UmbracoHelperWrapper.GetPreValueAsString(id);
		}
	}
}