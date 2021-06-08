using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using static MyFirstPlugin.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void RemoveFurnitureFromSavedEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.RemoveFurnitureFromSaved) {
				return;
			}

			List<Furniture> furniture = RetrieveFurniture();

			//Remove saved furniture??

			PluginHost.BroadcastEvent(
				target: ReciverGroup.All,
				senderActor: 0,
				targetGroup: 0,
				data: new Dictionary<byte, object>() {
					{245, JsonConvert.SerializeObject(furniture)}
				},
				evCode: info.Request.EvCode,
				cacheOp: CacheOperations.DoNotCache
			);
		}
	}
}