using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ResetButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private FurnitureManager furnitureManager;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ResetButton(): base() {
            furnitureManager = null;
        }

        static ResetButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            furnitureManager.ResetSelectedFurnitureTransform();
        }
    }
}