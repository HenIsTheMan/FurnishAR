namespace FurnishAR.App {
	internal static class SignUpStatuses: object {
		internal enum SignUpStatus: byte {
			None,
			NoFirstName,
			NoLastName,
			NoUsername,
			NoEmail,
			NoNewPassword,
			NoConfirmPassword,
			PasswordsNotMatching,
			UsernameNotUnique,
			EmailNotUnique,
			Success,
			Amt
		}
	}
}