using UnityEngine;

namespace FurnishAR.App {
    internal sealed class SceneBackButton: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal SceneBackButton(): base() {
        }

        static SceneBackButton() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(false);

            UnityEngine.SceneManagement.SceneManager.SetActiveScene(
                UnityEngine.SceneManagement.SceneManager.GetSceneByName("MainScene")
            );

            UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(true);
        }
    }
}