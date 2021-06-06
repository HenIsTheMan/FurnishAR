using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class LogIn: MonoBehaviour {
        #region Fields

        [SerializeField]
        private TMP_InputField userInputField;

        [SerializeField]
        private TMP_InputField passwordInputField;

        [SerializeField]
        internal TMP_Text logInInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogIn(): base() {
            userInputField = null;
            passwordInputField = null;

            logInInfoLabel = null;
        }

        static LogIn() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnLogInButtonClicked() { //Shld not have "LogIn" actually
            JSONNode node = new JSONArray();

            node.Add(userInputField.text);
            node.Add(passwordInputField.text);

            _ = PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.LogIn, node.ToString(), RaiseEventOptions.Default, SendOptions.SendReliable);
        }

        public void ClearLogIn() { //Shld not have "LogIn" actually
            userInputField.text = string.Empty;
            passwordInputField.text = string.Empty;

            logInInfoLabel.text = string.Empty;
        }

        internal void ClearPasswordInputField() {
            passwordInputField.text = string.Empty;
        }
    }
}