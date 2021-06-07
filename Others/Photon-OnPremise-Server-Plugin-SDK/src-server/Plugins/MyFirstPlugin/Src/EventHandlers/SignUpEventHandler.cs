using Newtonsoft.Json;
using Photon.Hive.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
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

			string middleName = signUpInfo[1];

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

			if(firstName.Contains(' ')) {
				signUpData.status = SignUpStatus.SpacesInFirstName;
				goto BroadcastEvent;
			}

			if(!string.IsNullOrEmpty(middleName) && middleName.Contains(' ')) {
				signUpData.status = SignUpStatus.SpacesInMiddleName;
				goto BroadcastEvent;
			}

			if(lastName.Contains(' ')) {
				signUpData.status = SignUpStatus.SpacesInLastName;
				goto BroadcastEvent;
			}

			if(username.Contains(' ')) {
				signUpData.status = SignUpStatus.SpacesInUsername;
				goto BroadcastEvent;
			}

			if(email.Contains(' ')) {
				signUpData.status = SignUpStatus.SpacesInEmail;
				goto BroadcastEvent;
			}

			if(firstName.Length > 40) {
				signUpData.status = SignUpStatus.FirstNameTooLong;
				goto BroadcastEvent;
			}

			if(middleName.Length > 40) {
				signUpData.status = SignUpStatus.MiddleNameTooLong;
				goto BroadcastEvent;
			}

			if(lastName.Length > 40) {
				signUpData.status = SignUpStatus.LastNameTooLong;
				goto BroadcastEvent;
			}

			if(username.Length > 20) {
				signUpData.status = SignUpStatus.UsernameTooLong;
				goto BroadcastEvent;
			}

			if(!firstName.All(char.IsLetter)) {
				signUpData.status = SignUpStatus.FirstNameHasInvalidChars;
				goto BroadcastEvent;
			}

			if(!string.IsNullOrEmpty(middleName) && !middleName.All(char.IsLetter)) {
				signUpData.status = SignUpStatus.MiddleNameHasInvalidChars;
				goto BroadcastEvent;
			}

			if(!lastName.All(char.IsLetter)) {
				signUpData.status = SignUpStatus.LastNameHasInvalidChars;
				goto BroadcastEvent;
			}

			if(!username.All(char.IsLetterOrDigit)) {
				signUpData.status = SignUpStatus.UsernameHasInvalidChars;
				goto BroadcastEvent;
			}

			int atIndex = email.IndexOf('@');
			int dotIndex = email.IndexOf('.');
			int emailLen = email.Length;

			if(email.Count(myChar => myChar == '@') != 1
				|| email.Count(myChar => myChar == '.') != 1
				|| atIndex < 1
				|| dotIndex < 3
				|| atIndex > emailLen - 4
				|| dotIndex > emailLen - 2
				|| (atIndex >= dotIndex - 1)
			) {
				signUpData.status = SignUpStatus.InvalidEmail;
				goto BroadcastEvent;
			} else {
				string substr0 = email.Substring(0, atIndex);
				string substr1 = email.Substring(atIndex + 1, dotIndex - atIndex - 1);
				string substr2 = email.Substring(dotIndex + 1, emailLen - dotIndex - 1);

				if(substr2.Length < 2
					|| !substr0.All(char.IsLetterOrDigit)
					|| !substr1.All(char.IsLetterOrDigit)
					|| !substr2.All(char.IsLetter)
				) {
					signUpData.status = SignUpStatus.InvalidEmail;
					goto BroadcastEvent;
				}
			}

			User user;
			User[] users = RetrieveUsers();
			int usersLen = users.Length;

			for(int i = 0; i < usersLen; ++i) {
				user = users[i];

				if(username.ToLower() == user.Username.ToLower()) {
					signUpData.status = SignUpStatus.UsernameNotUnique;
					signUpData.username = username;
					goto BroadcastEvent;
				}

				if(email.ToLower() == user.Email.ToLower()) {
					signUpData.status = SignUpStatus.EmailNotUnique;
					signUpData.email = email;
					goto BroadcastEvent;
				}
			}

			signUpData.status = SignUpStatus.Success;
			signUpData.username = username;
			signUpData.email = email;

			firstName = firstName.ToLower();
			firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);

			if(!string.IsNullOrEmpty(middleName)) {
				middleName = middleName.ToLower();
				middleName = char.ToUpper(middleName[0]) + middleName.Substring(1);
			}

			lastName = lastName.ToLower();
			lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);

			//* Password Encryption
			int passwordLen = newPassword.Length;
			int[] valsASCII = new int[(passwordLen & 1) == 1 ? passwordLen + 1 : passwordLen];
			int valsASCIILen = valsASCII.Length;

			/* valsASCII (follows col-major order)
				valsASCII[0] ...
				valsASCII[1] ...
			//*/

			for(int i = 0; i < passwordLen; ++i) {
				valsASCII[i] = newPassword[i];
			}
			if((passwordLen & 1) == 1) {
				valsASCII[valsASCIILen - 1] = -1; //Invalid val
			}

			int[] key = new int[4]{
				4,
				1,
				7,
				2
			};

			/* key (follows col-major order)
				4 7
				1 2
			//*/

			///Matrix Multiplication
			int[] encryptedValsASCII = new int[valsASCIILen];
			int limit = valsASCIILen / 2;
			int index0;
			int index1;

			for(int i = 0; i < limit; ++i) {
				index0 = 2 * i;
				index1 = index0 + 1;

				encryptedValsASCII[index0] = key[0] * valsASCII[index0] + key[2] * valsASCII[index1];
				encryptedValsASCII[index1] = key[1] * valsASCII[index0] + key[3] * valsASCII[index1]; 
			}
			///

			newPassword = JsonConvert.SerializeObject(encryptedValsASCII);
			//*/

			int myUserID = RetrieveHighestIDOfUser() + 1;

			//* Gen session token (includes encryption)
			int sessionTokenLen = 40;
			string sessionToken = string.Empty;

			for(int j = 0; j < sessionTokenLen; ++j) {
				sessionToken += (char)PseudorandRange(33.0f, 127.0f, (uint)(myUserID ^ j ^ DateTime.Now.Millisecond));
			}

			valsASCII = new int[(sessionTokenLen & 1) == 1 ? sessionTokenLen + 1 : sessionTokenLen];
			valsASCIILen = valsASCII.Length;

			for(int j = 0; j < sessionTokenLen; ++j) {
				valsASCII[j] = sessionToken[j];
			}
			if((sessionTokenLen & 1) == 1) {
				valsASCII[valsASCIILen - 1] = -1; //Invalid val
			}

			key[0] = 2;
			key[1] = 3;
			key[2] = 3;
			key[3] = 5;

			encryptedValsASCII = new int[valsASCIILen];
			limit = valsASCIILen / 2;

			for(int j = 0; j < limit; ++j) {
				index0 = 2 * j;
				index1 = index0 + 1;

				encryptedValsASCII[index0] = key[0] * valsASCII[index0] + key[2] * valsASCII[index1];
				encryptedValsASCII[index1] = key[1] * valsASCII[index0] + key[3] * valsASCII[index1];
			}
			//*/

			UpdateUserByID("sessionToken", JsonConvert.SerializeObject(encryptedValsASCII), myUserID);

			user = new User {
				ID = myUserID,
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Username = username,
				Email = email,
				Password = newPassword,
				SessionToken = sessionToken
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