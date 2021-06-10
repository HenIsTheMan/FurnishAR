using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FurnishAR.Photon {
    internal sealed class GetFurnitureInBrowseEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private GameObject selectionPrefab;

        [SerializeField]
        private Transform parentTransform;

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private ThinArrowButton thinDownArrowButton;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal GetFurnitureInBrowseEventHandler(): base() {
            selectionPrefab = null;

            parentTransform = null;

            furnitureManager = null;

            thinDownArrowButton = null;
        }

        static GetFurnitureInBrowseEventHandler() {
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
            if(photonEvent.Code != (byte)EventCodes.EventCode.GetFurnitureInBrowse) {
                return;
            }

            JSONNode data = JSON.Parse((string)photonEvent.CustomData);
            int arrLen = data.Count;
            Transform selectionTransform;
            string name;

            for(int i = 0; i < arrLen; ++i) {
                selectionTransform = Instantiate(selectionPrefab, parentTransform).transform;

                Selection selectionComponent = selectionTransform.GetComponent<Selection>();
                selectionComponent.storedIndex = furnitureManager.currIndex++;

                int storedIndexCopy = selectionComponent.storedIndex;
                selectionTransform.GetComponent<Button>().onClick.AddListener(() => {

                    thinDownArrowButton.OnClick();

                    furnitureManager.selectedIndex = storedIndexCopy;
                });

                name = data[i]["Name"].Value;
                selectionTransform.name = name;

                selectionTransform.Find("Name").GetComponent<TMP_Text>().text = name;
                selectionTransform.Find("Price").GetComponent<TMP_Text>().text = '$' + data[i]["Price"].Value;
                selectionTransform.Find("SaveButton").GetComponent<SaveButton>().ConfigIsSaved(false);
            }
        }
    }
}