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
            placementMarkerControl.PlacementMarkerParentTransform.gameObject.SetActive(false);

            translateAnim.IsUpdating = true;

            acctButtonGO.SetActive(true);
            thinUpArrowGO.SetActive(true);

            placementMarkerControl.shldRaycast = false;

            swipeDetectorGO.SetActive(true);
        }
    }
}