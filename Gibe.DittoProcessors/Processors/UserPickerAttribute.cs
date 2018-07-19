using System.Web.Mvc;
using Gibe.DittoProcessors.Wrappers;
using Ninject;
using Umbraco.Core.Services;

namespace Gibe.DittoProcessors.Processors
{
	public class UserPickerAttribute : TestableDittoProcessorAttribute
	{
		private readonly IUserServiceWrapper _userService;

		public UserPickerAttribute()
		{
			_userService = DependencyResolver.Current.GetService<IUserServiceWrapper>();
		}

		public UserPickerAttribute(IUserServiceWrapper userService)
		{
			_userService = userService;
		}

		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return _userService.GetUserService().GetUserById(id);
		}
	}
}