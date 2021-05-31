using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GGS_Framework
{
    [Serializable]
    public class AssetCreationArgs
    {
        #region Members
        [SerializeField] private string name;
        [SerializeField] private int id;
        
        private AssetCreationInfo creationInfo;
        #endregion

        #region Properties
        public int ID
        {
            get { return id; }
            set { id = Mathf.Clamp (value, 1, int.MaxValue); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public AssetCreationInfo CreationInfo
        {
            get { return creationInfo; }
            set { creationInfo = value; }
        }
        #endregion

        #region Implementation
        public AssetCreationArgs ()
        {
        }

        public AssetCreationArgs (string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public void ValidateName (List<string> names)
        {
            if (string.IsNullOrEmpty (name) || string.IsNullOrWhiteSpace (name))
            {
                name = "Unnamed";
                return;
            }

            // *white space*(*digits*)
            Regex regex = new Regex (string.Concat (name, @"\s\((\d+)\)"));

            int duplicatedNameMaxIndex = int.MinValue;

            for (int i = 0; i < names.Count; i++)
            {
                Match match = regex.Match (names[i]);
                if (match.Success)
                {
                    int duplicatedNameIndex = int.Parse (match.Groups[1].Value);

                    if (duplicatedNameIndex > duplicatedNameMaxIndex)
                        duplicatedNameMaxIndex = duplicatedNameIndex;
                }
                // If the duplicated name doesn't have index, starts index in 0
                else if (name == names[i])
                    duplicatedNameMaxIndex = 0;
            }

            // If no repeated name was found, return the original name
            if (duplicatedNameMaxIndex == int.MinValue)
                return;

            this.name = $"{name} ({duplicatedNameMaxIndex + 1})";
        }
        #endregion
    }
}