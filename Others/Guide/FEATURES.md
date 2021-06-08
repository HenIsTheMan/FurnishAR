## Done

User Account Management:
- Login
- Registration
- Password Encryption (done with Hill cipher [utilises matrices for encryption and decryption])
- Password Validation (Confirm Password)
- Unique Username
- Delete acct
- Others
	- Unique Email
	- Show/Hide Password
	- Figured out how to connect from mobile (had to change 127.0.0.1 to IPV4 Address of PC in multiple places)
	- Figured out how to connect using TCP (had to change port number)
	- Session token (string in client [uses player prefs to save], JSON representing arr of ints in DB)
	- Remember Me
	- Log Out

Inventory or Skill Tree Systems:
- Add
- Remove
X Categorisation (Could not finish in time)
- Others
	- Browse
	- Saved

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
- Display and change pos of words in full name
- Make border(s) of input field red when...
- Password verification for edit acct
- Deactivate Account
- Edit Account
- Reset Password
- Delete acct confirmation (easy)
- 2 d.p. for price
- Profile pic
- Upgrade or level up
- Multiple presses on button (easy)
- Remove from inv_table when delete acct (high priority)