﻿// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
    public class ShuffledCollection<T>
    {
        #region Class Members
        private List<T> elements;
        private int currentShuffleListIndex;

        private T lastReturnedElement;
        private bool preventShuffleStartsWithLastElement;
        #endregion

        #region Class Accesors
        public int LeftElements
        {
            get { return elements.Count - currentShuffleListIndex; }
        }

        public List<T> List
        {
            get;
            private set;
        }
        #endregion

        #region Class Implementation
        public ShuffledCollection (T[] array, bool preventShuffleStartsWithLastElement = false)
        {
            this.preventShuffleStartsWithLastElement = preventShuffleStartsWithLastElement;

            elements = new List<T> (array);
            Shuffle ();
        }

        public ShuffledCollection (List<T> list, bool preventShuffleStartsWithLastElement = false)
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