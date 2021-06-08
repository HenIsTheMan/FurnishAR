using Newtonsoft.Json;
using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table]
	internal sealed class FurnitureSaved {
		[Column(CanBeNull = false), JsonProperty]
		internal string Name {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal float Price {
			get;
			set;
		}
	}
}