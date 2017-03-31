using System;
using System.Web.Mvc;
using Gibe.DittoProcessors.Media;
using Gibe.DittoProcessors.Media.Models;
using Ninject;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class MultipleImagePickerAttribute : InjectableProcessorAttribute
	{
		public Func<IMediaService> MediaService => Inject<IMediaService>();
		private readonly Type _type;

		public MultipleImagePickerAttribute(Type type = null)
		{
			_type = type ?? typeof(MediaImageModel);
		}

		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			int id = int.TryParse(Value.ToString(), out id) ? id : 0;

			return id == 0 ? null : MediaService().GetImages(_type, id);
		}
	}
}