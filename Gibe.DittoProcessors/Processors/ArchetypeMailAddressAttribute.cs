using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Archetype.Models;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class ArchetypeMailAddressAttribute : DittoProcessorAttribute
	{
		public override object ProcessValue()
		{
			if (string.IsNullOrEmpty(Value?.ToString()))
			{
				return null;
			}

			var model = ConvertFromArchetype((ArchetypeModel)Value);

			return model;
		}
		
		private IEnumerable<MailAddress> ConvertFromArchetype(IEnumerable<ArchetypeFieldsetModel> value)
		{
			return value.Select(x => new MailAddress(x.GetValue<string>("emailAddress"), x.GetValue<string>("name")));
		}
	}
}
