using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SpawnButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private GameObject swipeDetectorGO;

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
        private GameObject undoButtonGO;

        [SerializeField]
        private GameObject resetButtonGO;

        [SerializeField]
        private GameObject redoButtonGO;

        [SerializeField]
        private GameObject shareButtonGO;

        [SerializeField]
        private GameObject translateRotateImgGO;

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private GameObject bloomVolGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SpawnButton(): base() {
            swipeDetectorGO = null;

            placementMarkerControl = null;

            furnitureManager = null;

            translateAnim = null;

            scanningTextGO = null;

            scanningText = null;

            backButtonGO = null;
            undoButtonGO = null;
            resetButtonGO = null;
            redoButtonGO = null;
            shareButtonGO = null;

            translateRotateImgGO = null;

            camTransform = null;

            bloomVolGO = null;
        }

        static SpawnButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            if(scanningTextGO.activeSelf) {
                scanningText.StartFlashing(0.7f);
                return;
            }

            Transform furnitureTransform = furnitureManager.SelectedFurnitureGO.transform;
            furnitureTransform.gameObject.SetActive(true);

            furnitureManager.anchorTransform = placementMarkerControl.RetrieveAnchorTransform();
            furnitureManager.FixedUpdate(); //Force FixedUpdate()
            furnitureManager.SetOGTranslationOfSelectedFurnitureGO(furnitureTransform.position);

            Vector3 front = furnitureManager.SelectedFurnitureGO.transform.position - camTransform.position;
            front.y = 0.0f;

            furnitureTransform.localRotation = Quaternion.FromToRotation(Vector3.forward, front) * furnitureTransform.localRotation;
            furnitureManager.SetOGRotationOfSelectedFurnitureGO(furnitureTransform.localRotation);

            translateAnim.IsUpdating = true;

            placementMarkerControl.shldRaycast = false;

            backButtonGO.SetActive(true);
            undoButtonGO.SetActive(true);
            resetButtonGO.SetActive(true);
            redoButtonGO.SetActive(true);
            shareButtonGO.SetActive(true);

            swipeDetectorGO.SetActive(false);
            translateRotateImgGO.SetActive(true);

            bloomVolGO.SetActive(false);
        }
    }
}