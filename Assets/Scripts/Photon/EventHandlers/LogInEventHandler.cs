using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using UnityEngine;
using static FurnishAR.App.LogInStatuses;

namespace FurnishAR.Photon {
    internal sealed class LogInEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private LogIn logIn;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInEventHandler(): base() {
            logIn = null;
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
                    logIn.logInInfoLabel.text = "\"Username or Email\" cannot be blank!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.NoPassword:
                    logIn.logInInfoLabel.text = "\"Password\" cannot be blank!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.Success:
                    logIn.logInInfoLabel.text = "Log In Success!";
                    logIn.logInInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                    break;
                case LogInStatus.WrongUsername:
                    logIn.logInInfoLabel.text = $"Username \"{logInDataJSON["username"].Value}\" is unregistered!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.WrongEmail:
                    logIn.logInInfoLabel.text = $"Email \"{logInDataJSON["email"].Value}\" is unregistered!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.WrongPassword:
                    logIn.logInInfoLabel.text = "Password is incorrect!";
                    logIn.logInInfoLabel.color = Color.red;

                    logIn.ClearPasswordInputField();

                    break;
            }
        }
    }
}