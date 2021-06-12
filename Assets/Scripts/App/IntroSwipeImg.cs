using FurnishAR.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class IntroSwipeImg: MonoBehaviour {
        #region Fields

        [SerializeField]
        private InitControl initControl;

        private float prevDragX;

        [SerializeField]
        private PageControl pageControl;

        [SerializeField]
        private ListDotControl listDotControl;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal IntroSwipeImg(): base() {
            initControl = null;

            prevDragX = 0.0f;

            pageControl = null;
            listDotControl = null;
        }

        static IntroSwipeImg() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.IntroSwipeImg, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.IntroSwipeImg, Init);
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
        }

        private void OnDragHandler(PointerEventData ptrEventData) {
            prevDragX = ptrEventData.delta.x;
        }

        private void OnEndDragHandler(PointerEventData _) {
            if(prevDragX < 0.0f) {
                pageControl.ProgressForward();
                listDotControl.ProgressForward();
            } else {
                pageControl.ProgressBackward();
                listDotControl.ProgressBackward();
            }
        }
    }
}