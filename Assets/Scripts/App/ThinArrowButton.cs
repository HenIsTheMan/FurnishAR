using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class ThinArrowButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private GameObject GOToActivate;

        [SerializeField]
        private RectTransformAnchoredTranslateAnim translateAnim;

        [SerializeField]
        private RectTransformAnchoredTranslateAnim otherTranslateAnim;

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
            FindObjectOfType<AudioCentralControl>().PlayAudio("ButtonPress");

            GOToActivate.SetActive(true);

            otherTranslateAnim.IsUpdating = false;
            translateAnim.IsUpdating = true;

            gameObject.SetActive(false);
        }
    }
}