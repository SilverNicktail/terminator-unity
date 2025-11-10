using System.Collections.Generic;
using System.Linq;
using DaggerfallWorkshop;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using XnGine;

namespace TerminatorUnity
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
            return loadedFolder == null || loadedFolder.GetGame() == XngineGame.T_FUTURE_SHOCK;
        }

        private void CreateGUI()
        {
            if (!dfUnity)
            {
                dfUnity = DaggerfallUnity.Instance;
            }

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

            // TODO: Have assset folder interface return available text archives
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