using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class AddFurnitureToSavedEventHandler: MonoBehaviour, IOnEventCallback {
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

        internal AddFurnitureToSavedEventHandler(): base() {
            selectionPrefab = null;

            browseParentTransform = null;
            savedParentTransform = null;
        }

        static AddFurnitureToSavedEventHandler() {
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
            if(photonEvent.Code != (byte)EventCodes.EventCode.AddFurnitureToSaved) {
                return;
            }

            object[] customData = (object[])photonEvent.CustomData;
            string name = (string)customData[0];

            Destroy(browseParentTransform.Find(name).gameObject);

            Transform selectionTransform = Instantiate(selectionPrefab, savedParentTransform).transform;
            selectionTransform.name = name;

            selectionTransform.Find("Name").GetComponent<TMP_Text>().text = name;
            selectionTransform.Find("Price").GetComponent<TMP_Text>().text = $"${(float)customData[1]}";
            selectionTransform.Find("SaveButton").GetComponent<SaveButton>().ConfigIsSaved(true);
        }
    }
}