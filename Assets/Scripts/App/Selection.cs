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

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal Selection(): base() {
            storedIndex = -1;

            furnitureManager = null;

            translateAnim = null;
        }

        static Selection() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            furnitureManager.selectedIndex = storedIndex;

            translateAnim.IsUpdating = true;
        }
    }
}