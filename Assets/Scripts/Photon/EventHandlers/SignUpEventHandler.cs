using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using UnityEngine;
using static FurnishAR.App.SignUpStatuses;

namespace FurnishAR.Photon {
    internal sealed class SignUpEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        private JSONNode signUpData;

        [SerializeField]
        private AcctManager acctManager;

        [SerializeField]
        private SignUp signUp;

        [SerializeField]
        private CanvasGroup logInSignUpGrpCanvasGrp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SignUpEventHandler(): base() {
            signUpData = null;

            acctManager = null;

            signUp = null;

            logInSignUpGrpCanvasGrp = null;
        }

        static SignUpEventHandler() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable() {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code != (byte)EventCodes.EventCode.SignUp) {
                return;
            }

            signUpData = JSON.Parse((string)photonEvent.CustomData);

            switch((SignUpStatus)signUpData["status"].AsInt) {
                case SignUpStatus.NoFirstName:
                    signUp.signUpInfoLabel.text = "\"First Name\" cannot be blank!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoLastName:
                    signUp.signUpInfoLabel.text = "\"Last Name\" cannot be blank!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoUsername:
                    signUp.signUpInfoLabel.text = "\"Username\" cannot be blank!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoEmail:
                    signUp.signUpInfoLabel.text = "\"Email\" cannot be blank!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoNewPassword:
                    signUp.signUpInfoLabel.text = "\"New Password\" cannot be blank!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoConfirmPassword:
                    signUp.signUpInfoLabel.text = "\"Confirm Password\" cannot be blank!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.PasswordsNotMatching:
                    signUp.signUpInfoLabel.text = "\"New Password\" and \"Confirm Password\" do not match!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.SpacesInFirstName:
                    signUp.signUpInfoLabel.text = "\"First Name\" should not contain space(s)!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.SpacesInMiddleName:
                    signUp.signUpInfoLabel.text = "\"Middle Name (optional)\" should not contain space(s)!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.SpacesInLastName:
                    signUp.signUpInfoLabel.text = "\"Last Name\" should not contain space(s)!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.SpacesInUsername:
                    signUp.signUpInfoLabel.text = "\"Username\" should not contain space(s)!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.SpacesInEmail:
                    signUp.signUpInfoLabel.text = "\"Email\" should not contain space(s)!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.FirstNameTooLong:
                    signUp.signUpInfoLabel.text = "\"First Name\" is limited to 40 characters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.MiddleNameTooLong:
                    signUp.signUpInfoLabel.text = "\"Middle Name\" is limited to 40 characters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.LastNameTooLong:
                    signUp.signUpInfoLabel.text = "\"Last Name\" is limited to 40 characters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.UsernameTooLong:
                    signUp.signUpInfoLabel.text = "\"Username\" is limited to 20 characters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.FirstNameHasInvalidChars:
                    signUp.signUpInfoLabel.text = "\"First Name\" should only contain letters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.MiddleNameHasInvalidChars:
                    signUp.signUpInfoLabel.text = "\"Middle Name\" should only contain letters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.LastNameHasInvalidChars:
                    signUp.signUpInfoLabel.text = "\"Last Name\" should only contain letters!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.UsernameHasInvalidChars:
                    signUp.signUpInfoLabel.text = "\"Username\" should only contain letters and numbers!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.InvalidEmail:
                    signUp.signUpInfoLabel.text = "Email is invalid!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.UsernameNotUnique:
                    signUp.signUpInfoLabel.text = $"Username \"{signUpData["username"].Value}\" is already in use!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.EmailNotUnique:
                    signUp.signUpInfoLabel.text = $"Email \"{signUpData["email"].Value}\" is already in use!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.Success:
                    signUp.signUpInfoLabel.text = "Sign Up Success!";
                    signUp.signUpInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                    _ = StartCoroutine(nameof(SignUpSuccess));

                    break;
            }
        }

        private System.Collections.IEnumerator SignUpSuccess() {
            yield return new WaitForSeconds(2.0f);

            logInSignUpGrpCanvasGrp.alpha = 0.0f;
            logInSignUpGrpCanvasGrp.blocksRaycasts = false;

            CanvasGroup myCanvasGrp = signUp.GetComponent<CanvasGroup>();
            myCanvasGrp.alpha = 0.0f;
            myCanvasGrp.blocksRaycasts = false;

            acctManager.bigInfoLabel.text = $"{signUpData["username"].Value}\n{signUpData["email"].Value}";
            acctManager.acctCanvasGrp.alpha = 1.0f;
            acctManager.acctCanvasGrp.blocksRaycasts = true;
        }
    }
}