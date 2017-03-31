using System;
using System.Web.Mvc;

namespace Gibe.DittoProcessors.Processors
{
	public abstract class InjectableProcessorAttribute : TestableDittoProcessorAttribute
	{
		public Func<T> Inject<T>() => () => DependencyResolver.Current.GetService<T>();
	}
}
