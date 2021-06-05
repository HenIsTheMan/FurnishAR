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
            if(photonEvent.Code == (byte)EventCodes.EventCode.LogIn) {
                JSONNode logInDataJSON = JSON.Parse((string)photonEvent.CustomData);

                switch((LogInStatus)logInDataJSON["status"].AsInt){
                    case LogInStatus.Success:
                        logInInfoLabel.text = "Log In Success!";
                        logInInfoLabel.color = Color.green;

                        break;
                    case LogInStatus.FailureDueToWrongUsername:
                        logInInfoLabel.text = "Log In Failed: Username \"" + logInDataJSON["username"].Value + "\" is unregistered";
                        logInInfoLabel.color = Color.red;

                        break;
                    case LogInStatus.FailureDueToWrongEmail:
                        logInInfoLabel.text = "Log In Failed: Email \"" + logInDataJSON["email"].Value + "\" is unregistered";
                        logInInfoLabel.color = Color.red;

                        break;
                    case LogInStatus.FailureDueToWrongPassword:
                        logInInfoLabel.text = "Log In Failed: Password is incorrect";
                        logInInfoLabel.color = Color.red;

                        break;
                }
            }
        }
    }
}