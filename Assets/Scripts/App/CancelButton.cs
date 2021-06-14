using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class CancelButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        [SerializeField]
        private RectTransformAnchoredTranslateAnim translateAnim;

        [SerializeField]
        private GameObject acctButtonGO;

        [SerializeField]
        private GameObject thinUpArrowGO;

        [SerializeField]
        private GameObject swipeDetectorGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal CancelButton(): base() {
            placementMarkerControl = null;

            translateAnim = null;

            acctButtonGO = null;
            thinUpArrowGO = null;

            swipeDetectorGO = null;
        }

        static CancelButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            FindObjectOfType<AudioCentralControl>().PlayAudio("ButtonPress");

            translateAnim.IsUpdating = true;

            placementMarkerControl.shldRaycast = false;

            acctButtonGO.SetActive(true);
            thinUpArrowGO.SetActive(true);

            swipeDetectorGO.SetActive(true);
        }
    }
}