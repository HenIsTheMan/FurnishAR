using System.Data.Linq.Mapping;

namespace MyFirstPlugin {
	[Table(Name="user_table")]
	internal sealed class User {
		[Column(IsPrimaryKey = true, CanBeNull = false, DbType = "int")]
		internal int id {
			get;
			set;
		}

		//[Column]
		//internal string firstName {
		//	get;
		//	set;
		//}

		//[Column(CanBeNull = true)]
		//internal string middleName {
		//	get;
		//	set;
		//}

		//[Column]
		//internal string lastName {
		//	get;
		//	set;
		//}

		//[Column]
		//internal string username {
		//	get;
		//	set;
		//}

		//[Column]
		//internal string email {
		//	get;
		//	set;
		//}

		//[Column]
		//internal string password {
		//	get;
		//	set;
		//}
	}
}