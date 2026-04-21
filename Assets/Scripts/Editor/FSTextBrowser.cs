using System;
using System.Collections.Generic;
using System.Linq;
using DaggerfallWorkshop;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using XnGine;

namespace TerminatorUnity.Editor
{
    public class FSTextBrowser : EditorWindow
    {

        const string windowTitle = "Future Shock Text Browser";

        const string menuPath = "XnGine/Future Shock/Text Browser";

        DaggerfallUnity dfUnity;

        private FSTextFile textFile;

        # region UI Bindings

        [SerializeField]
        private string recordContent = "";

        # endregion

        [MenuItem(menuPath)]
        static void Init()
        {
            FSTextBrowser window = (FSTextBrowser)GetWindow(typeof(FSTextBrowser));
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
                VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UXML/FSTextBrowser.uxml");
                StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/USS/FSTextBrowser.uss");
                VisualElement ui = uiAsset.CloneTree();

                root.name = "fsTextBrowser";
                root.styleSheets.Add(styleSheet);
                root.Add(ui);

                if (dfUnity.loadedAssetFolder == null)
                {
                    root.Q<Label>("no-asset-folder").style.display = DisplayStyle.Flex;
                    root.Q<Box>("font-form").style.display = DisplayStyle.None;
                    return;
                }

                this.textFile = new FSTextFile(
                    dfUnity.loadedAssetFolder.GetArchivePath(AssetType.MISSION_ARCHIVE),
                    DaggerfallConnect.FileUsage.UseDisk, true);

                List<string> textRecordList = new List<string>(this.textFile.GetAvailableRecords());
                textRecordList.Sort();
                string firstRecord = textRecordList.First();
                PopupField<string> textRecordField = new PopupField<string>("Text Record", new List<string>(textRecordList), firstRecord);
                
                textRecordField.RegisterValueChangedCallback(OnTextRecordSelectionChange);
                root.Q<VisualElement>("textRecordPlaceholder").Add(textRecordField);

                root.Bind(new SerializedObject(this));
                
                LoadTextRecord(firstRecord);

            } catch (Exception ex) {
                Debug.LogError("Text browser failed to load due to unexpected error: " + ex);
            }
        }

        private void OnTextRecordSelectionChange(ChangeEvent<string> changeEvent)
        {
            if (changeEvent.previousValue == changeEvent.newValue)
            {
                return;
            }

            LoadTextRecord(changeEvent.newValue);
        }

        private void LoadTextRecord(string recordName)
        {
            this.recordContent = this.textFile.GetTextRecord(recordName);            
        }

    }
}