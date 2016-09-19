using System.Web.Mvc;
using Ninject;
using Umbraco.Core.Services;

namespace Gibe.DittoProcessors.Processors
{
	public class UserPickerAttribute : TestableDittoProcessorAttribute
	{
		private readonly IUserService _userService;

		public UserPickerAttribute()
		{
			_userService = DependencyResolver.Current.GetService<IUserService>();
		}

		public UserPickerAttribute(IUserService userService)
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

			return _userService.GetUserById(id);
		}
	}
}