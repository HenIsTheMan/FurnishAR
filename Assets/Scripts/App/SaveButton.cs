using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
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
            ConfigIsSaved(!isSaved, true);
        }

        internal void ConfigIsSaved(bool flag, bool shldRaiseEvent = false) {
            isSaved = flag;

            if(isSaved) {
                imgComponent.sprite = savedSprite;
            } else {
                imgComponent.sprite = notSavedSprite;
            }

            if(shldRaiseEvent) {
                if(!PhotonNetwork.InRoom) {
                    return;
                }

                JSONNode node = new JSONArray();

                node.Add(transform.parent.name);
                node.Add(PlayerPrefs.GetString("sessionToken", string.Empty));

                _ = PhotonNetwork.RaiseEvent(
                    isSaved ? (byte)EventCodes.EventCode.AddFurnitureToSaved : (byte)EventCodes.EventCode.RemoveFurnitureFromSaved,
                    node.ToString(),
                    RaiseEventOptions.Default,
                    SendOptions.SendReliable
                );
            }
        }
    }
}