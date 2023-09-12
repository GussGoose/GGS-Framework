// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
    public static partial class AdvancedGenericDropdown
    {
        public abstract class Option
        {
            #region Class Accesors
            public string Path
            {
                get;
                private set;
            }

            public object Data
            {
                get;
                private set;
            }

            public bool Selected
            {
                get;
                private set;
            }

            public bool Use
            {
                get;
                private set;
            }
            #endregion

            #region Class Implementation
            protected Option (string path, bool selected = false, bool use = true)
            {
                Path = path;
                Selected = selected;
                Use = use;
            }

            protected Option (string path, object data, bool selected = false, bool use = true)
            {
                Path = path;
                Data = data;

                Selected = selected;
                Use = use;
            }

            public string GetValue ()
            {
                string[] splittedPath = Path.Split ('/');
                return splittedPath[splittedPath.Length - 1];
            }
            #endregion
        }
    }
}