namespace FurnishAR.App {
	internal static class SignUpStatuses: object {
		internal enum SignUpStatus: int {
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