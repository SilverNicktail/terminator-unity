using System;
using System.Collections.Generic;
using System.IO;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;

namespace TerminatorUnity.Asset
{
    
    public static class CFAParser
    {
        
        /// <summary>
        /// Reads the file header (metadata) of a CFA file
        /// </summary>
        /// <param name="rawData">Raw image record data</param>
        /// <returns>File header</returns>
        public static CfaFile.CFAHeader ReadHeader(byte[] rawData)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(rawData));
            return ReadHeader(ref reader);
        }

        /// <summary>
        /// Reads the file header (metadata) of a CFA file
        /// </summary>
        /// <param name="reader">Reader opened against raw image record data</param>
        /// <returns>File header</returns>
        public static CfaFile.CFAHeader ReadHeader(ref BinaryReader reader)
        {
            reader.BaseStream.Position = 0;
            CfaFile.CFAHeader header = new CfaFile.CFAHeader();
            header.WidthUncompressed = reader.ReadInt16();
            header.Height = reader.ReadInt16();
            header.WidthCompressed = reader.ReadInt16();
            header.Unknown1 = reader.ReadInt16();
            header.Unknown2 = reader.ReadInt16();
            header.BitsPerPixel = reader.ReadByte();
            header.FrameCount = reader.ReadByte();
            header.HeaderSize = reader.ReadInt16();
            return header;
        }

        /// <summary>
        /// Reads and decodes the run-length-encoded pixel data for each frame in a CFA image.
        /// </summary>
        /// <param name="rawData">Raw record data (in. header)</param>
        /// <returns>Image pixel data, decoded from RLE</returns>
        public static byte[] ReadImageData(byte[] rawData)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(rawData));
            return ReadImageData(ref reader, ReadHeader(ref reader));
        }

        /// <summary>
        /// Reads and decodes the run-length-encoded pixel data for each frame in a CFA image.
        /// </summary>
        /// <param name="reader">Reader opened against raw image record data (inc. header)</param>
        /// <returns>Image pixel data, decoded from RLE</returns>
        public static byte[] ReadImageData(ref BinaryReader reader)
        {
            return ReadImageData(ref reader, ReadHeader(ref reader));
        }

        /// <summary>
        /// Reads and decodes the run-length-encoded pixel data for each frame in a CFA image.
        /// </summary>
        /// <param name="rawData">Raw record data (in. header)</param>
        /// <param name="header">Previously decoded image header</param>
        /// <returns>Image pixel data, decoded from RLE</returns>
        public static byte[] ReadImageData(byte[] rawData, CfaFile.CFAHeader header) 
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(rawData));
            return ReadImageData(ref reader, header);
        }

        /// <summary>
        /// Reads and decodes the run-length-encoded pixel data for each frame in a CFA image.
        /// </summary>
        /// <param name="reader">Reader opened against raw image record data (inc. header)</param>
        /// <param name="header">Previously decoded image header</param>
        /// <returns>Image pixel data, decoded from RLE</returns>
        public static byte[] ReadImageData(ref BinaryReader reader, CfaFile.CFAHeader header)
        {
            // Create buffer to hold extracted image data
            byte[] imageData = new byte[header.WidthUncompressed * header.Height * header.FrameCount];

            // Extract image data from RLE
            // Image data is a series of sequential frames
            reader.BaseStream.Position = header.HeaderSize;
            BinaryWriter writer = new BinaryWriter(new MemoryStream(imageData));
            ReadRleData(ref reader, header.WidthCompressed * header.Height * header.FrameCount, ref writer);
            writer.Close();

            return imageData;
        }

        /// <summary>
        /// Converts run-length-encoded image record data into an array of bitmapped frames.
        /// </summary>
        /// <param name="rawData">Full raw image record from archive (inc. header)</param>
        /// <param name="palette">Palette to apply</param>
        /// <returns>Array of frame bitmaps, ready for display</returns>
        public static DFBitmap[] ReadFrames(byte[] rawData, DFPalette palette)
        {
            CfaFile.CFAHeader header = ReadHeader(rawData);
            return ReadFrames(rawData, header, palette);
        }

        /// <summary>
        /// Converts run-length-encoded image record data into an array of bitmapped frames.
        /// </summary>
        /// <param name="rawData">Full raw image record from archive (inc. header)</param>
        /// <param name="header">Previously decoded file header</param>
        /// <param name="palette">Palette to apply</param>
        /// <returns>Array of frame bitmaps, ready for display</returns>
        public static DFBitmap[] ReadFrames(byte[] rawData, CfaFile.CFAHeader header, DFPalette palette)
        {
            List<DFBitmap> frames = new List<DFBitmap>();
            byte[] decodedData = ReadImageData(rawData, header);
            int frameLength = header.WidthCompressed * header.Height;

            for (int x = 0; x < decodedData.Length; x += frameLength)
            {
                byte[] frameData = new byte[frameLength];
                Array.Copy(decodedData, x, frameData, 0, frameLength);
                frames.Add(new DFBitmap
                {
                    Width = header.WidthUncompressed,
                    Height = header.Height,
                    Data = frameData,
                    Palette = palette
                });
            }

            return frames.ToArray();
        }

        /// <summary>
        /// Decodes run-length encoded (compressed) data from source reader to destination writer.
        /// </summary>
        /// <param name="reader">Source reader positioned at start of input data.</param>
        /// <param name="length">Length of source data.</param>
        /// <param name="writer">Destination writer positioned at start of output data.</param>
        /// <returns>True if succeeded, otherwise false.</returns>
        public static void ReadRleData(ref BinaryReader reader, int length, ref BinaryWriter writer)
        {
            // Read image bytes
            byte pixel = 0;
            byte code = 0;
            int pos = 0;
            do
            {
                code = reader.ReadByte();
                if (code > 127)
                {
                    pixel = reader.ReadByte();
                    for (int i = 0; i < code - 127; i++)
                    {
                        writer.Write(pixel);
                        pos++;
                    }
                }
                else
                {
                    writer.Write(reader.ReadBytes(code + 1));
                    pos += (code + 1);
                }
            } while (pos < length);
        }


    }

}