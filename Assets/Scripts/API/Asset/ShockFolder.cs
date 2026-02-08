// Project:         Terminator Unity
// Original Author: Silver Nicktail <silver@nicktail.com>

#region Using Statements
using System;
using System.IO;
using System.Linq;
using XnGine;
#endregion

namespace TerminatorUnity.Asset
{
    public class ShockFolder : IAssetFolder
    {
        #region Filename Constants

        private const string fontSearchPattern = "FONT????.FNT";

        private const string heightMapSearchPattern = "WLD.???";

        private const string midiSearchPattern = "*.HMI";

        private const string paletteSearchPattern = "*.COL";

        private const string textureSearchPattern = "TEXTURE.???";

        private const string vidSearchPattern = "*.VID";


        // Equivalent of MONSTER.BSA
        private const string enemyArchive = "MDMDENMS.BSA";

        // In Terminator, no Daggerfall equivalent?
        private const string imageArchive = "MDMDIMGS.BSA";

        // Equivalent of MAPS.BSA
        private const string mapsArchive = "MDMDMAPS.BSA";

        // In Terminator, no Daggerfall equivalent
        private const string missionTextArchive = "MDMDBRIF.BSA";

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

        private bool hasEnemies = false;

        private bool hasImages = false;

        private bool hasMissionArchive = false;

        private bool hasModels = false;

        private bool hasMaps = false;

        private bool hasMusicArchive = false;

        private bool hasSounds = false;

        private string[] fontFiles = { };

        private string[] heightMapFiles = { };

        private string[] musicFiles = { };

        private string[] paletteFiles = {};

        private string[] textureFiles = { };

        private string[] videoFiles = { };

        private static readonly AssetType[] availableTypes = new AssetType[]
        {
            AssetType.COLOR_PALETTE,
            AssetType.ENEMY_MODEL_ARCHIVE,
            AssetType.FONT,
            AssetType.HEIGHT_MAP,
            AssetType.IMAGE_ARCHIVE,
            AssetType.MAP_ARCHIVE,
            AssetType.MISSION_ARCHIVE,
            AssetType.MODEL_ARCHIVE,
            AssetType.MUSIC,
            AssetType.MUSIC_ARCHIVE,
            AssetType.SFX_ARCHIVE,
            AssetType.TEXTURE,
            AssetType.VIDEO
        };

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
            this.fontFiles = Directory.GetFiles(path, fontSearchPattern);
            this.heightMapFiles = Directory.GetFiles(path, heightMapSearchPattern);
            this.musicFiles = Directory.GetFiles(path, midiSearchPattern);
            this.paletteFiles = Directory.GetFiles(path, paletteSearchPattern);
            this.textureFiles = Directory.GetFiles(path, textureSearchPattern);
            this.videoFiles = Directory.GetFiles(path, vidSearchPattern);

            // Check for BSAs
            this.hasMissionArchive = Directory.GetFiles(path, missionTextArchive).Length == 1;
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

        public string GetRootPath()
        {
            return this.path;
        }

        public bool ProvidesAssetType(AssetType type)
        {
            return availableTypes.Contains(type);
        }

        public string[] GetAssetPaths(AssetType type)
        {
            if (! availableTypes.Contains(type))
            {
                throw new NotSupportedException("Future Shock does not contain that asset type, or you passed an archive type.");
            }

            switch (type)
            {
                case AssetType.COLOR_PALETTE:
                    return paletteFiles;
                case AssetType.FONT:
                    return fontFiles;
                case AssetType.HEIGHT_MAP:
                    return heightMapFiles;
                case AssetType.MUSIC:
                    return musicFiles;
                case AssetType.TEXTURE:
                    return textureFiles;
                case AssetType.VIDEO:
                    return videoFiles;
                default:
                    return null;
            }
        }

        public string GetArchivePath(AssetType assetType)
        {
            string archiveName = null;

            if (assetType == AssetType.ENEMY_MODEL_ARCHIVE && hasEnemies)
            {
                archiveName = enemyArchive;
            }
            else if (assetType == AssetType.IMAGE_ARCHIVE && hasImages)
            {
                archiveName = imageArchive;
            }
            else if (assetType == AssetType.MAP_ARCHIVE & hasMaps)
            {
                archiveName = mapsArchive;
            }
            else if (assetType == AssetType.MISSION_ARCHIVE && hasMissionArchive)
            {
                archiveName = missionTextArchive;
            }
            else if (assetType == AssetType.MODEL_ARCHIVE && hasModels)
            {
                archiveName = modelArchive;
            }
            else if (assetType == AssetType.MUSIC_ARCHIVE && hasMusicArchive)
            {
                archiveName = musicArchive;
            }
            else if (assetType == AssetType.SFX_ARCHIVE && hasSounds)
            {
                archiveName = sfxArchive;
            }

            return (archiveName != null) ? Path.Combine(path, archiveName) : null;
        }

        #endregion

    }
}
