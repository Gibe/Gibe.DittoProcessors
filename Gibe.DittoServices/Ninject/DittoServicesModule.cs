using Gibe.DittoServices.ModelConverters;
using Ninject.Modules;

namespace Gibe.DittoServices.Ninject
{
	public class DittoServicesModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IModelConverter>().To<DittoModelConverter>();
			Bind<IUmbracoContentService>().To<UmbracoContentService>();
		}
	}
}
