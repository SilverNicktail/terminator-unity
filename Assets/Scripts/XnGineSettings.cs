using System.Diagnostics.CodeAnalysis;
using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
# endif

/// <summary>
/// Serialized object for settings from the Project Settings window.
/// </summary>
/// Might eventually move other options sources to here? Feels like DFU
/// has several different places/ways to store stettings. Should probably
/// read the docs on that, just sick of having to constantly reload my asset
/// folder after every edit.
namespace XnGine
{

    public class XnGineSettings: ScriptableObject
    {
        private const string resourceFolderPath = "Assets/Resources";
        private const string assetName = "XnGineSettings";

// Throws separate errors in IDE and compiler, and compiler warning can't be suppressed with SuppressMessage!
// One of those nice ugly things you end up with in C#
# pragma warning disable 0414
        [SuppressMessage("UI Binding", "IDE0052", 
            Justification = "No-use warning incorrect, used in UI binding.")]
        [SerializeField]
        private string assetFolderPath;
# pragma warning restore 0414

        public static XnGineSettings GetProjectSettings()
        {

            # if UNITY_EDITOR
            string assetPath = $"{resourceFolderPath}/{assetName}.asset";
            var settings = AssetDatabase.LoadAssetAtPath<XnGineSettings>(assetPath);

            if (settings == null)
            {
                // Init if it doesn't exist
                settings = CreateInstance<XnGineSettings>();
                settings.assetFolderPath = "";
                AssetDatabase.CreateAsset(settings, assetPath);
                AssetDatabase.SaveAssets();
            }

            # else
            var settings = Resources.Load<ProjectSettings>(assetName);

            # endif

            return settings;
        }

        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetProjectSettings());
        }

    }

}