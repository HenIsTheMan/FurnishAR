using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;
using static FurnishAR.App.LogInStatuses;

namespace FurnishAR.Photon {
    internal sealed class LogInEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private TMP_Text logInInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInEventHandler(): base() {
            logInInfoLabel = null;
        }

        static LogInEventHandler() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        private void OnDisable() {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code != (byte)EventCodes.EventCode.LogIn) {
                return;
            }

            JSONNode logInDataJSON = JSON.Parse((string)photonEvent.CustomData);

            switch((LogInStatus)logInDataJSON["status"].AsInt){
                case LogInStatus.NoUsernameOrEmail:
                    logInInfoLabel.text = "\"Username or Email\" cannot be blank!";
                    logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.NoPassword:
                    logInInfoLabel.text = "\"Password\" cannot be blank!";
                    logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.Success:
                    logInInfoLabel.text = "Log In Success!";
                    logInInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                    break;
                case LogInStatus.WrongUsername:
                    logInInfoLabel.text = $"Username \"{logInDataJSON["username"].Value}\" is unregistered!";
                    logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.WrongEmail:
                    logInInfoLabel.text = $"Email \"{logInDataJSON["email"].Value}\" is unregistered!";
                    logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.WrongPassword:
                    logInInfoLabel.text = "Password is incorrect!";
                    logInInfoLabel.color = Color.red;

                    break;
            }
        }
    }
}