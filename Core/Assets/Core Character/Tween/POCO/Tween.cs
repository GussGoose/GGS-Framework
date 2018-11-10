using System;
using UnityEngine;

public class Tween {

	#region Class members
	private Action<Tween> progressCallback;
	private Action<Tween> finishCallback;
	#endregion

	#region Class accesors
	/// <summary>
	/// Update mode
	/// </summary>
	public UpdateMode UpdateMode;

	/// <summary>
	/// Current state of tween
	/// </summary>
	public TweenState State { get; private set; }
	/// <summary>
	/// Reference of tween, used for get full identifier of tween based on InstanceID
	/// </summary>
	public GameObject Reference { get; private set; }
	/// <summary>
	/// Identifier of tween
	/// </summary>
	public string Identifier { get; private set; }
	/// <summary>
	/// Unique indentifier of tween
	/// </summary>
	public string FullIdentifier { get; private set; }

	/// <summary>
	/// Ease funcion of interpolation
	/// </summary>
	public Func<float, float> Easing { get; private set; }
	/// <summary>
	/// Custom ease funcion based on AnimationCurve.Evaluate method
	/// </summary>
	public AnimationCurve CustomEasing { get; private set; }

	public bool UseUnscaledTime { get; private set; }
	/// <summary>
	/// Start value of interpolation
	/// </summary>
	public float StartValue { get; private set; }
	/// <summary>
	/// End value of interpolation
	/// </summary>
	public float EndValue { get; private set; }
	/// <summary>
	/// Duration of interpolation
	/// </summary>
	public float Duration { get; set; }
	/// <summary>
	/// Current runing time of interpolation
	/// </summary>
	public float CurrentTime { get; private set; }
	/// <summary>
	/// Current value of interpolation between Start Value and End Value
	/// </summary>
	public float Value { get; private set; }
	/// <summary>
	/// Current progress of interpolation between Start Value and End Value
	/// </summary>
	public float Progress { get; private set; }
	#endregion

	#region Class implementation
	public Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, Func<float, float> easing, AnimationCurve customEasing, Action<Tween> progress, Action<Tween> finish, UpdateMode updateMode) {
		UpdateMode = updateMode;

		Reference = reference;
		Identifier = identifier;
		FullIdentifier = reference.GetInstanceID () + identifier;
		UseUnscaledTime = useUnscaledTime;
		StartValue = start;
		EndValue = end;
		Duration = duration;
		Easing = easing;
		CustomEasing = customEasing;
		progressCallback = progress;
		finishCallback = finish;

		CurrentTime = 0;
		UpdateValue ();
	}

	public void Pause () {
		if (State == TweenState.Running)
			State = TweenState.Paused;
	}

	public void Resume () {
		if (State == TweenState.Paused)
			State = TweenState.Running;
	}

	public void Stop (TweenStopState stopState) {
		if (State != TweenState.Stopped) {
			State = TweenState.Stopped;

			if (stopState == TweenStopState.Complete) {
				CurrentTime = Duration;
				UpdateValue ();

				if (finishCallback != null) {
					finishCallback.Invoke (this);
					finishCallback = null;
				}
			}

			if (stopState == TweenStopState.NotModifyWithCompleteCallback) {
				if (finishCallback != null) {
					finishCallback.Invoke (this);
					finishCallback = null;
				}
			}
		}
	}

	public bool Update (float elapsedTime) {
		if (Reference == null) {
			Stop (TweenStopState.NotModify);
			return false;
		}

		if (State == TweenState.Running) {
			CurrentTime += elapsedTime;

			if (CurrentTime >= Duration) {
				Stop (TweenStopState.Complete);
				return true;
			}
			else {
				UpdateValue ();
				return false;
			}
		}
		return (State == TweenState.Stopped);
	}

	private void UpdateValue () {
		if (Reference == null) {
			Stop (TweenStopState.NotModify);
			return;
		}

		Progress = CurrentTime / Duration;

		float time = Easing (Progress);
		if (CustomEasing != null)
			time = CustomEasing.Evaluate (Progress);

		Value = Mathf.LerpUnclamped (StartValue, EndValue, time);

		if (progressCallback != null)
			progressCallback.Invoke (this);
	}
	#endregion
}
