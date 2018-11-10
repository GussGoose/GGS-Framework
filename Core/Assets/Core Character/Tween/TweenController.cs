using System.Collections.Generic;
using UnityEngine;
using System;

public class TweenController : MonoBehaviour {

	#region Class members
	private static GameObject root;
	private static readonly List<Tween> tweens = new List<Tween> ();
	#endregion

	#region Class overrides
	private void Awake () {
		tweens.Clear ();
	}

	private void FixedUpdate () {
		for (int i = 0; i < tweens.Count; i++) {
			Tween tween = tweens[i];

			if (tween.UpdateMode == UpdateMode.Normal)
				continue;

			if (tween.Reference == null || !tween.Reference.activeSelf) {
				tweens.RemoveAt (i);
				continue;
			}

			tween.Update (tween.UseUnscaledTime ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime);
		}
	}

	private void Update () {
		for (int i = 0; i < tweens.Count; i++) {
			Tween tween = tweens[i];

			if (tween.UpdateMode == UpdateMode.Fixed)
				continue;

			if (tween.Reference == null || !tween.Reference.activeSelf) {
				tweens.RemoveAt (i);
				continue;
			}

			tween.Update (tween.UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
		}
	}
	#endregion

	#region Class implementation
	#region Overloads
	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, Action<Tween> progress) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, null, progress, null, UpdateMode.Normal);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, Action<Tween> progress, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, null, progress, finish, UpdateMode.Normal);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, Func<float, float> easing, Action<Tween> progress) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, easing, null, progress, null, UpdateMode.Normal);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, Func<float, float> easing, Action<Tween> progress, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, easing, null, progress, finish, UpdateMode.Normal);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, AnimationCurve easing, Action<Tween> progress) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, easing, progress, null, UpdateMode.Normal);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, AnimationCurve easing, Action<Tween> progress, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, easing, progress, finish, UpdateMode.Normal);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, UpdateMode updateMode, float start, float end, float duration, Action<Tween> progress) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, null, progress, null, updateMode);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, UpdateMode updateMode, float start, float end, float duration, Action<Tween> progress, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, null, progress, finish, updateMode);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, UpdateMode updateMode, float start, float end, float duration, Func<float, float> easing, Action<Tween> progress) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, easing, null, progress, null, updateMode);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, UpdateMode updateMode, float start, float end, float duration, Func<float, float> easing, Action<Tween> progress, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, easing, null, progress, finish, updateMode);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, UpdateMode updateMode, float start, float end, float duration, AnimationCurve easing, Action<Tween> progress) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, easing, progress, null, updateMode);
	}

	public static Tween Tween (GameObject reference, string identifier, bool useUnscaledTime, UpdateMode updateMode, float start, float end, float duration, AnimationCurve easing, Action<Tween> progress, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, start, end, duration, EasingCurves.linear, easing, progress, finish, updateMode);
	}

	/// <summary>
	/// Makes delay, when end, "Finish" is called.
	/// </summary>
	public static Tween Delay (GameObject reference, string identifier, bool useUnscaledTime, float time, Action<Tween> finish) {
		return StartTween (reference, identifier, useUnscaledTime, 0, 1, time, EasingCurves.linear, null, null, finish, UpdateMode.Normal);
	}
	#endregion

	protected static Tween StartTween (GameObject reference, string identifier, bool useUnscaledTime, float start, float end, float duration, Func<float, float> easing, AnimationCurve customEasing, Action<Tween> progress, Action<Tween> finish, UpdateMode updateMode) {
		if (!Application.isPlaying)
			return null;

		Tween tween = new Tween (reference, identifier, useUnscaledTime, start, end, duration, easing, customEasing, progress, finish, updateMode);
		AddTween (tween);
		return tween;
	}

	protected static void AddTween (Tween tween) {
		if (root == null) {
			root = new GameObject ("TweenController", typeof (TweenController));
			DontDestroyOnLoad (root.gameObject);
		}

		// If Tween exist in Controller
		if (tween.FullIdentifier != null)
			RemoveTween (tween.FullIdentifier, TweenStopState.NotModify);

		tweens.Add (tween);
	}

	protected static bool RemoveTween (Tween tween, TweenStopState stopState) {
		tween.Stop (stopState);
		return tweens.Remove (tween);
	}

	protected static bool RemoveTween (string fullIdentifier, TweenStopState stopState) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (fullIdentifier == tween.FullIdentifier) {
				tween.Stop (stopState);
				tweens.Remove (tween);
				found = true;
				break;
			}
		}

		return found;
	}

	public static bool StopTween (string identifier, TweenStopState stopState) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (identifier == tween.Identifier) {
				tween.Stop (stopState);
				found = true;
			}
		}

		return found;
	}

	public static bool StopTween (GameObject reference, TweenStopState stopState) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (reference == tween.Reference) {
				tween.Stop (stopState);
				found = true;
			}
		}

		return found;
	}

	public static bool PauseTween (string identifier) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (identifier == tween.Identifier) {
				tween.Pause ();
				found = true;
			}
		}

		return found;
	}

	public static bool PauseTween (GameObject reference) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (reference == tween.Reference) {
				tween.Pause ();
				found = true;
			}
		}

		return found;
	}

	public static bool ResumeTween (string identifier) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (identifier == tween.Identifier) {
				tween.Resume ();
				found = true;
			}
		}

		return found;
	}

	public static bool ResumeTween (GameObject reference) {
		bool found = false;

		foreach (Tween tween in tweens) {
			if (reference == tween.Reference) {
				tween.Resume ();
				found = true;
			}
		}

		return found;
	}
	#endregion
}
