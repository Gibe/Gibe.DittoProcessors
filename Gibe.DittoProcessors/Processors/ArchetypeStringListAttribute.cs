using System;
using System.Collections.Generic;
using System.Linq;
using Archetype.Models;
using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public class ArchetypeStringListAttribute : DittoProcessorAttribute
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

		private IEnumerable<String> ConvertFromArchetype(IEnumerable<ArchetypeFieldsetModel> value)
		{
			return value.Select(x => x.GetValue<string>("item")).ToList();
		}
	}
}