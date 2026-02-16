// Project:         Daggerfall Unity
// Copyright:       Copyright (C) 2009-2023 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

#region Using Statements
using System;
using System.IO;
using DaggerfallConnect.Utility;
using TerminatorUnity.Asset;
#endregion

namespace DaggerfallConnect.Arena2
{
    /// <summary>
    /// Connects to a *.IMG file to enumerate and extract image data. Each IMG file contains a single image.
    /// </summary>
    public class ImgFile : BaseImageFile
    {
        #region Class Variables

        /// <summary>
        /// File header.
        /// </summary>
        private ImgFileHeader header;

        /// <summary>
        /// The image data for this image. Each IMG file only contains a single image.
        /// </summary>
        private DFBitmap imgRecord = new DFBitmap();

        /// <summary>
        /// Specifies if this IMG file defines its own palette.
        /// </summary>
        private bool isPalettizedValue = false;

        /// <summary>
        /// Start of image data in file.
        /// </summary>
        private long imageDataPosition = -1;

        #endregion

        #region Class Structures

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImgFile()
        {
        }

        /// <summary>
        /// Load constructor.
        /// </summary>
        /// <param name="filePath">Absolute path to *.IMG file.</param>
        /// <param name="usage">Specify if file will be accessed from disk, or loaded into RAM.</param>
        /// <param name="readOnly">File will be read-only if true, read-write if false.</param>
        public ImgFile(string filePath, FileUsage usage, bool readOnly)
        {
            Load(filePath, usage, readOnly);
        }

        /// <summary>
        /// Load constructor with palette assignment.
        /// Some IMG files contain palette information, this will overwrite the specified palette.
        /// </summary>
        /// <param name="filePath">Absolute path to *.IMG file.</param>
        /// <param name="usage">Specify if file will be accessed from disk, or loaded into RAM.</param>
        /// <param name="palette">Palette to use when building images.</param>
        /// <param name="readOnly">File will be read-only if true, read-write if false.</param>
        public ImgFile(string filePath, FileUsage usage, DFPalette palette, bool readOnly)
        {
            myPalette = palette;
            Load(filePath, usage, readOnly);
        }

