using ExitGames.Client.Photon;
using FurnishAR.Generic;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class FurnitureManager: MonoBehaviour {
        #region Fields]

        [SerializeField]
        private InitControl initControl;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal FurnitureManager(): base() {
            initControl = null;
        }

        static FurnitureManager() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.FurnitureManager, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.FurnitureManager, Init);
        }

        #endregion

        private void Init() {
            if(PhotonNetwork.InRoom) {
                SendGetFurnitureEvents();
            } else {
                GameObject.Find("PhotonMaster").GetComponent<PhotonMaster>().onJoinedRoomDelegate += SendGetFurnitureEvents;
            }
        }

        private void SendGetFurnitureEvents() {
            _ = PhotonNetwork.RaiseEvent(
                (byte)EventCodes.EventCode.GetFurnitureInBrowse,
                PlayerPrefs.GetString("sessionToken", string.Empty),
                RaiseEventOptions.Default,
                SendOptions.SendReliable
            );

            _ = PhotonNetwork.RaiseEvent(
                (byte)EventCodes.EventCode.GetFurnitureInSaved,
                PlayerPrefs.GetString("sessionToken", string.Empty),
                RaiseEventOptions.Default,
                SendOptions.SendReliable
            );
        }
    }
}