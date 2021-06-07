using Newtonsoft.Json;
using static MyFirstPlugin.SignUpStatuses;

namespace MyFirstPlugin {
	[JsonObject(MemberSerialization.Fields)]
	internal sealed class SignUpData {
		internal SignUpStatus status = SignUpStatus.None;
		internal string username = string.Empty;
		internal string email = string.Empty;
		internal string sessionToken = string.Empty;
	}
}