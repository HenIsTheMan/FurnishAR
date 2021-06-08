using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.App {
    internal sealed class SaveButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private Image imgComponent;

        [SerializeField]
        private Sprite savedSprite;
        
        [SerializeField]
        private Sprite notSavedSprite;

        #endregion

        #region Properties
        
        internal bool IsSaved {
            get;
            set;
        }

        #endregion

        #region Ctors and Dtor

        internal SaveButton(): base() {
            imgComponent = null;

            savedSprite = null;
            notSavedSprite = null;

            IsSaved = false;
        }

        static SaveButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            IsSaved = !IsSaved;

            if(IsSaved) {
                imgComponent.sprite = savedSprite;
            } else {
                imgComponent.sprite = notSavedSprite;
            }
        }
    }
}