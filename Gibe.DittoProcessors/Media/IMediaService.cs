using System.Collections.Generic;
using Gibe.DittoProcessors.Media.Models;

namespace Gibe.DittoProcessors.Media
{
	public interface IMediaService
	{
		MediaImageModel GetImage(int id);

		List<MediaImageModel> GetImages(int id);

		List<MediaImageModel> GetImages(List<int> ids);

		MediaFileModel GetFile(int id);

		List<MediaFileModel> GetFiles(int id);

		List<MediaFileModel> GetFiles(List<int> ids);
	}
}