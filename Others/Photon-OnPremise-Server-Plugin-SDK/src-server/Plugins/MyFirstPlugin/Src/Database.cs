using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MyFirstPlugin {
	internal class Database {
		private readonly MySqlConnection connection;

		internal Database() {
			connection = new MySqlConnection();
		}

		internal void Connect(string host, int port, string db, string user, string password) {
			connection.ConnectionString = $"server={host};user={user};database={db};port={port};password={password}";
			connection.Open();
		}

		internal void Disconnect() {
			connection.Close();
		}

		internal DataTable Query(string query) {
			DataTable results = null;

			try {
				using(MySqlCommand cmd = new MySqlCommand(query, connection)) {
					results = new DataTable();

					using(MySqlDataReader reader = cmd.ExecuteReader()) {
						results.Load(reader);

						if(!reader.HasRows) {
							return results;
						}

						int i;
						int fieldCount = reader.FieldCount;
						string text;

						while(reader.Read()) {
							text = string.Empty;

							for(i = 0; i < fieldCount; ++i) {
								text += reader[i] + (i == fieldCount - 1 ? "" : ", ");
							}

							Console.WriteLine(text);
						}
					}
				}
			} catch(Exception e) {
				Console.WriteLine(e.ToString());
			}

			return results;
		}
	}
}
