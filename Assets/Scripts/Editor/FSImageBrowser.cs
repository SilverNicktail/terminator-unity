using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DaggerfallConnect;
using DaggerfallWorkshop;
using TerminatorUnity.Asset;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using XnGine;

namespace TerminatorUnity.Editor
{
    public class FSImageBrowser : EditorWindow
    {

        const string windowTitle = "Future Shock Image Browser";

        const string menuPath = "XnGine/Future Shock/Image Browser";

        DaggerfallUnity dfUnity;

        private FSImageArchive imageArchive;

        private Dictionary<string, string> paletteFilepaths;

        [SerializeField]
        private string selectedFile;

        [SerializeField]
        private string selectedPalette;

        [MenuItem(menuPath)]
        static void Init()
        {
            FSImageBrowser window = (FSImageBrowser)GetWindow(typeof(FSImageBrowser));
            window.titleContent = new GUIContent(windowTitle);
        }        

        [MenuItem(menuPath, true)]
        static bool EnableMenuItem()
        {
            IAssetFolder loadedFolder = DaggerfallUnity.Instance.loadedAssetFolder;
            return loadedFolder != null && loadedFolder.GetGame() == XngineGame.T_FUTURE_SHOCK;
        }

        private void CreateGUI()
        {
            Debug.Log("Attempting to init gui");

            if (!dfUnity)
            {
                dfUnity = DaggerfallUnity.Instance;
            }

            VisualElement root = rootVisualElement;
            VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UXML/FSImageBrowser.uxml");
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/USS/FSImageBrowser.uss");
            VisualElement ui = uiAsset.CloneTree();

            root.name = "fsImageBrowser";
            root.styleSheets.Add(styleSheet);
            root.Add(ui);

            if (dfUnity.loadedAssetFolder == null)
            {
                root.Q<Label>("no-asset-folder").style.display = DisplayStyle.Flex;
                root.Q<Box>("image-browser").style.display = DisplayStyle.None;
                return;
            }

            this.imageArchive = new FSImageArchive(
                dfUnity.loadedAssetFolder.GetArchivePath(AssetType.IMAGE_ARCHIVE),
                DaggerfallConnect.FileUsage.UseDisk);

            List<string> imageRecordList = new List<string>(this.imageArchive.GetAvailableFiles());
            imageRecordList.Sort();
            selectedFile = imageRecordList.First();

            PopupField<string> imageRecordField = new PopupField<string>(
                "Image Record", new List<string>(imageRecordList), 0
            ){
                bindingPath = "selectedFile",
                tooltip = "Select an image to view",
            };

            imageRecordField.RegisterValueChangedCallback(OnImageRecordSelectionChange);
            root.Q<VisualElement>("imageSelector").Add(imageRecordField);

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
                tooltip = "Select a palette to apply to the image"
            };

            paletteField.RegisterValueChangedCallback(OnPaletteSelectionChange);
            root.Q<VisualElement>("paletteSelector").Add(paletteField);

            root.Bind(new SerializedObject(this));
            
            LoadImageRecord(selectedFile, selectedPalette);
        }

        // TODO: Move to TrackPropertyValue() when it becomes available in later Unity version
        private void OnImageRecordSelectionChange(ChangeEvent<string> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }

            // Need to pass new value rather than relying on binding.
            // These handlers fire before the binding is updated.
            // No way to two-way bind in 2019?
            LoadImageRecord(changeEvent.newValue, selectedPalette);
        }

        private void OnPaletteSelectionChange(ChangeEvent<string> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }

            LoadImageRecord(selectedFile, changeEvent.newValue);
        }

        private void LoadImageRecord(String selectedFile, String selectedPalette)
        {
            DFPalette palette = new DFPalette(paletteFilepaths[selectedPalette]);

            DFBitmap bitmap = this.imageArchive.GetImageData(selectedFile, palette);
            Texture2D imageTex = new Texture2D(bitmap.Width, bitmap.Height, TextureFormat.ARGB32, false);
            imageTex.SetPixels32(bitmap.GetColor32());
            imageTex.Apply();
            Image image = new Image()
            {
                image = imageTex
            };

            // TODO: Add a scaling control
            image.style.width = bitmap.Width * 2;
            image.style.height = bitmap.Height * 2;

            ScrollView imageLoadArea = rootVisualElement.Q<ScrollView>("recordContentScroller");
            imageLoadArea.Clear();
            imageLoadArea.Add(image);
        }

    }
}