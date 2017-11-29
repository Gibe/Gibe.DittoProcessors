using System;
using System.Diagnostics;
using System.Web.Mvc;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Processors
{
	public abstract class InjectableProcessorAttribute : TestableDittoProcessorAttribute
	{
		protected static Func<T> Inject<T>()
		{
			return () => DependencyResolver.Current.GetService<T>();
		}

		protected IPublishedContent Content => Value as IPublishedContent ?? Context.Content;
	}
}
