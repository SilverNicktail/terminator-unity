using Bethesda;

namespace TerminatorUnity.Asset
{

    /// <summary>
    /// Generically represents an XnGine asset folder to the system.
    /// </summary>
    interface IAssetFolder
    {

        XngineGame GetGame();

        string GetPath();

        bool FolderValid();

    }

}