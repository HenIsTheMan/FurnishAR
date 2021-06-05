using System.Data;
using System.Data.Linq;

namespace MyFirstPlugin {
	[System.Data.Linq.Mapping.Database(Name = "furnishar_db")]
	internal sealed class Database: DataContext {
		internal Table<User> userTable;

		internal Database(IDbConnection connection): base(connection) {
			userTable = GetTable<User>();
		}

		//internal void Query(string query) {
		//	using(MySqlCommand cmd = new MySqlCommand(query, connection)) {
		//		using(MySqlDataReader reader = cmd.ExecuteReader()) {
		//		}
		//	}
		//}
	}
}
