using FurnishAR.Generic;
using FurnishAR.Math;
using UnityEngine;
using static FurnishAR.Anim.AnimAccessTypes;
using static FurnishAR.Generic.InitIDs;
using static FurnishAR.Math.EasingTypes;

namespace FurnishAR.Anim {
    internal abstract class AbstractAnim: MonoBehaviour {
		#region Fields

		[HideInInspector, SerializeField]
		internal InitControl initControl;

		private bool flag;

		[SerializeField]
		private AnimAccessType accessType;

		[HideInInspector, SerializeField]
		internal bool isUpdating;

		[HideInInspector, SerializeField]
		internal float periodicDelay;

		protected WaitForSeconds periodicWaitForSeconds;

		[HideInInspector, Min(0.0f), SerializeField]
		internal float startTimeOffset;

		private WaitForSeconds startingWaitForSeconds;

		protected float animTime;

		[HideInInspector, Min(0.0f), SerializeField]
		internal float animDuration;

		protected int count;

		[HideInInspector, Min(0), SerializeField]
		internal int countThreshold;

		[HideInInspector, SerializeField]
		internal EasingType easingType;

		protected delegate float MyDelegate(float x);
		protected MyDelegate easingDelegate;

		internal delegate void AnimDelegate();
		internal AnimDelegate animPreStartDelegate;
		internal AnimDelegate animPostStartDelegate;
		internal AnimDelegate animPreMidDelegate;
		internal AnimDelegate animPostMidDelegate;
		internal AnimDelegate animPrePeriodicDelegate;
		internal AnimDelegate animPostPeriodicDelegate;
		internal AnimDelegate animEndDelegate;

		#endregion

		#region Properties

		internal AnimAccessType AccessType {
			get {
				return accessType;
			}
			set {
				accessType = value;
			}
		}

		internal bool IsUpdating {
			get {
				return isUpdating;
			}
			set {
				if(!isUpdating && value) {
					_ = StartCoroutine(nameof(MyUpdateFunc));

					isUpdating = value;
				} else if(isUpdating && !value) {
					animEndDelegate?.Invoke();

					StopCoroutine(nameof(MyUpdateFunc));

					ResetMe();

					isUpdating = value;
				}
			}
		}

		#endregion

		#region Ctors and Dtor

		internal AbstractAnim(): base() {
			initControl = null;

			flag = false;
			accessType = AnimAccessType.Amt;
			isUpdating = false;

			periodicDelay = 0.0f;
			periodicWaitForSeconds = null;
			startTimeOffset = 0.0f;
			startingWaitForSeconds = null;

			animTime = 0.0f;
			animDuration = 0.0f;

			count = 0;
			countThreshold = 0;

			easingType = EasingType.Amt;
			easingDelegate = null;

			animPreStartDelegate = null;
			animPostStartDelegate = null;
			animPreMidDelegate = null;
			animPostMidDelegate = null;
			animPrePeriodicDelegate = null;
			animPostPeriodicDelegate = null;
			animEndDelegate = null;
		}

        static AbstractAnim() {
        }

		#endregion

		#region Unity User Callback Event Funcs

		protected virtual void OnEnable() {
			if(initControl != null) {
				initControl.AddMethod((uint)InitID.AbstractAnim, Init);
			}
		}

		protected virtual void OnDisable() {
			if(initControl != null) {
				initControl.RemoveMethod((uint)InitID.AbstractAnim, Init);
			}
		}

		#endregion

		protected virtual void ResetMe() {
			animTime = 0.0f;
			count = 0;
		}

		internal void InitMe() {
			InitStuff();
		}

		protected void Init() {
			InitStuff();
		}

		private void InitStuff() {
			easingDelegate = (MyDelegate)Easing.Type.GetMethod(easingType.ToString()).CreateDelegate(typeof(MyDelegate));

			if(!Mathf.Approximately(periodicDelay, 0.0f)) {
				periodicWaitForSeconds = new WaitForSeconds(periodicDelay);
			}
			if(!Mathf.Approximately(startTimeOffset, 0.0f)) {
				startingWaitForSeconds = new WaitForSeconds(startTimeOffset);
			}

			SubInitStuff();

			if(isUpdating) {
				_ = StartCoroutine(nameof(MyUpdateFunc));
			}
		}

		protected abstract void SubInitStuff();

		protected System.Collections.IEnumerator MyUpdateFunc() {
			animPreStartDelegate?.Invoke();

			if(startingWaitForSeconds != null) {
				yield return startingWaitForSeconds;
			}

			animPostStartDelegate?.Invoke();

			while(true) {
				animTime += Time.deltaTime;

				flag = (animTime >= animDuration * 0.5f && animTime - Time.deltaTime < animDuration * 0.5f);

				if(flag) {
					animPreMidDelegate?.Invoke();
				}

				UpdateAnim();

				if(flag) {
					animPostMidDelegate?.Invoke();
				}

				yield return CheckAnim();
			}
		}

		protected abstract void UpdateAnim();

		protected virtual System.Collections.IEnumerator CheckAnim() {
			if(animTime >= animDuration) {
				if(countThreshold != 0 && ++count == countThreshold) {
					IsUpdating = false;
					yield break;
				}

				animTime = 0.0f;

				animPrePeriodicDelegate?.Invoke();

				if(periodicWaitForSeconds != null) {
					yield return periodicWaitForSeconds;
				}

				animPostPeriodicDelegate?.Invoke();
			} else {
				yield return null;
			}
		}
    }
}