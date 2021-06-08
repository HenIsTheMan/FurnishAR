using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.App {
    internal sealed class BrowseSavedControl: MonoBehaviour {
        #region Fields

        [SerializeField]
        private Image browseButtonImg;

        [SerializeField]
        private Image savedButtonImg;

        [SerializeField]
        private GameObject browseGO;

        [SerializeField]
        private GameObject savedGO;

        #endregion

        #region Properties

        internal bool IsBrowse {
            get;
            set;
        }

        #endregion

        #region Ctors and Dtor

        internal BrowseSavedControl(): base() {
            IsBrowse = true;

            browseButtonImg = null;
            savedButtonImg = null;

            browseGO = null;
            savedGO = null;
        }

        static BrowseSavedControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnBrowseButtonClicked() {
            IsBrowse = true;

            browseButtonImg.color = new Color(1.0f, 1.0f, 1.0f);
            savedButtonImg.color = new Color(0.6f, 0.6f, 0.6f);

            browseGO.SetActive(true);
            savedGO.SetActive(false);
        }

        public void OnSavedButtonClicked() {
            IsBrowse = false;

            savedButtonImg.color = new Color(1.0f, 1.0f, 1.0f);
            browseButtonImg.color = new Color(0.6f, 0.6f, 0.6f);

            savedGO.SetActive(true);
            browseGO.SetActive(false);
        }
    }
}