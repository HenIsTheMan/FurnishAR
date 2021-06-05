using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class LogInButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private TMP_InputField logInUserInputField;

        [SerializeField]
        private TMP_InputField logInPasswordInputField;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInButton(): base() {
            logInUserInputField = null;
            logInPasswordInputField = null;
        }

        static LogInButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnLogInButtonClicked() {
            JSONNode node = new JSONArray();

            node.Add(logInUserInputField.text);
            node.Add(logInPasswordInputField.text);

            _ = PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.LogIn, node.ToString(), RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }
}