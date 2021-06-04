using ExitGames.Client.Photon;
using FurnishAR.Anim;
using FurnishAR.Generic;
using FurnishAR.Photon;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using TMPro;
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
        private TMP_InputField logInUserInputField;

        [SerializeField]
        private TMP_InputField logInPasswordInputField;

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

            logInUserInputField = null;
            logInPasswordInputField = null;
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

        public void OnLogInButtonClicked() {
            if(state == LogInSignUpState.SignUp) {
                JSONNode node = new JSONArray();

                node.Add(logInUserInputField.text);
                node.Add(logInPasswordInputField.text);

                PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.LogIn, node.ToString(), RaiseEventOptions.Default, SendOptions.SendReliable);

                logInToSignUpTranslateAnim.IsUpdating = false;
                logInToSignUpScaleAnim.IsUpdating = false;

                signUpToLogInTranslateAnim.IsUpdating = true;
                signUpToLogInScaleAnim.IsUpdating = true;

                logInGO.SetActive(true);
                signUpGO.SetActive(false);

                state = LogInSignUpState.LogIn;
            }
        }

        public void OnSignUpButtonClicked() {
            if(state == LogInSignUpState.LogIn) {
                signUpToLogInTranslateAnim.IsUpdating = false;
                signUpToLogInScaleAnim.IsUpdating = false;

                logInToSignUpTranslateAnim.IsUpdating = true;
                logInToSignUpScaleAnim.IsUpdating = true;

                signUpGO.SetActive(true);
                logInGO.SetActive(false);

                state = LogInSignUpState.SignUp;
            }
        }
    }
}