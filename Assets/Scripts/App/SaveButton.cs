using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
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

                _ = StartCoroutine(MyFunc());
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

        private System.Collections.IEnumerator MyFunc() {
            yield return new WaitForSeconds(0.04f);

            _ = PhotonNetwork.RaiseEvent(
                isSaved ? (byte)EventCodes.EventCode.AddFurnitureToSaved : (byte)EventCodes.EventCode.RemoveFurnitureFromSaved,
                transform.parent.name,
                RaiseEventOptions.Default,
                SendOptions.SendReliable
            );
        }
    }
}