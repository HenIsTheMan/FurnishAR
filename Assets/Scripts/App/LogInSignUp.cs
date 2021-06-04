using FurnishAR.Anim;
using UnityEngine;

namespace FurnishAR.App {
    internal sealed class LogInSignUp: MonoBehaviour {
        #region Fields

        [SerializeField]
        private RectTransformTranslateAnim logInToSignUpTranslateAnim;

        [SerializeField]
        private RectTransformScaleAnim logInToSignUpScaleAnim;

        [SerializeField]
        private RectTransformTranslateAnim signUpToLogInTranslateAnim;

        [SerializeField]
        private RectTransformScaleAnim signUpToLogInScaleAnim;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInSignUp(): base() {
            logInToSignUpTranslateAnim = null;
            logInToSignUpScaleAnim = null;

            signUpToLogInTranslateAnim = null;
            signUpToLogInScaleAnim = null;
        }

        static LogInSignUp() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void OnLogInButtonClicked() {
            Generic.Console.Log("here0");

            logInToSignUpTranslateAnim.IsUpdating = false;
            logInToSignUpScaleAnim.IsUpdating = false;

            signUpToLogInTranslateAnim.IsUpdating = true;
            signUpToLogInScaleAnim.IsUpdating = true;
        }

        public void OnSignUpButtonClicked() {
            Generic.Console.Log("here1");

            signUpToLogInTranslateAnim.IsUpdating = false;
            signUpToLogInScaleAnim.IsUpdating = false;

            logInToSignUpTranslateAnim.IsUpdating = true;
            logInToSignUpScaleAnim.IsUpdating = true;
        }
    }
}