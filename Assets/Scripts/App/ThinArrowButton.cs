using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ThinArrowButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private GameObject GOToActivate;

        [SerializeField]
        private RectTransformTranslateAnim translateAnim;

        [SerializeField]
        private RectTransformTranslateAnim otherTranslateAnim;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ThinArrowButton(): base() {
            GOToActivate = null;

            translateAnim = null;
            otherTranslateAnim = null;
        }

        static ThinArrowButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            GOToActivate.SetActive(true);

            otherTranslateAnim.IsUpdating = false;
            translateAnim.IsUpdating = true;

            gameObject.SetActive(false);
        }
    }
}