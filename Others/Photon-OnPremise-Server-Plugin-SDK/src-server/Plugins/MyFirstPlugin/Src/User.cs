using Newtonsoft.Json;
using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table(Name = "user_table")]
	internal sealed class User {
		[Column(IsPrimaryKey = true, CanBeNull = false), JsonProperty]
		internal int id {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string firstName {
			get;
			set;
		}

		[Column(CanBeNull = true), JsonProperty]
		internal string middleName {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string lastName {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string username {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string email {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string password {
			get;
			set;
		}
	}
}