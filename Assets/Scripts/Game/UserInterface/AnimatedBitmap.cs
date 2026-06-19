using System.Collections.Generic;
using DaggerfallConnect;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[assembly: UxmlNamespacePrefix("TerminatorUnity.Game.UserInterface", "xngine")]

namespace TerminatorUnity.Game.UserInterface
{

    public class AnimatedBitmap : VisualElement
    {

        public int fps { get; set; }

        public int scale { get; set; }

        private Texture2D[] frames;

        private int currentFrame = 0;

        private IVisualElementScheduledItem frameSchedule;

        public new class UxmlFactory: UxmlFactory<AnimatedBitmap> {}

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlIntAttributeDescription attrFps = new UxmlIntAttributeDescription
            {
                defaultValue = 6,
                name = "fps",
                use = UxmlAttributeDescription.Use.Optional
            };

            UxmlIntAttributeDescription attrScale = new UxmlIntAttributeDescription
            {
                defaultValue = 2,
                name = "scale",
                use = UxmlAttributeDescription.Use.Optional
            };

            // Element creates some children of its own, but you can't pass any
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((AnimatedBitmap)ve).fps = attrFps.GetValueFromBag(bag, cc);
                ((AnimatedBitmap)ve).scale = attrScale.GetValueFromBag(bag, cc);
            }

        }

        public AnimatedBitmap()
        {}

        public void SetImageData(DFBitmap[] bitmaps)
        {
            frames = new Texture2D[bitmaps.Length];
            for (int x = 0; x < bitmaps.Length; x++)
            {
                DFBitmap frame = bitmaps[x];    
                Texture2D imageTex = new Texture2D(frame.Width, frame.Height, TextureFormat.ARGB32, false);         
                imageTex.SetPixels32(frame.GetColor32());
                imageTex.Apply();
                frames[x] = imageTex;
            }
            
            ShowFrame(0);
            Animate();
        }

        public void SetImageData(Texture2D[] frames)
        {
            this.frames = frames;
            ShowFrame(0);
            Animate();
        }

        private void Animate()
        {
            if (frames == null || frames.Length < 1)
            {
                return;
            }

            if (frames.Length > 1)
            {
                this.frameSchedule = this.schedule.Execute(ShowNextFrame).Every(1000/fps);
            } 
            else if (this.frameSchedule != null)
            {
                this.frameSchedule.Pause();
                this.frameSchedule = null;
            }
        }

        private void ShowNextFrame()
        {
            if (frames.Length == 0)
            {
                return;
            }

            if (++currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }

            ShowFrame(currentFrame);            
        }

        private void ShowFrame(int frame)
        {
            if (frames.Length == 0 || frame >= frames.Length)
            {
                return;
            }


            Image imageRef = this.Q<Image>();

            if (imageRef == null)
            {
                Image image = new Image()
                {
                    image = frames[frame]
                };
                image.style.width = frames[frame].width * scale;
                image.style.height = frames[frame].height * scale;                
                this.Add(image);
            } else
            {
                imageRef.image = frames[frame];
            }
        }

    }

}