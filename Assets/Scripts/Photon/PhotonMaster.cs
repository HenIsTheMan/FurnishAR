using FurnishAR.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Photon {
    internal sealed class PhotonMaster: MonoBehaviourPunCallbacks {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        internal delegate void OnJoinedRoomDelegate();
        internal OnJoinedRoomDelegate onJoinedRoomDelegate;

        [SerializeField]
        private byte maxPlayersPerRoom;

        [SerializeField]
        private string gameVer;

        [SerializeField]
        private string localPlayerNickname;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PhotonMaster(): base() {
            initControl = null;

            onJoinedRoomDelegate = null;

            maxPlayersPerRoom = 0;

            gameVer = string.Empty;

            localPlayerNickname = string.Empty;
        }

        static PhotonMaster() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        public override void OnEnable() {
            base.OnEnable();

            initControl.AddMethod((uint)InitID.PhotonMaster, Init);
        }

        public override void OnDisable() {
            base.OnDisable();

            initControl.RemoveMethod((uint)InitID.PhotonMaster, Init);
        }

        #endregion

        private void Init() {
            PhotonNetwork.AutomaticallySyncScene = false;

            PhotonNetwork.GameVersion = gameVer;

            PhotonNetwork.LocalPlayer.NickName = localPlayerNickname;

            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SerializationProtocolType
                = ExitGames.Client.Photon.SerializationProtocol.GpBinaryV16;

            if(PhotonNetwork.IsConnected) {
                _ = PhotonNetwork.JoinRandomRoom();
            } else {
                _ = StartCoroutine(nameof(TryToConnect));
            }
        }

        private System.Collections.IEnumerator TryToConnect() {
            while(!PhotonNetwork.IsConnected) {
                _ = PhotonNetwork.ConnectUsingSettings();
                yield return new WaitForSeconds(7.0f);
            }
        }

        public override void OnConnectedToMaster() {
            Console.Log("OnConnectedToMaster()");
			_ = PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom() {
            Console.Log("Room joined!");
            onJoinedRoomDelegate?.Invoke();
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Console.Log("OnJoinRandomFailed " + '(' + returnCode + "): " + message);

            _ = PhotonNetwork.CreateRoom(null, new RoomOptions {
                MaxPlayers = maxPlayersPerRoom
            });
        }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogFormat("OnDisconnected() due to {0}", cause);
        }
    }
}