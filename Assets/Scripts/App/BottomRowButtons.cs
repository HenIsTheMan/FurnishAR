using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class BottomRowButtons: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal BottomRowButtons(): base() {
            initControl = null;
        }

        static BottomRowButtons() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.BottomRowButtons, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.BottomRowButtons, Init);
        }

        #endregion

        private void Init() {
            ((RectTransform)transform).anchoredPosition = new Vector3(0.0f, -200.0f, 0.0f); //Workaround
        }
    }
}