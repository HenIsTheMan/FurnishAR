using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class SelectionUnderline: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private RectTransform myRectTransform;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SelectionUnderline(): base() {
            initControl = null;

            myRectTransform = null;
        }

        static SelectionUnderline() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.SelectionUnderline, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.SelectionUnderline, Init);
        }

        #endregion

        private void Init() {
            myRectTransform.localPosition = new Vector3(-370.0f, 970.0f, 0.0f);
            myRectTransform.localScale = new Vector3(6.3f, 0.2f, 1.0f);
        }
    }
}