using UnityEngine;

namespace GGS_Framework
{
    public abstract class AssetCreationInfo : ScriptableObject
    {
        #region Implementation
        public abstract float GetLabelWidth ();
        #endregion
    }
}