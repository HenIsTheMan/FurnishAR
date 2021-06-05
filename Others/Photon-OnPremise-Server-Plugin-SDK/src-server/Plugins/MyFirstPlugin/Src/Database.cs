using System.Data;
using System.Data.Linq;

namespace MyFirstPlugin {
	[System.Data.Linq.Mapping.Database(Name = "furnishar_db")]
	internal sealed class Database: DataContext {
		internal Database(IDbConnection connection): base(connection) {
		}
	}
}