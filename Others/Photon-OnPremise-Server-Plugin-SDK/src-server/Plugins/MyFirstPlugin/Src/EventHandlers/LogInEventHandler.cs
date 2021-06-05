using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Data;
using System.Linq;
using static MyFirstPlugin.Src.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void LogInEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.LogIn) {
				return;
			}

			string[] logInInfo = JsonConvert.DeserializeObject<string[]>((string)info.Request.Data);
			int logInInfoLen = logInInfo.Length;

			//input validation??

			//database.Query(logInInfo[0].Contains('@')
			//	? "SELECT * FROM furnishar_db.user_table WHERE EXISTS"
			//	+ "(SELECT email FROM furnishar_db.user_table WHERE furnishar_db.user_table.password = " + logInInfo[1] + ");"
			//	: "SELECT * FROM furnishar_db.user_table WHERE EXISTS"
			//	+ "(SELECT username FROM furnishar_db.user_table WHERE furnishar_db.user_table.password = " + logInInfo[1] + ");"
			//);

			//DataTable myDataTable = database.Query("SELECT * FROM furnishar_db.user_table WHERE EXISTS"
			//	+ "(SELECT * FROM furnishar_db.user_table WHERE"
			//	+ "furnishar_db.user_table.username = " + logInInfo[0] + " AND furnishar_db.user_table.password = " + logInInfo[1] + ");");

			//bool isLogInSuccessful = true; //Send with acct info too??

			PluginHost.BroadcastEvent(
				target: ReciverGroup.All,
				senderActor: 0,
				targetGroup: 0,
				data: new System.Collections.Generic.Dictionary<byte, object>() {
					{245, logInInfoLen}
				},
				evCode: info.Request.EvCode,
				cacheOp: CacheOperations.DoNotCache
			);
		}
	}
}