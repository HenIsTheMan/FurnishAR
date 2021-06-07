using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class LogOutEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private AcctManager acctManager;

        [SerializeField]
        private CanvasGroup logInSignUpGrpCanvasGrp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogOutEventHandler(): base() {
            acctManager = null;

            logInSignUpGrpCanvasGrp = null;
        }

        static LogOutEventHandler() {
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
            if(photonEvent.Code != (byte)EventCodes.EventCode.LogOut) {
                return;
            }

            if((bool)photonEvent.CustomData) {
                acctManager.smallAcctInfoLabel.text = "Log Out Successful!";
                acctManager.smallAcctInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                _ = StartCoroutine(nameof(LogOutSuccessful));
            } else {
                acctManager.smallAcctInfoLabel.text = "Log Out Failed! Please try again.";
                acctManager.smallAcctInfoLabel.color = Color.red;
            }
        }

        private System.Collections.IEnumerator LogOutSuccessful() {
            yield return new WaitForSeconds(2.0f);

            acctManager.acctCanvasGrp.alpha = 0.0f;
            acctManager.acctCanvasGrp.blocksRaycasts = false;

            logInSignUpGrpCanvasGrp.alpha = 1.0f;
            logInSignUpGrpCanvasGrp.blocksRaycasts = true;
            logInSignUpGrpCanvasGrp.GetComponentInChildren<LogInSignUp>().InitMe();

            acctManager.smallAcctInfoLabel.text = string.Empty;
        }
    }
}