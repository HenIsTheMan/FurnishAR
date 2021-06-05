using MySql.Data.MySqlClient;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private readonly Database database;
		private readonly MySqlConnection connection;

		private delegate void OnRaiseEventDelegate(IRaiseEventCallInfo _);
		private OnRaiseEventDelegate myOnRaiseEventDelegate;

		public override string Name => "MyFirstPlugin";

		internal MyFirstPlugin() {
			connection = new MySqlConnection();

			connection.ConnectionString = FormConnectionStr("localhost", 3306, "furnishar_db", "root", "password");
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

		internal List<User> RetrieveUsers() {
			return database.ExecuteQuery<User>("SELECT * FROM furnishar_db.user_table").ToList();
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