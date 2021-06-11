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
        private RectTransformAnchoredTranslateAnim translateAnim;

        [SerializeField]
        private GameObject scanningTextGO;

        [SerializeField]
        private ScanningText scanningText;

        [SerializeField]
        private GameObject backButtonGO;

        [SerializeField]
        private GameObject shareButtonGO;

        [SerializeField]
        private GameObject translateRotateImgGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SpawnButton(): base() {
            placementMarkerControl = null;

            furnitureManager = null;

            translateAnim = null;

            scanningTextGO = null;

            scanningText = null;

            backButtonGO = null;
            shareButtonGO = null;

            translateRotateImgGO = null;
        }

        static SpawnButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            if(scanningTextGO.activeSelf) {
                scanningText.StartFlashing(0.7f);
                return;
            }

            Transform furnitureTransform = furnitureManager.SelectedFurnitureGO.transform;
            furnitureTransform.gameObject.SetActive(true);

            Vector3 pos = placementMarkerControl.PlacementMarkerParentTransform.position;
            furnitureTransform.position = pos;

            placementMarkerControl.PlacementMarkerParentTransform.gameObject.SetActive(false);

            translateAnim.IsUpdating = true;

            placementMarkerControl.shldRaycast = false;

            backButtonGO.SetActive(true);
            shareButtonGO.SetActive(true);

            translateRotateImgGO.SetActive(true);
        }
    }
}