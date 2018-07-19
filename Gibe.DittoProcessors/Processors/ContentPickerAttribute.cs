using System;
using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public class ContentPickerAttribute : InjectableProcessorAttribute
	{
		public Func<IUmbracoWrapper> UmbracoWrapper => Inject<IUmbracoWrapper>();
		
		public override object ProcessValue()
		{
			if (Value is IPublishedContent)
			{
				return Value;
			}

			int id;
			if (string.IsNullOrEmpty(Value?.ToString()) || !int.TryParse(Value.ToString(), out id) || id == 0)
			{
				return null;
			}

			return UmbracoWrapper().TypedContent(id);
		}
	}
}
