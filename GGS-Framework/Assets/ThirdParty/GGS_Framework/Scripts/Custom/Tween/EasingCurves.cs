using System;
using UnityEngine;

namespace GGS_Framework
{
	public class EasingCurves
	{
		public enum List
		{
			Linear, Quadratic,
			Clerp, Spring,
			EaseInQuad, EaseOutQuad, EaseInOutQuad,
			EaseInCubic, EaseOutCubic, EaseInOutCubic,
			EaseInQuart, EaseOutQuart, EaseInOutQuart,
			EaseInQuint, EaseOutQuint, EaseInOutQuint,
			EaseInSine, EaseOutSine, EaseInOutSine,
			EaseInExpo, EaseOutExpo, EaseInOutExpo,
			EaseInCirc, EaseOutCirc, EaseInOutCirc,
			EaseInBounce, EaseOutBounce, EaseInOutBounce,
			EaseInBack, EaseOutBack, EaseInOutBack,
			EaseInElastic, EaseOutElastic, EaseInOutElastic
		}

		#region Class members
		public static readonly Func<float, float> Linear = ComputeLinear;
		public static readonly Func<float, float> Quadratic = ComputeQuadratic;

		public static readonly Func<float, float> Clerp = ComputeClerp;
		public static readonly Func<float, float> Spring = ComputeSpring;

		public static readonly Func<float, float> EaseInQuad = ComputeEaseInQuad;
		public static readonly Func<float, float> EaseOutQuad = ComputeEaseOutQuad;
		public static readonly Func<float, float> EaseInOutQuad = ComputeEaseInOutQuad;

		public static readonly Func<float, float> EaseInCubic = ComputeEaseInCubic;
		public static readonly Func<float, float> EaseOutCubic = ComputeEaseOutCubic;
		public static readonly Func<float, float> EaseInOutCubic = ComputeEaseInOutCubic;

		public static readonly Func<float, float> EaseInQuart = ComputeEaseInQuart;
		public static readonly Func<float, float> EaseOutQuart = ComputeEaseOutQuart;
		public static readonly Func<float, float> EaseInOutQuart = ComputeEaseInOutQuart;

		public static readonly Func<float, float> EaseInQuint = ComputeEaseInQuint;
		public static readonly Func<float, float> EaseOutQuint = ComputeEaseOutQuint;
		public static readonly Func<float, float> EaseInOutQuint = ComputeEaseInOutQuint;

		public static readonly Func<float, float> EaseInSine = ComputeEaseInSine;
		public static readonly Func<float, float> EaseOutSine = ComputeEaseOutSine;
		public static readonly Func<float, float> EaseInOutSine = ComputeEaseInOutSine;

		public static readonly Func<float, float> EaseInExpo = ComputeEaseInExpo;
		public static readonly Func<float, float> EaseOutExpo = ComputeEaseOutExpo;
		public static readonly Func<float, float> EaseInOutExpo = ComputeEaseInOutExpo;

		public static readonly Func<float, float> EaseInCirc = ComputeEaseInCirc;
		public static readonly Func<float, float> EaseOutCirc = ComputeEaseOutCirc;
		public static readonly Func<float, float> EaseInOutCirc = ComputeEaseInOutCirc;

		public static readonly Func<float, float> EaseInBounce = ComputeEaseInBounce;
		public static readonly Func<float, float> EaseOutBounce = ComputeEaseOutBounce;
		public static readonly Func<float, float> EaseInOutBounce = ComputeEaseInOutBounce;

		public static readonly Func<float, float> EaseInBack = ComputeEaseInBack;
		public static readonly Func<float, float> EaseOutBack = ComputeEaseOutBack;
		public static readonly Func<float, float> EaseInOutBack = ComputeEaseInOutBack;

		public static readonly Func<float, float> EaseInElastic = ComputeEaseInElastic;
		public static readonly Func<float, float> EaseOutElastic = ComputeEaseOutElastic;
		public static readonly Func<float, float> EaseInOutElastic = ComputeEaseInOutElastic;
		#endregion

