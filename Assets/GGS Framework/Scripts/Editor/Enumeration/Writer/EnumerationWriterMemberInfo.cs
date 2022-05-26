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