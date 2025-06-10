// Project:         Terminator Unity
// Original Author: Silver Nicktail <silver@nicktail.com>

#region Using Statements
using System.IO;
using Bethesda;
#endregion

namespace TerminatorUnity.Asset
{
    /// <summary>
    /// Static methods to validate GAMEDATA folder of a Future Shock disc folder.
    /// Does not verify contents, just that critical files exist in minimum quantities.
    /// This allows test to be fast enough to be run at startup.
    /// </summary>
    public class ShockFolder : IAssetFolder
    {
        #region Filename Constants

        private const string textureSearchPattern = "TEXTURE.???";

        private const string vidSearchPattern = "*.VID";

        private const string fontSearchPattern = "FONT????.FNT";

        private const string heightMapSearchPattern = "WLD.???";

        private const string midiSearchPattern = "*.HMI";

        // In Terminator, no Daggerfall equivalent
        private const string briefingArchive = "MDMDBRIF.BSA";

        // Equivalent of MONSTER.BSA
        private const string enemyArchive = "MDMDENMS.BSA";

        // In Terminator, no Daggerfall equivalent?
        private const string imageArchive = "MDMDIMGS.BSA";

        // Equivalent of MAPS.BSA
        private const string mapsArchive = "MDMDMAPS.BSA";

        // Equivalent of ARCH3D.BSA - parameterise
        private const string modelArchive = "MDMDOBJS.BSA";

        private const string musicArchive = "MDMDMUSC.BSA";

        // Equivalent of DAGGER.SND
        private const string sfxArchive = "MDMDSFXS.BSA";

        #endregion

        #region Minimums

        private const int minTextureCount = 213;

        private const int minVidCount = 4;

        private const int minFontCount = 8;

        private const int minHeightMapCount = 16;

        private const int minMidiCount = 16;

        #endregion

        #region Detected

        private readonly string path;

        private bool hasBriefings = false;

        private bool hasEnemies = false;

        private bool hasImages = false;

        private bool hasModels = false;

        private bool hasMaps = false;

        private bool hasMusicArchive = false;

        private bool hasSounds = false;

        private string[] fontFiles = { };

        private string[] heightMapFiles = { };

        private string[] musicFiles = { };

        private string[] textureFiles = { };

        private string[] videoFiles = { };

        #endregion

        #region Logic

        public ShockFolder(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Validates a Future Shock data folder (usually SHOCK/GAMEDATA).
        ///  This currently just checks the right major files exist in the right quantities.
        ///  Does not verify contents so test is quite speedy and can be performed at startup.
        ///  Will also look for main .BSA files in Unity Resources folder.
        /// </summary>
        public bool FolderValid(bool requireVideos = false)
        {

            // Check folder exists
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return false;
            }

            // Check for files
            this.textureFiles = Directory.GetFiles(path, textureSearchPattern);
            this.fontFiles = Directory.GetFiles(path, fontSearchPattern);
            this.heightMapFiles = Directory.GetFiles(path, heightMapSearchPattern);
            this.musicFiles = Directory.GetFiles(path, midiSearchPattern);
            this.videoFiles = Directory.GetFiles(path, vidSearchPattern);

            // Check for BSAs
            this.hasBriefings = Directory.GetFiles(path, briefingArchive).Length == 1;
            this.hasEnemies = Directory.GetFiles(path, enemyArchive).Length == 1;
            this.hasImages = Directory.GetFiles(path, imageArchive).Length == 1;
            this.hasMusicArchive = Directory.GetFiles(path, musicArchive).Length == 1;
            this.hasModels = Directory.GetFiles(path, modelArchive).Length == 1;
            this.hasMaps = Directory.GetFiles(path, mapsArchive).Length == 1;
            this.hasSounds = Directory.GetFiles(path, sfxArchive).Length == 1;

            return
                textureFiles.Length >= minTextureCount &&
                fontFiles.Length >= minFontCount &&
                musicFiles.Length >= minMidiCount &&
                heightMapFiles.Length >= minHeightMapCount &&
                (!requireVideos || videoFiles.Length >= minVidCount) &&
                this.hasMusicArchive &&
                this.hasModels &&
                this.hasMaps &&
                this.hasSounds;
        }

        #endregion

        #region Accessors

        public XngineGame GetGame()
        {
            return XngineGame.T_FUTURE_SHOCK;
        }

        public string GetPath()
        {
            return this.path;
        }

        public string[] GetTextureFilepaths()
        {
            return this.textureFiles;
        }

        public string[] GetFontFilepaths()
        {
            return this.fontFiles;
        }

        public string[] GetHeightMapFilepaths()
        {
            return this.heightMapFiles;
        }

        public string[] GetMusicFilepaths()
        {
            return this.musicFiles;
        }

        public string[] GetVideoFilepaths()
        {
            return this.videoFiles;
        }

        public string GetBriefingArchivePath()
        {
            return this.hasBriefings ? Path.Combine(this.path, briefingArchive) : null;
        }

        public string GetEnemyArchivePath()
        {
            return this.hasEnemies ? Path.Combine(this.path, enemyArchive) : null;
        }

        public string GetImageArchivePath()
        {
            return this.hasImages ? Path.Combine(this.path, imageArchive) : null;
        }

        public string GetMusicArchivePath()
        {
            return this.hasMusicArchive ? Path.Combine(this.path, musicArchive) : null;
        }

        public string GetModelsArchivePath()
        {
            return this.hasModels ? Path.Combine(this.path, modelArchive) : null;
        }

        public string GetMapArchivePath()
        {
            return this.hasMaps ? Path.Combine(this.path, mapsArchive) : null;
        }

        public string GetMapBlockArchivePath()
        {
            return null; // FS doesn't have map blocks, AFAIK
        }

        public string GetSFXArchivePath()
        {
            return this.hasSounds ? Path.Combine(this.path, sfxArchive) : null;
        }

        public string GetWoodsArchivePath()
        {
            return null;
        }

        #endregion

    }
}
