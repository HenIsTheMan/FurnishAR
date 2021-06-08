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

        private Vector3 translate;
        private Quaternion rotate;
        private Vector3 scale;

        [SerializeField]
        private PlacementMarkerControl placementMarkerControlScript;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal FurnitureManager(): base() {
            initControl = null;

            translate = Vector3.zero;
            rotate = Quaternion.identity;
            scale = Vector3.one;

            placementMarkerControlScript = null;
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
            rotate = Quaternion.Euler(0.0f, 180.0f, 0.0f);

            placementMarkerControlScript.ConfigPlacementMarkerGO(GameObject.Find("BlackDragon"), ref translate, ref rotate, ref scale);

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