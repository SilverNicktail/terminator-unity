// Project:         Daggerfall Unity
// Copyright:       Copyright (C) 2009-2015 Gavin Clayton
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Web Site:        http://www.dfworkshop.net
// Contact:         Gavin Clayton (interkarma@dfworkshop.net)
// Project Page:    https://github.com/Interkarma/daggerfall-unity

using System.IO;
using System.Collections.Generic;
using DaggerfallWorkshop.Utility;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;
using System;

// Irritated non-American note: do you know how many times I've had to go
// back and change "colour" to "color" in my code so it's at least consistent? - Silver

namespace DaggerfallWorkshop
{
    /// <summary>
    /// Editor window to view and export Daggerfall .FNT font files.
    /// </summary>
    public class FontExporterWindow : EditorWindow
    {

        const string windowTitle = "Font Exporter";
        const string menuPath = "Daggerfall Tools/Font Exporter [Beta]";

        DaggerfallUnity dfUnity;
        FntFile fontLoader = new FntFile();

        private static readonly Color defaultBackgroundColor = Color.white;

        private static readonly Color defaultTextColor = Color.black;

        private static readonly FilterMode defaultFilterMode = FilterMode.Bilinear;

        private static readonly int defaultCharacterSpacing = -1;

        private static readonly string defaultOutputFolder = "Assets/Resources/Fonts/";

        private Dictionary<string, string> fontFilepaths;

        #region Form Bindings

        [SerializeField]
        private string selectedFilename = null;

        [SerializeField]
        private Color backgroundColor = defaultBackgroundColor;

        [SerializeField]
        private Color textColor = defaultTextColor;

        [SerializeField]
        private FilterMode filterMode = FilterMode.Bilinear;

        [SerializeField]
        private int characterSpacing = defaultCharacterSpacing;

        [SerializeField]
        private string outputPath = defaultOutputFolder;

        #endregion

        private ScrollView glyphArea = null;


        // TODO: Put this into a menu check?
        // Could disable the menu item if asset folder isn't available,
        // though the notification message in the panel is useful.
        [MenuItem(menuPath)]
        static void Init()
        {
            FontExporterWindow window = (FontExporterWindow)EditorWindow.GetWindow(typeof(FontExporterWindow));
            window.titleContent = new GUIContent(windowTitle);
        }

        #region GUI

