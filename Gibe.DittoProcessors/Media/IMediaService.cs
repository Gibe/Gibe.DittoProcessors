using System;
using System.Collections.Generic;
using Gibe.DittoProcessors.Media.Models;

namespace Gibe.DittoProcessors.Media
{
	public interface IMediaService
	{
		T GetImage<T>(int id) where T : MediaImageModel;

		MediaImageModel GetImage(Type mediaType, int id);

		List<T> GetImages<T>(int id) where T : MediaImageModel;

		List<MediaImageModel> GetImages(Type mediaType, int id);

		List<T> GetImages<T>(IEnumerable<int> ids) where T : MediaImageModel;

		List<MediaImageModel> GetImages(Type mediaType, IEnumerable<int> ids);

		MediaFileModel GetFile(int id);

		List<MediaFileModel> GetFiles(int id);

		List<MediaFileModel> GetFiles(List<int> ids);
	}
}