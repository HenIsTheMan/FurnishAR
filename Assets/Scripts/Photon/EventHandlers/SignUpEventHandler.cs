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

        [SerializeField]
        private SignUp signUp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SignUpEventHandler(): base() {
            signUp = null;
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

            JSONNode signUpDataJSON = JSON.Parse((string)photonEvent.CustomData);

            switch((SignUpStatus)signUpDataJSON["status"].AsInt) {
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
                case SignUpStatus.UsernameNotUnique:
                    signUp.signUpInfoLabel.text = $"Username \"{signUpDataJSON["username"].Value}\" is already in use!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.EmailNotUnique:
                    signUp.signUpInfoLabel.text = $"Email \"{signUpDataJSON["email"].Value}\" is already in use!";
                    signUp.signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.Success:
                    signUp.signUpInfoLabel.text = "Sign Up Success!";
                    signUp.signUpInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                    break;
            }
        }
    }
}