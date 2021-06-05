using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private readonly Database database;

		private delegate void OnRaiseEventDelegate(IRaiseEventCallInfo _);
		private OnRaiseEventDelegate myOnRaiseEventDelegate;

		public override string Name => "MyFirstPlugin"; //The reserved plugin names are "Default" and "ErrorPlugin"

		internal MyFirstPlugin() {
			database = new Database(FormConnectionStr("localhost", 3306, "test_db", "root", "password"));

			myOnRaiseEventDelegate = null;
			myOnRaiseEventDelegate += LogInEventHandler;
			myOnRaiseEventDelegate += SignUpEventHandler;
		}

		//~MyFirstPlugin() { //??
		//	database.Disconnect();
		//}

		private string FormConnectionStr(string host, int port, string db, string user, string password) {
			return $"server={host};user={user};database={db};port={port};password={password}";
		}

		internal List<User> RetrieveUsers() {
			return database.userTable.Select(row => row).ToList(); //return (from row in db.customerTable select row).ToList();
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