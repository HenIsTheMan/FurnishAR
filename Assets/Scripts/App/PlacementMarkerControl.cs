using FurnishAR.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class PlacementMarkerControl: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        private Vector2 screenPt;

        private List<ARRaycastHit> hits;

        [SerializeField]
        private ARRaycastManager raycastManager;

        [SerializeField]
        private Transform placementMarkerParentTransform;

        [SerializeField]
        private GameObject placementMarkerGO;

        #endregion

        #region Properties

        internal GameObject PlacementMarkerGO {
            get => placementMarkerGO;
        }

        #endregion

        #region Ctors and Dtor

        internal PlacementMarkerControl(): base() {
            initControl = null;

            screenPt = Vector2.zero;

            hits = null;

            raycastManager = null;

            placementMarkerParentTransform = null;

            placementMarkerGO = null;
        }

        static PlacementMarkerControl() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.PlacementMarkerControl, Init);
        }

        private void Update() {
            raycastManager.Raycast(screenPt, hits, TrackableType.All);

            if(hits.Count > 0) {
                placementMarkerParentTransform.localPosition = hits[0].pose.position;
                placementMarkerParentTransform.localRotation = hits[0].pose.rotation;
                placementMarkerGO.SetActive(true);
            } else {
                placementMarkerGO.SetActive(false);
            }
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.PlacementMarkerControl, Init);
        }

        #endregion

        private void Init() {
            screenPt = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            hits = new List<ARRaycastHit>();
        }
    }
}