namespace TerminatorUnity.Validation
{

    /// <summary>
    /// Generically represents an XnGine asset folder to the system.
    /// </summary>
    interface IAssetFolder
    {

        string GetPath();

        bool FolderValid();

    }

}