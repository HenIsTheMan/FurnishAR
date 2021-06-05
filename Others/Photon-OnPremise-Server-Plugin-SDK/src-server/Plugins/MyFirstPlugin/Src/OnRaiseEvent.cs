using Photon.Hive.Plugin;

namespace MyFirstPlugin {
    internal sealed partial class MyFirstPlugin: PluginBase {
		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);

			//switch(info.Request.EvCode) {
			//	case EventCode.LogIn: {
			//		break;
			//	}
			//	case 2: {
			//		break;
			//	}
			//}
		}
	}
}