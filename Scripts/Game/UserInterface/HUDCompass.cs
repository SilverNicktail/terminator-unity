﻿// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2015 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

using UnityEngine;

namespace DaggerfallWorkshop.Game.UserInterface
{
    /// <summary>
    /// Compass for HUD.
    /// </summary>
    public class HUDCompass : BaseScreenComponent
    {
        const string compassFilename = "COMPASS.IMG";
        const string compassBoxFilename = "COMPBOX.IMG";

        public float Scale = 2.0f;

        Camera mainCamera;
        Texture2D compassTexture;
        Texture2D compassBoxTexture;
        bool assetsLoaded = false;

        public HUDCompass()
            : base()
        {
            mainCamera = Camera.main;
            HorizontalAlignment = HorizontalAlignment.Right;
            VerticalAlignment = VerticalAlignment.Bottom;
        }

        public override void Update()
        {
            if (Enabled)
            {
                if (!assetsLoaded)
                    LoadAssets();

                base.Update();
            }
        }

        public override void Draw()
        {
            if (Enabled)
            {
                base.Draw();
                DrawCompass();
            }
        }

        void LoadAssets()
        {
            compassTexture = DaggerfallUI.GetTextureFromImg(compassFilename);
            compassBoxTexture = DaggerfallUI.GetTextureFromImg(compassBoxFilename);
            assetsLoaded = true;
        }

        void DrawCompass()
        {
            const int boxOutlineSize = 2;       // Pixel width of box outline
            const int boxInterior = 64;         // Pixel width of box interior
            const int nonWrappedPart = 258;     // Pixel width of non-wrapped part of compass strip

            // Calculate displacement
            float percent = mainCamera.transform.eulerAngles.y / 360f;
            int scroll = (int)((float)nonWrappedPart * percent);

            // Compass box rect
            Rect compassBoxRect = new Rect();
            compassBoxRect.x = Screen.width - (compassBoxTexture.width * Scale);
            compassBoxRect.y = Screen.height - (compassBoxTexture.height * Scale);
            compassBoxRect.width = compassBoxTexture.width * Scale;
            compassBoxRect.height = compassBoxTexture.height * Scale;

            // Compass strip source
            Rect compassSrcRect = new Rect();
            compassSrcRect.xMin = scroll / (float)compassTexture.width;
            compassSrcRect.yMin = 0;
            compassSrcRect.xMax = compassSrcRect.xMin + (float)boxInterior / (float)compassTexture.width;
            compassSrcRect.yMax = 1;

            // Compass strip destination
            Rect compassDstRect = new Rect();
            compassDstRect.x = compassBoxRect.x + boxOutlineSize * Scale;
            compassDstRect.y = compassBoxRect.y + boxOutlineSize * Scale;
            compassDstRect.width = compassBoxRect.width - (boxOutlineSize * 2) * Scale;
            compassDstRect.height = compassTexture.height * Scale;

            GUI.DrawTextureWithTexCoords(compassDstRect, compassTexture, compassSrcRect, false);
            GUI.DrawTexture(compassBoxRect, compassBoxTexture, ScaleMode.StretchToFill, true);
        }
    }
}