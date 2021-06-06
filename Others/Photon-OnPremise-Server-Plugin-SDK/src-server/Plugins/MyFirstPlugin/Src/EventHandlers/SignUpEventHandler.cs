using Newtonsoft.Json;
using Photon.Hive.Plugin;
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

			if(middleName.Contains(' ')) {
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

				if(!substr0.All(char.IsLetterOrDigit)
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

				if(username == user.Username) {
					signUpData.status = SignUpStatus.UsernameNotUnique;
					signUpData.username = username;
					goto BroadcastEvent;
				}

				if(email == user.Email) {
					signUpData.status = SignUpStatus.EmailNotUnique;
					signUpData.email = email;
					goto BroadcastEvent;
				}
			}

			signUpData.status = SignUpStatus.Success;
			signUpData.username = username;
			signUpData.email = email;

			user = new User {
				ID = RetrieveHighestIDOfUser() + 1,
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Username = username,
				Email = email,
				Password = newPassword
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