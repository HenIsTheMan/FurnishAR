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
        private CanvasGroup logInSignUpGrpCanvasGrp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AcctCheckEventHandler(): base() {
            acctManager = null;

            logInSignUpGrpCanvasGrp = null;
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
                logInSignUpGrpCanvasGrp.alpha = 1.0f;
                logInSignUpGrpCanvasGrp.blocksRaycasts = true;
                logInSignUpGrpCanvasGrp.GetComponentInChildren<LogInSignUp>().InitMe();
            } else {
                acctManager.bigAcctInfoLabel.text = $"{acctCheckData["username"].Value}\n{acctCheckData["email"].Value}";
                acctManager.acctCanvasGrp.alpha = 1.0f;
                acctManager.acctCanvasGrp.blocksRaycasts = true;

                PlayerPrefs.SetString("sessionToken", sessionToken); //Save session token
            }
        }
    }
}