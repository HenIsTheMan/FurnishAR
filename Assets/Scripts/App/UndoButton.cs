using UnityEngine;

namespace FurnishAR.App {
    internal sealed class UndoButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private TranslateRotateImg translateRotateImg;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal UndoButton(): base() {
            translateRotateImg = null;
        }

        static UndoButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            translateRotateImg.Undo();
        }
    }
}