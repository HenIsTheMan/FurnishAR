using UnityEngine;
using UnityEngine.SceneManagement;

namespace IWP.Generic {
	[DisallowMultipleComponent]
	internal sealed class SceneManager: MonoBehaviour {
		#region Fields

		internal delegate void DoneDelegate();

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal SceneManager(): base() {
		}

		static SceneManager() {
		}

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		internal void LoadScene(string sceneName, LoadSceneTypes.LoadSceneType type, DoneDelegate doneDelegate) {
			var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, (LoadSceneMode)type);

			if(operation != null) { //Need to check as operation is async
				operation.completed += (_) => {
					doneDelegate?.Invoke();
				};
			}
		}

		internal void UnloadScene(string sceneName, UnloadSceneTypes.UnloadSceneType type, DoneDelegate doneDelegate) {
			var operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName, (UnloadSceneOptions)type);

			if(operation != null) { //Need to check as operation is async
				operation.completed += (_) => {
					doneDelegate?.Invoke();
				};
			}
		}
	}
}