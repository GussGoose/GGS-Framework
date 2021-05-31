using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GGS_Framework
{
    [Serializable]
    public abstract class Enumeration : IComparable
    {
        #region Members
        private const BindingFlags FieldFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;

        private string name;
        [SerializeField] private int id;
        #endregion

        #region Properties
        public abstract Type Type { get; }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty (name))
                    name = GetMemberByID (Type, ID).Name;

                return name;
            }
            private set
            {
                name = value;
            }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region Constructors
        protected Enumeration (int id, string name)
        {
            ID = id;
            Name = name;
        }
        #endregion

        #region Implementation
        public override string ToString ()
        {
            return $"{Type}, ID: {ID}, Name: {Name}";
        }

        public int CompareTo (object other)
        {
            return ID.CompareTo (((Enumeration) other).ID);
        }

        public override bool Equals (object obj)
        {
            if (!(obj is Enumeration otherValue))
                return false;

            bool typeMatches = GetType () == obj.GetType ();
            bool valueMatches = ID.Equals (otherValue.ID);

            return typeMatches && valueMatches;
        }

        public static bool operator == (Enumeration enumerationA, Enumeration enumerationB)
        {
            if (ReferenceEquals (enumerationA, enumerationB))
                return true;

            if (ReferenceEquals (enumerationA, null))
                return false;

            if (ReferenceEquals (enumerationB, null))
                return false;

            return enumerationA.Equals (enumerationB);
        }

        public static bool operator != (Enumeration enumerationA, Enumeration enumerationB)
        {
            return !(enumerationA == enumerationB);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                return (id * 397) ^ (Type != null ? Type.GetHashCode () : 0);
            }
        }
        #endregion

        #region Static Functions
        public static T[] GetMerbersOf<T> () where T : Enumeration
        {
            return typeof(T).GetFields (FieldFlags).Select (fieldQuery => fieldQuery.GetValue (null)).Cast<T> ().ToArray ();
        }

        public static T GetMemberByID<T> (int id) where T : Enumeration
        {
            return GetMerbersOf<T> ().FirstOrDefault (member => member.ID == id);
        }

        public static T GetMemberByName<T> (string name) where T : Enumeration
        {
            return GetMerbersOf<T> ().FirstOrDefault (member => member.Name == name);
        }

        public static Enumeration[] GetMerbersOf (Type type)
        {
            return type.GetFields (FieldFlags).Select (fieldQuery => fieldQuery.GetValue (null)).Cast<Enumeration> ().ToArray ();
        }

        public static Enumeration GetMemberByID (Type enumerationType, int id)
        {
            return GetMerbersOf (enumerationType).FirstOrDefault (member => member.ID == id);
        }

        public static Enumeration GetMemberByName (Type enumerationType, string name)
        {
            return GetMerbersOf (enumerationType).FirstOrDefault (member => member.Name == name);
        }

        public static Type[] GetAllDefinitions ()
        {
            return Assembly.GetAssembly (typeof(Enumeration)).GetTypes ().Where (myType => myType.IsClass && myType.IsSubclassOf (typeof(Enumeration))).ToArray ();
        }
        #endregion
    }
}