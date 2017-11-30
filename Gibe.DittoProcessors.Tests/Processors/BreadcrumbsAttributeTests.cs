using System.Collections.Generic;
using System.Linq;
using Gibe.DittoProcessors.Processors;
using Gibe.DittoServices.ModelConverters;
using Gibe.UmbracoWrappers;
using NUnit.Framework;
using Umbraco.Core.Models;
using Moq;

namespace Gibe.DittoProcessors.Tests.Processors
{
	[TestFixture]
	public class BreadcrumbsAttributeTests
	{
		private string Name = "Test";
		private string Url = "https://github.com/Gibe";

		private IUmbracoWrapper UmbracoWrapper(List<IPublishedContent> listOfIPublishedContent)
		{
			var wrapper = new Mock<IUmbracoWrapper>();
			wrapper.Setup(u => u.Ancestors(It.IsAny<IPublishedContent>())).Returns(listOfIPublishedContent);
			return wrapper.Object;
		}

		private IModelConverter ModelConverters(List<IPublishedContent> listOfIPublishedContent,
			List<BreadcrumbItem> listOfBreadcrumbItems)
		{
			var modelConverter = new Mock<IModelConverter>();

			modelConverter.Setup(m => m.ToModel<BreadcrumbItem>(listOfIPublishedContent[0], null))
				.Returns(listOfBreadcrumbItems[0]);

			modelConverter.Setup(m => m.ToModel<BreadcrumbItem>(listOfIPublishedContent[1], null))
				.Returns(listOfBreadcrumbItems[1]);

			modelConverter.Setup(m => m.ToModel<BreadcrumbItem>(listOfIPublishedContent[2], null))
				.Returns(listOfBreadcrumbItems[2]);
			return modelConverter.Object;
		}

		[Test]
		public void Can_Make_BreadcrumbsItemModel()
		{
			var breadcrumbsItemModel = new BreadcrumbItemModel(Name, Url, true);

			Assert.That(breadcrumbsItemModel.Title, Is.EqualTo(Name));
			Assert.That(breadcrumbsItemModel.Url, Is.EqualTo(Url));
			Assert.That(breadcrumbsItemModel.IsActive, Is.EqualTo(true));
		}

		[Test]
		public void Can_Add_Current_Page()
		{
			var breadcrumbsAttribute = new BreadcrumbsAttribute();
			var breadcrumbs = breadcrumbsAttribute.CurrentPage(Content(Name, Url));

			Assert.That(breadcrumbs.Count, Is.EqualTo(1));
			Assert.That(breadcrumbs.First().Title, Is.EqualTo(Name));
			Assert.That(breadcrumbs.First().Url, Is.EqualTo(Url));
			Assert.That(breadcrumbs.First().IsActive, Is.EqualTo(true));
		}

		[Test]
		public void Can_Add_Ancestors_Pages_Which_Visible_Is_True_DocumentTypeAlias_Is_Not_Empty()
		{
			var listOfIPublishedContents = new List<IPublishedContent>()
				{
					Content("Ancestor1", "https://github.com/Gibe"),
					Content("Ancestor2", "https://test.com"),
					Content("Ancestor3", "https://test.com")
				};

			var listOfBreadcrumItems = new List<BreadcrumbItem>()
			{
				BreadcrumbItem(Content("Ancestor1", "https://github.com/Gibe"), true, "Test"),
				BreadcrumbItem(Content("Ancestor2", "https://test.com"), false, "Test"),
				BreadcrumbItem(Content("Ancestor3", "https://test.com"), true, "")
			};

			var breadcrumbsAttribute = new BreadcrumbsAttribute();
			breadcrumbsAttribute.UmbracoWrapper = () => UmbracoWrapper(listOfIPublishedContents);
			breadcrumbsAttribute.ModelConverter = () => ModelConverters(listOfIPublishedContents, listOfBreadcrumItems);

			var breadcrumbs = breadcrumbsAttribute.AncestorsPages(Content(Name, Url)).ToList();

			Assert.That(breadcrumbs.Count(), Is.EqualTo(1));
			Assert.That(breadcrumbs[0].Title, Is.EqualTo("Ancestor1"));
			Assert.That(breadcrumbs[0].Url, Is.EqualTo("https://github.com/Gibe"));
			Assert.That(breadcrumbs[0].IsActive, Is.EqualTo(false));
		}

		[Test]
		public void Can_Add_Home_Page()
		{
			var breadcrumbsAttribute = new BreadcrumbsAttribute();
			var breadcrumbs = breadcrumbsAttribute.HomePage();

			Assert.That(breadcrumbs.Count, Is.EqualTo(1));
			Assert.That(breadcrumbs.First().Title, Is.EqualTo("Home"));
			Assert.That(breadcrumbs.First().Url, Is.EqualTo("/"));
			Assert.That(breadcrumbs.First().IsActive, Is.EqualTo(false));
		}

		private IPublishedContent Content(string name, string url)
		{
			var content = new Mock<IPublishedContent>();
			content.Setup(c => c.Name).Returns(name);
			content.Setup(c => c.Url).Returns(url);
			content.Setup(c => c.Level).Returns(2);
			return content.Object;
		}

		private BreadcrumbItem BreadcrumbItem(IPublishedContent content, bool visible, string documentTypeAlias)
		{
			return new BreadcrumbItem()
			{
				Level = 2,
				Name = content.Name,
				Url = content.Url,
				Visible = visible,
				DocumentTypeAlias = documentTypeAlias
			};
		}
	}
}