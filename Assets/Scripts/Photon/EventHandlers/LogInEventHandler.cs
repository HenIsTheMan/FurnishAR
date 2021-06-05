using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class LogInEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInEventHandler(): base() {
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
                Generic.Console.Log(photonEvent.CustomData);
            }
        }
    }
}