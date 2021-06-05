using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using static MyFirstPlugin.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void SignUpEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.SignUp) {
				return;
			}

			SignUpData signUpData = new SignUpData();

			string[] signUpInfo = JsonConvert.DeserializeObject<string[]>((string)info.Request.Data);
			User user = new User {
				ID = RetrieveHighestIDOfUser() + 1,
				FirstName = signUpInfo[0],
				MiddleName = signUpInfo[1],
				LastName = signUpInfo[2],
				Username = signUpInfo[3],
				Email = signUpInfo[4],
				Password = signUpInfo[5]
			};

			AddUser(ref user);

			signUpData.username = user.Username;
			signUpData.email = user.Email;

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