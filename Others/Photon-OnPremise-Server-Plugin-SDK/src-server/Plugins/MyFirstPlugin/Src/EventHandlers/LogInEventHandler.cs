using Photon.Hive.Plugin;
using static MyFirstPlugin.Src.EventCodes;

namespace MyFirstPlugin {
	internal sealed class LogInEventHandler: PluginBase {
		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);

			if(info.Request.EvCode == (byte)EventCode.LogIn) {
				string data = System.Text.Encoding.Default.GetString((byte[])info.Request.Data);

				PluginHost.BroadcastEvent( 
					target: ReciverGroup.All, 
					senderActor: 0, 
					targetGroup: 0, 
					data: new System.Collections.Generic.Dictionary<byte, object>() { { 245, data } }, 
					evCode: info.Request.EvCode,
					cacheOp: CacheOperations.DoNotCache
				);
 
				//DataTable dt = db.Query("SELECT * FROM students"); 
				//List<Student> students = DataTableToList<Student>(dt); 
				//string response = string.Format("{0}", JsonConvert.SerializeObject(students)); 
 
				//PluginHost.BroadcastEvent( 
				//	recieverActors: new List<int>() { info.ActorNr }, 
				//	senderActor: 0, 
				//	data: new Dictionary<byte, object>() { { 245, response } }, 
				//	evCode: info.Request.EvCode, 
				//	cacheOp: CacheOperations.DoNotCache 
				//); 
			}
		}
	}
}