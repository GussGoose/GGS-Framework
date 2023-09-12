// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace GGS_Framework.Editor
{
    public static class EnumerationWriter
    {
        #region Members
        private static readonly string WriterTemplatePath = $"{GGS_FrameworkEditorPaths.ScriptsDirectoryPath}/Editor/Enumeration/Writer/EnumerationWriterTemplate.txt";
        #endregion

        #region Implemenetation
        public static void Write (EnumerationWriterInfo info)
        {
            List<string> newLines = new List<string> ();
            string[] writerTemplateLines = File.ReadAllLines (WriterTemplatePath);

            for (int i = 0; i < writerTemplateLines.Length; i++)
            {
                string currentLine = writerTemplateLines[i];

                if (currentLine.Contains ("#Namespace#"))
                    currentLine = currentLine.Replace ("#Namespace#", info.Namespace);

                if (currentLine.Contains ("#EnumerationType#"))
                    currentLine = currentLine.Replace ("#EnumerationType#", info.EnumerationType.Name);

                if (currentLine.Contains ("<MemberDeclaration>"))
                {
                    currentLine = currentLine.Replace ("<MemberDeclaration>", string.Empty);
                    currentLine = currentLine.Replace ("</MemberDeclaration>", string.Empty);

                    WriteMembersDeclaration (ref newLines, currentLine, info.Members);

                    continue;
                }

                newLines.Add (currentLine);
            }

            File.WriteAllLines (info.WritePath, newLines);
            AssetDatabase.Refresh ();
        }

        private static void WriteMembersDeclaration (ref List<string> lines, string memberDeclarationTemplate, EnumerationWriterMemberInfo[] members)
        {
            for (int i = 0; i < members.Length; i++)
            {
                string memberDeclaration = memberDeclarationTemplate.Replace ("#MemberName#", members[i].Name);
                memberDeclaration = memberDeclaration.Replace ("#MemberID#", members[i].ID.ToString ());

                lines.Add (memberDeclaration);
            }
        }
        #endregion
    }
}