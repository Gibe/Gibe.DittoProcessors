using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.DittoProcessors.Constants;
using Gibe.DittoProcessors.Media.Models;
using Gibe.UmbracoWrappers;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Media
{
	public class MediaService : IMediaService
	{
		private readonly string[] _allowedImageMediaTypes = { MediaTypes.Image };
		private readonly IUmbracoWrapper _umbracoWrapper;

		public MediaService(
			IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}

		#region images

		/// <summary>
		/// gets a single image. if the passed id is a folder, the 1st image is returned
		/// </summary>
		public MediaImageModel GetImage(int id)
		{
			if (id == 0) return null;

			var imageOrFolder = _umbracoWrapper.TypedMedia(id);

			if (imageOrFolder == null) return null;

			var isFolder = CheckForFolder(imageOrFolder);

			if (!isFolder)
			{
				if (_allowedImageMediaTypes.Contains(imageOrFolder.DocumentTypeAlias))
				{
					return ConvertImage(_umbracoWrapper, imageOrFolder);
				}
				throw new ArgumentException("Image id is not of an allowed document type!");
			}

			var childImages = imageOrFolder.Children().Where(x => _allowedImageMediaTypes.Contains(x.DocumentTypeAlias)).ToList();

			if (!childImages.Any()) return null;

			var image = _umbracoWrapper.TypedMedia(childImages.First().Id);

			return ConvertImage(_umbracoWrapper, image);
		}

		/// <summary>
		/// gets a list of images. if the passed id isnt a folder but is an image the image is returned in the list
		/// </summary>
		public List<MediaImageModel> GetImages(int id)
		{
			if (id == 0) return null;

			var imageOrFolder = _umbracoWrapper.TypedMedia(id);

			if (imageOrFolder == null) return null;

			var isFolder = CheckForFolder(imageOrFolder);

			if (!isFolder)
			{
				if (_allowedImageMediaTypes.Contains(imageOrFolder.DocumentTypeAlias))
				{
					return ConvertImage(_umbracoWrapper, imageOrFolder).AsEnumerableOfOne().ToList();
				}
				throw new ArgumentException("Image id is not of an allowed document type!");
			}

			var childImages = imageOrFolder.Children().Where(x => _allowedImageMediaTypes.Contains(x.DocumentTypeAlias)).ToList();

			return childImages.Any() ? childImages.Select(node => _umbracoWrapper.TypedMedia(node.Id)).Select(image => ConvertImage(_umbracoWrapper, image)).ToList() : null;
		}

		/// <summary>
		/// gets a list of images based on the passed ids
		/// </summary>
		public List<MediaImageModel> GetImages(List<int> ids)
		{
			return ids.Select(GetImage).Where(mediaImage => mediaImage != null).ToList();
		}

		#endregion

		#region files

		/// <summary>
		/// gets a single file. if the passed id is a folder, the 1st file is returned
		/// </summary>
		public MediaFileModel GetFile(int id)
		{
			if (id == 0) return null;

			var fileOrFolder = _umbracoWrapper.TypedMedia(id);

			if (fileOrFolder == null) return null;

			var isFolder = CheckForFolder(fileOrFolder);

			if (!isFolder) return (fileOrFolder.DocumentTypeAlias == MediaTypes.File || fileOrFolder.DocumentTypeAlias == MediaTypes.Video) ? ConvertFile(_umbracoWrapper, fileOrFolder) : null;

			var childFiles = fileOrFolder.Children().Where(x => x.DocumentTypeAlias == MediaTypes.File).ToList();

			if (!childFiles.Any()) return null;

			var image = _umbracoWrapper.TypedMedia(childFiles.First().Id);

			return ConvertFile(_umbracoWrapper, image);
		}

		/// <summary>
		/// gets a list of images. if the passed id isnt a folder but is an image the image is returned in the list
		/// </summary>
		public List<MediaFileModel> GetFiles(int id)
		{
			if (id == 0) return null;

			var fileOrFolder = _umbracoWrapper.TypedMedia(id);

			if (fileOrFolder == null) return null;

			var isFolder = CheckForFolder(fileOrFolder);

			if (!isFolder) return fileOrFolder.DocumentTypeAlias == MediaTypes.File ? new List<MediaFileModel> { ConvertFile(_umbracoWrapper, fileOrFolder) } : null;

			var childFiles = fileOrFolder.Children().Where(x => x.DocumentTypeAlias == MediaTypes.File).ToList();

			return childFiles.Any() ? childFiles.Select(node => _umbracoWrapper.TypedMedia(node.Id)).Select(image => ConvertFile(_umbracoWrapper, image)).ToList() : null;
		}

		/// <summary>
		/// gets a list of images based on the passed ids
		/// </summary>
		public List<MediaFileModel> GetFiles(List<int> ids)
		{
			return ids.Select(GetFile).Where(mediaFile => mediaFile != null).ToList();
		}

		#endregion

		#region helpers

		/// <summary>
		/// checks if the passed item is a folder
		/// </summary>
		private static bool CheckForFolder(IPublishedContent item)
		{
			return (item != null) && (item.DocumentTypeAlias == MediaTypes.Folder);
		}

		/// <summary>
		/// converts the passed umbraco content into a site media image
		/// </summary>
		private static MediaImageModel ConvertImage(IUmbracoWrapper umbracoWrapper, IPublishedContent image)
		{
			return new MediaImageModel
			{
				Url = image.Url,
				Width = umbracoWrapper.GetPropertyValue<int>(image, "umbracoWidth"),
				Height = umbracoWrapper.GetPropertyValue<int>(image, "umbracoHeight")
			};
		}

		/// <summary>
		/// converts the passed umbraco content into a site media file
		/// </summary>
		private static MediaFileModel ConvertFile(IUmbracoWrapper umbracoWrapper, IPublishedContent file)
		{
			var extension = umbracoWrapper.GetPropertyValue<string>(file, "umbracoExtension");
			var umbracoBytes = umbracoWrapper.GetPropertyValue<double>(file, "umbracoBytes");

			var size = Math.Round(umbracoBytes / 1000, 1);
			var unit = "KB";
			var type = extension.ToUpper();
			if (size > 1000)
			{
				size = Math.Round(size / 1000, 1);
				unit = "MB";
			} 
			
			return new MediaFileModel
			{
				Url = file.Url,
				Extension = extension,
				Size = $"{size}&nbsp;{unit}"
			};
		}

		#endregion
	}

	public class Cropper
	{
		public FocalPoint FocalPoint { get; set; }
		public string Src { get; set; }
		public List<Crop> Crops { get; set; }
	}

	public class FocalPoint
	{
		public double Left { get; set; }
		public double Top { get; set; }
	}

	public class Crop
	{
		public string Alias { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}


}