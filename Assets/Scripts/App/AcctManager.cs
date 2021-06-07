using ExitGames.Client.Photon;
using FurnishAR.Generic;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class AcctManager: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        internal GameObject acctGO;

        [SerializeField]
        internal TMP_Text bigInfoLabel;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AcctManager(): base() {
            initControl = null;

            acctGO = null;

            bigInfoLabel = null;
        }

        static AcctManager() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        public void OnEnable() {
            initControl.AddMethod((uint)InitID.AcctManager, Init);
        }

        public void OnDisable() {
            initControl.RemoveMethod((uint)InitID.AcctManager, Init);
        }

        #endregion

        private void Init() {
            _ = StartCoroutine(nameof(CheckForAcct));
        }

        private System.Collections.IEnumerator CheckForAcct() {
            while(!PhotonNetwork.InRoom) {
                yield return null;
            }

            _ = PhotonNetwork.RaiseEvent(
                (byte)EventCodes.EventCode.AcctCheck,
                PlayerPrefs.GetString("sessionToken", string.Empty),
                RaiseEventOptions.Default,
                SendOptions.SendReliable
            );
        }
    }
}