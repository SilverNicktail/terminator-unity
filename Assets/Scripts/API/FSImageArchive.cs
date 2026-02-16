using System;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using UnityEngine;

namespace TerminatorUnity.Asset
{
    /// <summary>
    /// An indexing/parsing wrapper for the image archive in Future Shock,
    /// usually MDMDIMGS.BSA. File usually contains IMG and CFA files.
    /// </summary>
    public class FSImageArchive
    {
        private readonly BsaFile imageArchive = new BsaFile();

        private string[] availableFiles;

        public struct TextEntry
        {
            public string filename;

            public byte[] bytes;
        }

        /// <summary>
        /// Load the image archive
        /// </summary>
        /// <param name="filePath">Path to archive file</param>
        /// <param name="usage">Affects whether archive will be fully loaded into memory</param>
        public FSImageArchive(string filePath, FileUsage usage)
        {
            LoadFile(filePath, usage);
        }

        /// <summary>
        /// Load the image archive
        /// </summary>
        /// <param name="filePath">Path to archive file</param>
        /// <param name="usage">Affects whether archive will be fully loaded into memory</param>
        private void LoadFile(string filePath, FileUsage usage)
        {
            Debug.Log($"Attempting to load image archive at {filePath}");

            bool success = imageArchive.Load(filePath, usage, readOnly: true, typedIndices: false);

            if (success)
            {
                availableFiles = new string[imageArchive.Count];
                Debug.Log($"Loaded {imageArchive.Count} image files from archive.");

                for (int x = 0; x < imageArchive.Count; x++)
                {
                    availableFiles[x] = imageArchive.GetRecordName(x);
                }

            }
        }

        /// <summary>
        /// Get a listing of every file in the archive
        /// </summary>
        /// <returns>List of filenames</returns>
        public string[] GetAvailableFiles()
        {
            return (this.availableFiles != null) ? this.availableFiles : new string[0];
        }

        /// <summary>
        /// Get a single image file by its position in the archive
        /// </summary>
        /// <param name="imagePosition">ID (position) of file</param>
        /// <returns>Raw bytes of full image file</returns>
        /// <exception cref="ArgumentOutOfRangeException">If file index passed does not exist</exception>
        public byte[] GetImageRaw(uint imagePosition)
        {
            if (imagePosition >= availableFiles.Length)
            {
                throw new ArgumentOutOfRangeException("imagePosition");
            }

            return this.imageArchive.GetRecordBytes(Convert.ToInt32(imagePosition));
        }

        /// <summary>
        /// Get a single image file by filename
        /// </summary>
        /// <param name="filename">Name of file to retrieve</param>
        /// <returns>Raw bytes of full image file</returns>
        /// <exception cref="ArgumentException">If filename is not in archive</exception>
        public byte[] GetImageRaw(string filename)
        {
            int filePos = Array.IndexOf(availableFiles, filename);

            if (filePos < 0)
            {
                throw new ArgumentException("Requested filename not found in archive", "filename");
            }

            return this.imageArchive.GetRecordBytes(filePos);
        }

        /// <summary>
        /// Get header (metadata) of an image in the archive
        /// </summary>
        /// <param name="filename">Name of file to fetch</param>
        /// <returns>Image file's header</returns>
        public BaseImageFile.ImgFileHeader GetImageHeader(string filename)
        {
            return IMGParser.GetImageHeader(GetImageRaw(filename));
        }

        /// <summary>
        /// Get pixel data of an image in the archive. If the image
        /// has an embedded palette, it will override any palette passed in.
        /// </summary>
        /// <param name="filename">Name of file to fetch</param>
        /// <param name="palette">Colour palette to apply to pixel data</param>
        /// <returns>Complete bitmap wrapper for file</returns>
        public DFBitmap GetImageData(string filename, DFPalette palette)
        {
            byte[] rawData = GetImageRaw(filename);
            BaseImageFile.ImgFileHeader header = IMGParser.GetImageHeader(rawData);
    
            return IMGParser.GetPixelData(rawData, ref header, palette);
        }

    }
}