        /// <summary>
        /// Load constructor that also loads a palette.
        /// Some IMG files contain palette information, this will overwrite the specified palette.
        /// </summary>
        /// <param name="filePath">Absolute path to *.IMG file.</param>
        /// <param name="usage">Specify if file will be accessed from disk, or loaded into RAM.</param>
        /// <param name="paletteFilePath">Absolute path to Daggerfall palette file.</param>
        /// <param name="readOnly">File will be read-only if true, read-write if false.</param>
        public ImgFile(string filePath, FileUsage usage, string paletteFilePath, bool readOnly)
        {
            LoadPalette(paletteFilePath);
            Load(filePath, usage, readOnly);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies if this IMG file defines its own palette.
        /// </summary>
        public bool IsPalettized
        {
            get { return isPalettizedValue; }
        }

        /// <summary>
        /// IMG files use a variety of palettes. This property returns the correct palette filename to use for this image.
        ///  Palettized images (check IsPalettized flag) will return String.Emtpy for the palette filename.
        /// </summary>
        public override string PaletteName
        {
            get
            {
                // No palette if file is palettized
                if (IsPalettized)
                    return string.Empty;

                string fn = Path.GetFileName(managedFile.FilePath);

                // Handle special palettes
                if (fn.Substring(0, 4) == "FMAP")
                    return "FMAP_PAL.COL";
                else if (fn.Substring(0, 4) == "NITE")
                    return "NIGHTSKY.COL";

                // Select palette based on filename
                switch (fn)
                {
                    case "DANK02I0.IMG":
                        return "DANKBMAP.COL";
                    case "TMAP00I0.IMG":
                        return "MAP.PAL";
                    default:
                        return "ART_PAL.COL";
                }
            }
        }

        /// <summary>
        /// Number of image records in this Img file.
        /// </summary>
        public override int RecordCount
        {
            get
            {
                if (managedFile.FilePath == string.Empty)
                    return 0;
                else
                    return 1;
            }

        }

        /// <summary>
        /// Description of this file (always "IMG File" as the game files contain no text descriptions for this file type).
        /// </summary>
        public override string Description
        {
            get { return "IMG File"; }
        }

        /// <summary>
        /// Gets XOffset and YOffset from image header.
        /// </summary>
        public DFPosition ImageOffset
        {
            get { return new DFPosition(header.XOffset, header.YOffset); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets unsupported filenames. The three IMG filenames returned have no image data or are otherwise invalid.
        /// </summary>
        /// <returns>Array of unsupported filenames.</returns>
        public string[] UnsupportedFilenames()
        {
            string[] names = new string[3];
            names[0] = "FMAP0I00.IMG";
            names[1] = "FMAP0I01.IMG";
            names[2] = "FMAP0I16.IMG";
            return names;
        }

        /// <summary>
        /// Tests if a filename is supported.
        /// </summary>
        /// <param name="filename">Name of *.IMG file.</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsFilenameSupported(string filename)
        {
            // Look for filename in list of unsupported filenames
            string[] names = UnsupportedFilenames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == filename)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Loads an IMG file.
        /// </summary>
        /// <param name="filePath">Absolute path to *.IMG file</param>
        /// <param name="usage">Specify if file will be accessed from disk, or loaded into RAM.</param>
        /// <param name="readOnly">File will be read-only if true, read-write if false.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public override bool Load(string filePath, FileUsage usage, bool readOnly)
        {
            // Exit if this file already loaded
            if (managedFile.FilePath == filePath)
                return true;

            // Validate filename
            string fn = Path.GetFileName(filePath);
            if (!fn.EndsWith(".IMG", StringComparison.InvariantCultureIgnoreCase))
                return false;

            // Handle unsupported files
            if (!IsFilenameSupported(fn))
            {
                Console.WriteLine(string.Format("{0} is unsupported.", fn));
                return false;
            }

            // Load file
            if (!managedFile.Load(filePath, usage, readOnly))
                return false;

            // Read file
            if (!Read())
                return false;

            return true;
        }

        /// <summary>
        /// Gets bitmap data as indexed 8-bit byte array.
        /// </summary>
        /// <returns>DFBitmap object.</returns>
        public DFBitmap GetDFBitmap()
        {
            // Read image data
            if (!ReadImageData())
                return new DFBitmap();

            return imgRecord;
        }

        /// <summary>
        /// Gets bitmap data as indexed 8-bit byte array for specified record and frame.
        ///  This method is an override for abstract method in parent class DFImageFile.
        ///  As IMG files only have 1 image the Record and Frame indices must always be 0
        ///  when calling this method.
        /// </summary>
        /// <param name="record">Index of record. Must be 0.</param>
        /// <param name="frame">Index of frame. Must be 0.</param>
        /// <returns>DFBitmap object.</returns>
        public override DFBitmap GetDFBitmap(int record, int frame)
        {
            // Validate
            if (0 != record || 0 != frame)
                return new DFBitmap();

            return GetDFBitmap();
        }

        /// <summary>
        /// Gets number of frames in specified record.
        /// </summary>
        /// <param name="record">Index of record. Must be 0 for Img files.</param>
        /// <returns>Number of frames. Always 1 for loaded Img files.</returns>
        public override int GetFrameCount(int record)
        {
            // Validate
            if (record != 0)
                return -1;

            // Read image data
            if (!ReadImageData())
                return -1;

            return 1;
        }

        /// <summary>
        /// Gets width and height of specified record. As IMG files only have 1 record,
        ///  this must be 0.
        /// </summary>
        /// <param name="record">Index of record. Must be 0.</param>
        /// <returns>DFSize object.</returns>
        public override DFSize GetSize(int record)
        {
            // Validate
            if (record != 0)
                return new DFSize(0, 0);

            return new DFSize(imgRecord.Width, imgRecord.Height);
        }

        #endregion

        #region Readers

        /// <summary>
        /// Read file.
        /// </summary>
        /// <returns>True if succeeded, otherwise false.</returns>
        private bool Read()
        {
            try
            {
                // Step through file
                BinaryReader Reader = managedFile.GetReader();
                ReadHeader(ref Reader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads file header.
        /// </summary>
        /// <param name="reader">Source reader.</param>
        private void ReadHeader(ref BinaryReader reader)
        {
            
            header = IMGParser.GetImageHeader(ref reader);
            // Store image data position separately
            // TODO: Needed? Data position is in header.
            imageDataPosition = header.DataPosition;
        }

        /// <summary>
        /// Reads image data.
        /// </summary>
        private bool ReadImageData()
        {
            // Exit if this image already read
            if (imgRecord.Data != null)
                return true;

            // Setup frame to hold extracted image
            imgRecord.Width = header.Width;
            imgRecord.Height = header.Height;
            imgRecord.Data = new byte[header.Width * header.Height];
            imgRecord.Palette = Palette;

            // Create reader
            BinaryReader Reader = managedFile.GetReader(imageDataPosition);
            imgRecord = IMGParser.GetPixelData(ref Reader, ref header, Palette);
            isPalettizedValue = header.HasEmbeddedPalette;

            return true;
        }

        #endregion
    }
}
