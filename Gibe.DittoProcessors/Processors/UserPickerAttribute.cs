using System;
using System.Web.Mvc;
using Gibe.DittoProcessors.Wrappers;
using Ninject;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Services;

namespace Gibe.DittoProcessors.Processors
{
	public class UserPickerAttribute : InjectableProcessorAttribute
	{
		public Func<IUserServiceWrapper> UserServiceWrapper => Inject<IUserServiceWrapper>();
		
		public override object ProcessValue()
		{
			if (Value is IUser)
			{
				return Value;
			}

			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return UserServiceWrapper().GetUserService().GetUserById(id);
		}
	}
}