using System;
using System.Linq;
using Gibe.DittoProcessors.Constants;
using Gibe.DittoProcessors.Media;
using Gibe.DittoProcessors.Media.Models;
using Gibe.DittoServices.ModelConverters;
using Gibe.UmbracoWrappers;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Tests
{
	[TestFixture]
    internal class MediaServiceTests
    {
		[SetUp]
	    public void Init()
	    {
			_repository = new MockRepository(MockBehavior.Strict);
		    _umbracoWrapper = _repository.Create<IUmbracoWrapper>();
			_modelConverter = _repository.Create<IModelConverter>();
			_mediaContent = _repository.Create<IPublishedContent>();

		}

	    private MockRepository _repository;
		private Mock<IUmbracoWrapper> _umbracoWrapper;
		private Mock<IModelConverter> _modelConverter;
	    private Mock<IPublishedContent> _mediaContent;

		private IMediaService MediaService() => new MediaService(_umbracoWrapper.Object, _modelConverter.Object);

		[Test]
		public void GetImage_Throws_NotSupportedException_When_Type_Does_Not_Implement_MediaImageModel()
		{
			try
			{
				MediaService().GetImage(typeof(int), 123);
			}
			catch (NotSupportedException e)
			{
				Assert.Pass();
			}
			Assert.Fail("MediaService did not throw a NotSupportedException");
		}

		[Test]
		public void GetImage_Returns_Null_When_Id_Equals_Zero()
	    {
		    var result = MediaService().GetImage(typeof(MediaImageModelSubType), 0);
			var genericResult = MediaService().GetImage<MediaImageModelSubType>(0);

			Assert.IsNull(result);
			Assert.IsNull(genericResult);
		}
		
		[Test]
		public void GetImage_Returns_Null_When_Media_Not_Found()
		{
			var id = 123;
			_umbracoWrapper.Setup(x => x.TypedMedia(id)).Returns((IPublishedContent) null);

			var result = MediaService().GetImage(typeof(MediaImageModelSubType), id);
			var genericResult = MediaService().GetImage<MediaImageModelSubType>(id);

			Assert.IsNull(result);
			Assert.IsNull(genericResult);
		}

		[Test]
		public void GetImage_Throws_ArgumentException_When_Media_Is_Not_Folder_And_Is_Not_Convertable()
		{
			var id = 123;
			
			_umbracoWrapper.Setup(x => x.TypedMedia(id)).Returns(_mediaContent.Object);
			_mediaContent.Setup(x => x.DocumentTypeAlias).Returns(MediaTypes.Image);
			_mediaContent.Setup(x => x.Url).Returns("/image.jpg");
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoHeight")).Returns(false);
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoWidth")).Returns(false);

			try
			{
				MediaService().GetImage(typeof(MediaImageModelSubType), id);
				MediaService().GetImage<MediaImageModelSubType>(id);
			}
			catch (ArgumentException e)
			{
				Assert.Pass();
			}

			Assert.Fail("MediaService did not throw an ArgumentException");
		}

		[Test]
		public void GetImage_Returns_Image_When_Media_Is_Not_Folder_And_Is_Convertable()
		{
			var id = 123;
			var image = new MediaImageModelSubType
			{
				Url = "/",
				Height = 120,
				Width = 60,
				AdditionalText = "abc"
			};

			_umbracoWrapper.Setup(x => x.TypedMedia(id)).Returns(_mediaContent.Object);
			_mediaContent.Setup(x => x.DocumentTypeAlias).Returns(MediaTypes.Image);
			_mediaContent.Setup(x => x.Url).Returns("/image.jpg");
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoHeight")).Returns(true);
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoWidth")).Returns(true);
			_modelConverter.Setup(x => x.ToModel(typeof(MediaImageModelSubType), _mediaContent.Object, null)).Returns(image);

			var result = (MediaImageModelSubType)MediaService().GetImage(typeof(MediaImageModelSubType), id);
			var genericResult = MediaService().GetImage<MediaImageModelSubType>(id);

			Assert.That(result.Url, Is.EqualTo(image.Url));
			Assert.That(result.Height, Is.EqualTo(image.Height));
			Assert.That(result.Width, Is.EqualTo(image.Width));
			Assert.That(result.AdditionalText, Is.EqualTo(image.AdditionalText));

			Assert.That(genericResult.Url, Is.EqualTo(image.Url));
			Assert.That(genericResult.Height, Is.EqualTo(image.Height));
			Assert.That(genericResult.Width, Is.EqualTo(image.Width));
			Assert.That(genericResult.AdditionalText, Is.EqualTo(image.AdditionalText));
		}

		[Test]
		public void GetImage_Returns_Null_When_Media_Is_Folder_And_Has_No_Convertable_Children()
		{
			var id = 123;

			_umbracoWrapper.Setup(x => x.TypedMedia(id)).Returns(_mediaContent.Object);
			_mediaContent.Setup(x => x.DocumentTypeAlias).Returns(MediaTypes.Folder);
			_mediaContent.Setup(x => x.Url).Returns("/image.jpg");
			_mediaContent.Setup(x => x.Children).Returns(Enumerable.Empty<IPublishedContent>());
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoHeight")).Returns(false);
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoWidth")).Returns(false);

			var result = MediaService().GetImage(typeof(MediaImageModelSubType), id);
			var genericResult = MediaService().GetImage<MediaImageModelSubType>(id);

			Assert.That(result, Is.Null);
			Assert.That(genericResult, Is.Null);
		}

		[Test]
		public void GetImage_Returns_First_Child_Image_When_Media_Is_Folder_And_Has_Convertable_Children()
		{
			var id = 123;
			var image = new MediaImageModelSubType
			{
				Url = "/",
				Height = 120,
				Width = 60,
				AdditionalText = "abc"
			};

			var childImage = _repository.Create<IPublishedContent>();
			childImage.Setup(x => x.DocumentTypeAlias).Returns(MediaTypes.Image);
			childImage.Setup(x => x.Url).Returns("/child_image.jpg");
			_umbracoWrapper.Setup(x => x.HasProperty(childImage.Object, "umbracoHeight")).Returns(true);
			_umbracoWrapper.Setup(x => x.HasProperty(childImage.Object, "umbracoWidth")).Returns(true);
			_modelConverter.Setup(x => x.ToModel(typeof(MediaImageModelSubType), childImage.Object, null)).Returns(image);

			_umbracoWrapper.Setup(x => x.TypedMedia(id)).Returns(_mediaContent.Object);
			_mediaContent.Setup(x => x.DocumentTypeAlias).Returns(MediaTypes.Folder);
			_mediaContent.Setup(x => x.Url).Returns("/image.jpg");
			_mediaContent.Setup(x => x.Children).Returns(new[] { childImage .Object });
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoHeight")).Returns(false);
			_umbracoWrapper.Setup(x => x.HasProperty(_mediaContent.Object, "umbracoWidth")).Returns(false);

			var result = (MediaImageModelSubType)MediaService().GetImage(typeof(MediaImageModelSubType), id);
			var genericResult = MediaService().GetImage<MediaImageModelSubType>(id);

			Assert.That(result.Url, Is.EqualTo(image.Url));
			Assert.That(result.Height, Is.EqualTo(image.Height));
			Assert.That(result.Width, Is.EqualTo(image.Width));
			Assert.That(result.AdditionalText, Is.EqualTo(image.AdditionalText));

			Assert.That(genericResult.Url, Is.EqualTo(image.Url));
			Assert.That(genericResult.Height, Is.EqualTo(image.Height));
			Assert.That(genericResult.Width, Is.EqualTo(image.Width));
			Assert.That(genericResult.AdditionalText, Is.EqualTo(image.AdditionalText));
		}
	}

	internal class MediaImageModelSubType : MediaImageModel
	{
		public string AdditionalText { get; set; }
	}
}
