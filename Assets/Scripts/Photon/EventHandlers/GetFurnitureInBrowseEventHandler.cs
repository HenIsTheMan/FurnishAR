using ExitGames.Client.Photon;
using FurnishAR.App;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using UnityEngine;

namespace FurnishAR.Photon {
    internal sealed class GetFurnitureInBrowseEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal GetFurnitureInBrowseEventHandler(): base() {
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

            //JSONNode GetFurnitureInBrowseData = JSON.Parse((string)photonEvent.CustomData);
            //string sessionToken = GetFurnitureInBrowseData["sessionToken"].Value;

            Generic.Console.Log((string)photonEvent.CustomData);
        }
    }
}