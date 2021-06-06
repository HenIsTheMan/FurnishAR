namespace FurnishAR.App {
	internal static class LogInStatuses: object {
		internal enum LogInStatus: int {
			None,
			NoUsernameOrEmail,
			NoPassword,
			Success,
			FailureDueToWrongUsername,
			FailureDueToWrongEmail,
			FailureDueToWrongPassword,
			Amt
		}
	}
}