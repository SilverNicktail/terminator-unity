using System.IO;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallConnect.Utility;

namespace TerminatorUnity.Asset
{
    /// <summary>
    /// Common parsing methods for *.IMG files, abstracted
    /// from original DFU implementations in ImgFile so they
    /// can also be used with image archives.
    /// </summary>
    public static class IMGParser
    {
        /// <summary>
        /// Fetches the header (metadata) of an image from pre-loaded byte data
        /// </summary>
        /// <param name="rawData">Unparsed image file</param>
        /// <returns>Image file's header</returns>
        public static BaseImageFile.ImgFileHeader GetImageHeader(byte[] rawData)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(rawData));
            return GetImageHeader(ref reader);
        }

        /// <summary>
        /// Fetches the header (metadata) of an image from a provided data stream.
        /// BinaryReader lets this be directly from memory (for archives) or from a file
        /// (for single-image files).
        /// </summary>
        /// <param name="reader">Data stream of image file to read</param>
        /// <returns>Image file's header</returns>
        public static BaseImageFile.ImgFileHeader GetImageHeader(ref BinaryReader reader)
        {
            BaseImageFile.ImgFileHeader header = default;
            reader.BaseStream.Position = 0;
            header.Position = 0;

            // Create header based on RCI byte-size test
            DFSize sz = GetHeaderlessFileImageDimensions((uint) reader.BaseStream.Length);
            if (sz.Width == 0 && sz.Height == 0)
            {
                // This image has a header
                ReadImgFileHeader(ref reader, ref header);
            }
            else
            {
                // This is an RCI-style image that has no header, so we need to build one
                // Note that RCI-style images are never compressed
                header.XOffset = 0;
                header.YOffset = 0;
                header.Width = (short)sz.Width;
                header.Height = (short)sz.Height;
                header.Compression = BaseImageFile.CompressionFormats.Uncompressed;
                header.PixelDataLength = (ushort) (header.Width * header.Height);
                header.DataPosition = reader.BaseStream.Position;
                header.HasEmbeddedPalette = false;
            }

            return header;

        }

        /// <summary>
        /// Reads the pixel data (body) of an image file. If the file contains an
        /// embedded palette, this will be detected and parsed into the returned
        /// object. Embedded palettes override any palette passed in.
        /// </summary>
        /// <param name="rawData">Full data of image file</param>
        /// <param name="header">Previously parsed file header (used to get positions)</param>
        /// <param name="palette">Palette to apply to image, can be null if image is known to have an embedded palette</param>
        /// <returns></returns>
        public static DFBitmap GetPixelData(byte[] rawData, ref BaseImageFile.ImgFileHeader header, DFPalette palette)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(rawData));
            return GetPixelData(ref reader, ref header, palette);
        }

        /// <summary>
        /// Reads the pixel data (body) of an image file. If the file contains an
        /// embedded palette, this will be detected and parsed into the returned
        /// object. Embedded palettes override any palette passed in.
        /// </summary>
        /// <param name="reader">Data stream for image file</param>
        /// <param name="header">Previously parsed file header (used to get positions)</param>
        /// <param name="palette">Palette to apply to image, can be null if image is known to have an embedded palette</param>
        /// <returns></returns>
        public static DFBitmap GetPixelData(ref BinaryReader reader, ref BaseImageFile.ImgFileHeader header, DFPalette palette)
        {
            DFBitmap imageData = new DFBitmap
            {
                Width = header.Width,
                Height = header.Height,
                Data = new byte[header.Width * header.Height],
                Palette = palette
            };

            reader.BaseStream.Position = header.DataPosition;

            BinaryWriter writer = new BinaryWriter(new MemoryStream(imageData.Data));
            writer.Write(reader.ReadBytes(imageData.Data.Length));

            // Some images in Daggerfall have embedded palettes following
            // the pixel data, which should override anything passed in.
            long remaining = reader.BaseStream.Length - reader.BaseStream.Position;
            if (remaining >= 768)
            {
                header.HasEmbeddedPalette = true;
                imageData.Palette = ReadEmbeddedPalette(ref reader);
            }

            return imageData;
        }

        /// <summary>
        /// Reads a standard IMG file header from the source stream into the desination header struct.
        ///  This header is found in multiple image files which is why it's implemented here in the base.
        /// </summary>
        /// <param name="reader">Source reader positioned at start of header data.</param>
        /// <param name="header">Destination header structure.</param>
        private static void ReadImgFileHeader(ref BinaryReader reader, ref BaseImageFile.ImgFileHeader header)
        {
            // Read IMG header data
            header.Position = reader.BaseStream.Position;
            header.XOffset = reader.ReadInt16();
            header.YOffset = reader.ReadInt16();
            header.Width = reader.ReadInt16();
            header.Height = reader.ReadInt16();
            header.Compression = (BaseImageFile.CompressionFormats)reader.ReadUInt16();
            header.PixelDataLength = reader.ReadUInt16();
            header.FrameCount = 1;
            header.DataPosition = reader.BaseStream.Position;
            header.HasEmbeddedPalette = false;
        }

        /// <summary>
        /// IMG files have a fixed width and height not specified in a header.
        ///  This method returns the correct dimensions of images inside these files.
        /// </summary>
        /// <returns>Dimensions of image.</returns>
        private static DFSize GetHeaderlessFileImageDimensions(uint fileLength)
        {
            // Set image dimensions
            switch (fileLength)
            {
                case 44:
                    return new DFSize(22, 22);
                case 289:
                    return new DFSize(17, 17);
                case 441:
                    return new DFSize(49, 9);
                case 512:
                    return new DFSize(32, 16);
                case 720:
                    return new DFSize(9, 80);
                case 990:
                    return new DFSize(45, 22);
                case 1720:
                    return new DFSize(43, 40);
                case 2140:
                    return new DFSize(107, 20);
                case 2916:
                    return new DFSize(81, 36);
                case 3200:
                    return new DFSize(40, 80);
                case 3938:
                    return new DFSize(179, 22);
                case 4280:
                    return new DFSize(107, 40);
                case 4508:
                    return new DFSize(322, 14);
                case 20480:
                    return new DFSize(320, 64);
                case 26496:
                    return new DFSize(184, 144);
                case 64000:
                    return new DFSize(320, 200);
                case 64768:
                    return new DFSize(320, 200);
                case 68800:
                    return new DFSize(320, 215);
                case 112128:
                    return new DFSize(512, 219);
                default:
                    return new DFSize(0, 0);
            }
        }


        /// <summary>
        /// Parses out palette information in image files that have their palettes embedded
        /// after their pixel data.
        /// </summary>
        /// <param name="reader">Image file reader positioned at end of image data.</param>
        private static DFPalette ReadEmbeddedPalette(ref BinaryReader reader)
        {
            DFPalette palette = new DFPalette();
            palette.Read(ref reader);

            // The palette for palettized images is very dark. 
            // Multiplying the RGB values by 4 results in correct-looking colours.
            for (int i = 0; i < 256; i++)
            {
                int r = palette.GetRed(i) * 4;
                int g = palette.GetGreen(i) * 4;
                int b = palette.GetBlue(i) * 4;
                palette.Set(i, (byte)r, (byte)g, (byte)b);
            }

            return palette;

        }

    }
}