using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace FurnishAR.Apps {
    internal sealed class CamFlipButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private ARCameraManager camManager;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal CamFlipButton(): base() {
        }

        static CamFlipButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void FlipCam() {
            camManager.requestedFacingDirection = camManager.requestedFacingDirection == CameraFacingDirection.User
                ? CameraFacingDirection.World
                : CameraFacingDirection.User;
        }
    }
}