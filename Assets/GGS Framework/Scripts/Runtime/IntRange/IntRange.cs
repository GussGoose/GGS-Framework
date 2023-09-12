// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework
{
	[System.Serializable]
    public class IntRange
    {
        #region Class Members
        public int start;
        public int end;
        #endregion

        #region Class Accesors
        public int Lenght
        {
            get { return end - start; }
        }
        #endregion

        #region Class Implementation
        public IntRange ()
        {
            start = 0;
            end = 0;
        }

        public IntRange (int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public bool InRange (int value)
        {
            return (value >= start && value <= end);
        }

        public int GetRandomValue ()
        {
            return Random.Range (start, end);
        }
        #endregion
    }
}