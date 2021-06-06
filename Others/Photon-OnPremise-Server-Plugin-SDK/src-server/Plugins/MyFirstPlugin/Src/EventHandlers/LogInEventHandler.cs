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
			if(string.IsNullOrEmpty(usernameOrEmail)) {
				logInData.status = LogInStatus.NoUsernameOrEmail;
				goto BroadcastEvent;
			}

			string password = logInInfo[1];
			if(string.IsNullOrEmpty(password)) {
				logInData.status = LogInStatus.NoPassword;
				goto BroadcastEvent;
			}

			if(usernameOrEmail.Contains(' ')) {
				logInData.status = LogInStatus.SpacesInUsernameOrEmail;
				goto BroadcastEvent;
			}

			bool isEmail = true;
			int atIndex = usernameOrEmail.IndexOf('@');
			int dotIndex = usernameOrEmail.IndexOf('.');
			int emailLen = usernameOrEmail.Length;

			if(usernameOrEmail.Count(myChar => myChar == '@') != 1
				|| usernameOrEmail.Count(myChar => myChar == '.') != 1
				|| atIndex < 1
				|| dotIndex < 3
				|| atIndex > emailLen - 4
				|| dotIndex > emailLen - 2
				|| (atIndex >= dotIndex - 1)
			) {
				isEmail = false;
			} else {
				string substr0 = usernameOrEmail.Substring(0, atIndex);
				string substr1 = usernameOrEmail.Substring(atIndex + 1, dotIndex - atIndex - 1);
				string substr2 = usernameOrEmail.Substring(dotIndex + 1, emailLen - dotIndex - 1);

				if(!substr0.All(char.IsLetterOrDigit)
					|| !substr1.All(char.IsLetterOrDigit)
					|| !substr2.All(char.IsLetterOrDigit)
				) { //Diff from Sign Up (on purpose)
					isEmail = false;
				}
			}

			User user;
			User[] users = RetrieveUsers();
			int usersLen = users.Length;

			for(int i = 0; i < usersLen; ++i) {
				user = users[i];

				if(user.Username.ToLower() == usernameOrEmail.ToLower() || user.Email.ToLower() == usernameOrEmail.ToLower()) {
					if(user.Password == password) {
						logInData.status = LogInStatus.Success;
						logInData.username = user.Username;
						logInData.email = user.Email;

						break;
					} else {
						logInData.status = LogInStatus.WrongPassword;

						if(isEmail) {
							logInData.email = user.Email;
						} else {
							logInData.username = user.Username;
						}

						break;
					}
				}
			}

			if(logInData.status != LogInStatus.Success && logInData.status != LogInStatus.WrongPassword) {
				if(isEmail) {
					logInData.status = LogInStatus.WrongEmail;
					logInData.email = usernameOrEmail;
				} else {
					logInData.status = LogInStatus.WrongUsername;
					logInData.username = usernameOrEmail;
				}
			}

			BroadcastEvent: {
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
}