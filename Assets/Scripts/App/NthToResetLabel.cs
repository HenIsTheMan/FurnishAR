using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class NthToResetLabel: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private CanvasGroup canvasGrp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal NthToResetLabel(): base() {
            initControl = null;

            canvasGrp = null;
        }

        static NthToResetLabel() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.NthToResetLabel, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.NthToResetLabel, Init);
        }

        #endregion

        private void Init() {
            canvasGrp.alpha = 0.0f; //Workaround
        }
    }
}