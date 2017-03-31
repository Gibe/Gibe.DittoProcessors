using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.DittoProcessors.Constants;
using Gibe.DittoProcessors.Media.Models;
using Gibe.DittoServices.ModelConverters;
using Gibe.UmbracoWrappers;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Media
{
	public class MediaService : IMediaService
	{
		private readonly IUmbracoWrapper _umbracoWrapper;
		private readonly IModelConverter _modelConverter;

		public MediaService(IUmbracoWrapper umbracoWrapper, IModelConverter modelConverter)
		{
			_umbracoWrapper = umbracoWrapper;
			_modelConverter = modelConverter;
		}


		// TODO: Constrain on IMediaImageModel instead of MediaImageModel // Why?

		#region images

		/// <summary>
		/// gets a single image. if the passed id is a folder, the 1st image is returned
		/// </summary>
		public T GetImage<T>(int id) where T : MediaImageModel
		{
			return (T)GetImage(typeof(T), id);
		}

		/// <summary>
		/// gets a list of images. if the passed id isnt a folder but is an image the image is returned in the list
		/// </summary>
		public List<T> GetImages<T>(int id) where T : MediaImageModel
		{
			return GetImages(typeof(T), id).Select(x => (T)x).ToList();
		}
		
		/// <summary>
		/// gets a list of images based on the passed ids
		/// </summary>
		public List<T> GetImages<T>(IEnumerable<int> ids) where T : MediaImageModel
		{
			return ids.Select(GetImage<T>).Where(mediaImage => mediaImage != null).ToList();
		}

		public MediaImageModel GetImage(Type mediaType, int id)
		{
			if (!mediaType.Implements<IMediaImageModel>())
			{
				throw new NotSupportedException($"{mediaType.Name} must implement IMediaImageModel");
			}

			if (id == 0) return null;
			var media = IsMediaOrFolder(id);
			if (media.mediaItem == null) return null;

			if (!media.isFolder)
			{
				if (CanConvertImage(_umbracoWrapper, media.mediaItem))
				{
					return (MediaImageModel)ConvertImage(_modelConverter, mediaType, media.mediaItem);
				}
				throw new ArgumentException("Image id is not of an allowed document type!");
			}

			var children = ChildImages(media.mediaItem);
			if (children.FirstOrDefault() == null) return null;
			return (MediaImageModel)ConvertImage(_modelConverter, mediaType, children.First());
		}

		public List<MediaImageModel> GetImages(Type mediaType, int id)
		{
			if (!mediaType.Implements<IMediaImageModel>())
			{
				throw new NotSupportedException($"{mediaType.Name} must implement IMediaImageModel");
			}

			if (id == 0) return null;
			var media = IsMediaOrFolder(id);
			if (media.mediaItem == null) return null;

			if (!media.isFolder)
			{
				if (CanConvertImage(_umbracoWrapper, media.mediaItem))
				{
					return ConvertImage(_modelConverter, mediaType, media.mediaItem).AsEnumerableOfOne()
						.Select(x => (MediaImageModel)x).ToList();
				}
				throw new ArgumentException("Image id is not of an allowed document type!");
			}

			var childImages = ChildImages(media.mediaItem);

			return childImages.Any() ? childImages.Select(node => _umbracoWrapper.TypedMedia(node.Id))
				.Select(x => (MediaImageModel)ConvertImage(_modelConverter, mediaType, x)).ToList() : null;
		}
		
		private (IPublishedContent mediaItem, bool isFolder) IsMediaOrFolder(int id)
		{
			var mediaItem = _umbracoWrapper.TypedMedia(id);
			var isFolder = CheckForFolder(mediaItem);
			return (mediaItem, isFolder);
		}

		private IEnumerable<IPublishedContent> ChildImages(IPublishedContent folder)
		{
			return folder.Children().Where(x => CanConvertImage(_umbracoWrapper, x)).ToList();
		}

		public List<MediaImageModel> GetImages(Type mediaType, IEnumerable<int> ids)
		{
			return ids.Select(id => GetImage(mediaType, id)).Where(mediaImage => mediaImage != null).ToList();
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
			return (item != null) && (item?.DocumentTypeAlias == MediaTypes.Folder);
		}

		/// <summary>
		/// checks if we can convert the passed umbraco content into a site media image
		/// </summary>
		private static bool CanConvertImage(IUmbracoWrapper umbracoWrapper, IPublishedContent image)
		{
			return !string.IsNullOrEmpty(image.Url)
				&& umbracoWrapper.HasProperty(image, "umbracoWidth")
				&& umbracoWrapper.HasProperty(image, "umbracoHeight");
		}

        /// <summary>
        /// converts the passed umbraco content into a site media image
        /// </summary>
        private static T ConvertImage<T>(IModelConverter modelConverter, IPublishedContent image) where T : MediaImageModel
        {
	        return modelConverter.ToModel<T>(image);
		}

		private static object ConvertImage(IModelConverter modelConverter, Type type, IPublishedContent image)
		{
			return modelConverter.ToModel(type, image);
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