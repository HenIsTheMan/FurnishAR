using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class NthToLabel: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private CanvasGroup canvasGrp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal NthToLabel(): base() {
            initControl = null;

            canvasGrp = null;
        }

        static NthToLabel() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.NthToLabel, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.NthToLabel, Init);
        }

        #endregion

        private void Init() {
            canvasGrp.alpha = 0.0f; //Workaround
        }
    }
}