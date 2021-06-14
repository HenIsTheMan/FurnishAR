using TMPro;
using UnityEngine;
using static TMPro.TMP_InputField;

namespace FurnishAR.App {
    internal sealed class ShowHidePasswordButton: MonoBehaviour {
        #region Fields

        private bool isPasswordHidden;

        [SerializeField]
        private TextMeshProUGUI showHidePasswordText;

        [SerializeField]
        private TMP_InputField inputField;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ShowHidePasswordButton(): base() {
            isPasswordHidden = true;

            showHidePasswordText = null;

            inputField = null;
        }

        static ShowHidePasswordButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            isPasswordHidden = !isPasswordHidden;

            if(isPasswordHidden) {
                showHidePasswordText.SetText("Show Password");

                inputField.contentType = ContentType.Password;
            } else {
                showHidePasswordText.SetText("Hide Password");

                inputField.contentType = ContentType.Standard;
            }

            inputField.textComponent.SetAllDirty();
            //inputField.Select();
        }
    }
}