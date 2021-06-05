using Newtonsoft.Json;

namespace MyFirstPlugin {
	[JsonObject(MemberSerialization.Fields)]
	internal sealed class SignUpData {
		internal string username = string.Empty;
		internal string email = string.Empty;
	}
}