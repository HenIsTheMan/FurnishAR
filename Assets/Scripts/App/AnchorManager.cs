using FurnishAR.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class AnchorManager: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        private List<ARAnchor> anchors;

        [SerializeField]
        private ARAnchorManager ARAnchorManager;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AnchorManager(): base() {
            initControl = null;

            anchors = null;

            ARAnchorManager = null;
        }

        static AnchorManager() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.AnchorManager, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.AnchorManager, Init);
        }

        #endregion

        private void Init() {
            anchors = new List<ARAnchor>();
        }

        internal ARAnchor CreateAnchor(in ARRaycastHit hit) {
            if(!(hit.trackable is ARPlane)) {
                return null;
            }

            ARAnchor anchor;

            //var planeManager = GetComponent<ARPlaneManager>(); //??

            GameObject oldAnchorPrefab = ARAnchorManager.anchorPrefab;

            ARAnchorManager.anchorPrefab = null; //??
            anchor = ARAnchorManager.AttachAnchor((ARPlane)hit.trackable, hit.pose); //??
            ARAnchorManager.anchorPrefab = oldAnchorPrefab;

            return anchor;
        }

        internal void ClearAllAnchors() {
            foreach(ARAnchor anchor in anchors) {
                Destroy(anchor);
            }

            anchors.Clear();
        }
    }
}