using Bethesda;
using DaggerfallConnect.Utility;

namespace TerminatorUnity.Asset
{

    /// <summary>
    /// Represents the Daggerfall asset folder to the rest of the program.
    /// (Largely delegates to pre-existing code from Daggerfall Unity for
    /// upstream compatibility, but allows us to slot other games in alongside.)
    /// </summary>
    public class DaggerfallFolder : IAssetFolder
    {

        private string path;

        public DaggerfallFolder(string path) {
            this.path = path;
        }

        public XngineGame GetGame() {
            return XngineGame.ES_DAGGERFALL;
        }

        public string GetPath() {
            return path;
        }

        public bool FolderValid() {
            DFValidator.ValidateArena2Folder(this.path, out DFValidator.ValidationResults results);
            return results.AppearsValid;
        }

    }
}