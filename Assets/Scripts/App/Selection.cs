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
        private GameObject scanningTextGO;

        [SerializeField]
        private GameObject thinUpArrowGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal Selection(): base() {
            storedIndex = -1;

            furnitureManager = null;

            translateAnim = null;

            acctButtonGO = null;
            scanningTextGO = null;
            thinUpArrowGO = null;
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
            scanningTextGO.SetActive(false);
            thinUpArrowGO.SetActive(false);
        }
    }
}