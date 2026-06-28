// Project:         Daggerfall Unity
// Copyright:       Copyright (C) 2009-2023 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

using UnityEngine;
using UnityEngine.SceneManagement;
using XnGine;

namespace DaggerfallWorkshop.Game.Utility
{
    /// <summary>
    /// Launches game or startup scene based on path validation.
    /// </summary>
    public class SceneControl : MonoBehaviour
    {

        public const string GAME_SCENE_FS = "FutureShockGame";

        public const string GAME_SCENE_DFU = "DaggerfallUnity";

        public const int StartupSceneIndex = 0;
        public const int GameSceneIndex = 1;
        public GameObject defaultSky = null;

        void Start()
        {
            // Resolution
            if (DaggerfallUnity.Settings.ExclusiveFullscreen && DaggerfallUnity.Settings.Fullscreen)
            {
                Screen.SetResolution(
                    DaggerfallUnity.Settings.ResolutionWidth,
                    DaggerfallUnity.Settings.ResolutionHeight,
                    FullScreenMode.ExclusiveFullScreen);
            }
            else
            {
                Screen.SetResolution(
                    DaggerfallUnity.Settings.ResolutionWidth,
                    DaggerfallUnity.Settings.ResolutionHeight,
                    DaggerfallUnity.Settings.Fullscreen);
            }

            // Check asset folder is validated OK, otherwise start game setup
            if (! DaggerfallUnity.Instance.IsPathValidated || DaggerfallUnity.Settings.ShowOptionsAtStart || Input.anyKey)
            {
                // Enable sky for test models
                if (defaultSky != null)
                    defaultSky.SetActive(true);

                // Post message to launch game setup
                DaggerfallUI.PostMessage(DaggerfallUIMessages.dfuiSetupGameWizard);
            }
            else
            {
                LoadGameScene(DaggerfallUnity.Instance.loadedAssetFolder.GetGame());
            }
        }

        public static bool StartupSceneLoaded()
        {
            return SceneManager.GetActiveScene().buildIndex == 0;
        }


        public static void LoadGameScene()
        {
            if (DaggerfallUnity.Instance.IsPathValidated)
            {
                LoadGameScene(DaggerfallUnity.Instance.loadedAssetFolder.GetGame());
            }
        }

        public static void LoadGameScene(XngineGame game)
        {
            if (game == XngineGame.ES_DAGGERFALL)
            {
                SceneManager.LoadScene(GAME_SCENE_DFU);
            } 
            else if (game == XngineGame.T_FUTURE_SHOCK)
            {
                SceneManager.LoadScene(GAME_SCENE_FS);
            }
        }

    }
}
