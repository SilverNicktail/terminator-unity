using System.Collections;
using System.Collections.Generic;
using System.IO;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Utility;
using TerminatorUnity.Asset;
using TerminatorUnity.Game.Asset;
using UnityEngine;
using UnityEngine.UIElements;
using XnGine;

// TODO: Turn into components
// Prototype code to test image & video loading and display.
// Needs to be converted to re-usable runtime UI components.
namespace TerminatorUnity.Game.UI
{

    public class MainMenu : MonoBehaviour
    {
        
        private UIDocument ui;

        private AssetManager assetManager;

        private VidFile vidFile;

        private Texture2D vidTexture;

        private AudioSource[] audioSources;

        private AudioClip[] audioClips;

        private bool loaded = false;

        private bool lastBlockWasAudio = false;

        private double nextFrameTime = 0;

        private uint nextAudioClip = 0;

        void Awake()
        {
            this.ui = GetComponent<UIDocument>();
            this.assetManager = FindObjectOfType<AssetManager>();
            this.audioSources = GetComponents<AudioSource>();
            this.audioClips = new AudioClip[this.audioSources.Length];
        }

        // Can't do this OnEnable, can't guarantee order in which other objects become active
        void Update()
        {
            if (! loaded && assetManager.AssetFolder != null && 
                assetManager.AssetFolder.FolderValid())
            {
                // TODO: Add try-catch here to prevent game trying over and over to
                // initialise the GUI if something is wrong
                InitGUI();
            
            } else if (loaded && ! vidFile.EndOfFile && 
                AudioSettings.dspTime + vidFile.FrameDelay >= nextFrameTime)
            {

                vidFile.ReadNextBlock();

                if (vidFile.LastBlockType == VidBlockTypes.Null)
                {
                    vidFile.ReadNextBlock();                    
                
                } else if (vidFile.LastBlockType == VidBlockTypes.Audio_StartFrame ||
                    vidFile.LastBlockType == VidBlockTypes.Audio_IncrementalFrame)
                {
                    // Add empty sample at front and end of clip to prevent clicks and pops
                    int srcLength = vidFile.AudioBuffer.Length;
                    int dstLength = srcLength + 2;
                    int pos = 1;

                    // Create audio clip for this block
                    AudioClip clip = AudioClip.Create(string.Empty, dstLength, 1, vidFile.SampleRate, false);

                    // Fill clip data
                    const float divisor = 1.0f / 128.0f;
                    float[] data = new float[dstLength];
                    for (int i = 0; i < srcLength; i++)
                    {
                        data[pos++] = (vidFile.AudioBuffer[i] - 128) * divisor;
                    }
                    clip.SetData(data, 0);
                    audioClips[nextAudioClip] = clip;

                    // Schedule clip
                    audioSources[nextAudioClip].clip = audioClips[nextAudioClip];
                    audioSources[nextAudioClip].volume = DaggerfallUnity.Settings.SoundVolume;
                    audioSources[nextAudioClip].PlayScheduled(nextFrameTime);
                    nextFrameTime += vidFile.FrameDelay;
                    nextAudioClip = (nextAudioClip == audioClips.Length - 1) ? 0 : nextAudioClip + 1;
                    lastBlockWasAudio = true;
                
                } else if (vidFile.LastBlockType == VidBlockTypes.Video_StartFrame ||
                    vidFile.LastBlockType == VidBlockTypes.Video_IncrementalFrame ||
                    vidFile.LastBlockType == VidBlockTypes.Video_IncrementalRowOffsetFrame)
                {
                    // Update video
                    vidTexture.SetPixels32(vidFile.FrameBuffer);
                    vidTexture.Apply(false);


                    if (! lastBlockWasAudio)
                    {
                        nextFrameTime += vidFile.FrameDelay;
                    }

                    lastBlockWasAudio = false;
                }
  
            }

        }

        private void InitGUI()
        {
            string path = assetManager.AssetFolder.GetArchivePath(AssetType.IMAGE_ARCHIVE);
            FSImageArchive imageArchive = new FSImageArchive(
                path,
                DaggerfallConnect.FileUsage.UseDisk
            );

            DFPalette palette = new DFPalette(
                Path.Combine(assetManager.AssetFolder.GetRootPath(), ImageConstants.Palette.MENU)
            );

            BaseImageFile.ImgFileHeader header = imageArchive.GetImageHeader(ImageConstants.Menu.MAIN_MENU_BAR);
            DFBitmap menuBar = imageArchive.GetImageData(ImageConstants.Menu.MAIN_MENU_BAR, palette)[0];
            Texture2D menuBarTex = new Texture2D(header.Width, header.Height, TextureFormat.ARGB32, false);
            menuBarTex.SetPixels32(menuBar.GetColor32());
            menuBarTex.Apply();

            Image menuBarBack = ui.rootVisualElement.Q<Image>("menu-bar-background");
            menuBarBack.image = menuBarTex;

            string vidFilePath = Path.Combine(assetManager.AssetFolder.GetRootPath(), "LOGO.VID");
            vidFile = new VidFile(vidFilePath, 555f);

            vidTexture = TextureReader.CreateFromSolidColor(vidFile.FrameWidth, vidFile.FrameHeight, Color.black, false, false);
            vidTexture.wrapMode = TextureWrapMode.Clamp;
            vidTexture.filterMode = (FilterMode)DaggerfallUnity.Settings.VideoFilterMode;

            Image titleVid = ui.rootVisualElement.Q<Image>("title");
            titleVid.image = vidTexture;

            nextFrameTime = AudioSettings.dspTime;
            loaded = true;
        }

    }

}

