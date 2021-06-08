using Newtonsoft.Json;
using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table(Name = "furniture_table")]
	internal sealed class Furniture {
		[Column(IsPrimaryKey = true, CanBeNull = false), JsonProperty]
		internal int ID {
			get;
			set;
		}

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

		[Column(CanBeNull = false), JsonProperty]
		internal int Tags {
			get;
			set;
		}
	}
}