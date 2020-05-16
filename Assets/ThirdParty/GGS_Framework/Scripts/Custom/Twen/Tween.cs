using System;
using UnityEngine;

namespace GGS_Framework
{
	public class Tween
	{
		#region Class Accesors
		public Config CurrentConfig
		{
			get;
			private set;
		}

		public State CurrentState
		{
			get;
			private set;
		}

		public float Time
		{
			get;
			private set;
		}

		public float Progress
		{
			get;
			private set;
		}

		public float DeltaProgress
		{
			get;
			private set;
		}

		public float Value
		{
			get;
			private set;
		}

		public float DeltaValue
		{
			get;
			private set;
		}

		public Action<Tween> UpdateCallback
		{
			get;
			private set;
		}

		public Action<Tween> FinishCallback
		{
			get;
			private set;
		}

		public Func<float, float> EaseFunction
		{
			get;
			private set;
		}
		#endregion

		#region Constructors
		public Tween (Config config, Func<float, float> easeFunction, Action<Tween> updateCallback, Action<Tween> finishCallback)
		{
			CurrentConfig = config;
			EaseFunction = easeFunction;
			UpdateCallback = updateCallback;
			FinishCallback = finishCallback;
		}
		#endregion

		#region Class Implementation
		public void Update (float elapsedTime)
		{
			if (CurrentState != State.Running)
				return;

			Time = Mathf.Clamp (Time + elapsedTime, 0, CurrentConfig.Duration);

			if (Time >= CurrentConfig.Duration)
				Stop (StopAction.CompleteInterpolationAndCallFinish);
			else
				UpdateInterpolation ();
		}

		private void UpdateInterpolation ()
		{
			float previousProgress = Progress;
			Progress = Time / CurrentConfig.Duration;
			DeltaProgress = Progress - previousProgress;

			float previousValue = Value;
			Value = Mathf.LerpUnclamped (CurrentConfig.StartValue, CurrentConfig.EndValue, EaseFunction (Progress));
			DeltaValue = Value - previousValue;

			UpdateCallback?.Invoke (this);
		}

		public void Pause ()
		{
			if (CurrentState == State.Running)
				CurrentState = State.Paused;
		}

		public void Resume ()
		{
			if (CurrentState == State.Paused)
				CurrentState = State.Running;
		}

		public void Stop (StopAction stopAction)
		{
			if (CurrentState == State.Stopped)
				return;

			CurrentState = State.Stopped;

			switch (stopAction)
			{
				case StopAction.CompleteInterpolationAndCallFinish:
					Time = CurrentConfig.Duration;
					UpdateInterpolation ();

					FinishCallback?.Invoke (this);
					break;
				case StopAction.CallFinish:
					FinishCallback?.Invoke (this);
					break;
			}
		}
		#endregion

		#region Nested Classes
		public class Config
		{
			#region Class Accesors
			public GameObject GameObject
			{
				get;
				private set;
			}

			public string Id
			{
				get;
				private set;
			}

			public string UniqueId
			{
				get;
				private set;
			}

			public float StartValue
			{
				get;
				private set;
			}

			public float EndValue
			{
				get;
				private set;
			}

			public float Length
			{
				get;
				private set;
			}

			public float Duration
			{
				get;
				private set;
			}

			public bool UseUnscaledTime
			{
				get;
				private set;
			}
			#endregion

			#region Class Constructors
			public Config (GameObject gameObject, string id, float startValue, float endValue, float duration) :
				this (gameObject, id, startValue, endValue, duration, false)
			{
			}

			public Config (GameObject gameObject, string id, float startValue, float endValue, float duration, bool useUnscaledTime)
			{
				GameObject = gameObject;
				Id = id;
				UniqueId = string.Concat (gameObject.GetInstanceID (), id);

				StartValue = startValue;
				EndValue = endValue;
				Length = Mathf.Abs (endValue - startValue);

				Duration = duration;
				UseUnscaledTime = useUnscaledTime;
			}

			public Config (string uniqueId, float startValue, float endValue, float duration) :
				this (uniqueId, startValue, endValue, duration, false)
			{
			}

			public Config (string uniqueId, float startValue, float endValue, float duration, bool useUnscaledTime)
			{
				Id = uniqueId;
				UniqueId = uniqueId;

				StartValue = startValue;
				EndValue = endValue;
				Length = Mathf.Abs (endValue - startValue);

				Duration = duration;
				UseUnscaledTime = useUnscaledTime;
			}
			#endregion
		}

		public enum State
		{
			Running,
			Paused,
			Stopped
		}

		public enum StopAction
		{
			CompleteInterpolationAndCallFinish,
			CallFinish,
			DoNothing
		}
		#endregion
	}
}