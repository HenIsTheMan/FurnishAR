using FurnishAR.Anim;
using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.App {
    internal sealed class LogInSignUp: MonoBehaviour {
        private enum LogInSignUpState: byte {
            LogIn,
            SignUp,
            Amt
        }

        #region Fields

        [SerializeField]
        private InitControl initControl;

        [SerializeField]
        private RectTransform myRectTransform;

        private LogInSignUpState state;

        [SerializeField]
        private RectTransformTranslateAnim logInToSignUpTranslateAnim;

        [SerializeField]
        private RectTransformScaleAnim logInToSignUpScaleAnim;

        [SerializeField]
        private RectTransformTranslateAnim signUpToLogInTranslateAnim;

        [SerializeField]
        private RectTransformScaleAnim signUpToLogInScaleAnim;

        [SerializeField]
        private GameObject logInGO;

        [SerializeField]
        private GameObject signUpGO;

        [SerializeField]
        private LogIn logIn;

        [SerializeField]
        private SignUp signUp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LogInSignUp(): base() {
            initControl = null;

            myRectTransform = null;

            state = LogInSignUpState.Amt;

            logInToSignUpTranslateAnim = null;
            logInToSignUpScaleAnim = null;

            signUpToLogInTranslateAnim = null;
            signUpToLogInScaleAnim = null;

            logInGO = null;
            signUpGO = null;

            logIn = null;
            signUp = null;
        }

        static LogInSignUp() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            initControl.AddMethod((uint)InitID.LogInSignUp, Init);
        }

        private void OnDisable() {
            initControl.RemoveMethod((uint)InitID.LogInSignUp, Init);
        }

        #endregion

        private void Init() {
            myRectTransform.localPosition = new Vector3(-370.0f, 1370.0f, 0.0f);
            myRectTransform.localScale = new Vector3(6.3f, 0.2f, 1.0f);

            logInGO.SetActive(true);
            signUpGO.SetActive(false);

            state = LogInSignUpState.LogIn;
        }

        public void OnLogInTabClicked() {
            if(state == LogInSignUpState.SignUp) {
                logInToSignUpTranslateAnim.IsUpdating = false;
                logInToSignUpScaleAnim.IsUpdating = false;

                signUpToLogInTranslateAnim.IsUpdating = true;
                signUpToLogInScaleAnim.IsUpdating = true;

                logInGO.SetActive(true);
                signUpGO.SetActive(false);

                signUp.ClearSignUp();

                state = LogInSignUpState.LogIn;
            }
        }

        public void OnSignUpTabClicked() {
            if(state == LogInSignUpState.LogIn) {
                signUpToLogInTranslateAnim.IsUpdating = false;
                signUpToLogInScaleAnim.IsUpdating = false;

                logInToSignUpTranslateAnim.IsUpdating = true;
                logInToSignUpScaleAnim.IsUpdating = true;

                signUpGO.SetActive(true);
                logInGO.SetActive(false);

                logIn.ClearLogIn();

                state = LogInSignUpState.SignUp;
            }
        }
    }
}