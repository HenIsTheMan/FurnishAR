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

        internal int selectedIndex;

        private GameObject[] furnitureGOs;

        #endregion

        #region Properties

        internal GameObject SelectedFurnitureGO {
            get => furnitureGOs[selectedIndex];
        }

        #endregion

        #region Ctors and Dtor

        internal FurnitureManager(): base() {
            initControl = null;

            selectedIndex = -1;

            furnitureGOs = System.Array.Empty<GameObject>();
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
            furnitureGOs = new GameObject[transform.childCount];

            int index = 0;
            foreach(Transform child in transform) {
                furnitureGOs[index++] = child.gameObject;
            }

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