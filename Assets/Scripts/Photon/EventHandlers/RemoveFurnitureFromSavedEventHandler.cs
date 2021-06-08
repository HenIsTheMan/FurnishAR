using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class RemoveFurnitureFromSavedEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private GameObject selectionPrefab;

        [SerializeField]
        private Transform browseParentTransform;

        [SerializeField]
        private Transform savedParentTransform;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal RemoveFurnitureFromSavedEventHandler(): base() {
            selectionPrefab = null;

            browseParentTransform = null;
            savedParentTransform = null;
        }

        static RemoveFurnitureFromSavedEventHandler() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable() {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code != (byte)EventCodes.EventCode.RemoveFurnitureFromSaved) {
                return;
            }
        }
    }
}