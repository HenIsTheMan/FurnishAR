using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class Selection: MonoBehaviour {
        #region Fields

        [SerializeField]
        private int storedIndex;

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private RectTransformTranslateAnim translateAnim;

        [SerializeField]
        private GameObject acctButtonGO;

        [SerializeField]
        private GameObject thinUpArrowGO;

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        [SerializeField]
        private GameObject swipeDetectorGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal Selection(): base() {
            storedIndex = -1;

            furnitureManager = null;

            translateAnim = null;

            acctButtonGO = null;
            thinUpArrowGO = null;

            placementMarkerControl = null;

            swipeDetectorGO = null;
        }

        static Selection() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            furnitureManager.selectedIndex = storedIndex;

            translateAnim.IsUpdating = true;

            acctButtonGO.SetActive(false);
            thinUpArrowGO.SetActive(false);

            placementMarkerControl.shldRaycast = true;

            swipeDetectorGO.SetActive(false);
        }
    }
}