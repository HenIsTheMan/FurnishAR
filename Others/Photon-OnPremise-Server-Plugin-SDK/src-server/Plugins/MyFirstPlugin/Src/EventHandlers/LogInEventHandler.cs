using Newtonsoft.Json;
using Photon.Hive.Plugin;
using static MyFirstPlugin.Src.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void LogInEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.LogIn) {
				return;
			}

			string[] logInInfo = JsonConvert.DeserializeObject<string[]>((string)info.Request.Data);
			int logInInfoLen = logInInfo.Length;

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