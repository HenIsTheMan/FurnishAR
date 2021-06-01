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
        private float selectedColorFactor;
        private float notSelectedColorFactor;
        private float selectedScaleFactor;
        private float notSelectedScaleFactor;

        private int index;

        [SerializeField]
        private Image[] listDotImgs;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ListDotControl(): base() {
            initControl = null;

            alpha = 1.0f;
            selectedColorFactor = 0.0f;
            notSelectedColorFactor = 0.0f;
            selectedScaleFactor = 1.0f;
            notSelectedScaleFactor = 1.0f;

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
            alpha = 100.0f / 255.0f;
            selectedColorFactor = 20.0f / 255.0f;
            notSelectedColorFactor = 67.0f / 255.0f;
            selectedScaleFactor = 1.2f;
            notSelectedScaleFactor = 1.0f;

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
            Image myImg = listDotImgs[index];
            myImg.color = new Color(notSelectedColorFactor, notSelectedColorFactor, notSelectedColorFactor, alpha);
            myImg.transform.localScale = new Vector3(notSelectedScaleFactor, notSelectedScaleFactor, 1.0f);
        }

        private void ProgressAfter() {
            Image myImg = listDotImgs[index];
            myImg.color = new Color(selectedColorFactor, selectedColorFactor, selectedColorFactor, alpha);
            myImg.transform.localScale = new Vector3(selectedScaleFactor, selectedScaleFactor, 1.0f);
        }
    }
}