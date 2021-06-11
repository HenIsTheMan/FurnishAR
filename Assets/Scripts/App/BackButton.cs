using UnityEngine;

namespace FurnishAR.App {
    internal sealed class BackButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private GameObject swipeDetectorGO;

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private GameObject acctButtonGO;

        [SerializeField]
        private GameObject thinUpArrowGO;

        [SerializeField]
        private GameObject shareButtonGO;

        [SerializeField]
        private GameObject translateRotateImgGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal BackButton(): base() {
            swipeDetectorGO = null;

            furnitureManager = null;

            acctButtonGO = null;
            thinUpArrowGO = null;

            shareButtonGO = null;

            translateRotateImgGO = null;
        }

        static BackButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            swipeDetectorGO.SetActive(true);

            furnitureManager.SelectedFurnitureGO.SetActive(false);

            acctButtonGO.SetActive(true);
            thinUpArrowGO.SetActive(true);

            shareButtonGO.SetActive(false);
            gameObject.SetActive(false);

            translateRotateImgGO.SetActive(false);
        }
    }
}