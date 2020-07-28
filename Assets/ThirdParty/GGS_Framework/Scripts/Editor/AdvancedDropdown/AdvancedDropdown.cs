#if UNITY_EDITOR
using UnityEngine;

namespace GGS_Framework.Editor
{
    public abstract class AdvancedDropdown
    {
        #region Members
        private Vector2 minimumSize;
        private Vector2 maximumSize;

        internal AdvancedDropdownWindow windowInstance;
        internal AdvancedDropdownState state;
        internal AdvancedDropdownDataSource dataSource;
        internal AdvancedDropdownGUI gui;
        #endregion

        #region Accesors
        protected Vector2 MinimumSize
        {
            get { return minimumSize; }
            set { minimumSize = value; }
        }

        protected Vector2 MaximumSize
        {
            get { return maximumSize; }
            set { maximumSize = value; }
        }
        #endregion

        #region Constructors
        public AdvancedDropdown (AdvancedDropdownState state)
        {
            this.state = state;
        }
        #endregion

        #region Implementation
        public void Show (Rect rect)
        {
            if (windowInstance != null)
            {
                windowInstance.Close ();
                windowInstance = null;
            }

            if (dataSource == null)
                dataSource = new CallbackDataSource (BuildRoot);

            if (gui == null)
                gui = new AdvancedDropdownGUI ();

            windowInstance = ScriptableObject.CreateInstance<AdvancedDropdownWindow> ();
            if (minimumSize != Vector2.zero)
                windowInstance.minSize = minimumSize;
            if (maximumSize != Vector2.zero)
                windowInstance.maxSize = maximumSize;
            
            windowInstance.State = state;
            windowInstance.DataSource = dataSource;
            windowInstance.Gui = gui;
            windowInstance.WindowClosed += (w) => ItemSelected (w.GetSelectedItem ());
            windowInstance.Init (rect);
        }

        internal void SetFilter (string searchString)
        {
            windowInstance.SearchString = searchString;
        }

        protected abstract AdvancedDropdownItem BuildRoot ();

        protected virtual void ItemSelected (AdvancedDropdownItem item)
        {
        }
        #endregion
    }
}
#endif