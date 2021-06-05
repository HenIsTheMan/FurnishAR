using TMPro;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SignUpButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private TMP_InputField logInUserInputField;

        [SerializeField]
        private TMP_InputField logInPasswordInputField;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SignUpButton() : base() {
        }

        static SignUpButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnSignUpButtonClicked() {
        }
    }
}