        // Could argue that this should be split up among other methods but...
        /// https://issuetracker.unity3d.com/issues/creategui-gets-executed-before-awake-and-onenable-when-opening-a-project-with-a-custom-window-already-open
        /// TODO: Move most of this to XML/CSS
        private void CreateGUI()
        {

            if (!dfUnity)
            {
                dfUnity = DaggerfallUnity.Instance;
            }

            VisualElement root = rootVisualElement;

            if (dfUnity.loadedAssetFolder == null)
            {
                // TODO: Improve layout of this message
                root.Add(new Label(
                    "Game asset folder has not been set or is not valid. " +
                    "Please make sure your asset path is set and correct."
                ));
                return;
            }

            // 2019 version of PopupField can't handle matching custom objects, so we'll
            // feed it filenames and use them as keys in a dictionary for the full path
            // https://discussions.unity.com/t/popupfield-binding-gives-error-field-type-is-not-compatible-with-property/780775
            fontFilepaths = dfUnity.loadedAssetFolder.GetFontFilepaths().ToDictionary(
                path => Path.GetFileName(path),
                path => path
            );

            var filenames = fontFilepaths.Keys.ToList();
            filenames.Sort();
            selectedFilename = filenames[0];

            // TODO: Error if no font files found
            PopupField<string> fontFileField = new PopupField<string>(
                "Font File", filenames, selectedFilename)
            {
                bindingPath = "selectedFilename",
                tooltip = "Select a detected font file from the asset folder."
            };
            fontFileField.RegisterValueChangedCallback(OnFontSelectionChange);
            root.Add(fontFileField);

            ColorField bgColor = new ColorField("Background Color")
            {
                bindingPath = "backgroundColor",
                tooltip = "Background color of generated font."
            };
            bgColor.RegisterValueChangedCallback(OnFontColorChange);
            root.Add(bgColor);

            ColorField textColor = new ColorField("Text Color")
            {
                bindingPath = "textColor",
                tooltip = "Foreground/stroke color of generated font."
            };
            textColor.RegisterValueChangedCallback(OnFontColorChange);
            root.Add(textColor);

            Button resetColorButton = new Button()
            {
                name = "colorResetButton",
                text = "Reset Colors",
                tooltip = "Resets foreground & background colors to defaults."
            };
            resetColorButton.clicked += ResetColors;
            root.Add(resetColorButton);

            EnumField filteringMode = new EnumField("Preview Filtering", filterMode)
            {
                bindingPath = "filterMode",
                tooltip = "Filtering to apply to font preview."
            };
            filteringMode.RegisterValueChangedCallback(OnFilterModeChange);
            root.Add(filteringMode);

            // TODO: Do we need this?
            root.Add(new SliderInt("Character Spacing", -1, 4)
            {
                bindingPath = "characterSpacing",
                tooltip = "Number of pixels between characters in generated font."
            });

            root.Add(new TextField("Output Path")
            {
                bindingPath = "outputPath",
                isReadOnly = true,
                tooltip = "Location where the generated font will be saved."
            });
            Button pathSelector = new Button()
            {
                name = "outputPathButton",
                text = "Select Path",
                tooltip = "Select the location where the generated font will be saved."
            };
            pathSelector.clicked += SelectOutputPath;
            root.Add(pathSelector);

            Button generateButton = new Button()
            {
                name = "generateButton",
                text = "Generate Custom Font",
                tooltip = "Generate the custom font file."
            };
            generateButton.clicked += GenerateFont;
            root.Add(generateButton);

            glyphArea = new ScrollView()
            {
                name = "fontPreviewScroller",
                showHorizontal = true,
                showVertical = true,
                tooltip = "Font Preview"
            };

            // TODO: Move styling to USS when layout goes to XML
            glyphArea.contentContainer.style.flexDirection = FlexDirection.Row;
            glyphArea.contentContainer.style.flexWrap = Wrap.Wrap;
            glyphArea.contentContainer.style.paddingTop = new StyleLength(5);
            glyphArea.contentContainer.style.paddingRight = new StyleLength(5);
            glyphArea.contentContainer.style.paddingBottom = new StyleLength(5);
            glyphArea.contentContainer.style.paddingLeft = new StyleLength(5);
            root.Add(glyphArea);

            root.Bind(new SerializedObject(this));

            // Init values to defaults, they get overwritten to element defaults on bind
            ResetForm();

            LoadFont(selectedFilename);
        }

        #endregion

        #region Event Handlers

        // TODO: Combine and debounce
        private void OnFontSelectionChange(ChangeEvent<string> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            LoadFont(changeEvent.newValue);
        }

        private void OnFontColorChange(ChangeEvent<Color> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            LoadFont();
        }

        private void OnFilterModeChange(ChangeEvent<Enum> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            LoadFont();
        }

        private void OnDestroy()
        {
            Debug.Log("Destroying Font Exporter window");
        }

        private void ResetForm()
        {
            ResetColors();
            filterMode = defaultFilterMode;
            characterSpacing = defaultCharacterSpacing;
            outputPath = defaultOutputFolder;
        }

        private void ResetColors()
        {
            backgroundColor = defaultBackgroundColor;
            textColor = defaultTextColor;
            LoadFont();
        }

        private void SelectOutputPath()
        {
            string selectedPath = EditorUtility.SaveFilePanelInProject(
                "Select Output Location", selectedFilename, "fnt",
                "Please select where you want to save the custom font file"
            );

            if (selectedPath != null && selectedPath.Length > 0)
            {
                outputPath = selectedPath;
            }
        }

        #endregion

        #region Private Methods

        private void LoadFont(string targetFilename = null)
        {
            var filename = targetFilename ?? selectedFilename;

            if (filename == null || filename.Length == 0)
            {
                return;
            }

            // TODO: Show load error
            fontLoader.Load(fontFilepaths[filename], FileUsage.UseMemory, true);
            Texture2D[] previewGlyphs = ImageProcessing.CreateFixedWidthGlyphArray(fontLoader, backgroundColor, textColor);

            DrawFontPreview(previewGlyphs);
        }

        private void DrawFontPreview(Texture2D[] previewGlyphs)
        {
            // Exit if texture not present
            if (previewGlyphs.Length == 0)
            {
                Debug.Log("Font glyphs are not loaded,cannot render preview.");
                return;
            }

            glyphArea.Clear();

            foreach (Texture2D glyph in previewGlyphs)
            {
                Image glyphImage = new Image()
                {
                    image = glyph
                };

                // TODO: Move to stylesheet when layout goes XML, make flexible
                // TODO: Add zoom control like the Project panel has
                glyphImage.style.width = 16;
                glyphImage.style.height = 16;
                glyphArea.Add(glyphImage);

            }

        }

