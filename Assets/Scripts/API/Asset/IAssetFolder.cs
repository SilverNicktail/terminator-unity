using Bethesda;

namespace TerminatorUnity.Asset
{

    /// <summary>
    /// Generically represents an XnGine asset folder to the system.
    /// </summary>
    public interface IAssetFolder
    {

        XngineGame GetGame();

        string GetPath();

        bool FolderValid(bool requireVideos = false);

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