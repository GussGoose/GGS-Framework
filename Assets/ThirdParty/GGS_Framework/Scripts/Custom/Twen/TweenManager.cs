using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
	public class TweenManager : MonoBehaviour
	{
		#region Class Members
		private static GameObject root;
		private static Dictionary<string, Tween> tweens;

		private static int lastFrame = -1;
		#endregion

		#region Unity Callbacks
		private void Update ()
		{
			if (lastFrame == Time.frameCount)
				return;

			lastFrame = Time.frameCount;

			CheckForStoppedTweens ();
			UpdateTweens ();
		}
		#endregion

		#region Class Implementation
		public static Tween Create (Tween.Config config, AnimationCurve animationCurve, Action<Tween> updateCallback, Action<Tween> finishCallback)
		{
			return Create (config, animationCurve.Evaluate, updateCallback, finishCallback);
		}

		public static Tween Create (Tween.Config config, Easings.All easeType, Action<Tween> updateCallback, Action<Tween> finishCallback)
		{
			return Create (config, Easings.Functions[easeType], updateCallback, finishCallback);
		}

		public static Tween Create (Tween.Config config, Func<float, float> easeFunction, Action<Tween> updateCallback, Action<Tween> finishCallback)
		{
			if (root == null)
			{
				root = new GameObject ("TweenManager", typeof (TweenManager));
				DontDestroyOnLoad (root.gameObject);
			}

			if (tweens == null)
				tweens = new Dictionary<string, Tween> ();

			Tween tween = new Tween (config, easeFunction, updateCallback, finishCallback);
			string uniqueId = tween.CurrentConfig.UniqueId;
			
			if (tweens.ContainsKey (uniqueId))
			{
				tweens[uniqueId].Stop (Tween.StopAction.DoNothing);
				tweens.Remove (uniqueId);
			}

			tweens.Add (uniqueId, tween);
			return tween;
		}

		/// <summary>
		/// Makes delay, when end, "Finish" is called.
		/// </summary>
		public static Tween Delay (GameObject reference, string id, float time, bool useUnscaledTime, Action<Tween> finishCallback)
		{
			return Create (new Tween.Config (reference, id, 0, 1, time, useUnscaledTime), Easings.All.Linear, null, finishCallback);
		}

		private static void CheckForStoppedTweens ()
		{
			List<string> keys = new List<string> (tweens.Keys);
			foreach (string key in keys)
			{
				Tween tween = tweens[key];

				if (tween.CurrentState == Tween.State.Stopped)
					tweens.Remove (key);
			}
		}

		private static void UpdateTweens ()
		{
			List<string> keys = new List<string> (tweens.Keys);
			foreach (string key in keys)
			{
				Tween tween = tweens[key];
				tween.Update ((tween.CurrentConfig.UseUnscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime);
			}
		}

		public static void StopTween (string id, Tween.StopAction stopAction)
		{
			foreach (KeyValuePair<string, Tween> tween in tweens)
			{
				if (tween.Value.CurrentConfig.Id == id)
					tween.Value.Stop (stopAction);
			}
		}

		public static void StopTween (GameObject reference, Tween.StopAction stopAction)
		{
			foreach (KeyValuePair<string, Tween> tween in tweens)
			{
				if (tween.Value.CurrentConfig.GameObject == reference)
					tween.Value.Stop (stopAction);
			}
		}

		public static void PauseTween (string id)
		{
			foreach (KeyValuePair<string, Tween> tween in tweens)
			{
				if (tween.Value.CurrentConfig.Id == id)
					tween.Value.Pause ();
			}
		}

		public static void PauseTween (GameObject reference)
		{
			foreach (KeyValuePair<string, Tween> tween in tweens)
			{
				if (tween.Value.CurrentConfig.GameObject == reference)
					tween.Value.Pause ();
			}
		}

		public static void ResumeTween (string id)
		{
			foreach (KeyValuePair<string, Tween> tween in tweens)
			{
				if (tween.Value.CurrentConfig.Id == id)
					tween.Value.Resume ();
			}
		}

		public static void ResumeTween (GameObject reference)
		{
			foreach (KeyValuePair<string, Tween> tween in tweens)
			{
				if (tween.Value.CurrentConfig.GameObject == reference)
					tween.Value.Resume ();
			}
		}
		#endregion
	}
}