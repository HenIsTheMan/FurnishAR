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

        private JSONNode logInData;

        [SerializeField]
        private AcctManager acctManager;

        [SerializeField]
        private LogIn logIn;

        [SerializeField]
        private CanvasGroup logInSignUpGrpCanvasGrp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInEventHandler(): base() {
            logInData = null;

            acctManager = null;

            logIn = null;

            logInSignUpGrpCanvasGrp = null;
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

            logInData = JSON.Parse((string)photonEvent.CustomData);

            switch((LogInStatus)logInData["status"].AsInt){
                case LogInStatus.NoUsernameOrEmail:
                    logIn.logInInfoLabel.text = "\"Username or Email\" cannot be blank!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.NoPassword:
                    logIn.logInInfoLabel.text = "\"Password\" cannot be blank!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.SpacesInUsernameOrEmail:
                    logIn.logInInfoLabel.text = "\"Username or Email\" should not contain space(s)!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.Success:
                    logIn.logInInfoLabel.text = "Log In Successful!";
                    logIn.logInInfoLabel.color = new Color(0.0f, 0.7f, 0.0f);

                    _ = StartCoroutine(nameof(LogInSuccessful));

                    break;
                case LogInStatus.WrongUsername:
                    logIn.logInInfoLabel.text = $"Username \"{logInData["username"].Value}\" is unregistered!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.WrongEmail:
                    logIn.logInInfoLabel.text = $"Email \"{logInData["email"].Value}\" is unregistered!";
                    logIn.logInInfoLabel.color = Color.red;

                    break;
                case LogInStatus.WrongPassword:
                    logIn.logInInfoLabel.text = "Password is incorrect!";
                    logIn.logInInfoLabel.color = Color.red;

                    logIn.ClearPasswordInputField();

                    break;
            }
        }

        private System.Collections.IEnumerator LogInSuccessful() {
            yield return new WaitForSeconds(2.0f);

            logInSignUpGrpCanvasGrp.alpha = 0.0f;
            logInSignUpGrpCanvasGrp.blocksRaycasts = false;

            CanvasGroup myCanvasGrp = logIn.GetComponent<CanvasGroup>();
            myCanvasGrp.alpha = 0.0f;
            myCanvasGrp.blocksRaycasts = false;

            acctManager.bigAcctInfoLabel.text = $"{logInData["username"].Value}\n{logInData["email"].Value}";
            acctManager.acctCanvasGrp.alpha = 1.0f;
            acctManager.acctCanvasGrp.blocksRaycasts = true;

            PlayerPrefs.SetString("sessionToken", logInData["sessionToken"].Value); //Save session token

            logIn.ClearLogIn();
        }
    }
}