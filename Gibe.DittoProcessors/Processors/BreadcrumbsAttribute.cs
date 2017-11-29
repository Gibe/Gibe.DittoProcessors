using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.DittoServices.ModelConverters;
using Gibe.UmbracoWrappers;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class BreadcrumbsAttribute : InjectableProcessorAttribute
	{
		public Func<IUmbracoWrapper> UmbracoWrapper { get; set; } = Inject<IUmbracoWrapper>();
		public Func<IModelConverter> ModelConverter { get; set; } = Inject<IModelConverter>();

		public override object ProcessValue()
		{
			var content = Context.Content;

			var breadcrumbs = CurrentPage(content)
				.Concat(AncestorsPages(content))
				.Concat(HomePage())
				.Reverse();

			return breadcrumbs;
		}

		public IEnumerable<BreadcrumbItemModel> CurrentPage(IPublishedContent content)
		{
			var breadcrumbs = new List<BreadcrumbItemModel>
			{
				new BreadcrumbItemModel(content.Name, content.Url, true)
			};
			return breadcrumbs;
		}

		public IEnumerable<BreadcrumbItemModel> AncestorsPages(IPublishedContent content)
		{
			foreach (var item in UmbracoWrapper().Ancestors(content).Select(i => ModelConverter().ToModel<BreadcrumbItem>(i)).Where(i => i.Visible && i.DocumentTypeAlias != ""))
			//foreach (var item in content.Ancestors().Where("Visible && DocumentTypeAlias !=\"\""))
			{
				if (item.Level <= 1) break;
				yield return new BreadcrumbItemModel(item.Name, item.Url, false);
			}
		}

		public IEnumerable<BreadcrumbItemModel> HomePage()
		{
			yield return new BreadcrumbItemModel("Home", "/", false);
		}
	}

	public class BreadcrumbItem
	{
		public bool Visible { get; set; }
		public string DocumentTypeAlias { get; set; }
		public int Level { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
	}

	public class BreadcrumbItemModel
	{
		public BreadcrumbItemModel(string title, string url, bool isActive)
		{
			Title = title;
			Url = url;
			IsActive = isActive;
		}
		public string Title { get; }
		public string Url { get; }
		public bool IsActive { get; }
	}
}
