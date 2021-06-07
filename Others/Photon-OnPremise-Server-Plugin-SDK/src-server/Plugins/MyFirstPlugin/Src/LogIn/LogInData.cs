using Newtonsoft.Json;
using static MyFirstPlugin.LogInStatuses;

namespace MyFirstPlugin {
	[JsonObject(MemberSerialization.Fields)]
	internal sealed class LogInData {
		internal LogInStatus status = LogInStatus.None;
		internal string username = string.Empty;
		internal string email = string.Empty;
		internal string sessionToken = string.Empty;
	}
}