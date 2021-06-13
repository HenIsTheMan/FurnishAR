using FurnishAR.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class TranslateRotateImg: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private FurnitureManager furnitureManager;

        [SerializeField]
        private float translationSensX;

        [SerializeField]
        private float translationSensZ;

        [SerializeField]
        private float rotationSens;

        private Stack<KeyValuePair<bool, object>> dataForUndo;
        private Stack<KeyValuePair<bool, object>> dataForRedo;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal TranslateRotateImg() : base() {
            initControl = null;

            camTransform = null;

            furnitureManager = null;

            translationSensX = 1.0f;
            translationSensZ = 1.0f;

            rotationSens = 1.0f;

            dataForUndo = null;
            dataForRedo = null;
        }

        static TranslateRotateImg() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.TranslateRotateImg, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.TranslateRotateImg, Init);
        }

        #endregion

        private void Init() {
            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((eventData) => {
                OnDragHandler((PointerEventData)eventData);
            });

            EventTrigger.Entry endDragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.EndDrag
            };
            endDragEntry.callback.AddListener((eventData) => {
                OnEndDragHandler((PointerEventData)eventData);
            });

            eventTrigger.triggers.Add(dragEntry);
            eventTrigger.triggers.Add(endDragEntry);

            dataForUndo = new Stack<KeyValuePair<bool, object>>();
            dataForRedo = new Stack<KeyValuePair<bool, object>>();
        }

        private void OnDragHandler(PointerEventData ptrEventData) {
            if(Input.touchCount == 2) {
                furnitureManager.SelectedFurnitureGO.transform.localRotation
                    = Quaternion.AngleAxis(-ptrEventData.delta.x * rotationSens, Vector3.up)
                    * furnitureManager.SelectedFurnitureGO.transform.localRotation;
            } else if(Input.touchCount == 1) {
                Vector3 front = furnitureManager.SelectedFurnitureGO.transform.localPosition - camTransform.localPosition;
                front.y = 0.0f;

                furnitureManager.SelectedFurnitureGO.transform.localPosition
                    += ptrEventData.delta.x * translationSensX * Vector3.Cross(Vector3.up, front)
                    + ptrEventData.delta.y * translationSensZ * front;
            }
        }

        private void OnEndDragHandler(PointerEventData ptrEventData) {
            if(Input.touchCount == 2) {
                dataForUndo.Push(new KeyValuePair<bool, object>(true, -ptrEventData.delta.x * rotationSens));
            } else if(Input.touchCount == 1) {
                Vector3 front = furnitureManager.SelectedFurnitureGO.transform.localPosition - camTransform.localPosition;
                front.y = 0.0f;

                dataForUndo.Push(new KeyValuePair<bool, object>(false, ptrEventData.delta.x * translationSensX * Vector3.Cross(Vector3.up, front)
                    + ptrEventData.delta.y * translationSensZ * front));
            }

            dataForRedo.Clear();
        }

        internal void Undo() {
            if(dataForUndo.Count == 0) {
                return;
            }

            KeyValuePair<bool, object> myData = dataForUndo.Pop();
            dataForRedo.Push(myData);

            Do(ref myData);
        }

        internal void Redo() {
            if(dataForRedo.Count == 0) {
                return;
            }

            KeyValuePair<bool, object> myData = dataForRedo.Pop();
            dataForUndo.Push(myData);

            Do(ref myData);
        }

        private void Do(ref KeyValuePair<bool, object> myData) {
            if(myData.Key) {
                furnitureManager.SelectedFurnitureGO.transform.localRotation
                    = Quaternion.AngleAxis((float)myData.Value, Vector3.up)
                    * furnitureManager.SelectedFurnitureGO.transform.localRotation;
            } else {
                Vector3 front = furnitureManager.SelectedFurnitureGO.transform.localPosition - camTransform.localPosition;
                front.y = 0.0f;

                furnitureManager.SelectedFurnitureGO.transform.localPosition
                    += (Vector3)myData.Value;
            }
        }
    }
}