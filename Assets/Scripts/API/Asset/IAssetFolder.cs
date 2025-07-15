using Bethesda;

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

        string GetPath();

        bool FolderValid(bool requireVideos = false);

        string[] GetFontFilepaths();

        string GetBriefingArchivePath();

        string GetEnemyArchivePath();

        string GetImageArchivePath();

        string GetMapArchivePath();

        string GetMapBlockArchivePath();

        string GetModelsArchivePath();

        string GetMusicArchivePath();

        string GetSFXArchivePath();

        string GetWoodsArchivePath();

    }

}