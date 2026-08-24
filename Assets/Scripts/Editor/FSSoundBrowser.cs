using System;
using System.Collections.Generic;
using System.Linq;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop;
using TerminatorUnity.Game.Asset;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using XnGine;

namespace TerminatorUnity.Editor
{
    public class FSSoundBrowser : EditorWindow
    {

        const string windowTitle = "Future Shock Sound Browser";

        const string menuPath = "XnGine/Future Shock/Sound Browser";

        // TODO: Remove this dependency
        DaggerfallUnity dfUnity;

        private SoundArchive soundArchive;

        private GameObject audioPlayer;

        [SerializeField]
        private string selectedFile;

        [MenuItem(menuPath)]
        static void Init()
        {
            FSSoundBrowser window = (FSSoundBrowser)GetWindow(typeof(FSSoundBrowser));
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
                VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UXML/FSSoundBrowser.uxml");
                StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/USS/FSSoundBrowser.uss");
                VisualElement ui = uiAsset.CloneTree();

                root.name = "FSSoundBrowser";
                root.styleSheets.Add(styleSheet);
                root.Add(ui);

                if (dfUnity.loadedAssetFolder == null)
                {
                    root.Q<Label>("no-asset-folder").style.display = DisplayStyle.Flex;
                    root.Q<Box>("sound-browser").style.display = DisplayStyle.None;
                    return;
                }

                this.soundArchive = new SoundArchive(
                    dfUnity.loadedAssetFolder.GetArchivePath(AssetType.SFX_ARCHIVE),
                    DaggerfallConnect.FileUsage.UseDisk, readOnly: true, typedIndices: false);

                List<string> soundRecordList = new List<string>(this.soundArchive.RecordNames);
                soundRecordList.Sort();

                if (soundRecordList.Count > 0)
                {
                    selectedFile = soundRecordList.First();
                }

                PopupField<string> soundRecordField = new PopupField<string>(
                    "Sound Record", new List<string>(soundRecordList), 0
                ){
                    bindingPath = "selectedFile",
                    tooltip = "Select a sound",
                };

                root.Q<VisualElement>("soundSelector").Add(soundRecordField);

                root.Q<Button>("button-play").clicked += Play;
                root.Q<Button>("button-stop").clicked += Stop;
                root.Bind(new SerializedObject(this));                
                
            } catch (Exception ex) {
                Debug.LogError("Sound browser failed to load due to unexpected error: " + ex);
            }
        }

        private GameObject GetAudioPlayer()
        {
            if (audioPlayer == null)
            {
                audioPlayer = new GameObject("EditorAudioPlayer")
                {
                    hideFlags = HideFlags.HideAndDontSave
                };
                audioPlayer.AddComponent<AudioSource>();
            }

            return audioPlayer;
        }

        private AudioSource GetAudioSource()
        {
            GameObject audioPlayer = GetAudioPlayer();
            return audioPlayer.GetComponent<AudioSource>();
        }

        private void Play()
        {
            AudioSource audioSource = GetAudioSource();
            DFSound sound = soundArchive.GetSound(selectedFile);
            
            byte[] decodedClip = VigenereDecoder.Decode(sound.WaveData);

            // Copying this from SoundReader for now to test with.
            // Need to do some refactoring here to clean up the interface.
            AudioClip clip = AudioClip.Create(
                name: sound.Name, 
                lengthSamples: decodedClip.Length, 
                channels: 1, 
                frequency: SoundArchive.SampleRate, 
                stream: false);

            const float divisor = 1.0f / 128.0f;
            float[] data = new float[decodedClip.Length];
            for (int i = 0; i < decodedClip.Length; i++)
                data[i] = (decodedClip[i] - 128) * divisor;

            clip.SetData(data, 0);

            audioSource.PlayOneShotWhenReady(clip, 1.0f);
        }

        private void Stop()
        {
            AudioSource audioSource = GetAudioSource();
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        private void OnDisable()
        {
            if (audioPlayer != null)
            {
                DestroyImmediate(audioPlayer);
                audioPlayer = null;
            }
        }

    }

}