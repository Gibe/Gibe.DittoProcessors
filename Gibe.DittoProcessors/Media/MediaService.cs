using System.Collections.Generic;
using System.Linq;
using Gibe.UmbracoWrappers;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Media
{
	public class MediaService : IMediaService
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public MediaService(IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}
		
		public IPublishedContent Media(int id)
		{
			return _umbracoWrapper.TypedMedia(id);
		}

		public IPublishedContent Media(string udi)
		{
			return _umbracoWrapper.TypedMedia(Udi.Parse(udi));
		}

		public IEnumerable<IPublishedContent> Media(IEnumerable<int> ids)
		{
			return ids.Select(_umbracoWrapper.TypedMedia);
		}

		public IEnumerable<IPublishedContent> Media(IEnumerable<string> udis)
		{
			return udis.Select(udi => _umbracoWrapper.TypedMedia(Udi.Parse(udi)));
		}
	}
}