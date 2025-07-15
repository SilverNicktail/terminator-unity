// Project:         Terminator Unity
// Original Author: Silver Nicktail <silver@nicktail.com>
// I coded this file around a cat while she headbutted me in the nose.
// Any bugs present were caused by her tail.

#region Using Statements
using Bethesda;
using System.IO;
using System.Linq;
using UnityEngine;
#endregion

namespace TerminatorUnity.Asset
{

    /// <summary>
    /// Represents the Daggerfall asset folder to the rest of the program.
    /// </summary>
    public class DaggerfallFolder : IAssetFolder
    {

        #region Filename Constants

        private const string fontSearchPattern = "FONT????.FNT";

        private const string textureSearchPattern = "TEXTURE.???";

        private const string vidSearchPattern = "*.VID";

        private const string vidAlternateTestFile = "ANIM0011.VID";

        private const string enemyArchive = "MONSTER.BSA";

        private const string mapBlockArchive = "BLOCKS.BSA";

        private const string mapsArchive = "MAPS.BSA";

        private const string modelArchive = "ARCH3D.BSA";

        private const string sfxArchive = "DAGGER.SND";

        private const string woodsArchive = "WOODS.WLD";

        #endregion

        #region Minimums

        private const int minFontCount = 4;

        private const int minTextureCount = 472;

        private const int minVidCount = 17;

        #endregion

        #region Detected

        private string path;

        private string[] fontFiles = { };

        private string[] textureFiles = { };

        private string[] videoFiles = { };

        private bool hasEnemies = false;

        private bool hasMapBlocks = false;

        private bool hasMaps = false;

        private bool hasModels = false;

        private bool hasSfx = false;

        private bool hasWoods = false;

        private bool hasAltBlocks = false;

        private bool hasAltMaps = false;

        private bool hasAltModels = false;

        private bool hasAltSfx = false;

        private bool hasAltVids = false;

        private bool hasAltWoods = false;

        #endregion

        #region Logic

        public DaggerfallFolder(string path)
        {
            this.path = path;
        }

        public bool FolderValid(bool requireVideos = false)
        {
            // Check folder exists
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return false;
            }

            // Check for files
            this.fontFiles = Directory.GetFiles(path, fontSearchPattern);
            this.textureFiles = Directory.GetFiles(path, textureSearchPattern);
            this.videoFiles = Directory.GetFiles(path, vidSearchPattern);

            // Check for archives
            this.hasEnemies = Directory.GetFiles(path, enemyArchive).Length == 1;
            this.hasMapBlocks = Directory.GetFiles(path, mapBlockArchive).Length == 1;
            this.hasMaps = Directory.GetFiles(path, mapsArchive).Length == 1;
            this.hasModels = Directory.GetFiles(path, modelArchive).Length == 1;
            this.hasSfx = Directory.GetFiles(path, sfxArchive).Length == 1;
            this.hasWoods = Directory.GetFiles(path, woodsArchive).Length == 1;

            // For supported assets, check Unity Resources for alternative files.
            this.hasAltBlocks = UnityEngine.Resources.Load(mapBlockArchive) != null;
            this.hasAltMaps = UnityEngine.Resources.Load(mapsArchive) != null;
            this.hasAltModels = UnityEngine.Resources.Load(modelArchive) != null;
            this.hasAltSfx = UnityEngine.Resources.Load(sfxArchive) != null;
            this.hasAltVids = UnityEngine.Resources.Load(vidAlternateTestFile) != null;

            return
                this.fontFiles.Length >= minFontCount &&
                this.textureFiles.Length >= minTextureCount &&
                (!requireVideos || this.videoFiles.Length >= minVidCount || this.hasAltVids) &&
                (this.hasMapBlocks || this.hasAltBlocks) &&
                (this.hasMaps || this.hasAltMaps) &&
                (this.hasModels || this.hasAltModels) &&
                (this.hasSfx || this.hasAltSfx) &&
                (this.hasWoods || this.hasAltWoods);

        }

        #endregion

        #region Accessors

        public XngineGame GetGame()
        {
            return XngineGame.ES_DAGGERFALL;
        }

        public string GetPath()
        {
            return path;
        }

        // TODO: Encapsulate file loading/paths?
        // Having other items need to load the actual files is breaking
        // encapsulation so maybe the folder class should just hand back the 
        // actual files on request?

        public string[] GetFontFilepaths()
        {
            return this.fontFiles;
        }

        public string[] GetTextureFilepaths()
        {
            return this.textureFiles;
        }

        public string[] GetVideoFilepaths()
        {
            return this.videoFiles;
        }

        public string GetBriefingArchivePath()
        {
            return null; // DF doesn't have one
        }

        public string GetEnemyArchivePath()
        {
            return hasEnemies ? Path.Combine(path, enemyArchive) : null;
        }

        public string GetImageArchivePath()
        {
            return null; // DF doesn't have an image archive
        }

        public string GetMapArchivePath()
        {
            return hasMaps ? Path.Combine(path, mapsArchive) : null;
        }

        public string GetMapBlockArchivePath()
        {
            return hasMapBlocks ? Path.Combine(path, mapBlockArchive) : null;
        }

        public string GetModelsArchivePath()
        {
            return hasModels ? Path.Combine(path, modelArchive) : null;
        }

        public string GetMusicArchivePath()
        {
            return null; // DF doesn't have a single music archive
        }

        public string GetSFXArchivePath()
        {
            return hasSfx ? Path.Combine(path, sfxArchive) : null;
        }

        public string GetWoodsArchivePath()
        {
            return hasWoods ? Path.Combine(path, woodsArchive) : null;
        }

        #endregion

    }
}