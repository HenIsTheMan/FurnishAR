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
        private TMP_InputField userInputField;

        [SerializeField]
        private TMP_InputField passwordInputField;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInButton(): base() {
            userInputField = null;
            passwordInputField = null;
        }

        static LogInButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnLogInButtonClicked() {
            JSONNode node = new JSONArray();

            node.Add(userInputField.text);
            node.Add(passwordInputField.text);

            _ = PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.LogIn, node.ToString(), RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }
}