		#region Class implementation
		public static float GetEaseValue (List ease, float value)
		{
			switch (ease)
			{
				case List.Linear:
					return ComputeLinear (value);
				case List.Quadratic:
					return ComputeQuadratic (value);

				case List.Clerp:
					return ComputeClerp (value);
				case List.Spring:
					return ComputeSpring (value);

				case List.EaseInQuad:
					return ComputeEaseInQuad (value);
				case List.EaseOutQuad:
					return ComputeEaseOutQuad (value);
				case List.EaseInOutQuad:
					return ComputeEaseInOutQuad (value);

				case List.EaseInCubic:
					return ComputeEaseInCubic (value);
				case List.EaseOutCubic:
					return ComputeEaseOutCubic (value);
				case List.EaseInOutCubic:
					return ComputeEaseInOutCubic (value);

				case List.EaseInQuart:
					return ComputeEaseInQuart (value);
				case List.EaseOutQuart:
					return ComputeEaseOutQuart (value);
				case List.EaseInOutQuart:
					return ComputeEaseInOutQuart (value);

				case List.EaseInQuint:
					return ComputeEaseInQuint (value);
				case List.EaseOutQuint:
					return ComputeEaseOutQuint (value);
				case List.EaseInOutQuint:
					return ComputeEaseInOutQuint (value);

				case List.EaseInSine:
					return ComputeEaseInSine (value);
				case List.EaseOutSine:
					return ComputeEaseOutSine (value);
				case List.EaseInOutSine:
					return ComputeEaseInOutSine (value);

				case List.EaseInExpo:
					return ComputeEaseInExpo (value);
				case List.EaseOutExpo:
					return ComputeEaseOutExpo (value);
				case List.EaseInOutExpo:
					return ComputeEaseInOutExpo (value);

				case List.EaseInCirc:
					return ComputeEaseInCirc (value);
				case List.EaseOutCirc:
					return ComputeEaseOutCirc (value);
				case List.EaseInOutCirc:
					return ComputeEaseInOutCirc (value);

				case List.EaseInBounce:
					return ComputeEaseInBounce (value);
				case List.EaseOutBounce:
					return ComputeEaseOutBounce (value);
				case List.EaseInOutBounce:
					return ComputeEaseInOutBounce (value);

				case List.EaseInBack:
					return ComputeEaseInBack (value);
				case List.EaseOutBack:
					return ComputeEaseOutBack (value);
				case List.EaseInOutBack:
					return ComputeEaseInOutBack (value);

				case List.EaseInElastic:
					return ComputeEaseInElastic (value);
				case List.EaseOutElastic:
					return ComputeEaseOutElastic (value);
				case List.EaseInOutElastic:
					return ComputeEaseInOutElastic (value);
			}

			return 0;
		}

		public static Func<float, float> GetEaseFunc (List ease)
		{
			switch (ease)
			{
				case List.Linear:
					return Linear;
				case List.Quadratic:
					return Quadratic;
				case List.Clerp:
					return Clerp;
				case List.Spring:
					return Spring;
				case List.EaseInQuad:
					return EaseInQuad;
				case List.EaseOutQuad:
					return EaseOutQuad;
				case List.EaseInOutQuad:
					return EaseInOutQuad;
				case List.EaseInCubic:
					return EaseInCubic;
				case List.EaseOutCubic:
					return EaseOutCubic;
				case List.EaseInOutCubic:
					return EaseInOutCubic;
				case List.EaseInQuart:
					return EaseInQuart;
				case List.EaseOutQuart:
					return EaseOutQuart;
				case List.EaseInOutQuart:
					return EaseInOutQuart;
				case List.EaseInQuint:
					return EaseInQuint;
				case List.EaseOutQuint:
					return EaseOutQuint;
				case List.EaseInOutQuint:
					return EaseInOutQuint;
				case List.EaseInSine:
					return EaseInSine;
				case List.EaseOutSine:
					return EaseOutSine;
				case List.EaseInOutSine:
					return EaseInOutSine;
				case List.EaseInExpo:
					return EaseInExpo;
				case List.EaseOutExpo:
					return EaseOutExpo;
				case List.EaseInOutExpo:
					return EaseInOutExpo;
				case List.EaseInCirc:
					return EaseInCirc;
				case List.EaseOutCirc:
					return EaseOutCirc;
				case List.EaseInOutCirc:
					return EaseInOutCirc;
				case List.EaseInBounce:
					return EaseInBounce;
				case List.EaseOutBounce:
					return EaseOutBounce;
				case List.EaseInOutBounce:
					return EaseInOutBounce;
				case List.EaseInBack:
					return EaseInBack;
				case List.EaseOutBack:
					return EaseOutBack;
				case List.EaseInOutBack:
					return EaseInOutBack;
				case List.EaseInElastic:
					return EaseInElastic;
				case List.EaseOutElastic:
					return EaseOutElastic;
				case List.EaseInOutElastic:
					return EaseInOutElastic;
			}

			return null;
		}

