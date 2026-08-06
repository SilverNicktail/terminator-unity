using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XnGine;

namespace TerminatorUnity.Game.Asset
{
    
    public class AssetManager : MonoBehaviour
    {
        
        public static AssetManager Instance { get; private set; }

        public IAssetFolder AssetFolder { get { return assetFolder; } }

        private IAssetFolder assetFolder;

        private void Awake()
        {
            
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;

        }

        private void Start()
        {
            string[] potentialPaths = PotentialAssetPaths();
            this.assetFolder = AssetFolderFactory.LocateAssetFolder(potentialPaths);

                if (assetFolder == null)
                {
                    Debug.Log("Could not find game asset folder.");
                }
                else
                {
                    Debug.Log($"Asset folder was found at {assetFolder.GetRootPath()} but is not a valid/supported game folder");
                }

        }

        private string[] PotentialAssetPaths()
        {
            List<string> pathsToSearch = new List<string>();
            
            XnGineSettings projectSettings = XnGineSettings.GetProjectSettings();
            if (projectSettings.assetFolderPath != null)
            {
                pathsToSearch.Add(projectSettings.assetFolderPath);
            }

            // TODO: Add read of game settings file
            // Cheat for now

            if (Application.isPlaying) {
                pathsToSearch.Add(Application.dataPath);
                pathsToSearch.Add(Path.Combine(Application.streamingAssetsPath, "GameFiles"));
            }

            return pathsToSearch.ToArray();

        }

    }

}