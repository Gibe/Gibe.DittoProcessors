using System;
using System.Linq;
using Gibe.DittoProcessors.Media;
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
			_mediaContent = _repository.Create<IPublishedContent>();

			_mediaContent.Setup(c => c.Id).Returns(1);

			_umbracoWrapper.Setup(u => u.TypedMedia(It.IsAny<int>()))
				.Returns(_mediaContent.Object);
		}

		private MockRepository _repository;
		private Mock<IUmbracoWrapper> _umbracoWrapper;
		private Mock<IPublishedContent> _mediaContent;

		private MediaService MediaService() => new MediaService(_umbracoWrapper.Object);

		[Test]
		public void Media_Gets_Media_By_Id()
		{
			var media = MediaService().Media(1);
			Assert.That(media.Id, Is.EqualTo(1));
		}

		[Test]
		public void Media_Gets_Multiple_Media_By_Ids()
		{
			var media = MediaService().Media(new [] { 1, 2, 3});
			Assert.That(media.Count(), Is.EqualTo(3));
		}
	}

}
