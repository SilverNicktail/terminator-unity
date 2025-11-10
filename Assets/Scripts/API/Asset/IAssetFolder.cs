using Bethesda;
using XnGine;

namespace TerminatorUnity.Asset
{

    /// <summary>
    /// Generically represents an XnGine asset folder to the system.
    /// TODO: Add some method of describing a game/asset bundles capabilities
    /// The interface is already getting long. Would be better to have the
    /// asset bundle describe which capabilities were available rather than
    /// having a single massive interface with them all in and null-checking
    /// on each call.
    /// </summary>
    public interface IAssetFolder
    {

        XngineGame GetGame();

        string GetRootPath();

        bool FolderValid(bool requireVideos = false);

        bool ProvidesAssetType(AssetType type);

        string[] GetAssetPaths(AssetType type);

        string GetArchivePath(AssetType archiveType);

    }

}