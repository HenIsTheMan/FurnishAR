using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System;
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

			object[] logInInfo = JsonConvert.DeserializeObject<object[]>((string)info.Request.Data);

			string usernameOrEmail = (string)logInInfo[0];
			if(string.IsNullOrEmpty(usernameOrEmail)) {
				logInData.status = LogInStatus.NoUsernameOrEmail;
				goto BroadcastEvent;
			}

			string password = (string)logInInfo[1];
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
					//* Password Decryption
					string userPassword = string.Empty;
					{
						int[] encryptedValsASCII = JsonConvert.DeserializeObject<int[]>(user.Password);
						int encryptedValsASCIILen = encryptedValsASCII.Length;

						int[] keyInverse = new int[4]{
							5,
							-3,
							-3,
							2
						};

						/* keyInverse (follows col-major order)
							5 -3
							-3 2
						//*/

						///Matrix Multiplication
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
						///

						for(int j = 0; j < encryptedValsASCIILen; ++j) {
							if(decryptedValsASCII[j] >= 0) {
								userPassword += (char)decryptedValsASCII[j];
							}
						}
					}
					//*/

					if(userPassword == password) {
						logInData.status = LogInStatus.Success;
						logInData.username = user.Username;
						logInData.email = user.Email;

						//* Gen session token (includes encryption)
						if((bool)logInInfo[2]) {
							int sessionTokenLen = 40;
							string sessionToken = string.Empty;

							for(int j = 0; j < sessionTokenLen; ++j) {
								sessionToken += (char)PseudorandRange(33.0f, 127.0f, (uint)(user.ID ^ j ^ 2645));
							}

							logInData.sessionToken = sessionToken;

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
						}
						//*/

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