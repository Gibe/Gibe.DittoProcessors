using Our.Umbraco.Ditto;

namespace Gibe.DittoProcessors.Processors
{
	public abstract class TestableDittoProcessorAttribute : DittoProcessorAttribute
	{
		public new object Value 
		{ 
			get => base.Value;
			set => base.Value = value;
		}
	}
}
