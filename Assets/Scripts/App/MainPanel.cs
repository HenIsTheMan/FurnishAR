using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class MainPanel: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal MainPanel(): base() {
            initControl = null;
        }

        static MainPanel() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.MainPanel, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.MainPanel, Init);
        }

        #endregion

        private void Init() {
            ((RectTransform)transform).anchoredPosition = new Vector3(0.0f, -2000.0f, 0.0f); //Workaround

            ((RectTransform)transform).localScale = new Vector3(1.0f, 1.0f / 1920.0f * Screen.width, 1.0f);
        }
    }
}