using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private readonly Database database;

		private delegate void OnRaiseEventDelegate(IRaiseEventCallInfo _);
		private OnRaiseEventDelegate myOnRaiseEventDelegate;

		public override string Name => "MyFirstPlugin"; //The reserved plugin names are "Default" and "ErrorPlugin"

		internal MyFirstPlugin() {
			database = new Database();
			database.Connect("localhost", 3306, "test_db", "root", "password");

			myOnRaiseEventDelegate = null;
			myOnRaiseEventDelegate += LogInEventHandler;
			myOnRaiseEventDelegate += SignUpEventHandler;
		}

		~MyFirstPlugin() {
			database.Disconnect();
		}

		private static List<T> DataTableToList<T>(DataTable dt) {
			List<T> data = new List<T>();

			foreach(DataRow row in dt.Rows) {
				T item = GetItem<T>(row);
				data.Add(item);
			}

			return data;
		}

		private static T GetItem<T>(DataRow dr) {
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach(DataColumn column in dr.Table.Columns) {
				foreach(PropertyInfo pro in temp.GetProperties()) {
					if(pro.Name != column.ColumnName) {
						continue;
					}

					pro.SetValue(obj, dr[column.ColumnName], null);
				}
			}

			return obj;
		}

		public override void OnCreateGame(ICreateGameCallInfo info) {
            //PluginHost.LogInfo(string.Format("OnCreateGame {0} by user {1}", info.Request.GameId, info.UserId));

			base.OnCreateGame(info); //info.Continue();
        }

		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);

			PluginHost.LogInfo("here0"); //

			myOnRaiseEventDelegate?.Invoke(info);
		}
	}
}