using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Linq;
using static MyFirstPlugin.EventCodes;
using static MyFirstPlugin.LogInStatuses;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void LogInEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.LogIn) {
				return;
			}

			LogInData logInData = new LogInData();

			string[] logInInfo = JsonConvert.DeserializeObject<string[]>((string)info.Request.Data);
			string usernameOrEmail = logInInfo[0];
			string password = logInInfo[1];
			bool isEmail = usernameOrEmail.Contains('@');

			User user;
			User[] users = RetrieveUsers();
			int usersLen = users.Length;

			for(int i = 0; i < usersLen; ++i) {
				user = users[i];

				if(user.Username == usernameOrEmail || user.Email == usernameOrEmail) {
					if(user.Password == password) {
						logInData.status = LogInStatus.Success;
						logInData.username = user.Username;
						logInData.email = user.Email;

						break;
					} else {
						logInData.status = LogInStatus.FailureDueToWrongPassword;

						if(isEmail) {
							logInData.email = user.Email;
						} else {
							logInData.username = user.Username;
						}

						break;
					}
				}
			}

			if(logInData.status != LogInStatus.Success && logInData.status != LogInStatus.FailureDueToWrongPassword) {
				if(isEmail) {
					logInData.status = LogInStatus.FailureDueToWrongEmail;
					logInData.email = usernameOrEmail;
				} else {
					logInData.status = LogInStatus.FailureDueToWrongUsername;
					logInData.username = usernameOrEmail;
				}
			}

			PluginHost.BroadcastEvent(
				target: ReciverGroup.All,
				senderActor: 0,
				targetGroup: 0,
				data: new Dictionary<byte, object>() {
					{245, JsonConvert.SerializeObject(logInData)}
				},
				evCode: info.Request.EvCode,
				cacheOp: CacheOperations.DoNotCache
			);
		}
	}
}