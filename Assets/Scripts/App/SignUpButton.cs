using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SignUpButton: MonoBehaviour {
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
        private TMP_Text signUpInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SignUpButton(): base() {
            firstNameInputField = null;
            middleNameInputField = null;
            lastNameInputField = null;
            usernameInputField = null;
            emailInputField = null;
            newPasswordInputField = null;
            confirmPasswordInputField = null;

            signUpInfoLabel = null;
        }

        static SignUpButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnSignUpButtonClicked() {
            if(newPasswordInputField.text != confirmPasswordInputField.text) {
                signUpInfoLabel.text = "New Password and Confirm Password do not match";
                signUpInfoLabel.color = Color.red;

                return;
            }

            JSONNode node = new JSONArray();

            node.Add(firstNameInputField.text);
            node.Add(middleNameInputField.text);
            node.Add(lastNameInputField.text);
            node.Add(usernameInputField.text);
            node.Add(emailInputField.text);
            node.Add(newPasswordInputField.text);

            _ = PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.SignUp, node.ToString(), RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }
}