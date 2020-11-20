#if UNITY_EDITOR
using System;

namespace GGS_Framework.Editor
{
    public class CallbackDataSource : AdvancedDropdownDataSource
    {
        #region Members
        private Func<AdvancedDropdownItem> buildCallback;
        #endregion

        #region Constructors
        internal CallbackDataSource (Func<AdvancedDropdownItem> buildCallback)
        {
            this.buildCallback = buildCallback;
        }
        #endregion

        #region Overrides
        protected override AdvancedDropdownItem FetchData ()
        {
            return buildCallback ();
        }
        #endregion
    }
}
#endif // UNITY_EDITOR