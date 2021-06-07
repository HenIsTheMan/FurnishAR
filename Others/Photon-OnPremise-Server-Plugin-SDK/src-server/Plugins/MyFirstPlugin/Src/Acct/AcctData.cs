using Newtonsoft.Json;

namespace MyFirstPlugin {
	[JsonObject(MemberSerialization.Fields)]
	internal sealed class AcctData {
		internal string username = string.Empty;
		internal string email = string.Empty;
		internal string sessionToken = string.Empty;
	}
}