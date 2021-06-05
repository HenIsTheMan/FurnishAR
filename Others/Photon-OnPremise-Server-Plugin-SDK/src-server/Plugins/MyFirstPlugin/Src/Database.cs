using System.Data.Linq;

namespace MyFirstPlugin {
	internal sealed class Database: DataContext {
		internal Table<User> userTable;

		internal Database(string connection): base(connection) {
			userTable = null;
		}

		//internal void Query(string query) {
		//	using(MySqlCommand cmd = new MySqlCommand(query, connection)) {
		//		using(MySqlDataReader reader = cmd.ExecuteReader()) {
		//		}
		//	}
		//}
	}
}
