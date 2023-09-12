// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
    public class EnumerationWriterMemberInfo
    {
        #region Properties
        public int ID { get; }

        public string Name { get; }
        #endregion

        #region Implementation
        public EnumerationWriterMemberInfo (int id, string name)
        {
            ID = id;
            Name = name;
        }
        #endregion
    }
}