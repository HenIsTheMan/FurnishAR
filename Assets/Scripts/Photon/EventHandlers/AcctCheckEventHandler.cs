using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class AcctCheckEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private AcctManager acctManager;

        [SerializeField]
        private GameObject logInSignUpGrpGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AcctCheckEventHandler(): base() {
            acctManager = null;

            logInSignUpGrpGO = null;
        }

        static AcctCheckEventHandler() {
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
            if(photonEvent.Code != (byte)EventCodes.EventCode.AcctCheck) {
                return;
            }

            JSONNode acctCheckData = JSON.Parse((string)photonEvent.CustomData);
            string sessionToken = acctCheckData["sessionToken"].Value;

            if(sessionToken == string.Empty) {
                logInSignUpGrpGO.SetActive(true);
            } else {
                acctManager.bigInfoLabel.text = $"{acctCheckData["username"].Value}\n{acctCheckData["email"].Value}";
                acctManager.acctGO.SetActive(true);

                Generic.Console.Log(sessionToken); //??

                PlayerPrefs.SetString("sessionToken", sessionToken); //Save session token
            }
        }
    }
}