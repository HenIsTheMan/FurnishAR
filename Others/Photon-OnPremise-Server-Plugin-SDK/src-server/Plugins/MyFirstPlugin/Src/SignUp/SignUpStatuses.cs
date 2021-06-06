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
			SpacesInFirstName,
			SpacesInMiddleName,
			SpacesInLastName,
			SpacesInUsername,
			SpacesInEmail,
			FirstNameTooLong,
			MiddleNameTooLong,
			LastNameTooLong,
			UsernameTooLong,
			FirstNameHasInvalidChars,
			MiddleNameHasInvalidChars,
			LastNameHasInvalidChars,
			UsernameHasInvalidChars,
			InvalidEmail,
			UsernameNotUnique,
			EmailNotUnique,
			Success,
			Amt
		}
	}
}