using System;
using System.Linq;
using System.Web.Mvc;
using Gibe.DittoProcessors.Constants;
using Gibe.DittoProcessors.Media;
using Gibe.DittoProcessors.Media.Models;
using Gibe.DittoProcessors.Processors.Models;
using Newtonsoft.Json;
using Ninject;
using Our.Umbraco.Ditto;
using Umbraco.Web.Templates;

namespace Gibe.DittoProcessors.Processors
{
	public class GridAttribute : DittoProcessorAttribute
	{
		private readonly IMediaService _mediaService;

		public GridAttribute(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		public GridAttribute()
		{
			_mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			var gridContentModel = JsonConvert.DeserializeObject<GridContentModel>(Value.ToString());

			foreach (var control in (from section in gridContentModel.Sections from row in section.Rows from area in row.Areas from control in area.Controls select control).Where(control => control.Value != null))
			{
				switch (control.Editor.Alias)
				{
					case GridEditorAliases.Embed:
					case GridEditorAliases.Quote:
						control.QuoteOrEmbed = control.Value.ToString();
						break;
					case GridEditorAliases.Media:
						var gridContentMediaValue = JsonConvert.DeserializeObject<GridContentMediaValue>(control.Value.ToString());
						var mediaImage = _mediaService.GetImage<CaptionMediaImageModel>(gridContentMediaValue.Id);
						mediaImage.Caption = gridContentMediaValue.Caption;
						control.MediaImage = mediaImage;
						break;
					case GridEditorAliases.Rte:
						control.Html = new MvcHtmlString(TemplateUtilities.ParseInternalLinks(control.Value.ToString()));
						break;
					case GridEditorAliases.Headline:
						control.Text = control.Value.ToString();
						break;
					default:
						throw new ArgumentException("unknown grid editor aliases", control.Editor.Alias);
				}
			}

			return gridContentModel;
		}
	}
}