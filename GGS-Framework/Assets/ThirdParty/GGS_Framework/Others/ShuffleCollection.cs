using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
	public class ShuffleCollection<T>
	{
		#region Class members
		private List<T> elements;
		private int currentShuffleListIndex;

		private T lastReturnedElement;
		private bool preventShuffleStartsWithLastElement;
		#endregion

		#region Class accesors
		public int LeftElements
		{
			get { return elements.Count - currentShuffleListIndex; }
		}

		public List<T> List
		{
			get; private set;
		}
		#endregion

		#region Class implementation
		public ShuffleCollection (T[] array, bool preventShuffleStartsWithLastElement = false)
		{
			this.preventShuffleStartsWithLastElement = preventShuffleStartsWithLastElement;

			elements = new List<T> (array);
			Shuffle ();
		}

		public ShuffleCollection (List<T> list, bool preventShuffleStartsWithLastElement = false)
		{
			this.preventShuffleStartsWithLastElement = preventShuffleStartsWithLastElement;

			elements = list;
			Shuffle ();
		}

		private void Shuffle ()
		{
			List<T> list = new List<T> (elements);

			if (List == null)
				List = new List<T> ();

			List.Clear ();

			for (int i = 0; i < elements.Count; i++)
			{
				int randomIndex = Random.Range (0, list.Count);

				if (i == 0)
				{
					if (preventShuffleStartsWithLastElement && lastReturnedElement != null)
					{
						int lastElementIndex = list.IndexOf (lastReturnedElement);

						if (randomIndex == lastElementIndex)
						{
							while (randomIndex == lastElementIndex)
								randomIndex = Random.Range (0, list.Count);
						}
					}
				}

				List.Add (list[randomIndex]);
				list.RemoveAt (randomIndex);
			}
		}

		public T GetNextElement ()
		{
			if (currentShuffleListIndex == elements.Count)
			{
				Shuffle ();
				currentShuffleListIndex = 0;
			}

			T element = List[currentShuffleListIndex];
			lastReturnedElement = element;
			currentShuffleListIndex++;

			return element;
		}
		#endregion
	}
}