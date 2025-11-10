// Project:         Daggerfall Unity
// Copyright:       Copyright (C) 2009-2023 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Michael Rauter (Nystul)
// Contributors:    
// 
// Notes:
//

using UnityEngine;
using UnityEditor;
using DaggerfallConnect.Arena2;
using XnGine;

namespace DaggerfallWorkshop
{
    /// <summary>
    /// Text Record Viewer in Unity editor.
    /// </summary>
    class TextRecordViewerWindow : EditorWindow
    {
        const string windowTitle = "Text Record Viewer";
        const string menuPath = "XnGine/Daggerfall/Text Record Viewer";

        DaggerfallUnity dfUnity;

        int recordIndex = -1;

        [MenuItem(menuPath)]
        static void Init()
        {
            TextRecordViewerWindow window = (TextRecordViewerWindow)EditorWindow.GetWindow(typeof(TextRecordViewerWindow));
            window.titleContent = new GUIContent(windowTitle);
        }

        [MenuItem(menuPath, true)]
        static bool EnableMenuItem()
        {
            IAssetFolder loadedFolder = DaggerfallUnity.Instance.loadedAssetFolder;
            return loadedFolder != null && loadedFolder.GetGame() == XngineGame.ES_DAGGERFALL;
        }
        
        void OnGUI()
        {
            if (!IsReady())
            {
                EditorGUILayout.HelpBox("DaggerfallUnity instance not ready. Have you set your Arena2 path?", MessageType.Info);
                return;
            }

            EditorGUILayout.Space();
            recordIndex = EditorGUILayout.IntField(new GUIContent("Record Index"), recordIndex);
            TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.GetRSCTokens(recordIndex);
            if (tokens == null)
                return;

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].text != null)
                {
                    string text = tokens[i].text;
                    EditorGUILayout.TextArea(text, GUILayout.MinWidth(600));
                }
            }
        }

        bool IsReady()
        {
            if (!dfUnity)
                dfUnity = DaggerfallUnity.Instance;

            return true;
        }
    }
}
