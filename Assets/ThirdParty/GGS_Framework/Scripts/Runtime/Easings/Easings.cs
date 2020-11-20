using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
	public static class Easings
	{
		#region Class Members
		public static Dictionary<All, Func<float, float>> Functions = new Dictionary<All, Func<float, float>>
		{
			{
				All.Linear, ComputeLinear
			},
			{
				All.Quadratic, ComputeQuadratic
			},
			// { All.Clerp, ComputeClerp },
			{
				All.Spring, ComputeSpring
			},
			{
				All.EaseInQuad, ComputeEaseInQuad
			},
			{
				All.EaseOutQuad, ComputeEaseOutQuad
			},
			{
				All.EaseInOutQuad, ComputeEaseInOutQuad
			},
			{
				All.EaseInCubic, ComputeEaseInCubic
			},
			{
				All.EaseOutCubic, ComputeEaseOutCubic
			},
			{
				All.EaseInOutCubic, ComputeEaseInOutCubic
			},
			{
				All.EaseInQuart, ComputeEaseInQuart
			},
			{
				All.EaseOutQuart, ComputeEaseOutQuart
			},
			{
				All.EaseInOutQuart, ComputeEaseInOutQuart
			},
			{
				All.EaseInQuint, ComputeEaseInQuint
			},
			{
				All.EaseOutQuint, ComputeEaseOutQuint
			},
			{
				All.EaseInOutQuint, ComputeEaseInOutQuint
			},
			{
				All.EaseInSine, ComputeEaseInSine
			},
			{
				All.EaseOutSine, ComputeEaseOutSine
			},
			{
				All.EaseInOutSine, ComputeEaseInOutSine
			},
			{
				All.EaseInExpo, ComputeEaseInExpo
			},
			{
				All.EaseOutExpo, ComputeEaseOutExpo
			},
			{
				All.EaseInOutExpo, ComputeEaseInOutExpo
			},
			{
				All.EaseInCirc, ComputeEaseInCirc
			},
			{
				All.EaseOutCirc, ComputeEaseOutCirc
			},
			{
				All.EaseInOutCirc, ComputeEaseInOutCirc
			},
			{
				All.EaseInBounce, ComputeEaseInBounce
			},
			{
				All.EaseOutBounce, ComputeEaseOutBounce
			},
			{
				All.EaseInOutBounce, ComputeEaseInOutBounce
			},
			{
				All.EaseInBack, ComputeEaseInBack
			},
			{
				All.EaseOutBack, ComputeEaseOutBack
			},
			{
				All.EaseInOutBack, ComputeEaseInOutBack
			},
			{
				All.EaseInElastic, ComputeEaseInElastic
			},
			{
				All.EaseOutElastic, ComputeEaseOutElastic
			},
			{
				All.EaseInOutElastic, ComputeEaseInOutElastic
			}
		};
		#endregion

		#region Class Implementation
		public static float GetValue (Mode mode, Type type, float time)
		{
			int easeId = (int) mode | (int) type;

			if (Enum.IsDefined (typeof (All), easeId))
				return GetValue ((All) easeId, time);

			return -1;
		}

		public static float GetValue (All ease, float time)
		{
			return Functions[ease] (time);
		}

		#region Functions
		private static float ComputeLinear (float value)
		{
			float start = 0;
			float end = 1;
			return Mathf.Lerp (start, end, value);
		}

		private static float ComputeQuadratic (float value)
		{
			return -4 * Mathf.Pow (value, 2) + value * 4;
		}

		private static float ComputeClerp (float value)
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

		private static float ComputeSpring (float value)
		{
			float start = 0;
			float end = 1;
			value = Mathf.Clamp01 (value);
			value = (Mathf.Sin (value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow (1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
			return start + (end - start) * value;
		}

		// Not used by any method
		private static float ComputePunch (float amplitude, float value)
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

		private static float ComputeEaseInQuad (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value + start;
		}

		private static float ComputeEaseOutQuad (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * value * (value - 2) + start;
		}

		private static float ComputeEaseInOutQuad (float value)
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

		private static float ComputeEaseInCubic (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value * value + start;
		}

		private static float ComputeEaseOutCubic (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return end * (value * value * value + 1) + start;
		}

		private static float ComputeEaseInOutCubic (float value)
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

		private static float ComputeEaseInQuart (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value * value * value + start;
		}

		private static float ComputeEaseOutQuart (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return -end * (value * value * value * value - 1) + start;
		}

		private static float ComputeEaseInOutQuart (float value)
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

		private static float ComputeEaseInQuint (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * value * value * value * value * value + start;
		}

		private static float ComputeEaseOutQuint (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return end * (value * value * value * value * value + 1) + start;
		}

		private static float ComputeEaseInOutQuint (float value)
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

		private static float ComputeEaseInSine (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * Mathf.Cos (value * (Mathf.PI * 0.5f)) + end + start;
		}

		private static float ComputeEaseOutSine (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * Mathf.Sin (value * (Mathf.PI * 0.5f)) + start;
		}

		private static float ComputeEaseInOutSine (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * 0.5f * (Mathf.Cos (Mathf.PI * value) - 1) + start;
		}

		private static float ComputeEaseInExpo (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * Mathf.Pow (2, 10 * (value - 1)) + start;
		}

		private static float ComputeEaseOutExpo (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return end * (-Mathf.Pow (2, -10 * value) + 1) + start;
		}

		private static float ComputeEaseInOutExpo (float value)
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

		private static float ComputeEaseInCirc (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			return -end * (Mathf.Sqrt (1 - value * value) - 1) + start;
		}

		private static float ComputeEaseOutCirc (float value)
		{
			float start = 0;
			float end = 1;
			value--;
			end -= start;
			return end * Mathf.Sqrt (1 - value * value) + start;
		}

		private static float ComputeEaseInOutCirc (float value)
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

		private static float ComputeEaseInBounce (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			float d = 1f;
			return end - ComputeEaseOutBounce (d - value) + start;
		}

		private static float ComputeEaseOutBounce (float value)
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

		private static float ComputeEaseInOutBounce (float value)
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

		private static float ComputeEaseInBack (float value)
		{
			float start = 0;
			float end = 1;
			end -= start;
			value /= 1;
			float s = 1.70158f;
			return end * (value) * value * ((s + 1) * value - s) + start;
		}

		private static float ComputeEaseOutBack (float value)
		{
			float start = 0;
			float end = 1;
			float s = 1.70158f;
			end -= start;
			value = (value) - 1;
			return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
		}

		private static float ComputeEaseInOutBack (float value)
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

		private static float ComputeEaseInElastic (float value)
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

		private static float ComputeEaseOutElastic (float value)
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

		private static float ComputeEaseInOutElastic (float value)
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

		#region Nested Classes
		public enum All
		{
			Linear = Mode.Basic | Type.Linear,
			Quadratic = Mode.Basic | Type.Quadratic,
			// Clerp = Mode.Basic | Type.Clerp,
			Spring = Mode.Basic | Type.Spring,

			EaseInQuad = Mode.In | Type.Quad,
			EaseOutQuad = Mode.Out | Type.Quad,
			EaseInOutQuad = Mode.InOut | Type.Quad,

			EaseInCubic = Mode.In | Type.Cubic,
			EaseOutCubic = Mode.Out | Type.Cubic,
			EaseInOutCubic = Mode.InOut | Type.Cubic,

			EaseInQuart = Mode.In | Type.Quart,
			EaseOutQuart = Mode.Out | Type.Quart,
			EaseInOutQuart = Mode.InOut | Type.Quart,

			EaseInQuint = Mode.In | Type.Quint,
			EaseOutQuint = Mode.Out | Type.Quint,
			EaseInOutQuint = Mode.InOut | Type.Quint,

			EaseInSine = Mode.In | Type.Sine,
			EaseOutSine = Mode.Out | Type.Sine,
			EaseInOutSine = Mode.InOut | Type.Sine,

			EaseInExpo = Mode.In | Type.Expo,
			EaseOutExpo = Mode.Out | Type.Expo,
			EaseInOutExpo = Mode.InOut | Type.Expo,

			EaseInCirc = Mode.In | Type.Circ,
			EaseOutCirc = Mode.Out | Type.Circ,
			EaseInOutCirc = Mode.InOut | Type.Circ,

			EaseInBounce = Mode.In | Type.Bounce,
			EaseOutBounce = Mode.Out | Type.Bounce,
			EaseInOutBounce = Mode.InOut | Type.Bounce,

			EaseInBack = Mode.In | Type.Back,
			EaseOutBack = Mode.Out | Type.Back,
			EaseInOutBack = Mode.InOut | Type.Back,

			EaseInElastic = Mode.In | Type.Elastic,
			EaseOutElastic = Mode.Out | Type.Elastic,
			EaseInOutElastic = Mode.InOut | Type.Elastic
		}

		public enum Mode
		{
			Nothing = 0,
			Basic = 1 << 1,
			In = 1 << 2,
			Out = 1 << 3,
			InOut = 1 << 4,

			Everything = Basic | In | Out | InOut,
			// Everything = 894399898
		}

		public enum Type
		{
			Nothing = 0,
			Linear = 1 << 10,
			Quadratic = 1 << 11,
			// Clerp = 1 << 12,
			Spring = 1 << 13,

			Quad = 1 << 14,
			Cubic = 1 << 15,
			Quart = 1 << 16,
			Quint = 1 << 17,
			Sine = 1 << 18,
			Expo = 1 << 19,
			Circ = 1 << 20,
			Bounce = 1 << 21,
			Back = 1 << 22,
			Elastic = 1 << 23,

			Everything = Linear | Quadratic | Spring | Quad | Cubic | Quart | Quint | Sine | Expo | Circ | Bounce | Back | Elastic
		}
		#endregion
	}
}