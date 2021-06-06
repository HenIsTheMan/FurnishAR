using MySql.Data.MySqlClient;
using Photon.Hive.Plugin;
using System.Linq;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private readonly Database database;
		private readonly MySqlConnection connection;

		private delegate void OnRaiseEventDelegate(IRaiseEventCallInfo _);
		private OnRaiseEventDelegate myOnRaiseEventDelegate;

		public override string Name => "MyFirstPlugin";

		internal MyFirstPlugin() {
			connection = new MySqlConnection {
				ConnectionString = FormConnectionStr("localhost", 3306, "furnishar_db", "root", "password")
			};
			connection.Open();

			database = new Database(connection);

			myOnRaiseEventDelegate = null;
			myOnRaiseEventDelegate += LogInEventHandler;
			myOnRaiseEventDelegate += SignUpEventHandler;
		}

		~MyFirstPlugin() {
			connection.Close();
		}

		private string FormConnectionStr(string host, int port, string db, string user, string password) {
			return $"server={host};user={user};database={db};port={port};password={password}";
		}

		private void AddUser(ref User user) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			try {
				_ = database.ExecuteQuery<User>(
					"INSERT INTO furnishar_db.user_table "
					+ "(id, firstName, middleName, lastName, username, email, password)"
					+ " VALUES "
					+ $"({user.ID}, \"{user.FirstName}\", \"{user.MiddleName}\", \"{user.LastName}\", \"{user.Username}\", \"{user.Email}\", \"{user.Password}\");"
				);
			} catch(System.Exception) {
			}
		}

		private User[] RetrieveUsers() {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			return database.ExecuteQuery<User>("SELECT * FROM furnishar_db.user_table").ToArray();
		}

		private int RetrieveHighestIDOfUser(int minExclusive = 0) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			User[] users = database.ExecuteQuery<User>($"SELECT * FROM furnishar_db.user_table ORDER BY id DESC LIMIT {minExclusive}, 1").ToArray();
			return users.Length == 0 ? minExclusive : users[0].ID;
		}

		public override void OnCreateGame(ICreateGameCallInfo info) {
            //PluginHost.LogInfo(string.Format("OnCreateGame {0} by user {1}", info.Request.GameId, info.UserId));

			base.OnCreateGame(info); //info.Continue();
        }

		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);

			myOnRaiseEventDelegate?.Invoke(info);
		}
	}
}