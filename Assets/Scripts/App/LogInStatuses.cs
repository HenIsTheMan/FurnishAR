namespace FurnishAR.App {
	internal static class LogInStatuses: object {
		internal enum LogInStatus: int {
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