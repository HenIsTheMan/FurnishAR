using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using static MyFirstPlugin.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void GetFurnitureInSavedEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.GetFurnitureInSaved) {
				return;
			}

			//PluginHost.BroadcastEvent(
			//	target: ReciverGroup.All,
			//	senderActor: 0,
			//	targetGroup: 0,
			//	data: new Dictionary<byte, object>() {
			//		{245, JsonConvert.SerializeObject(furniture)}
			//	},
			//	evCode: info.Request.EvCode,
			//	cacheOp: CacheOperations.DoNotCache
			//);
		}
	}
}