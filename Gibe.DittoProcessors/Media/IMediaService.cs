using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Media
{
	public interface IMediaService
	{
		IPublishedContent Media(int id);
		IEnumerable<IPublishedContent> Media(IEnumerable<int> ids);
	}
}