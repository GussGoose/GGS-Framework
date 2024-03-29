﻿// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;
using UnityEngine.UI;

namespace GGS_Framework
{
	public class ButtonResizer : MonoBehaviour
	{
		#region Class Members
		public float scaleMutiplier;

		[SerializeField, HideInInspector] private RectTransform rectTransform;
		#endregion

		#region Class Overrides
		private void Awake ()
		{
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform> ();

			Create ();
		}

		private void OnDrawGizmosSelected ()
		{
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform> ();

			Vector3[] corners = new Vector3[4];
			rectTransform.GetWorldCorners (corners);

			Vector3 center = (new Vector2 ((corners[0].x + corners[2].x) / 2f, (corners[1].y + corners[3].y) / 2f));

			for (int i = 0; i < corners.Length; i++)
			{
				Vector3 corner = corners[i];
				corners[i] = center + (corner - center) * scaleMutiplier;
			}

			Gizmos.color = Color.green;
			Gizmos.DrawLine (corners[0], corners[1]);
			Gizmos.DrawLine (corners[1], corners[2]);
			Gizmos.DrawLine (corners[2], corners[3]);
			Gizmos.DrawLine (corners[3], corners[0]);
		}
		#endregion

		#region Class Implementation
		public void Create ()
		{
			GameObject gameObject = new GameObject ("ResizedButton", typeof (RectTransform), typeof (Image));

			RectTransform rect = gameObject.GetComponent<RectTransform> ();
			Image image = gameObject.GetComponent<Image> ();

			rect.SetParent (transform, false);

			rect.anchoredPosition = Vector2.zero;
			rect.sizeDelta = rectTransform.sizeDelta * scaleMutiplier / 2f;

			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;
			rect.pivot = Vector2.one / 2f;

			image.color = Color.clear;
		}
		#endregion
	}
}