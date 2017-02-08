using Umbraco.Core;
using Umbraco.Core.Services;

namespace Gibe.DittoProcessors.Wrappers
{
	public class UserServiceWrapper : IUserServiceWrapper
	{
		public IUserService GetUserService()
		{
			return ApplicationContext.Current.Services.UserService;
		}
	}
}
