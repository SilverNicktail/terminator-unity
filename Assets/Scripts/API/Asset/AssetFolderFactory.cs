using System.Collections.Generic;
using System.IO;
using Bethesda;
using UnityEngine;

namespace TerminatorUnity.Asset {

    static class AssetFolderFactory {

        // List of asset folder names for each game
        private static readonly Dictionary<XngineGame, string> folderNames =
            new Dictionary<XngineGame, string> {
                {XngineGame.ES_DAGGERFALL, "Arena2"},
                {XngineGame.T_FUTURE_SHOCK, "GAMEDATA"}
            };

        // Quick list of some asset folder contents for each game,
        // in case the user selects the asset folder directly. If all of these
        // files are present, the folder will be detected as being for that game.
        // (Full validation will be done by the IAssetFolder implementation.)
        private static readonly Dictionary<XngineGame, List<string>> folderContents =
            new Dictionary<XngineGame, List<string>> {
                {XngineGame.ES_DAGGERFALL, new List<string> {"ARCH3D.BSA", "DAGGER.SND"}},
                {XngineGame.T_FUTURE_SHOCK, new List<string> {"MDMDBRIF.BSA", "SHOCK.COL"}}
            };

        public static IAssetFolder LocateAssetFolder(string[] potentialPaths) {

            foreach (string path in potentialPaths) {
                if (path == null || path.Length == 0) {
                    continue;
                }

                IAssetFolder attemptPath = DetectAssetFolderType(path);
                if (attemptPath != null) {
                    return attemptPath;
                }
            }
            
            return null;
        }


        // Does basic checks on folder contents to recognise which game they belong to
        // More advanced checks will be done by the asset folder implementation
        public static IAssetFolder DetectAssetFolderType(string path) {

            if (path == null || path.Trim().Length == 0)
            {
                Debug.Log("Empty path was handed to folder detection, skipping.");
                return null;
            }
            
            // Case sensitivity depends on target filesystem
            // Insensitive on Windows, sensitive on Linux, etc
            // C# doesn't provide a way to check case-insensitively that I know of,
            // so we have to get a little clunky

            XngineGame detectedGame = XngineGame.ES_DAGGERFALL;
            string detectedPath = null;

            Debug.Log($"Checking asset folder at {path}");

            // User has selected parent folder
            foreach (KeyValuePair<XngineGame, string> folder in folderNames) {

                string pathRegular = Path.Combine(path, folder.Value);
                string pathLower = Path.Combine(path, folder.Value.ToLower());
                string pathUpper = Path.Combine(path, folder.Value.ToUpper());
                
                if (Directory.Exists(pathRegular)) {
                    detectedPath = pathRegular;
                } else if (Directory.Exists(pathLower)) {
                    detectedPath = pathLower;
                } else if (Directory.Exists(pathUpper)) {
                    detectedPath = pathUpper;
                }

                if (detectedPath != null) {
                    detectedGame = folder.Key;
                    break;
                }

            }

            if (detectedPath == null) {

                // User has selected asset folder directly
                foreach (KeyValuePair<XngineGame, List<string>> contents in folderContents) {
                    bool allPresent = true;

                    foreach (string targetFile in contents.Value) {
                        string fullPath = Path.Combine(path, targetFile);
                        allPresent = allPresent && Directory.Exists(fullPath);
                    }

                    if (allPresent) {
                        detectedPath = path;
                        detectedGame = contents.Key;
                    }
                }

            }

            if (detectedPath != null) {
                Debug.Log($"Detected asset folder for {detectedGame} at {detectedPath}");
                return LoadAssetFolder(detectedGame, detectedPath);
            } else {
                Debug.Log($"Could not find valid asset folder at or within {detectedPath}");
                return null;
            }

        }

        private static IAssetFolder LoadAssetFolder(XngineGame gameType, string path) {

            switch (gameType) {
                case XngineGame.ES_DAGGERFALL:
                    return new DaggerfallFolder(path);

                case XngineGame.T_FUTURE_SHOCK:
                    return new ShockFolder(path);

                default:
                    return null;
            }

        }

    }

}

