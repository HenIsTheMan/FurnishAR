namespace MyFirstPlugin {
	internal static class LogInStatuses: object {
		internal enum LogInStatus: byte {
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