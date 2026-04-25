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

        private bool loading = false;

        private DFBitmap[] imageFrames;

        private int currentFrame = 0;

        private double lastFrameTime = 0.0f;

        [SerializeField]
        private string selectedFile;

        [SerializeField]
        private string selectedPalette;

        [SerializeField]
        private int fps = 4;

        [SerializeField]
        private int scale = 2;

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

            if (!dfUnity)
            {
                dfUnity = DaggerfallUnity.Instance;
            }

            // I normally despise generic exception handling, but it's necessary in createGUI().
            // Need to catch all unhandled exceptions in here, or the GUI will fail to initialise and 
            // end up in an invalid state we might not be able to recover from. Had to comment the 
            // method body out and delete my editor's UI layout files!
            try {

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

            } catch (Exception ex) {
                Debug.LogError("Image browser failed to load due to unexpected error: " + ex);
            }
        }

        private void Update()
        {
            // GUI hasn't finished initialising
            if (!dfUnity)
            {
                return;
            }

            double interval = 1.0d / fps;
            if (!loading && imageFrames.Length > 1 && EditorApplication.timeSinceStartup >= (lastFrameTime + interval))
            {
                int nextFrame = (currentFrame + 1 >= imageFrames.Length) ? 0 : currentFrame + 1;
                DisplayImageFrame(nextFrame);
            }
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
            loading = true;
            DFPalette palette = new DFPalette(paletteFilepaths[selectedPalette]);
            imageFrames = this.imageArchive.GetImageData(selectedFile, palette);
            DisplayImageFrame(0);
            loading = false;
        }

        private void DisplayImageFrame(int frameIdx)
        {           
            if (frameIdx < 0 || frameIdx > imageFrames.Length)
            {
                Debug.LogWarning($"Invalid frame index {frameIdx} requested for display, resetting to 0.");
                frameIdx = 0;
            }

            DFBitmap frame = imageFrames[frameIdx];
            ScrollView imageLoadArea = rootVisualElement.Q<ScrollView>("recordContentScroller");
            imageLoadArea.Clear();
 
            Texture2D imageTex = new Texture2D(frame.Width, frame.Height, TextureFormat.ARGB32, false);         
            imageTex.SetPixels32(frame.GetColor32());
            imageTex.Apply();
            Image image = new Image()
            {
                image = imageTex
            };

            image.style.width = frame.Width * scale;
            image.style.height = frame.Height * scale;                

            imageLoadArea.Add(image);

            currentFrame = frameIdx;
            lastFrameTime = EditorApplication.timeSinceStartup;
        }

    }

}