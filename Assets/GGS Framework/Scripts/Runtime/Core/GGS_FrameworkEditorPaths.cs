// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GGS_Framework
{
    public static class GGS_FrameworkEditorPaths
    {
        #region Members
        public static readonly string SystemSlash = (Application.platform == RuntimePlatform.WindowsEditor) ? "\\" : "/";
        public static readonly string EditorResourcesDirectoryPath = GetDirectoryWithPartialPath ($"GGS Framework{SystemSlash}EditorResources");
        public static readonly string ScriptsDirectoryPath = GetDirectoryWithPartialPath ($"GGS Framework{SystemSlash}Scripts");
        #endregion

        #region Implementation
        public static string GetDirectoryWithPartialPath (string partialPath)
        {
            Stack<string> stack = new Stack<string> ();

            // Add the root directory to the stack
            stack.Push ("Assets");

            // While we have directories to process...
            while (stack.Count > 0)
            {
                // Grab a directory off the stack
                string currentDirectory = stack.Pop ();
                try
                {
                    foreach (string directory in Directory.GetDirectories (currentDirectory))
                    {
                        if (directory.Contains ("."))
                            continue;

                        if (directory.Contains (partialPath))
                            return directory;

                        // Add directories at the current level into the stack
                        stack.Push (directory);
                    }
                }
                catch
                {
                    Debug.LogError ("Directory " + currentDirectory + " couldn't be read from.");
                }
            }

            return string.Empty;
        }
        #endregion
    }
}