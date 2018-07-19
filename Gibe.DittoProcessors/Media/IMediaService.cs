using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Media
{
	public interface IMediaService
	{
		IPublishedContent Media(int id);
		IPublishedContent Media(string udi);
		IEnumerable<IPublishedContent> Media(IEnumerable<int> ids);
		IEnumerable<IPublishedContent> Media(IEnumerable<string> udis);
	}
}