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
					acctData.username = user.Username;
					acctData.email = user.Email;

					//* Gen session token (includes encryption)
					int sessionTokenLen = 40;
					string sessionToken = string.Empty;

					for(int j = 0; j < sessionTokenLen; ++j) {
						sessionToken += (char)PseudorandRange(33.0f, 127.0f, (uint)(user.ID ^ j ^ 2645));
					}

					acctData.sessionToken = sessionToken;

					int[] valsASCII = new int[(sessionTokenLen & 1) == 1 ? sessionTokenLen + 1 : sessionTokenLen];
					int valsASCIILen = valsASCII.Length;

					for(int j = 0; j < sessionTokenLen; ++j) {
						valsASCII[j] = sessionToken[j];
					}
					if((sessionTokenLen & 1) == 1) {
						valsASCII[valsASCIILen - 1] = -1; //Invalid val
					}

					int[] key = new int[4]{
						2,
						3,
						3,
						5
					};

					int[] encryptedValsASCII = new int[valsASCIILen];
					int limit = valsASCIILen / 2;
					int index0;
					int index1;

					for(int j = 0; j < limit; ++j) {
						index0 = 2 * j;
						index1 = index0 + 1;

						encryptedValsASCII[index0] = key[0] * valsASCII[index0] + key[2] * valsASCII[index1];
						encryptedValsASCII[index1] = key[1] * valsASCII[index0] + key[3] * valsASCII[index1];
					}

					UpdateUserByID("sessionToken", JsonConvert.SerializeObject(encryptedValsASCII), user.ID);
					//*/

					break;
				}
			}

			BroadcastEvent: {
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
}