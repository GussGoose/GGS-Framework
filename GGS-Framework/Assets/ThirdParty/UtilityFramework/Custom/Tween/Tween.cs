namespace UtilityFramework
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	public class Tween : MonoBehaviour
	{
		#region Class members
		private static GameObject root;
		private static List<TweenElement> tweens = new List<TweenElement> ();
		#endregion

		#region Class overrides
		private void Update ()
		{
			for (int i = 0; i < tweens.Count; i++)
			{
				TweenElement tween = tweens[i];

				if (tween.State == TweenState.Stopped)
				{
					tweens.Remove (tween);
					continue;
				}

				tween.Update ((tween.UnscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime);
			}
		}
		#endregion

		#region Class implementation
		public static TweenElement Create (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, Action<TweenElement> updateCallback)
		{
			return AddTween (reference, id, unscaledTime, start, end, duration, EasingCurves.Linear, null, updateCallback, null);
		}

		public static TweenElement Create (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, Action<TweenElement> updateCallback, Action<TweenElement> finishCallback)
		{
			return AddTween (reference, id, unscaledTime, start, end, duration, EasingCurves.Linear, null, updateCallback, finishCallback);
		}

		public static TweenElement Create (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, Func<float, float> ease, Action<TweenElement> updateCallback)
		{
			return AddTween (reference, id, unscaledTime, start, end, duration, ease, null, updateCallback, null);
		}

		public static TweenElement Create (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, Func<float, float> ease, Action<TweenElement> updateCallback, Action<TweenElement> finishCallback)
		{
			return AddTween (reference, id, unscaledTime, start, end, duration, ease, null, updateCallback, finishCallback);
		}

		public static TweenElement Create (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, AnimationCurve ease, Action<TweenElement> updateCallback)
		{
			return AddTween (reference, id, unscaledTime, start, end, duration, EasingCurves.Linear, ease, updateCallback, null);
		}

		public static TweenElement Create (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, AnimationCurve ease, Action<TweenElement> updateCallback, Action<TweenElement> finishCallback)
		{
			return AddTween (reference, id, unscaledTime, start, end, duration, EasingCurves.Linear, ease, updateCallback, finishCallback);
		}

		/// <summary>
		/// Makes delay, when end, "Finish" is called.
		/// </summary>
		public static TweenElement Delay (GameObject reference, string id, bool unscaledTime, float time, Action<TweenElement> finishCallback)
		{
			return AddTween (reference, id, unscaledTime, 0, 1, time, EasingCurves.Linear, null, null, finishCallback);
		}

		protected static TweenElement AddTween (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, Func<float, float> ease, AnimationCurve customEase, Action<TweenElement> updateCallback, Action<TweenElement> finishCallback)
		{
			if (root == null)
			{
				root = new GameObject ("TweenManager", typeof (Tween));
				DontDestroyOnLoad (root.gameObject);
			}

			TweenElement tween = new TweenElement (reference, id, unscaledTime, start, end, duration, ease, customEase, updateCallback, finishCallback);

			foreach (TweenElement currentTween in tweens)
			{
				if (tween.UniqueId == currentTween.UniqueId)
				{
					currentTween.Stop (TweenStopAction.Nothing);
					tweens.Remove (currentTween);
					break;
				}
			}

			tweens.Add (tween);
			return tween;
		}

		public static void StopTween (string id, TweenStopAction stopAction)
		{
			foreach (TweenElement tween in tweens)
			{
				if (tween.Id == id)
					tween.Stop (stopAction);
			}
		}

		public static void StopTween (GameObject reference, TweenStopAction stopAction)
		{
			foreach (TweenElement tween in tweens)
			{
				if (tween.Reference == reference)
					tween.Stop (stopAction);
			}
		}

		public static void PauseTween (string id)
		{
			foreach (TweenElement tween in tweens)
			{
				if (tween.Id == id)
					tween.Pause ();
			}
		}

		public static void PauseTween (GameObject reference)
		{
			foreach (TweenElement tween in tweens)
			{
				if (tween.Reference == reference)
					tween.Pause ();
			}
		}

		public static void ResumeTween (string id)
		{
			foreach (TweenElement tween in tweens)
			{
				if (tween.Id == id)
					tween.Resume ();
			}
		}

		public static void ResumeTween (GameObject reference)
		{
			foreach (TweenElement tween in tweens)
			{
				if (tween.Reference == reference)
					tween.Resume ();
			}
		}
		#endregion
	}
}