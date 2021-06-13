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

        private Vector3[] OGTranslations;

        private Quaternion[] OGRotations;

        private Vector3[] OGScales;

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

            OGTranslations = System.Array.Empty<Vector3>();
            OGRotations = System.Array.Empty<Quaternion>();
            OGScales = System.Array.Empty<Vector3>();
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
            int arrLen = transform.childCount;

            furnitureGOs = new GameObject[arrLen];

            OGTranslations = new Vector3[arrLen];
            OGRotations = new Quaternion[arrLen];
            OGScales = new Vector3[arrLen];

            int index = 0;
            foreach(Transform childTransform in transform) {
                furnitureGOs[index] = childTransform.gameObject;

                OGTranslations[index] = childTransform.position;
                OGRotations[index] = childTransform.localRotation;
                OGScales[index] = childTransform.localScale;

                _ = ++index;
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

        internal void SetOGTranslationOfSelectedFurnitureGO(Vector3 pos) {
            OGTranslations[selectedIndex] = pos;
        }

        internal bool ResetSelectedFurnitureTransform() {
            Transform myTransform = SelectedFurnitureGO.transform;

            if(myTransform.position == OGTranslations[selectedIndex]
                || myTransform.localRotation == OGRotations[selectedIndex]
                || myTransform.localScale == OGScales[selectedIndex]
            ) {
                return false;
            }

            myTransform.position = OGTranslations[selectedIndex];
            myTransform.localRotation = OGRotations[selectedIndex];
            myTransform.localScale = OGScales[selectedIndex];

            return true;
        }
    }
}