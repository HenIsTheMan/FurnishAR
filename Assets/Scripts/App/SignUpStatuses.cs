namespace FurnishAR.App {
	internal static class SignUpStatuses: object {
		internal enum SignUpStatus: byte {
			None,
			NoFirstName,
			NoLastName,
			NoUsername,
			NoEmail,
			NoPassword,
			NoConfirmPassword,
			PasswordsNotMatching,
			Success,
			UsernameNotUnique,
			EmailNotUnique,
			Amt
		}
	}
}