namespace MyFirstPlugin {
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
			InvalidEmail,
			UsernameNotUnique,
			EmailNotUnique,
			Success,
			Amt
		}
	}
}