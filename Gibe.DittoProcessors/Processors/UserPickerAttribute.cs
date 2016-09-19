using Ninject;
using Umbraco.Core.Services;

namespace Gibe.DittoProcessors.Processors
{
	public class UserPickerAttribute : TestableDittoProcessorAttribute
	{
		[Inject]
		public IUserService UserService { private get; set; }
		
		public override object ProcessValue()
		{
			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return UserService.GetUserById(id);
		}
	}
}