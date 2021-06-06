## Doing

User Account Management:
- Edit Account
- Delete/Deactivate Account
- Reset Password

Inventory or Skill Tree Systems:
- Add
- Remove
- Upgrade or level up
- Categorisation

Others:
profile pic, remember me

## Done

User Account Management:
- Login
- Registration
- Password Encryption (done with Hill cipher [utilises matrices for encryption and decryption])
- Password Validation (Confirm Password)
- Unique Username
- Others
	- Unique Email
	- Show/Hide Password
	- Figured out how to connect from mobile (had to change 127.0.0.1 to IPV4 Address of PC in multiple places)
	- Figured out how to connect using TCP (had to change port number)

User Interface:
- Feedback
	- Info label which informs the user whether log in is successful or not (with the reason)
	- Info label which informs the user whether sign up is successful or not (with the reason)
- Proper input fields
- Input validations
	- Accounted for casing of all inputs
	- Checked for invalid characters in firstName, middleName, lastName and username when the user tries to sign up
	- Checked whether every input field is not empty
	- Checked whether every input has the correct format
	- Checked whether every input exceeds length limits
	- Checked whether input email is valid
	- Disallowed the user to successfully log in or sign up if there are spaces in any of the input fields except for password input fields
	- Placed limits on the lengths of firstName, middleName, lastName and username when the user tries to sign up
- Others
	- Cleared password on wrong password
	- Cleared the relevant input fields and info labels when switching tabs or when going back to MainScene from AcctScene

Software Engineering:
- Hierarchy and Project Structure
- Code Design
- Database Structure Design

Others:
- Auto-start of Photon Socket Server (use of Photon Control is therefore unnecessary and more inconvenient)
- Obj-Relational Mapping (ORM)
- Serialization and Deserialization (JSON is involved)

## Maybe

- change 245
- Better ORM
- Display name or email (userDisplayLabelType)
- Password requirements
- Password strength
- Password cannot be username
- User Prefs (audio, ...)