using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class PageControl: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private GameObject[] pageGOs;

        private int index;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PageControl(): base() {
            initControl = null;

            pageGOs = System.Array.Empty<GameObject>();

            index = 0;
        }

        static PageControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.PageControl, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.PageControl, Init);
        }

        #endregion

        private void Init() { //Oops
        }

        public void ProgressBackward() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            ProgressBefore();

            if(index > 0) {
                --index;
            }

            ProgressAfter();
        }

        public void ProgressForward() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            ProgressBefore();

            if(index < pageGOs.Length - 1) {
                ++index;
            }

            ProgressAfter();
        }

        private void ProgressBefore() {
            CanvasGroup[] canvasGrps = pageGOs[index].GetComponentsInChildren<CanvasGroup>();
            foreach(CanvasGroup canvasGrp in canvasGrps) {
                canvasGrp.alpha = 0.0f;
            }
        }

        private void ProgressAfter() {
            CanvasGroup[] canvasGrps = pageGOs[index].GetComponentsInChildren<CanvasGroup>();
            foreach(CanvasGroup canvasGrp in canvasGrps) {
                canvasGrp.alpha = 1.0f;
            }
        }
    }
}