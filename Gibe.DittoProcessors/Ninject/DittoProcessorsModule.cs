using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibe.DittoProcessors.Media;
using Gibe.DittoProcessors.Wrappers;
using Ninject.Modules;

namespace Gibe.DittoProcessors.Ninject
{
	public class DittoProcessorsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserServiceWrapper>().To<UserServiceWrapper>();
			Bind<IMediaService>().To<MediaService>();
		}
	}
}
