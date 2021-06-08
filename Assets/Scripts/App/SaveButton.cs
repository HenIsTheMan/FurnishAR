using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.App {
    internal sealed class SaveButton: MonoBehaviour {
        #region Fields

        private bool isSaved;

        [SerializeField]
        private Image imgComponent;

        [SerializeField]
        private Sprite savedSprite;
        
        [SerializeField]
        private Sprite notSavedSprite;

        #endregion

        #region Properties
        
        internal bool IsSaved {
            get {
                return isSaved;
            }
            set {
                isSaved = value;

                if(isSaved) {
                    imgComponent.sprite = savedSprite;
                } else {
                    imgComponent.sprite = notSavedSprite;
                }
            }
        }

        #endregion

        #region Ctors and Dtor

        internal SaveButton(): base() {
            isSaved = false;

            imgComponent = null;

            savedSprite = null;
            notSavedSprite = null;
        }

        static SaveButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            isSaved = !isSaved;
        }
    }
}