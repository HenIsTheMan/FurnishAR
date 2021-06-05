using Photon.Hive.Plugin;
using static MyFirstPlugin.Src.EventCodes;

namespace MyFirstPlugin {
	internal sealed class SignUpEventHandler: PluginBase {
		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);

			if(info.Request.EvCode == (byte)EventCode.SignUp) {
			}
		}
	}
}