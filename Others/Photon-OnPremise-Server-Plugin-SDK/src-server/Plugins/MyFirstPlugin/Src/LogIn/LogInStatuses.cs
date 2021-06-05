namespace MyFirstPlugin.Src {
	internal static class LogInStatuses: object {
		internal enum LogInStatus: byte {
			None,
			Success,
			FailureDueToWrongUsername,
			FailureDueToWrongEmail,
			FailureDueToWrongPassword,
			Amt
		}
	}
}