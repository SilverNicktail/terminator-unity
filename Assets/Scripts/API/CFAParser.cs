using System.IO;
using DaggerfallConnect.Arena2;

namespace TerminatorUnity.Asset
{
    
    public static class CFAParser
    {
        
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

        public static byte[] ReadImageData(ref BinaryReader reader)
        {
            return ReadImageData(ref reader, ReadHeader(ref reader));
        }

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
        /// Reads RLE compressed data from source reader to destination writer.
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