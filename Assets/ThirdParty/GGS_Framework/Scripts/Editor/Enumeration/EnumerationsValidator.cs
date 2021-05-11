using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    [InitializeOnLoad]
    public static class EnumerationsValidator
    {
        #region Members
        private static List<string> cachedValidationErrors;
        #endregion

        #region Properties
        public static bool AreAllEnumerationsValid { get; private set; }

        public static Type[] CatchedEnumerationTypes { get; private set; }
        #endregion

        #region Constructors
        static EnumerationsValidator ()
        {
            ValidateEnums ();
        }
        #endregion

        #region Implementation
        private static void ValidateEnums ()
        {
            CatchedEnumerationTypes = CatchAllEnumerationTypes ();

            AreAllEnumerationsValid = true;
            cachedValidationErrors = new List<string> ();

            foreach (Type enumeration in Enumeration.GetAllDefinitions ())
            {
                if (!ValidateEnum (enumeration))
                    AreAllEnumerationsValid = false;
            }
        }

        private static bool ValidateEnum (Type enumerationType)
        {
            Enumeration[] members = Enumeration.GetMerbersOf (enumerationType);

            IEnumerable<IGrouping<int, Enumeration>> groupsByID = members.GroupBy (member => member.ID);
            IEnumerable<IGrouping<string, Enumeration>> groupsByName = members.GroupBy (member => member.Name);

            List<List<Enumeration>> groups = groupsByID.Select (group => group.ToList ()).ToList ();
            groups.AddRange (groupsByName.Select (group => group.ToList ()));

            foreach (List<Enumeration> group in groups)
            {
                if (group.Count () == 1)
                    continue;

                Enumeration[] duplicatedMembers = group.ToArray ();

                string message = $"There's a member definition " +
                                 $"declared {duplicatedMembers.Length} times " +
                                 $"in <color=red>{enumerationType}</color>, " +
                                 $"the members are: " +
                                 $"{string.Join (", ", duplicatedMembers.Select (@enum => $"<color=red>ID: {@enum.ID}, Name: {@enum.Name}</color>"))}";

                cachedValidationErrors.Add (message);
                Debug.LogError (message);
            }

            if (cachedValidationErrors.Count > 0)
                return false;

            return true;
        }

        public static void LogValidationErrors ()
        {
            foreach (string validationError in cachedValidationErrors)
                Debug.LogError (validationError);
        }

        private static Type[] CatchAllEnumerationTypes ()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies ();
            List<Type> allTypes = new List<Type> ();

            foreach (Assembly assembly in assemblies)
                allTypes.AddRange (assembly.GetTypes ());

            return allTypes.Where (type => type.IsSubclassOf (typeof(Enumeration))).ToArray ();
        }
        #endregion
    }
}