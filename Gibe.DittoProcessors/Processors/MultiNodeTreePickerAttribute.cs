using System;
using System.Linq;
using System.Web.Mvc;
using Gibe.UmbracoWrappers;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class MultiNodeTreePickerAttribute : DittoProcessorAttribute
	{
		private readonly IUmbracoWrapper _umbracoWrapper;

		public MultiNodeTreePickerAttribute()
		{
			_umbracoWrapper = DependencyResolver.Current.GetService<IUmbracoWrapper>();
		}

		public MultiNodeTreePickerAttribute(IUmbracoWrapper umbracoWrapper)
		{
			_umbracoWrapper = umbracoWrapper;
		}

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			var ids = Convert.ToString(Value)
				.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse);

			return ids
				.Where(i => i > 0)
				.Select(_umbracoWrapper.TypedContent);
		}
	}
}
