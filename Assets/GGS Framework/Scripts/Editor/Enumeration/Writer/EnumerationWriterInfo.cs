// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;

namespace GGS_Framework.Editor
{
    public class EnumerationWriterInfo
    {
        #region Properties
        public string WritePath { get; }

        public string Namespace { get; }

        public Type EnumerationType { get; }

        public EnumerationWriterMemberInfo[] Members { get; }
        #endregion

        #region Implementation
        public EnumerationWriterInfo (string writePath, string @namespace, Type enumerationType, EnumerationWriterMemberInfo[] members)
        {
            WritePath = writePath;
            Namespace = @namespace;
            EnumerationType = enumerationType;
            Members = members;
        }
        #endregion
    }
}