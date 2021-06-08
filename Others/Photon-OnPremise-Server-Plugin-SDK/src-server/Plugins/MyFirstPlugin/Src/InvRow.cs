using Newtonsoft.Json;
using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table(Name = "inv_table")]
	internal sealed class InvRow {
		[Column(CanBeNull = false), JsonProperty]
		internal int UserID {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal int FurnitureID {
			get;
			set;
		}
	}
}