using MySql.Data.MySqlClient;
using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstPlugin {
	internal sealed partial class MyFirstPlugin: PluginBase {
		private Database database;
		private readonly MySqlConnection connection;

		private delegate void OnRaiseEventDelegate(IRaiseEventCallInfo _);
		private OnRaiseEventDelegate myOnRaiseEventDelegate;

		public override string Name => "MyFirstPlugin";

		internal MyFirstPlugin() {
			connection = new MySqlConnection {
				ConnectionString = FormConnectionStr("localhost", 3306, "furnishar_db", "root", "password")
			};
			connection.Open();

			database = new Database(connection);

			myOnRaiseEventDelegate = null;
			myOnRaiseEventDelegate += LogInEventHandler;
			myOnRaiseEventDelegate += SignUpEventHandler;
			myOnRaiseEventDelegate += AcctCheckEventHandler;
			myOnRaiseEventDelegate += LogOutEventHandler;
			myOnRaiseEventDelegate += DeleteAcctEventHandler;
			myOnRaiseEventDelegate += GetFurnitureInBrowseEventHandler;
			myOnRaiseEventDelegate += AddFurnitureToSavedEventHandler;
			myOnRaiseEventDelegate += RemoveFurnitureFromSavedEventHandler;
		}

		~MyFirstPlugin() {
			connection.Close();
		}

		private string FormConnectionStr(string host, int port, string db, string user, string password) {
			return $"server={host};user={user};database={db};port={port};password={password}";
		}

		private void AddUser(ref User user) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			try {
				_ = database.ExecuteQuery<User>(
					"INSERT INTO furnishar_db.user_table "
					+ "(id, firstName, middleName, lastName, username, email, password, sessionToken)"
					+ " VALUES "
					+ $"({user.ID}, \"{user.FirstName}\", \"{user.MiddleName}\", \"{user.LastName}\","
					+ $"\"{user.Username}\", \"{user.Email}\", \"{user.Password}\", \"{user.SessionToken}\");"
				);
			} catch(System.Exception) {
			}
		}

		private void UpdateUserByID(string colName, object val, int ID) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			try {
				_ = database.ExecuteQuery<User>(
					"UPDATE furnishar_db.user_table SET "
					+ $"{colName} = '{val}'"
					+ $" WHERE id = {ID};"
				);
			} catch(System.Exception) {
			} finally {
				database = new Database(connection);
			}
		}

		private void RemoveUserByID(int ID) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			try {
				_ = database.ExecuteQuery<User>(
					$"DELETE FROM furnishar_db.user_table WHERE id = {ID};"
				);
			} catch(System.Exception) {
			}
		}

		private User[] RetrieveUsers() {
			return database.ExecuteQuery<User>("SELECT * FROM furnishar_db.user_table").ToArray();
		}

		private int RetrieveHighestIDOfUser(int minExclusive = 0) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			User[] users = database.ExecuteQuery<User>($"SELECT * FROM furnishar_db.user_table ORDER BY id DESC LIMIT {minExclusive}, 1").ToArray();
			return users.Length == 0 ? minExclusive : users[0].ID;
		}

		private List<Furniture> RetrieveFurniture() {
			return database.ExecuteQuery<Furniture>("SELECT * FROM furnishar_db.furniture_table").ToList();
		}

		private void AddToInv(ref User user, ref Furniture furniture) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			try {
				_ = database.ExecuteQuery<User>(
					"INSERT INTO furnishar_db.inv_table "
					+ "(user_id, furniture_id)"
					+ " VALUES "
					+ $"({user.ID}, {furniture.ID});"
				);
			} catch(System.Exception) {
			}
		}

		private void RemoveFromInv(ref User user, ref Furniture furniture) {
			if(database.Connection.State == System.Data.ConnectionState.Open) {
				database.Connection.Close();
			}

			try {
				_ = database.ExecuteQuery<User>(
					"DELETE FROM furnishar_db.inv_table WHERE "
					+ $"user_id = {user.ID} AND furniture_id = {furniture.ID};"
				);
			} catch(System.Exception) {
			}
		}

		private InvRow[] RetrieveInvRow() {
			return database.ExecuteQuery<InvRow>("SELECT * FROM furnishar_db.inv_table").ToArray();
		}

		private FurnitureSaved[] Test() {
			return database.ExecuteQuery<FurnitureSaved>("SELECT furnishar_db.furniture_table.name, furnishar_db.furniture_table.price FROM furnishar_db.furniture_table").ToArray();
		}

		private FurnitureSaved[] RetrieveFurnitureInBrowse(int userID) {
			return database.ExecuteQuery<FurnitureSaved>(
				"SELECT furnishar_db.furniture_table.name, furnishar_db.furniture_table.price "
				+ $"FROM (SELECT * FROM furnishar_db.inv_table WHERE user_id = {userID}) AS A "
				+ "RIGHT JOIN furnishar_db.furniture_table ON A.user_id = furnishar_db.furniture_table.id WHERE user_id IS NULL;"
			).ToArray();
		}

		private FurnitureSaved[] RetrieveFurnitureInSaved(int userID) {
			return database.ExecuteQuery<FurnitureSaved>(
				"SELECT furnishar_db.furniture_table.name, furnishar_db.furniture_table.price "
				+ $"FROM (SELECT * FROM furnishar_db.inv_table WHERE user_id = {userID}) AS A "
				+ "INNER JOIN furnishar_db.furniture_table ON A.user_id = furnishar_db.furniture_table.id;"
			).ToArray();
		}

		public override void OnCreateGame(ICreateGameCallInfo info) {
            //PluginHost.LogInfo(string.Format("OnCreateGame {0} by user {1}", info.Request.GameId, info.UserId));

			base.OnCreateGame(info); //info.Continue();
        }

		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);

			myOnRaiseEventDelegate?.Invoke(info);
		}

		private static uint Hash(uint seed) {
			seed ^= 2747636419u;
			seed *= 2654435769u;
			seed ^= seed >> 16;
			seed *= 2654435769u;
			seed ^= seed >> 16;
			seed *= 2654435769u;
			return seed;
		}

		private static float PseudorandRange(float min, float max, uint seed) {
			return min + (Hash(seed) / 4294967295.0f) * (max - min);
		}
	}
}