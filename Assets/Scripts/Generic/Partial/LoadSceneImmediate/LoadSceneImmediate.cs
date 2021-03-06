using UnityEngine;
using static FurnishAR.Generic.InitIDs;
using static FurnishAR.Generic.SceneManager;

namespace FurnishAR.Generic {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		private bool canClickOnSplash;

		private SceneManager sceneManager;

		[SerializeField]
		private string sceneName;

		[SerializeField]
		private LoadSceneTypes.LoadSceneType type;

		[SerializeField]
		private string prelimMethodName;

		[SerializeField]
		private string doneMethodName;

		private DoneDelegate doneDelegate;

		private static LoadSceneImmediate globalObj;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal LoadSceneImmediate(): base() {
			initControl = null;

			canClickOnSplash = true;

			sceneManager = null;

			sceneName = string.Empty;
			type = LoadSceneTypes.LoadSceneType.Amt;

			prelimMethodName = string.Empty;

			doneMethodName = string.Empty;
			doneDelegate = null;
		}

		static LoadSceneImmediate() {
			globalObj = null;
		}

		#endregion

		#region Unity User Callback Event Funcs

		private void OnValidate() {
			UnityEngine.Assertions.Assert.AreNotEqual(
				type, LoadSceneTypes.LoadSceneType.Amt,
				"type, LoadSceneTypes.LoadSceneType.Amt"
			);
		}

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.LoadSceneImmediate, Init);
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.LoadSceneImmediate, Init);
		}

		#endregion

		private void Init() {
			globalObj = this;

			sceneManager = FindObjectsOfType<SceneManager>()[0];

			if(doneMethodName != string.Empty) {
				doneDelegate = (DoneDelegate)GetType().GetMethod(doneMethodName).CreateDelegate(typeof(DoneDelegate));
			}
			if(prelimMethodName != string.Empty) {
				_ = GetType().GetMethod(prelimMethodName).Invoke(this, null);
			}

			sceneManager.LoadScene(sceneName, type, doneDelegate);
		}
	}
}