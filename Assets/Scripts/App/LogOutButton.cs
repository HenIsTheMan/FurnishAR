using ExitGames.Client.Photon;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class LogOutButton: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogOutButton(): base() {
        }

        static LogOutButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            _ = PhotonNetwork.RaiseEvent(
                (byte)EventCodes.EventCode.LogOut,
                PlayerPrefs.GetString("sessionToken", string.Empty),
                RaiseEventOptions.Default,
                SendOptions.SendReliable
            );
        }
    }
}