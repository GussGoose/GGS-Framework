// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Random = System.Random;

namespace GGS_Framework.Editor
{
    [Serializable]
    public class ReorderableListState
    {
        #region Members
        [SerializeField] private TreeViewState treeViewState;
        [SerializeField] private int uniqueID;
        #endregion

        #region Properties
        public int UniqueID
        {
            get
            {
                if (uniqueID == 0)
                    uniqueID = IdGenerator.GetUniqueID ();
                else
                {
                    // If the assigned ID its already available, get a new one for avoid multiple lists with the same ID.
                    if (IdGenerator.GeneratedIDs.Contains (uniqueID))
                        uniqueID = IdGenerator.GetUniqueID ();
                }

                return uniqueID;
            }
        }

        internal TreeViewState TreeViewState
        {
            get { return treeViewState; }
        }

        public List<int> SelectedIDs
        {
            get
            {
                return treeViewState.selectedIDs.Select (id => id - UniqueID).ToList ();
            }
            set
            {
                treeViewState.selectedIDs = value.Select (id => id + UniqueID).ToList ();
            }
        }

        public int LastClickedID
        {
            get { return treeViewState.lastClickedID - UniqueID; }
            set { treeViewState.lastClickedID = value + UniqueID; }
        }

        public string Search
        {
            get { return treeViewState.searchString; }
            set { treeViewState.searchString = value; }
        }
        #endregion

        #region Constructors
        public ReorderableListState ()
        {
            treeViewState = new TreeViewState ();
        }
        #endregion

        #region Nested Types
        private static class IdGenerator
        {
            #region Members
            private const int GeneratedIDsCapacity = 100000;

            private const int SpaceBetweenIDs = 10000;
            private const int MinimunValue = int.MinValue / SpaceBetweenIDs;
            private const int MaximunValue = int.MaxValue / SpaceBetweenIDs;

            private static readonly Random generator;
            private static HashSet<int> generatedIds;
            #endregion

            #region Properties
            private static string StoredIDs
            {
                get { return EditorPrefs.GetString ("ReorderableListState_IDs"); }
                set { EditorPrefs.SetString ("ReorderableListState_IDs", value); }
            }

            public static HashSet<int> GeneratedIDs
            {
                get { return generatedIds; }
            }
            #endregion

            #region Constructors
            static IdGenerator ()
            {
                generator = new Random (DateTime.Now.Ticks.GetHashCode ());
                generatedIds = new HashSet<int> ();

                CatchStoredIDs ();
            }
            #endregion

            #region Implementation
            [InitializeOnLoadMethod]
            private static void Initialize ()
            {
                EditorApplication.quitting += UpdateStoredIDs;
                AssemblyReloadEvents.beforeAssemblyReload += UpdateStoredIDs;
            }

            public static int GetUniqueID ()
            {
                int id = generatedIds.First ();
                generatedIds.Remove (id);

                if (generatedIds.Count == 0)
                    CatchStoredIDs ();

                return id;
            }

            private static void UpdateStoredIDs ()
            {
                StoredIDs = string.Join (",", generatedIds);
            }

            private static void CatchStoredIDs ()
            {
                string storedIDs = StoredIDs;

                if (string.IsNullOrEmpty (storedIDs))
                    generatedIds = GenerateIDs ();
                else
                {
                    string[] splitedIDs = storedIDs.Split (new[] {","}, StringSplitOptions.RemoveEmptyEntries);

                    generatedIds.Clear ();
                    for (int i = 0; i < splitedIDs.Length; i++)
                        generatedIds.Add (int.Parse (splitedIDs[i]));
                }
            }

            private static HashSet<int> GenerateIDs ()
            {
                HashSet<int> ids = new HashSet<int> ();

                for (int i = 0; i < GeneratedIDsCapacity; i++)
                {
                    int id = GetRandomValue ();

                    if (ids.Contains (id))
                    {
                        while (ids.Contains (id))
                            id = GetRandomValue ();
                    }

                    ids.Add (id);
                }

                return ids;
            }

            private static int GetRandomValue ()
            {
                return generator.Next (MinimunValue, MaximunValue) * SpaceBetweenIDs;
            }
            #endregion
        }
        #endregion
    }
}