		#region Methods
		protected static float ComputeLinear (float value)
		{
			float start = 0;
			float end = 1;
			return Mathf.Lerp (start, end, value);
		}

		protected static float ComputeQuadratic (float value)
		{
			return -4 * Mathf.Pow (value, 2) + value * 4;
		}

		protected static float ComputeClerp (float value)
		{
			float start = 0;
			float end = 1;
			float min = 0.0f;
			float max = 360.0f;
			float half = Mathf.Abs ((max - min) * 0.5f);
			float retval = 0.0f;
			float diff = 0.0f;

			if ((end - start) < -half)
			{
				diff = ((max - start) + end) * value;
				retval = start + diff;
			}
			else if ((end - start) > half)
			{
				diff = -((max - end) + start) * value;
				retval = start + diff;
			}
			else retval = start + (end - start) * value;
			return retval;
		}

		protected static float ComputeSpring (float value)
		{
			float start = 0;
			float end = 1;
			value = Mathf.Clamp01 (value);
			value = (Mathf.Sin (value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow (1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
			return start + (end - start) * value;
		}

		// Not use by any method
		protected static float ComputePunch (float amplitude, float value)
		{
			float s = 9;
			if (value == 0)
				return 0;
			else if (value == 1)
				return 0;

			float period = 1 * 0.3f;
			s = period / (2 * Mathf.PI) * Mathf.Asin (0);
			return (amplitude * Mathf.Pow (2, -10 * value) * Mathf.Sin ((value * 1 - s) * (2 * Mathf.PI) / period));
		}

		protected static float ComputeEaseInQuad (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value + start;
		}

		protected static float ComputeEaseOutQuad (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * value * (value - 2) + start;
		}

		protected static float ComputeEaseInOutQuad (float value)
		{
			float start = 0;
			float end = 1;
			value /= 0.5f;
			end -= start;

			if (value < 1)
				return end * 0.5f * value * value + start;

			value--;
			return -end * 0.5f * (value * (value - 2) - 1) + start;
		}

		protected static float ComputeEaseInCubic (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value * value + start;
		}

		protected static float ComputeEaseOutCubic (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return end * (value * value * value + 1) + start;
		}

		protected static float ComputeEaseInOutCubic (float value)
		{
			float start = 0;
			float end = 1;
			value /= 0.5f;
			end -= start;

			if (value < 1)
				return end * 0.5f * value * value * value + start;

			value -= 2;
			return end * 0.5f * (value * value * value + 2) + start;
		}

		protected static float ComputeEaseInQuart (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value * value * value + start;
		}

		protected static float ComputeEaseOutQuart (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return -end * (value * value * value * value - 1) + start;
		}

		protected static float ComputeEaseInOutQuart (float value)
		{
			float start = 0;
			float end = 1;
			value /= 0.5f;
			end -= start;

			if (value < 1)
				return end * 0.5f * value * value * value * value + start;

			value -= 2;
			return -end * 0.5f * (value * value * value * value - 2) + start;
		}

		protected static float ComputeEaseInQuint (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value * value * value * value + start;
		}

		protected static float ComputeEaseOutQuint (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return end * (value * value * value * value * value + 1) + start;
		}

		protected static float ComputeEaseInOutQuint (float value)
		{
			float start = 0;
			float end = 1;
			value /= 0.5f;
			end -= start;

			if (value < 1)
				return end * 0.5f * value * value * value * value * value + start;

			value -= 2;
			return end * 0.5f * (value * value * value * value * value + 2) + start;
		}

		protected static float ComputeEaseInSine (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * Mathf.Cos (value * (Mathf.PI * 0.5f)) + end + start;
		}

		protected static float ComputeEaseOutSine (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * Mathf.Sin (value * (Mathf.PI * 0.5f)) + start;
		}

		protected static float ComputeEaseInOutSine (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * 0.5f * (Mathf.Cos (Mathf.PI * value) - 1) + start;
		}

		protected static float ComputeEaseInExpo (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * Mathf.Pow (2, 10 * (value - 1)) + start;
		}

		protected static float ComputeEaseOutExpo (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * (-Mathf.Pow (2, -10 * value) + 1) + start;
		}

		protected static float ComputeEaseInOutExpo (float value)
		{
			float start = 0;
			float end = 1;
			value /= 0.5f;
			end -= start;

			if (value < 1)
				return end * 0.5f * Mathf.Pow (2, 10 * (value - 1)) + start;

			value--;
			return end * 0.5f * (-Mathf.Pow (2, -10 * value) + 2) + start;
		}

		protected static float ComputeEaseInCirc (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * (Mathf.Sqrt (1 - value * value) - 1) + start;
		}

		protected static float ComputeEaseOutCirc (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return end * Mathf.Sqrt (1 - value * value) + start;
		}

		protected static float ComputeEaseInOutCirc (float value)
		{
			float start = 0;
			float end = 1;
			value /= 0.5f;
			end -= start;
			if (value < 1)
				return -end * 0.5f * (Mathf.Sqrt (1 - value * value) - 1) + start;
			value -= 2;
			return end * 0.5f * (Mathf.Sqrt (1 - value * value) + 1) + start;
		}

		protected static float ComputeEaseInBounce (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			float d = 1f;
			return end - ComputeEaseOutBounce (d - value) + start;
		}

		protected static float ComputeEaseOutBounce (float value)
		{
			float start = 0;
			float end = 1;
			value /= 1f;
			end -= start;

			if (value < (1 / 2.75f))
				return end * (7.5625f * value * value) + start;
			else if (value < (2 / 2.75f))
			{
				value -= (1.5f / 2.75f);
				return end * (7.5625f * (value) * value + .75f) + start;
			}
			else if (value < (2.5 / 2.75))
			{
				value -= (2.25f / 2.75f);
				return end * (7.5625f * (value) * value + .9375f) + start;
			}
			else
			{
				value -= (2.625f / 2.75f);
				return end * (7.5625f * (value) * value + .984375f) + start;
			}
		}

		protected static float ComputeEaseInOutBounce (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			float d = 1f;

			if (value < d * 0.5f)
				return ComputeEaseInBounce (value * 2) * 0.5f + start;
			else
				return ComputeEaseOutBounce (value * 2 - d) * 0.5f + end * 0.5f + start;
		}

		protected static float ComputeEaseInBack (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			value /= 1;
			float s = 1.70158f;
			return end * (value) * value * ((s + 1) * value - s) + start;
		}

		protected static float ComputeEaseOutBack (float value)
		{
			float start = 0;
			float end = 1;
			float s = 1.70158f;
			end -= start;
			value = (value) - 1;
			return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
		}

		protected static float ComputeEaseInOutBack (float value)
		{
			float start = 0;
			float end = 1;
			float s = 1.70158f;
			end -= start;
			value /= 0.5f;

			if ((value) < 1)
			{
				s *= (1.525f);
				return end * 0.5f * (value * value * (((s) + 1) * value - s)) + start;
			}
			value -= 2;
			s *= (1.525f);
			return end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + start;
		}

		protected static float ComputeEaseInElastic (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;

			float d = 1f;
			float p = d * 0.3f;
			float s = 0;
			float a = 0;

			if (value == 0)
				return start;

			if ((value /= d) == 1)
				return start + end;

			if (a == 0f || a < Mathf.Abs (end))
			{
				a = end;
				s = p / 4;
			}
			else
				s = p / (2 * Mathf.PI) * Mathf.Asin (end / a);

			return -(a * Mathf.Pow (2, 10 * (value -= 1)) * Mathf.Sin ((value * d - s) * (2 * Mathf.PI) / p)) + start;
		}

		protected static float ComputeEaseOutElastic (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;

			float d = 1f;
			float p = d * 0.3f;
			float s = 0;
			float a = 0;

			if (value == 0)
				return start;

			if ((value /= d) == 1)
				return start + end;

			if (a == 0f || a < Mathf.Abs (end))
			{
				a = end;
				s = p * 0.25f;
			}
			else
				s = p / (2 * Mathf.PI) * Mathf.Asin (end / a);

			return (a * Mathf.Pow (2, -10 * value) * Mathf.Sin ((value * d - s) * (2 * Mathf.PI) / p) + end + start);
		}

		protected static float ComputeEaseInOutElastic (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;

			float d = 1f;
			float p = d * 0.3f;
			float s = 0;
			float a = 0;

			if (value == 0)
				return start;

			if ((value /= d * 0.5f) == 2)
				return start + end;

			if (a == 0f || a < Mathf.Abs (end))
			{
				a = end;
				s = p / 4;
			}
			else
				s = p / (2 * Mathf.PI) * Mathf.Asin (end / a);

			if (value < 1)
				return -0.5f * (a * Mathf.Pow (2, 10 * (value -= 1)) * Mathf.Sin ((value * d - s) * (2 * Mathf.PI) / p)) + start;
			return a * Mathf.Pow (2, -10 * (value -= 1)) * Mathf.Sin ((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
		}
		#endregion
		#endregion
	}
}