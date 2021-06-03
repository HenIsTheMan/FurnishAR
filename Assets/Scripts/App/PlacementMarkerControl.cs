using FurnishAR.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private Transform placementMarkerParentTransform;

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
            if(placementMarkerGO == null) {
                return;
            }

            raycastManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), hits, TrackableType.Planes);

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
            hits = new List<ARRaycastHit>();

            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((eventData) => {
                OnDragHandler((PointerEventData)eventData);
            });

            eventTrigger.triggers.Add(dragEntry);
        }

        private void OnDragHandler(PointerEventData ptrEventData) {
            placementMarkerGO.transform.localRotation *= Quaternion.Euler(-ptrEventData.delta.y, -ptrEventData.delta.x, 0.0f);
        }

        internal void ConfigPlacementMarkerGO(GameObject GO, ref Vector3 translate, ref Quaternion rotate, ref Vector3 scale) {
            placementMarkerGO = GO;
            placementMarkerGO.SetActive(false);

            placementMarkerGO.transform.localPosition = translate;
            placementMarkerGO.transform.localRotation = rotate;
            placementMarkerGO.transform.localScale = scale;
        }
    }
}