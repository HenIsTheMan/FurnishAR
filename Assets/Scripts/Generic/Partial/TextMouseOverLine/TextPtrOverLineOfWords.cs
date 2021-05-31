using TMPro;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Generic {
    internal sealed partial class TextPtrOverLineOfWords: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		private Rect rect;
		private Vector2 pt;

		[SerializeField]
		private TMP_Text tmpTextComponent;

		[SerializeField]
		private string ptrOverMethodName;

		[SerializeField]
		private string ptrNotOverMethodName;

		private delegate void MyDelegate();
		private MyDelegate ptrOverDelegate;
		private MyDelegate ptrNotOverDelegate;

		private static TextPtrOverLineOfWords globalObj;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TextPtrOverLineOfWords(): base() {
			initControl = null;

			rect = Rect.zero;
			pt = Vector2.zero;

			tmpTextComponent = null;
			ptrOverMethodName = string.Empty;
			ptrNotOverMethodName = string.Empty;

			ptrOverDelegate = null;
			ptrNotOverDelegate = null;
		}

        static TextPtrOverLineOfWords() {
			globalObj = null;
		}

		#endregion

		#region Unity User Callback Event Funcs

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.TextPtrOverLineOfWords, Init);
		}

		private void Update() {
			rect.width = tmpTextComponent.textBounds.size.x;
			rect.height = tmpTextComponent.textBounds.size.y;

			pt.x = Input.mousePosition.x
				- ScreenManager.ScreenWidth * 0.5f
				- tmpTextComponent.transform.localPosition.x + rect.width * 0.5f;
			pt.y = Input.mousePosition.y
				- ScreenManager.ScreenHeight * 0.5f
				- tmpTextComponent.transform.localPosition.y + rect.height * 0.5f;

			if(rect.Contains(pt)) {
				ptrOverDelegate?.Invoke();
			} else {
				ptrNotOverDelegate?.Invoke();
			}
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.TextPtrOverLineOfWords, Init);
		}

		#endregion

		private void Init() {
			globalObj = this;

			if(ptrOverMethodName != string.Empty) {
				ptrOverDelegate = (MyDelegate)GetType().GetMethod(ptrOverMethodName).CreateDelegate(typeof(MyDelegate));
			}

			if(ptrNotOverMethodName != string.Empty) {
				ptrNotOverDelegate = (MyDelegate)GetType().GetMethod(ptrNotOverMethodName).CreateDelegate(typeof(MyDelegate));
			}

			rect = new Rect();
			tmpTextComponent.ForceMeshUpdate();
		}
	}
}