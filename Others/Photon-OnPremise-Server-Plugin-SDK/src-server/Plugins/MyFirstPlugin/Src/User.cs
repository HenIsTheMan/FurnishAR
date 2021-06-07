using Newtonsoft.Json;
using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table(Name = "user_table")]
	internal sealed class User {
		[Column(IsPrimaryKey = true, CanBeNull = false), JsonProperty]
		internal int ID {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string FirstName {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string MiddleName {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string LastName {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string Username {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string Email {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string Password {
			get;
			set;
		}

		[Column(CanBeNull = false), JsonProperty]
		internal string SessionToken {
			get;
			set;
		}
	}
}