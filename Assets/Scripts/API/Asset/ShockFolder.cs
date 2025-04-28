// Project:         Terminator Unity
// Original Author: Silver Nicktail <silver@nicktail.com>

#region Using Statements
using System.IO;
using Bethesda;
using UnityEngine;
#endregion

namespace TerminatorUnity.Asset
{
    /// <summary>
    /// Static methods to validate GAMEDATA folder of a Future Shock disc folder.
    /// Does not verify contents, just that critical files exist in minimum quantities.
    /// This allows test to be fast enough to be run at startup.
    /// </summary>
    public class ShockFolder: IAssetFolder
    {
        #region Fields

        private const string textureSearchPattern = "TEXTURE.???";

        private const string vidSearchPattern = "*.VID";

        private const string fontSearchPattern = "FONT????.FNT";

        private const string heightMapSearchPattern = "WLD.???";

        private const string midiSearchPattern = "*.HMI";

        private const string briefingArchive = "MDMDBRIF.BSA";

        private const string enemyArchive = "MDMDENMS.BSA";

        private const string imageArchive = "MDMDIMGS.BSA";

        // Equivalent of ARCH3D.BSA - parameterise
        private const string levelObjectArchive = "MDMDOBJS.BSA";

        // Equivalent of DAGGER.SND
        private const string sfxArchive = "MDMDSFXS.BSA";

        // Equivalent of MAPS.BSA
        private const string mapsArchive = "MDMDMAPS.BSA";
        
        private const int minTextureCount = 213;

        private const int minVidCount = 4;

        private const int minFontCount = 8;

        private const int minHeightMapCount = 16;

        private const int minMidiCount = 16;

        private readonly string path;

        private bool hasTextures = false;

        private bool hasFonts = false;

        private bool hasModels = false;

        private bool hasMaps = false;

        private bool hasMusic = false;

        private bool hasSounds = false;

        private bool hasHeightMaps = false;

        private bool hasVideos = false;

        #endregion

        public ShockFolder(string path) {
            this.path = path;
        }

        public XngineGame GetGame() {
            return XngineGame.T_FUTURE_SHOCK;
        }

        public string GetPath() {
            return this.path;
        }

        /// <summary>
        /// Validates a Future Shock data folder (usually SHOCK/GAMEDATA).
        ///  This currently just checks the right major files exist in the right quantities.
        ///  Does not verify contents so test is quite speedy and can be performed at startup.
        ///  Will also look for main .BSA files in Unity Resources folder.
        /// </summary>
        public bool FolderValid() {

            // Check folder exists
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) {
                return false;
            }

            // Get files
            string[] textures = Directory.GetFiles(path, textureSearchPattern);
            string[] fonts = Directory.GetFiles(path, fontSearchPattern);
            string[] music = Directory.GetFiles(path, midiSearchPattern);
            string[] models = Directory.GetFiles(path, levelObjectArchive);
            string[] maps = Directory.GetFiles(path, mapsArchive);
            string[] sounds = Directory.GetFiles(path, sfxArchive);
            string[] heightMaps = Directory.GetFiles(path, heightMapSearchPattern);
            string[] videos = Directory.GetFiles(path, vidSearchPattern);

            // Validate texture count
            this.hasTextures = textures.Length >= minTextureCount;
            this.hasFonts = fonts.Length >= minFontCount;
            this.hasMusic = music.Length >= minMidiCount;
            this.hasModels = models.Length == 1;
            this.hasMaps = maps.Length == 1;
            this.hasSounds = sounds.Length == 1;
            this.hasHeightMaps = heightMaps.Length >= minHeightMapCount;
            this.hasVideos = videos.Length >= minVidCount;

            Debug.Log($"Textures found? {this.hasTextures}");
            Debug.Log($"Fonts found? {this.hasFonts}");
            Debug.Log($"Music found? {this.hasMusic}");
            Debug.Log($"Models found? {this.hasModels}");
            Debug.Log($"Maps found? {this.hasMaps}");
            Debug.Log($"SFX found? {this.hasSounds}");
            Debug.Log($"Height maps found? {this.hasHeightMaps}");
            Debug.Log($"Videos found? {this.hasVideos}");

            return 
                this.hasTextures &&
                this.hasFonts &&
                this.hasMusic &&
                this.hasModels &&
                this.hasMaps &&
                this.hasSounds &&
                this.hasHeightMaps &&
                this.hasVideos;
        }

    }
}
