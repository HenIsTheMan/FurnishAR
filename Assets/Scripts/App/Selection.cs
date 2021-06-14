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
        private RectTransformAnchoredTranslateAnim translateAnim;

        [SerializeField]
        private GameObject acctButtonGO;

        [SerializeField]
        private GameObject thinUpArrowGO;

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        [SerializeField]
        private GameObject swipeDetectorGO;

        [SerializeField]
        private GameObject borderGO;

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

            borderGO = null;
        }

        static Selection() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            furnitureManager.selectedIndex = storedIndex;

            translateAnim.IsUpdating = true;

            placementMarkerControl.shldRaycast = true;

            acctButtonGO.SetActive(false);
            thinUpArrowGO.SetActive(false);

            swipeDetectorGO.SetActive(false);

            Selection[] selections = FindObjectsOfType<Selection>();
            foreach(Selection selection in selections) {
                selection.DeactivateBorderGO();
            }

            borderGO.SetActive(true);
        }

        internal void DeactivateBorderGO() {
            borderGO.SetActive(false);
        }
    }
}