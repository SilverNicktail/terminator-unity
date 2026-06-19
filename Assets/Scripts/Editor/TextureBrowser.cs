using System.IO;
using System.Collections.Generic;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;
using System;
using XnGine;
using DaggerfallWorkshop.Utility;
using DaggerfallConnect.Utility;

namespace TerminatorUnity.Editor
{
    /// <summary>
    /// Editor window to view the contents of texture files.
    /// </summary>
    public class TextureBrowser : EditorWindow
    {

        const string windowTitle = "Texture Browser";
        const string menuPath = "XnGine/Texture Browser";

        DaggerfallUnity dfUnity;

        TextureFile textureLoader = new TextureFile();

        private static readonly Color defaultBackgroundColor = Color.white;

        private static readonly FilterMode defaultFilterMode = FilterMode.Bilinear;

        private Dictionary<string, string> textureFilepaths;

        private Dictionary<string, string> paletteFilepaths;

        #region Form Bindings

        [SerializeField]
        private string selectedFilename = null;

        [SerializeField]
        private string selectedPalette;

        [SerializeField]
        private Color backgroundColor = defaultBackgroundColor;

        [SerializeField]
        private FilterMode filterMode = FilterMode.Bilinear;

        [SerializeField]
        private int scale = 2;

        #endregion

        private ScrollView textureArea = null;


        // TODO: Put this into a menu check?
        // Could disable the menu item if asset folder isn't available,
        // though the notification message in the panel is useful.
        [MenuItem(menuPath)]
        static void Init()
        {
            TextureBrowser window = (TextureBrowser)EditorWindow.GetWindow(typeof(TextureBrowser));
            window.titleContent = new GUIContent(windowTitle);
        }

        #region GUI

        // Could argue that this should be split up among other methods but...
        /// https://issuetracker.unity3d.com/issues/creategui-gets-executed-before-awake-and-onenable-when-opening-a-project-with-a-custom-window-already-open
        private void CreateGUI()
        {

            if (!dfUnity)
            {
                dfUnity = DaggerfallUnity.Instance;
            }

            if (dfUnity.loadedAssetFolder != null)
            {
                textureLoader.activeGame = dfUnity.loadedAssetFolder.GetGame();
            }

            // I normally despise generic exception handling, but it's necessary in createGUI().
            // Need to catch all unhandled exceptions in here, or the GUI will fail to initialise and 
            // end up in an invalid state we might not be able to recover from. Had to comment the 
            // method body out and delete my editor's UI layout files!
            try {
                VisualElement root = rootVisualElement;
                VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UXML/TextureBrowser.uxml");
                StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/USS/TextureBrowser.uss");
                VisualElement ui = uiAsset.CloneTree();

                root.name = "textureBrowser";
                root.styleSheets.Add(styleSheet);
                root.Add(ui);

                
                if (dfUnity.loadedAssetFolder == null)
                {
                    root.Q<Label>("no-asset-folder").style.display = DisplayStyle.Flex;
                    root.Q<Box>("texture-form").style.display = DisplayStyle.None;
                    return;
                }

                string[] invalidFiles = TextureFile.unsupportedFilenames[dfUnity.loadedAssetFolder.GetGame()];

                // 2019 version of PopupField can't handle matching custom objects, so we'll
                // feed it filenames and use them as keys in a dictionary for the full path
                // https://discussions.unity.com/t/popupfield-binding-gives-error-field-type-is-not-compatible-with-property/780775
                textureFilepaths = dfUnity.loadedAssetFolder
                    .GetAssetPaths(AssetType.TEXTURE)
                    .Where(path => !invalidFiles.Contains(Path.GetFileName(path)))
                    .ToDictionary(
                        path => Path.GetFileName(path),
                        path => path
                    );

                var filenames = textureFilepaths.Keys.ToList();
                filenames.Sort();
                selectedFilename = filenames[0];

                // PopupField also has no UXML in this version, so we get to manually instantiate it
                // TODO: Error if no texture files found
                PopupField<string> textureFileField = new PopupField<string>(
                    "Texture File", filenames, selectedFilename)
                {
                    bindingPath = "selectedFilename",
                    tooltip = "Select a detected texture file from the asset folder."
                };
                textureFileField.RegisterValueChangedCallback(OnTextureSelectionChange);
                root.Q<VisualElement>("textureFilePlaceholder").Add(textureFileField);

                paletteFilepaths = dfUnity.loadedAssetFolder.GetAssetPaths(AssetType.COLOR_PALETTE).ToDictionary(
                    path => Path.GetFileName(path),
                    path => path
                );
                List<string> paletteNames = new List<string>(paletteFilepaths.Keys);
                paletteNames.Sort();
                selectedPalette = paletteNames.First();

                PopupField<string> paletteField = new PopupField<string>(
                    "Palette", paletteNames, 0
                )
                {
                    bindingPath = "selectedPalette",
                    tooltip = "Select a palette to apply to the texture"
                };

                paletteField.RegisterValueChangedCallback(OnPaletteSelectionChange);
                root.Q<VisualElement>("paletteSelector").Add(paletteField);

                ColorField bgColor = root.Q<ColorField>("backgroundColor");
                bgColor.RegisterValueChangedCallback(OnColorChange);

                EnumField filteringMode = root.Q<EnumField>("filterMode");
                filteringMode.RegisterValueChangedCallback(OnFilterModeChange);

                textureArea = root.Q<ScrollView>("texturePreview");

                root.Bind(new SerializedObject(this));

                // Init values to defaults, they get overwritten to element defaults on bind
                ResetForm();

                LoadTexture(selectedFilename);

            } catch (Exception ex) {
                Debug.LogError("Texture browser failed to load due to unexpected error: " + ex);
            }
        }

        #endregion

        #region Event Handlers

        // TODO: Combine and debounce
        private void OnTextureSelectionChange(ChangeEvent<string> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            LoadTexture(changeEvent.newValue);
        }

        private void OnPaletteSelectionChange(ChangeEvent<string> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            textureLoader.Palette = new DFPalette(paletteFilepaths[selectedPalette]);
            LoadTexture();
        }

        private void OnColorChange(ChangeEvent<Color> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            LoadTexture();
        }

        private void OnFilterModeChange(ChangeEvent<Enum> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }
            LoadTexture();
        }

        private void OnDestroy()
        {
            Debug.Log("Destroying Texture Browser window");
        }

        private void ResetForm()
        {
            backgroundColor = defaultBackgroundColor;
            filterMode = defaultFilterMode;
        }

        #endregion

        #region Private Methods

        private void LoadTexture(string targetFilename = null)
        {
            var filename = targetFilename ?? selectedFilename;

            if (filename == null || filename.Length == 0)
            {
                return;
            }

            // TODO: Show load error
            textureLoader.Load(textureFilepaths[filename], FileUsage.UseMemory, true);
            Debug.Log($"{filename} contains {textureLoader.GetRecordCount()} records");

            textureArea.Clear();

            for (int x = 0; x < textureLoader.GetRecordCount(); x++)
            {
                DFSize textureSize = textureLoader.GetSize(x);
                Texture2D recordTexture = TextureReader.CreateFromAPIImage(textureLoader, x, 0);
                
                Image image = new Image()
                {
                     image = recordTexture
                };
                image.style.width = textureSize.Width * scale;
                image.style.height = textureSize.Height * scale;                

                textureArea.Add(image);

            }

        }

        #endregion

    }
}