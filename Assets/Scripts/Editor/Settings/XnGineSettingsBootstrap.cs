using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using XnGine;

namespace XnGine.Editor
{
    public static class XnGineSettingsBootstrap
    {
        
        [SettingsProvider]
        public static SettingsProvider XnGineSettingsProvider()
        {
            var provider = new SettingsProvider("Project/XnGine", SettingsScope.Project)
            {
                label = "XnGine Project Settings",
                keywords = new HashSet<string>(new[] { "Daggerfall", "Future Shock", "XnGine", "Asset Folder" }),

                activateHandler = (searchElement, rootElement) =>
                {
                    var settings = XnGineSettings.GetSerializedSettings();
                    var uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UXML/XnGineSettings.uxml");
                    rootElement.Add(uiAsset.CloneTree());

                    // TODO: Validate folder when selected
                    rootElement.Q<Button>("outputPathButton").clicked += () =>
                    {
                        string selectedPath = EditorUtility.OpenFolderPanel("Select asset folder", "GAMEDATA", null);

                        if (selectedPath != null && selectedPath.Length > 0)
                        {
                            settings.FindProperty("assetFolderPath").stringValue = selectedPath;
                            settings.ApplyModifiedProperties();
                        }
                    };

                    rootElement.Bind(settings);

                }
            };

            return provider;
        }

    }
}