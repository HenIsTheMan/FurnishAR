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

        [SerializeField]
        private GameObject prefab;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal AnchorManager(): base() {
            initControl = null;

            anchors = null;

            ARAnchorManager = null;

            prefab = null;
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
            ARAnchor anchor;

            if(hit.trackable is ARPlane plane) {
                var planeManager = GetComponent<ARPlaneManager>();
                if(planeManager != null) {
                    var oldPrefab = ARAnchorManager.anchorPrefab;

                    ARAnchorManager.anchorPrefab = prefab;
                    anchor = ARAnchorManager.AttachAnchor(plane, hit.pose);
                    ARAnchorManager.anchorPrefab = oldPrefab;

                    return anchor;
                }
            }

            var gameObject = Instantiate(prefab, hit.pose.position, hit.pose.rotation); //Anchor can be anywhere in scene hierarchy

            anchor = gameObject.GetComponent<ARAnchor>();
            if(anchor == null) {
                anchor = gameObject.AddComponent<ARAnchor>();
            }

            anchors.Add(anchor);
            return anchor;
        }

        internal void ClearAllAnchors() {
            foreach(ARAnchor anchor in anchors) {
                Destroy(anchor.gameObject);
            }

            anchors.Clear();
        }
    }
}