        private void GenerateFont()
        {
            Texture2D fontAtlas;
            if (SaveFontTextureAsset(selectedFilename, out fontAtlas))
            {
                SaveOtherFontAssets(selectedFilename, fontAtlas, out _, out _);
                //ImportFontSettings(fntFile,font, fontRects, CharacterSpacing);
            }
        }

        #endregion

        #region Font Saving

        bool SaveFontTextureAsset(string fontName, out Texture2D fontAtlas)
        {
            string assetPath = Path.Combine(outputPath, fontName);
            string filePath = assetPath + ".png";

            // Get font atlas
            ImageProcessing.CreateFontAtlas(fontLoader, backgroundColor, textColor, out fontAtlas, out _);

            // Save atlas texture
            byte[] fontAtlasPNG = fontAtlas.EncodeToPNG();
            File.WriteAllBytes(filePath, fontAtlasPNG);

            // Loading back asset to modify importer properties
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            fontAtlas = Resources.Load<Texture2D>(fontName);
            string assetTexturePath = AssetDatabase.GetAssetPath(fontAtlas);

            // Modify asset importer properties
            TextureImporter importer = AssetImporter.GetAtPath(assetTexturePath) as TextureImporter;
            if (importer == null)
            {
                DaggerfallUnity.LogMessage("FontGeneratorWindow: Failed to get TextureImporter. Ensure your target folder is called 'Resources'.", true);
                return false;
            }
            importer.textureType = TextureImporterType.Default;
            importer.maxTextureSize = 256;
            importer.mipmapEnabled = false;
            importer.isReadable = false;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.filterMode = filterMode;

            // Reimport asset with new importer settings
            AssetDatabase.ImportAsset(assetTexturePath, ImportAssetOptions.ForceUpdate);

            // Finish up
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return true;
        }

        void SaveOtherFontAssets(string fontName, Texture2D fontAtlas, out Font fontOut, out Material materialOut)
        {
            string assetPath = Path.Combine(outputPath, fontName);
            string fontPath = assetPath + ".fontsettings";
            string materialPath = assetPath + ".mat";

            fontOut = new Font();
            AssetDatabase.CreateAsset(fontOut, fontPath);

            materialOut = new Material(Shader.Find("Unlit/Transparent"));
            materialOut.SetTexture("_MainTex", fontAtlas);
            AssetDatabase.CreateAsset(materialOut, materialPath);
            fontOut.material = materialOut;
        }

        #endregion

        #region Incomplete Code from Daggerfall Unity

        //public void ImportFontSettings(FntFile fntFile, Font font, Rect[] fontRects, int spacing)
        //{
        //    const int asciiOffset = 32;

        //    List<CharacterInfo> infoList = new List<CharacterInfo>();

        //    // Add missing space character
        //    CharacterInfo space = new CharacterInfo();
        //    space.width = fntFile.FixedWidth / 2;
        //    infoList.Add(space);

        //    // Add Daggerfall characters
        //    for (int i = 0; i < FntFile.MaxGlyphCount; i++)
        //    {
        //        int width = fntFile.GetGlyphWidth(i);
        //        int height = fntFile.FixedHeight;

        //        CharacterInfo info = new CharacterInfo();
        //        info.uv = fontRects[i];
        //        info.vert.x = 0;
        //        info.vert.y = 0;
        //        info.vert.width = width;
        //        info.vert.height = -height;
        //        info.width = width + spacing;
        //        info.index = infoList.Count;
        //        infoList.Add(info);
        //    }

        //    this.SetAsciiStartOffset(font, asciiOffset);
        //    font.characterInfo = infoList.ToArray();
        //}

        // private void SetAsciiStartOffset(Font font, int asciiStartOffset)
        // {
        //     Editor editor = Editor.CreateEditor(font);

        //     SerializedProperty startOffsetProperty = editor.serializedObject.FindProperty("m_AsciiStartOffset");
        //     startOffsetProperty.intValue = asciiStartOffset;

        //     editor.serializedObject.ApplyModifiedProperties();
        // }

        #endregion

    }
}