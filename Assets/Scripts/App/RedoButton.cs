using UnityEngine;

namespace FurnishAR.App {
    internal sealed class RedoButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private TranslateRotateImg translateRotateImg;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal RedoButton(): base() {
            translateRotateImg = null;
        }

        static RedoButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            translateRotateImg.Redo();
        }
    }
}