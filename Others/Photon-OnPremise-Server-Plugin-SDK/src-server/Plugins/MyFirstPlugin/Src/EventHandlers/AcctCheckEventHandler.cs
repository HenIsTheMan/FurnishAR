using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using static MyFirstPlugin.EventCodes;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private void AcctCheckEventHandler(IRaiseEventCallInfo info) {
			if(info.Request.EvCode != (byte)EventCode.AcctCheck) {
				return;
			}

			AcctData acctData = new AcctData();

			User user;
			User[] users = RetrieveUsers();
			int usersLen = users.Length;

			for(int i = 0; i < usersLen; ++i) {
				user = users[i];

				int[] encryptedValsASCII = JsonConvert.DeserializeObject<int[]>(user.SessionToken);
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

				string sessionToken = string.Empty;
				for(int j = 0; j < encryptedValsASCIILen; ++j) {
					if(decryptedValsASCII[j] >= 0) {
						sessionToken += (char)decryptedValsASCII[j];
					}
				}

				if(sessionToken == (string)info.Request.Data) {
					acctData.username = user.Username;
					acctData.email = user.Email;

					break;
				}
			}

			PluginHost.BroadcastEvent(
				target: ReciverGroup.All,
				senderActor: 0,
				targetGroup: 0,
				data: new Dictionary<byte, object>() {
					{245, JsonConvert.SerializeObject(acctData)}
				},
				evCode: info.Request.EvCode,
				cacheOp: CacheOperations.DoNotCache
			);
		}
	}
}