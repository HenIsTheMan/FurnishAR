using Newtonsoft.Json;
using Photon.Hive.Plugin;
using static MyFirstPlugin.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void SignUpEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.SignUp) {
				return;
			}

			SignUpData signUpData = new SignUpData();

			string[] signUpInfo = JsonConvert.DeserializeObject<string[]>((string)info.Request.Data);
			string firstName = signUpInfo[0];
			string middleName = signUpInfo[1];
			string lastName = signUpInfo[2];
			string username = signUpInfo[3];
			string email = signUpInfo[4];
			string password = signUpInfo[5];

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