using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SceneBackButton: MonoBehaviour {
        #region Fields

        [SerializeField]
        private string sceneName;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SceneBackButton(): base() {
            sceneName = string.Empty;
        }

        static SceneBackButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            FindObjectOfType<AudioCentralControl>().PlayAudio("ButtonPress");

            UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(false);

            UnityEngine.SceneManagement.SceneManager.SetActiveScene(
                UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName)
            );

            UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(true);
        }
    }
}