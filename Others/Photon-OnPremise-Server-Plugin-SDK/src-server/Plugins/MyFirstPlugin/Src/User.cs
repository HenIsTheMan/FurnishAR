using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table(Name = "user_table")]
	internal sealed class User {
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		internal int id {
			get;
			set;
		}

		[Column(CanBeNull = false)]
		internal string firstName {
			get;
			set;
		}

		[Column(CanBeNull = true)]
		internal string middleName {
			get;
			set;
		}

		[Column(CanBeNull = false)]
		internal string lastName {
			get;
			set;
		}

		[Column(CanBeNull = false)]
		internal string username {
			get;
			set;
		}

		[Column(CanBeNull = false)]
		internal string email {
			get;
			set;
		}

		[Column(CanBeNull = false)]
		internal string password {
			get;
			set;
		}
	}
}