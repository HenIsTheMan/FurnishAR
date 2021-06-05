using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using static MyFirstPlugin.EventCodes;
using static MyFirstPlugin.SignUpStatuses;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void SignUpEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.SignUp) {
				return;
			}

			SignUpData signUpData = new SignUpData();
			string[] signUpInfo = JsonConvert.DeserializeObject<string[]>((string)info.Request.Data);

			string firstName = signUpInfo[0];
			if(string.IsNullOrEmpty(firstName)) {
				signUpData.status = SignUpStatus.NoFirstName;
				goto BroadcastEvent;
			}

			string lastName = signUpInfo[2];
			if(string.IsNullOrEmpty(lastName)) {
				signUpData.status = SignUpStatus.NoLastName;
				goto BroadcastEvent;
			}

			string username = signUpInfo[3];
			if(string.IsNullOrEmpty(username)) {
				signUpData.status = SignUpStatus.NoUsername;
				goto BroadcastEvent;
			}

			string email = signUpInfo[4];
			if(string.IsNullOrEmpty(email)) {
				signUpData.status = SignUpStatus.NoEmail;
				goto BroadcastEvent;
			}

			string newPassword = signUpInfo[5];
			if(string.IsNullOrEmpty(newPassword)) {
				signUpData.status = SignUpStatus.NoNewPassword;
				goto BroadcastEvent;
			}

			string confirmPassword = signUpInfo[6];
			if(string.IsNullOrEmpty(confirmPassword)) {
				signUpData.status = SignUpStatus.NoConfirmPassword;
				goto BroadcastEvent;
			} else if(newPassword != confirmPassword) {
				signUpData.status = SignUpStatus.PasswordsNotMatching;
				goto BroadcastEvent;
			}

			signUpData.status = SignUpStatus.Success;
			signUpData.username = username;
			signUpData.email = email;

			User user = new User {
				ID = RetrieveHighestIDOfUser() + 1,
				FirstName = firstName,
				MiddleName = signUpInfo[1],
				LastName = lastName,
				Username = username,
				Email = email,
				Password = newPassword
			};
			AddUser(ref user);

			BroadcastEvent: {
				PluginHost.BroadcastEvent(
					target: ReciverGroup.All,
					senderActor: 0,
					targetGroup: 0,
					data: new Dictionary<byte, object>() {
						{245, JsonConvert.SerializeObject(signUpData)}
					},
					evCode: info.Request.EvCode,
					cacheOp: CacheOperations.DoNotCache
				);
			}
		}
	}
}