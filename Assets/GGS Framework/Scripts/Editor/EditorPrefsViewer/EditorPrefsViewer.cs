// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public class EditorPrefsViewer : EditorWindow
    {
        #region Members
        private const string EditorPrefsFilePath = "Library/Preferences/com.unity3d.UnityEditor5.x.plist";
        private PlistDocument plistDocument;
        #endregion

        #region Implementation
        [MenuItem ("Window/GGS Framework/Editor Prefs Viewer")]
        public static void Open ()
        {
            EditorPrefsViewer window = GetWindow<EditorPrefsViewer> ();
            window.titleContent = new GUIContent ("Editor Prefs Viewer");

            window.Show ();
            // window.LoadStoredData ();
        }

        private void LoadStoredData ()
        {
            string plistPath = $"{Environment.GetFolderPath (Environment.SpecialFolder.Personal)}/{EditorPrefsFilePath}";
            FileInfo fileInfo = new FileInfo (plistPath);

            plistDocument = new PlistDocument ();
            plistDocument.ReadFromString(File.ReadAllText(plistPath));
            
            PlistElementDict rootDict = plistDocument.root;


            // plistDocument.ReadFromFile (fileInfo.FullName);
            // Dictionary<string, object> plist = (Dictionary<string, object>) Plist.readPlist (fileInfo.FullName);
            //
            //
            //
            // foreach (KeyValuePair<string, object> keyValuePair in plist)
            // {
            //     Debug.LogError ($"Key: {keyValuePair.Key}, Object: {keyValuePair.Value.ToString ()}");
            // }

            //
            //
            // plistDocument.root[0][]
        }

        // public static object readPlist (string path)
        // {
        //     using (FileStream f = new FileStream (path, FileMode.Open, FileAccess.Read))
        //     {
        //         return readPlist (f, plistType.Auto);
        //     }
        // }
        //
        // public static object readPlist (Stream stream, plistType type)
        // {
        //     if (type == plistType.Auto)
        //     {
        //         type = getPlistType (stream);
        //         stream.Seek (0, SeekOrigin.Begin);
        //     }
        //
        //     if (type == plistType.Binary)
        //     {
        //         using (BinaryReader reader = new BinaryReader (stream))
        //         {
        //             byte[] data = reader.ReadBytes ((int) reader.BaseStream.Length);
        //             return readBinary (data);
        //         }
        //     }
        //     else
        //     {
        //         XmlDocument xml = new XmlDocument ();
        //         xml.XmlResolver = null;
        //         xml.Load (stream);
        //         return readXml (xml);
        //     }
        // }
        //
        // private static object readXml (XmlDocument xml)
        // {
        //     XmlNode rootNode = xml.DocumentElement.ChildNodes[0];
        //     return (Dictionary<string, object>) parse (rootNode);
        // }
        //
        // private static object readBinary (byte[] data)
        // {
        //     offsetTable.Clear ();
        //     List<byte> offsetTableBytes = new List<byte> ();
        //     objectTable.Clear ();
        //     refCount = 0;
        //     objRefSize = 0;
        //     offsetByteSize = 0;
        //     offsetTableOffset = 0;
        //
        //     List<byte> bList = new List<byte> (data);
        //
        //     List<byte> trailer = bList.GetRange (bList.Count - 32, 32);
        //
        //     parseTrailer (trailer);
        //
        //     objectTable = bList.GetRange (0, (int) offsetTableOffset);
        //
        //     offsetTableBytes = bList.GetRange ((int) offsetTableOffset, bList.Count - (int) offsetTableOffset - 32);
        //
        //     parseOffsetTable (offsetTableBytes);
        //
        //     return parseBinary (0);
        // }
        //
        // public static plistType getPlistType (Stream stream)
        // {
        //     byte[] magicHeader = new byte[8];
        //     stream.Read (magicHeader, 0, 8);
        //
        //     if (BitConverter.ToInt64 (magicHeader, 0) == 3472403351741427810)
        //     {
        //         return plistType.Binary;
        //     }
        //     else
        //     {
        //         return plistType.Xml;
        //     }
        // }

        /// <summary>
        /// On Mac OS X PlayerPrefs are stored in ~/Library/Preferences folder, in a file named unity.[company name].[product name].plist, where company and product names are the names set up in Project Settings. The same .plist file is used for both Projects run in the Editor and standalone players. 
        /// </summary>
        // private string[] GetAllMacKeys()
        // {
        //     string companyName = ReplaceSpecialCharacters(PlayerSettings.companyName);
        //     string productName = ReplaceSpecialCharacters(PlayerSettings.productName);
        //     string plistPath = string.Format("{0}/Library/Preferences/unity.{1}.{2}.plist", Environment.GetFolderPath(Environment.SpecialFolder.Personal), companyName, productName);
        //     string[] keys = new string[0];
        //
        //     if (File.Exists(plistPath))
        //     {
        //         FileInfo fi = new FileInfo(plistPath);
        //         Dictionary<string, object> plist = (Dictionary<string, object>)Plist.readPlist(fi.FullName);
        //
        //         keys = new string[plist.Count];
        //         plist.Keys.CopyTo(keys, 0);
        //     }
        //
        //     return keys;
        // }
        #endregion

        #region Unity Callbacks
        private void OnGUI ()
        {
            // foreach (KeyValuePair<string, PlistElement> element in plistDocument.root.values)
            // {
            //     EditorGUILayout.LabelField (element.Key, element.Value.AsString ());
            // }

            if (GUILayout.Button ("AAA"))
                // Debug.LogError (EditorUtility.OpenFilePanel ("Import PlayerPrefs", "", "ppe"));
                LoadStoredData ();
        }
        #endregion
    }
}