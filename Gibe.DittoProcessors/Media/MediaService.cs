using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.UmbracoWrappers;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

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

		public IEnumerable<IPublishedContent> Media(IEnumerable<int> ids)
		{
			return ids.Select(_umbracoWrapper.TypedMedia);
		}
	}
}