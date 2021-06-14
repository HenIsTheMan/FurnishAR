using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class DeleteAcctButton: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal DeleteAcctButton(): base() {
        }

        static DeleteAcctButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            AudioCentralControl.globalObj.PlayAudio("ButtonPress");

            if(PhotonNetwork.InRoom) {
                SendDeleteAcctEvent();
            } else {
                GameObject.Find("PhotonMaster").GetComponent<PhotonMaster>().onJoinedRoomDelegate += SendDeleteAcctEvent;
            }
        }

        private void SendDeleteAcctEvent() {
            _ = PhotonNetwork.RaiseEvent(
                (byte)EventCodes.EventCode.DeleteAcct,
                PlayerPrefs.GetString("sessionToken", string.Empty),
                RaiseEventOptions.Default,
                SendOptions.SendReliable
            );
        }
    }
}