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
        private float rotationSensX;

        [SerializeField]
        private float rotationSensY;

        [SerializeField]
        private Transform placementMarkerParentTransform;

        private GameObject placementMarkerGO;

        private Vector3 translateOG;
        private Quaternion rotateOG;
        private Vector3 scaleOG;

        private Vector3 frontVec;

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

            rotationSensX = 1.0f;
            rotationSensY = 1.0f;

            placementMarkerParentTransform = null;

            placementMarkerGO = null;

            translateOG = Vector3.zero;
            rotateOG = Quaternion.identity;
            scaleOG = Vector3.one;

            frontVec = Vector3.forward;
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
            if(Input.touchCount == 2) {
                //placementMarkerGO.transform.localRotation *= Quaternion.AngleAxis(-ptrEventData.delta.x * rotationSensY, Vector3.up);
                placementMarkerGO.transform.localRotation *= Quaternion.AngleAxis(ptrEventData.delta.y * rotationSensX, Vector3.right);
            }
        }

        internal void ConfigPlacementMarkerGO(GameObject GO, ref Vector3 translate, ref Quaternion rotate, ref Vector3 scale) {
            placementMarkerGO = GO;
            placementMarkerGO.SetActive(false);

            placementMarkerGO.transform.localPosition = translateOG = translate;
            placementMarkerGO.transform.localRotation = rotateOG = rotate;
            placementMarkerGO.transform.localScale = scaleOG = scale;
        }

        public void ResetTransformOfPlacementMarkerGO() {
            if(placementMarkerGO != null) {
                placementMarkerGO.transform.localPosition = translateOG;
                placementMarkerGO.transform.localRotation = rotateOG;
                placementMarkerGO.transform.localScale = scaleOG;
            }
        }
    }
}