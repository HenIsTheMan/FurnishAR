namespace MyFirstPlugin {
	internal static class LogInStatuses: object {
		internal enum LogInStatus: byte {
			None,
			NoUsernameOrEmail,
			NoPassword,
			SpacesInUsernameOrEmail,
			Success,
			WrongUsername,
			WrongEmail,
			WrongPassword,
			Amt
		}
	}
}