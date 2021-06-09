using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SignUp: MonoBehaviour {
        #region Fields

        [SerializeField]
        private TMP_InputField firstNameInputField;

        [SerializeField]
        private TMP_InputField middleNameInputField;

        [SerializeField]
        private TMP_InputField lastNameInputField;

        [SerializeField]
        private TMP_InputField usernameInputField;

        [SerializeField]
        private TMP_InputField emailInputField;

        [SerializeField]
        private TMP_InputField newPasswordInputField;

        [SerializeField]
        private TMP_InputField confirmPasswordInputField;

        [SerializeField]
        internal TMP_Text signUpInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SignUp(): base() {
            firstNameInputField = null;
            middleNameInputField = null;
            lastNameInputField = null;
            usernameInputField = null;
            emailInputField = null;
            newPasswordInputField = null;
            confirmPasswordInputField = null;

            signUpInfoLabel = null;
        }

        static SignUp() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnSignUpButtonClicked() { //Shld not have "SignUp" actually
            if(!PhotonNetwork.InRoom) {
                return;
            }

            JSONNode node = new JSONArray();

            node.Add(firstNameInputField.text);
            node.Add(middleNameInputField.text);
            node.Add(lastNameInputField.text);
            node.Add(usernameInputField.text);
            node.Add(emailInputField.text);
            node.Add(newPasswordInputField.text);
            node.Add(confirmPasswordInputField.text);

            _ = PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.SignUp, node.ToString(), RaiseEventOptions.Default, SendOptions.SendReliable);
        }

        public void ClearSignUp() { //Shld not have "SignUp" actually
            firstNameInputField.text = string.Empty;
            middleNameInputField.text = string.Empty;
            lastNameInputField.text = string.Empty;
            usernameInputField.text = string.Empty;
            emailInputField.text = string.Empty;
            newPasswordInputField.text = string.Empty;
            confirmPasswordInputField.text = string.Empty;

            signUpInfoLabel.text = string.Empty;
        }
    }
}