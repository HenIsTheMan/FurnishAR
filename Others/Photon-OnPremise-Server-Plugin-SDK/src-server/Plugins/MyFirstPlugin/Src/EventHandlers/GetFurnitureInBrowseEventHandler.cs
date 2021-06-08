using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using static MyFirstPlugin.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void GetFurnitureInBrowseEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.GetFurnitureInBrowse) {
				return;
			}

			FurnitureSaved[] furnitureSaved = null;

			string sessionTokenFromClient = (string)info.Request.Data;
			if(string.IsNullOrEmpty(sessionTokenFromClient)) {
				goto BroadcastEvent;
			}

			User user;
			User[] users = RetrieveUsers();
			int usersLen = users.Length;

			for(int i = 0; i < usersLen; ++i) {
				user = users[i];

				//* Retrieving session token from DB (includes decryption)
				string sessionTokenFromDB = string.Empty;
				{
					int[] encryptedValsASCII = JsonConvert.DeserializeObject<int[]>(user.SessionToken);
					if(encryptedValsASCII == null) {
						if(i == usersLen - 1) {
							goto BroadcastEvent;
						}

						continue;
					}
					int encryptedValsASCIILen = encryptedValsASCII.Length;

					int[] keyInverse = new int[4]{
						5,
						-3,
						-3,
						2
					};

					int[] decryptedValsASCII = new int[encryptedValsASCIILen];
					int limit = encryptedValsASCIILen / 2;
					int index0;
					int index1;

					for(int j = 0; j < limit; ++j) {
						index0 = 2 * j;
						index1 = index0 + 1;

						decryptedValsASCII[index0] = keyInverse[0] * encryptedValsASCII[index0] + keyInverse[2] * encryptedValsASCII[index1];
						decryptedValsASCII[index1] = keyInverse[1] * encryptedValsASCII[index0] + keyInverse[3] * encryptedValsASCII[index1];
					}

					for(int j = 0; j < encryptedValsASCIILen; ++j) {
						if(decryptedValsASCII[j] >= 0) {
							sessionTokenFromDB += (char)decryptedValsASCII[j];
						}
					}
				}
				//*/

				if(sessionTokenFromDB == sessionTokenFromClient) {
					furnitureSaved = RetrieveFurnitureInBrowse(user.ID);
					PluginHost.LogInfo(furnitureSaved.Length + "YASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");

					break;
				}
			}

			PluginHost.LogInfo(furnitureSaved.Length + "HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");

			BroadcastEvent: {
				PluginHost.BroadcastEvent(
					target: ReciverGroup.All,
					senderActor: 0,
					targetGroup: 0,
					data: new Dictionary<byte, object>() {
						{245, JsonConvert.SerializeObject(furnitureSaved)}
					},
					evCode: info.Request.EvCode,
					cacheOp: CacheOperations.DoNotCache
				);
			}
		}
	}
}