using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class GetFurnitureInBrowseEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField]
        private GameObject selectionPrefab;

        [SerializeField]
        private Transform parentTransform;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal GetFurnitureInBrowseEventHandler(): base() {
            selectionPrefab = null;

            parentTransform = null;
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

            Generic.Console.Log(arrLen); //??

            for(int i = 0; i < arrLen; ++i) {
                selectionTransform = Instantiate(selectionPrefab, parentTransform).transform;
                selectionTransform.Find("Name").GetComponent<TMP_Text>().text = data[i]["Name"].Value;
                selectionTransform.Find("Price").GetComponent<TMP_Text>().text = data[i]["Price"].Value; //2d.p.??
                selectionTransform.Find("SaveButton").GetComponent<SaveButton>().IsSaved = false;
            }
        }
    }
}