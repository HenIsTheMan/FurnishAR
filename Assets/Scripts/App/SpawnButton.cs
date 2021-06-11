using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SpawnButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlacementMarkerControl placementMarkerControl;

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private RectTransformTranslateAnim translateAnim;

        [SerializeField]
        private GameObject acctButtonGO;

        [SerializeField]
        private GameObject scanningTextGO;

        [SerializeField]
        private GameObject thinUpArrowGO;

        [SerializeField]
        private GameObject swipeUpDetectorGO;

        [SerializeField]
        private GameObject swipeDownDetectorGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SpawnButton(): base() {
            placementMarkerControl = null;

            furnitureManager = null;

            translateAnim = null;

            acctButtonGO = null;
            scanningTextGO = null;
            thinUpArrowGO = null;

            swipeUpDetectorGO = null;
            swipeDownDetectorGO = null;
        }

        static SpawnButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            if(scanningTextGO.activeSelf) {
                return;
            }

            Transform furnitureTransform = furnitureManager.SelectedFurnitureGO.transform;
            furnitureTransform.gameObject.SetActive(true);

            Vector3 pos = placementMarkerControl.PlacementMarkerParentTransform.position;
            furnitureTransform.position = pos;

            placementMarkerControl.PlacementMarkerParentTransform.gameObject.SetActive(false);

            translateAnim.IsUpdating = true;

            acctButtonGO.SetActive(true);
            thinUpArrowGO.SetActive(true);

            placementMarkerControl.shldRaycast = false;

            swipeUpDetectorGO.SetActive(true);
            swipeDownDetectorGO.SetActive(true);
        }
    }
}