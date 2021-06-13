using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ResetButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private GameObject nthToResetTextGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ResetButton(): base() {
            furnitureManager = null;

            nthToResetTextGO = null;
        }

        static ResetButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            if(!furnitureManager.ResetSelectedFurnitureTransform()) {
                nthToResetTextGO.GetComponent<CanvasGrpFadeAnim>().IsUpdating = true;
                nthToResetTextGO.GetComponent<RectTransformScaleAnim>().IsUpdating = true;
                nthToResetTextGO.GetComponent<RectTransformTranslateAnim>().IsUpdating = true;
            }
        }
    }
}