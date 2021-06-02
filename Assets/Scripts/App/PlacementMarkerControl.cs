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

        private List<ARRaycastHit> hits;

        [SerializeField]
        private ARRaycastManager raycastManager;

        [SerializeField]
        private GameObject placementMarkerGO;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PlacementMarkerControl(): base() {
            initControl = null;

            hits = null;

            raycastManager = null;

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
            raycastManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), hits, TrackableType.Planes);

            if(hits.Count > 0) {
                transform.localPosition = hits[0].pose.position;
                transform.localRotation = hits[0].pose.rotation;
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
            hits = new List<ARRaycastHit>();

            placementMarkerGO.SetActive(false);
        }
    }
}