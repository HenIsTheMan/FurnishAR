using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Generic {
	[DisallowMultipleComponent]
	internal sealed class PtrManager: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		[SerializeField]
		private GameObject[] ptrTrailPrefabs;

		private Vector2 hotspot;
		private Vector3 pos;
		internal float displacementFromCam;
		internal Camera camComponent;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal PtrManager(): base() {
			initControl = null;

			ptrTrailPrefabs = System.Array.Empty<GameObject>();

			hotspot = Vector2.zero;
			pos = Vector3.zero;
			displacementFromCam = 0.0f;
			camComponent = null;
		}

        static PtrManager() {
        }

		#endregion

		#region Unity User Callback Event Funcs

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.PtrManager, Init);
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.PtrManager, Init);

			StopCoroutine(nameof(FollowPtr));
		}

		#endregion

		private void Init() {
			pos.x = 0.0f;
			pos.y = 0.0f;
			pos.z = displacementFromCam;

			foreach(GameObject ptrTrailPrefab in ptrTrailPrefabs) {
				CreateInactivePtrTrail(ptrTrailPrefab);
			}

			_ = StartCoroutine(nameof(FollowPtr));
		}

		private System.Collections.IEnumerator FollowPtr() {
			for(;;) {
				if(camComponent != null) {
					pos.x = Input.mousePosition.x;
					pos.y = Input.mousePosition.y;
					transform.localPosition = camComponent.ScreenPointToRay(pos).GetPoint(0);
				}

				yield return null;
			}
		}

		internal void ChangeCursor(string imgName, Vector2 hotspot, CursorModes.CursorMode cursorMode) {
			Cursor.SetCursor((Texture2D)Resources.Load(imgName), hotspot, (CursorMode)cursorMode);
		}

		internal void ChangeCursorCentered(string imgName, CursorModes.CursorMode cursorMode) {
			Texture2D tex2D = (Texture2D)Resources.Load(imgName);
			hotspot.x = tex2D.width * 0.5f;
			hotspot.y = tex2D.height * 0.5f;
			Cursor.SetCursor(tex2D, hotspot, (CursorMode)cursorMode);
		}

		private void CreateInactivePtrTrail(GameObject ptrTrailPrefab) {
			GameObject GO = Instantiate(ptrTrailPrefab, transform, false);
			GO.SetActive(false);
			GO.name = ptrTrailPrefab.name;
		}

		internal void ActivatePtrTrail(string name) {
			foreach(Transform child in transform) {
				if(child.name == name) {
					child.gameObject.SetActive(true);
					return;
				}
			}

			Console.LogError("Ptr trail named \"" + name + "\" could not be found");
		}

		internal void DeactivateAllPtrTrails() {
			foreach(Transform child in transform) {
				child.gameObject.SetActive(false);
			}
		}
	}
}