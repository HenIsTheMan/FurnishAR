using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;
using static FurnishAR.App.SignUpStatuses;

namespace FurnishAR.Photon {
    internal sealed class SignUpEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private TMP_Text signUpInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SignUpEventHandler(): base() {
            signUpInfoLabel = null;
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
                    signUpInfoLabel.text = "\"First Name\" cannot be blank!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoLastName:
                    signUpInfoLabel.text = "\"Last Name\" cannot be blank!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoUsername:
                    signUpInfoLabel.text = "\"Username\" cannot be blank!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoEmail:
                    signUpInfoLabel.text = "\"Email\" cannot be blank!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoNewPassword:
                    signUpInfoLabel.text = "\"New Password\" cannot be blank!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.NoConfirmPassword:
                    signUpInfoLabel.text = "\"Confirm Password\" cannot be blank!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.PasswordsNotMatching:
                    signUpInfoLabel.text = "\"New Password\" and \"Confirm Password\" do not match!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.UsernameNotUnique:
                    signUpInfoLabel.text = $"Username \"{signUpDataJSON["username"].Value}\" is already in use!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.EmailNotUnique:
                    signUpInfoLabel.text = $"Email \"{signUpDataJSON["email"].Value}\" is already in use!";
                    signUpInfoLabel.color = Color.red;

                    break;
                case SignUpStatus.Success:
                    signUpInfoLabel.text = "Sign Up Success!";
                    signUpInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                    break;
            }
        }
    }
}