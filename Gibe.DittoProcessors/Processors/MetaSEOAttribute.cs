using System;
using Gibe.DittoProcessors.Processors.Models;
using Newtonsoft.Json.Linq;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	// ReSharper disable once InconsistentNaming
	public class MetaSEOAttribute : DittoProcessorAttribute
	{

		public override object ProcessValue()
		{
			if (Value == null)
			{
				throw new NullReferenceException();
			}

			var jo = Value as JObject;

			return new MetaModel
			{
				Title = jo.GetValue("title")?.ToString(),
				Description = jo.GetValue("description")?.ToString(),
				Keywords = jo.GetValue("keywords")?.ToString(),
				RobotsNoindexFollow = bool.Parse(jo.GetValue("noIndex")?.ToString() ?? "false"),
				ShowInSitemap = bool.Parse(jo.GetValue("sitemap")?.ToString() ?? "false"),
			};
		}
	}
}
