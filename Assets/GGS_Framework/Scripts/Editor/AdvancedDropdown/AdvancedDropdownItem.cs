#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public class AdvancedDropdownItem : IComparable
    {
        #region Members
        private static readonly AdvancedDropdownItem SeparatorItem = new SeparatorDropdownItem ();

        private string name;
        private int id;
        private int elementIndex = -1;
        
        private bool enabled = true;
        private Texture2D icon;
        private List<AdvancedDropdownItem> children = new List<AdvancedDropdownItem> ();

        protected object additionalData;
        protected string searchableName;
        #endregion

        #region Accesors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        internal int ElementIndex
        {
            get { return elementIndex; }
            set { elementIndex = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Texture2D Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public IEnumerable<AdvancedDropdownItem> Children
        {
            get { return children; }
        }

        public virtual string SearchableName
        {
            get { return string.IsNullOrEmpty (searchableName) ? Name : searchableName; }
        }

        public virtual object AdditionalData
        {
            get { return additionalData; }
        }
        #endregion

        #region Constructors
        public AdvancedDropdownItem (string name)
        {
            this.name = name;
            id = name.GetHashCode ();
        }
        
        public AdvancedDropdownItem (string name, object additionalData)
        {
            this.name = name;
            id = name.GetHashCode ();
            
            this.additionalData = additionalData;
        }
        #endregion

        #region Implementation
        public void AddChild (AdvancedDropdownItem child)
        {
            children.Add (child);
        }

        public virtual int CompareTo (object o)
        {
            return Name.CompareTo ((o as AdvancedDropdownItem).Name);
        }

        public void AddSeparator ()
        {
            AddChild (SeparatorItem);
        }

        internal bool IsSeparator ()
        {
            return SeparatorItem == this;
        }

        public override string ToString ()
        {
            return name;
        }
        #endregion

        #region Nested Classes
        private class SeparatorDropdownItem : AdvancedDropdownItem
        {
            public SeparatorDropdownItem () : base ("SEPARATOR")
            {
            }
        }
        #endregion
    }
}
#endif // UNITY_EDITOR