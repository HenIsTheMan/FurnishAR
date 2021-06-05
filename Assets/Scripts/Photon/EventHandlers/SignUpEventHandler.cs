using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

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
            if(photonEvent.Code == (byte)EventCodes.EventCode.SignUp) {
                signUpInfoLabel.text = "Sign Up Success!";
                signUpInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);
            }
        }
    }
}