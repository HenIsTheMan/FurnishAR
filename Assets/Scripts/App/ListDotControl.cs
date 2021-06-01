using FurnishAR.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class ListDotControl: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        private float alpha;
        private float selectedFactor;
        private float notSelectedFactor;

        private int index;

        [SerializeField]
        private Image[] listDotImgs;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ListDotControl(): base() {
            initControl = null;

            alpha = 100.0f / 255.0f;
            selectedFactor = 20.0f / 255.0f;
            notSelectedFactor = 67.0f / 255.0f;

            index = 0;

            listDotImgs = System.Array.Empty<Image>();
        }

        static ListDotControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.ListDotControl, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.ListDotControl, Init);
        }

        #endregion

        private void Init() {
            ProgressAfter();
        }

        internal void ProgressBackward() {
            ProgressBefore();

            if(index > 0) {
                --index;
            }

            ProgressAfter();
        }

        internal void ProgressForward() {
            ProgressBefore();

            if(index < listDotImgs.Length - 1) {
                ++index;
            }

            ProgressAfter();
        }

        private void ProgressBefore() {
            listDotImgs[index].color = new Color(notSelectedFactor, notSelectedFactor, notSelectedFactor, alpha);
        }

        private void ProgressAfter() {
            listDotImgs[index].color = new Color(selectedFactor, selectedFactor, selectedFactor, alpha);
        }
    }
}