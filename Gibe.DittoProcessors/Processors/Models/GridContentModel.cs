using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Gibe.DittoProcessors.Media.Models;
using Newtonsoft.Json;

namespace Gibe.DittoProcessors.Processors.Models
{
	public class GridContentModel
	{
		public IList<GridSection> Sections { get; set; }

		public class GridSection
		{
			public int Grid { get; set; }

			public IList<GridRow> Rows { get; set; }

			public class GridRow
			{
				public string Name { get; set; }

				[JsonProperty("config")]
				public GridConfig Settings { get; set; }

				public class GridConfig
				{
					public string Class { get; set; }
				}

				public IList<GridArea> Areas { get; set; }

				public class GridArea
				{
					public int Grid { get; set; }

					public IList<GridControl> Controls { get; set; }

					public class GridControl
					{
						public object Value { get; set; }

						public GridEditor Editor { get; set; }

						public class GridEditor
						{
							public string Alias { get; set; }
						}

						[Description("this value is for referance only and should not be used in the converter or view")]
						public object TypedValue { get; set; }

						public string QuoteOrEmbed { get; set; }

						public MvcHtmlString Html { get; set; }

						public MediaImageModel MediaImage { get; set; }

						public string Text { get; set; }
					}
				}
			}
		}
	}
}