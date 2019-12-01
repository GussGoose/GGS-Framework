﻿using System;
using UnityEngine;

namespace GGS_Framework
{
	public class TweenElement
	{
		#region Class Members
		private Action<TweenElement> updateCallback;
		private Action<TweenElement> finishCallback;
		#endregion

		#region Class Accesors
		/// <summary>
		/// Current state
		/// </summary>
		public TweenState State
		{
			get;
			private set;
		}

		/// <summary>
		/// Used for get full identifier based on InstanceID
		/// </summary>
		public GameObject Reference
		{
			get;
			private set;
		}

		/// <summary>
		/// "Local" identifier
		/// </summary>
		public string Id
		{
			get;
			private set;
		}

		/// <summary>
		/// Unique identifier
		/// </summary>
		public string UniqueId
		{
			get;
			private set;
		}

		public bool UnscaledTime
		{
			get;
			private set;
		}

		/// <summary>
		/// Start value of interpolation
		/// </summary>
		public float StartValue
		{
			get;
			private set;
		}

		/// <summary>
		/// End value of interpolation
		/// </summary>
		public float EndValue
		{
			get;
			private set;
		}

		/// <summary>
		/// Duration of interpolation
		/// </summary>
		public float Duration
		{
			get;
			private set;
		}

		/// <summary>
		/// Current running time of interpolation
		/// </summary>
		public float CurrentTime
		{
			get;
			private set;
		}

		/// <summary>
		/// Current value of interpolation between Start Value and End Value
		/// </summary>
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

		/// <summary>
		/// Current progress of interpolation between Start Value and End Value
		/// </summary>
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

		/// <summary>
		/// Ease funcion used by interpolation
		/// </summary>
		public Func<float, float> Ease
		{
			get;
		}

		/// <summary>
		/// Custom ease funcion based on AnimationCurve.Evaluate method
		/// </summary>
		public AnimationCurve CustomEase
		{
			get;
		}
		#endregion

		#region Class Implementation
		public TweenElement (GameObject reference, string id, bool unscaledTime, float start, float end, float duration, Func<float, float> ease, AnimationCurve customEase, Action<TweenElement> updateCallback, Action<TweenElement> finishCallback)
		{
			Reference = reference;
			Id = id;
			UniqueId = string.Concat (Reference.GetInstanceID (), Id);

			UnscaledTime = unscaledTime;
			StartValue = start;
			EndValue = end;
			Duration = duration;

			Ease = ease;
			CustomEase = customEase;

			this.updateCallback = updateCallback;
			this.finishCallback = finishCallback;
		}

		public void Update (float elapsedTime)
		{
			if (Reference == null)
			{
				Stop (TweenStopAction.Nothing);
				return;
			}

			if (State == TweenState.Running)
			{
				CurrentTime += elapsedTime;

				if (CurrentTime >= Duration)
					Stop (TweenStopAction.CompleteInterpolationAndCallFinish);
				else
					UpdateInterpolation ();
			}
		}

		private void UpdateInterpolation ()
		{
			float previousProgress = Progress;
			Progress = CurrentTime / Duration;
			DeltaProgress = Progress - previousProgress;

			float previousValue = Value;
			Value = Mathf.LerpUnclamped (StartValue, EndValue, (CustomEase == null) ? Ease (Progress) : CustomEase.Evaluate (Progress));
			DeltaValue = Value - previousValue;

			if (updateCallback != null)
				updateCallback.Invoke (this);
		}

		public void Pause ()
		{
			if (State == TweenState.Running)
				State = TweenState.Paused;
		}

		public void Resume ()
		{
			if (State == TweenState.Paused)
				State = TweenState.Running;
		}

		public void Stop (TweenStopAction stopAction)
		{
			if (State == TweenState.Stopped)
				return;

			State = TweenState.Stopped;

			if (stopAction == TweenStopAction.CompleteInterpolationAndCallFinish)
			{
				CurrentTime = Duration;
				UpdateInterpolation ();

				if (finishCallback != null)
				{
					finishCallback.Invoke (this);
					finishCallback = null;
				}
			}
			else if (stopAction == TweenStopAction.CallFinish)
			{
				if (finishCallback != null)
				{
					finishCallback.Invoke (this);
					finishCallback = null;
				}
			}
		}
		#endregion
	}
}