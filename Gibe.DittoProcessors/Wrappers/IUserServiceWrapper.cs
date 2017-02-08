using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Services;

namespace Gibe.DittoProcessors.Wrappers
{
	public interface IUserServiceWrapper
	{
		IUserService GetUserService();
	}
}
