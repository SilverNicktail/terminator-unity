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
using System.Linq;
using DaggerfallConnect.Utility;
#endregion

namespace DaggerfallConnect.Arena2
{

    /// <summary>
    /// Connects to an XnGine sound archive (DAGGER.SND, MDMDSFXS.BSA, etc) 
    /// to enumerate and extract sound data.
    /// </summary>
    public class SoundArchive
    {

        #region Class Variables

        public const int SampleRate = 11025;

        /// <summary>
        /// The BsaFile representing the sound archive
        /// </summary>
        private readonly BsaFile bsaFile = new();

        /// <summary>
        /// Array of decomposed sound records.
        /// </summary>
        internal SoundRecord[] sounds;

        private string[] recordNames;

        #endregion

        #region Class Structures

        /// <summary>
        /// Represents a single sound record.
        /// </summary>
        internal struct SoundRecord
        {
            public FileProxy MemoryFile;
            public DFSound DFSound;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Number of records in the sound archive
        /// </summary>
        public int Count
        {
            get { return bsaFile.Count; }
        }

        /// <summary>
        /// BSA file for sound effects.
        /// </summary>
        public BsaFile BsaFile
        {
            get { return bsaFile; }
        }

        public string[] RecordNames
        {
            get
            {
                return recordNames ?? (new string[0]);
            }
        }

        #endregion

        #region Static Properties

        /// <summary>
        /// Gets default DAGGER.SND filename.
        /// </summary>
        static public string Filename
        {
            get { return "DAGGER.SND"; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SoundArchive() {}

        /// <summary>
        /// Load constructor.
        /// </summary>
        /// <param name="filePath">Absolute path to DAGGER.SND.</param>
        /// <param name="usage">Determines if the BSA file will read from disk or memory.</param>
        /// <param name="readOnly">File will be read-only if true, read-write if false.</param>
        /// <param name="typedIndices">Whether BSA dictionary can have non-string indices (original v1 BSA files cannot)</param>
        public SoundArchive(string filePath, FileUsage usage, bool readOnly, bool typedIndices = true)
        {
            Load(filePath, usage, readOnly, typedIndices);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets index of sound record with specified id.
        /// </summary>
        /// <param name="id">ID of mesh.</param>
        /// <returns>Index of sound if located, or -1 if not found.</returns>
        public int GetRecordIndex(uint id)
        {
            // Otherwise find and store index by searching for id
            for (int i = 0; i < Count; i++)
            {
                if (bsaFile.GetRecordId(i) == id)
                    return i;
            }

            return -1;
        }

        public int GetRecordIndex(string name)
        {
            return Array.IndexOf(recordNames, name);
        }

        /// <summary>
        /// Load DAGGER.SND file.
        /// </summary>
        /// <param name="filePath">Absolute path to DAGGER.SND file.</param>
        /// <param name="usage">Specify if file will be accessed from disk, or loaded into RAM.</param>
        /// <param name="readOnly">File will be read-only if true, read-write if false.</param>
        /// <param name="typedIndices">Whether BSA dictionary can have non-string indices (original v1 BSA files cannot)</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Load(string filePath, FileUsage usage, bool readOnly, bool typedIndices = true)
        {
            // Validate filename
            if (!filePath.EndsWith(".SND", StringComparison.InvariantCultureIgnoreCase) &&
                !filePath.EndsWith(".BSA", StringComparison.InvariantCultureIgnoreCase))
                return false;

            // Load file
            if (!bsaFile.Load(filePath, usage, readOnly, typedIndices: typedIndices))
                return false;

            // Create records array
            sounds = new SoundRecord[bsaFile.Count];

            // Would use a map here rather than two separate structures, but
            // a string index is optional
            if (bsaFile.DirectoryType == BsaFile.DirectoryTypes.NameRecord)
            {
                recordNames = new string[bsaFile.Count];

                for (int x = 0; x < bsaFile.Count; x++)
                {
                    recordNames[x] = bsaFile.GetRecordName(x);
                }
            }

            return true;
        }

        public DFSound GetSound(string soundName)
        {
            int recordIndex = GetRecordIndex(soundName);
            if (recordIndex < 0)
            {
                throw new ArgumentException("Record name does not exist");
            }

            // Check if already loaded
            if (sounds[recordIndex].DFSound.WaveHeader != null &&
               sounds[recordIndex].DFSound.WaveData != null)
            {
                return sounds[recordIndex].DFSound;
            }

            DFSound sound;
            GetSound(recordIndex, out sound);
            return sound;
        }

        /// <summary>
        /// Get a sound from index.
        /// </summary>
        /// <param name="sound">SoundEffect.</param>
        /// <param name="soundOut">Sound data out.</param>
        /// <returns>True if successful.</returns>
        public bool GetSound(int sound, out DFSound soundOut)
        {
            soundOut = new DFSound();

            if (!IsValidIndex(sound))
                return false;

            // Just return sound if already loaded
            if (sounds[sound].DFSound.WaveHeader != null &&
                sounds[sound].DFSound.WaveData != null)
            {
                soundOut = sounds[sound].DFSound;
                return true;
            }

            // Load sound data
            sounds[sound].MemoryFile = bsaFile.GetRecordProxy(sound);
            if (sounds[sound].MemoryFile == null)
                return false;

            // Attempt to read sound
            ReadSound(sound);
            soundOut = sounds[sound].DFSound;

            return true;
        }

        /// <summary>
        /// Helper method to get an entire WAV file in a memory stream.
        /// </summary>
        /// <param name="sound">Sound index.</param>
        /// <returns>Wave file in MemoryStream.</returns>
        public MemoryStream GetStream(int sound)
        {
            // Get sound
            DFSound dfSound;
            GetSound(sound, out dfSound);
            if (dfSound.WaveHeader == null ||
                dfSound.WaveData == null)
            {
                return null;
            }

            // Create stream
            byte[] data = new byte[dfSound.WaveHeader.Length + dfSound.WaveData.Length];
            MemoryStream ms = new MemoryStream(data);

            // Write header and data
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(dfSound.WaveHeader);
            writer.Write(dfSound.WaveData);

            // Reset start position in stream
            ms.Position = 0;

            return ms;
        }

        /// <summary>
        /// Discard a sound from memory.
        /// </summary>
        /// <param name="sound">Index of sound to discard.</param>
        public void DiscardSound(int sound)
        {
            // Validate
            if (sound >= bsaFile.Count)
                return;

            // Discard memory files and other data
            if (sounds[sound].MemoryFile != null) sounds[sound].MemoryFile.Close();
            sounds[sound].MemoryFile = null;
            sounds[sound].DFSound = new DFSound();
        }

        /// <summary>
        /// Quickly check if sound index in valid range.
        /// </summary>
        /// <param name="sound">Index of sound.</param>
        /// <returns>True if index is within a valid range.</returns>
        public bool IsValidIndex(int sound)
        {
            return sound >= 0 && sound < bsaFile.Count;
        }

        #endregion

        #region Readers

        /// <summary>
        /// Read a sound.
        /// </summary>
        /// <param name="sound">Sound index.</param>
        private bool ReadSound(int sound)
        {
            try
            {
                CreatePcmHeader(sound);
                ReadWaveData(sound);
            }
            catch (Exception e)
            {
                DiscardSound(sound);
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads wave data for the sound.
        /// </summary>
        /// <param name="sound">Sound index.</param>
        private void ReadWaveData(int sound)
        {
            // The entire BSA record is just raw sound bytes
            sounds[sound].DFSound.Name = sounds[sound].MemoryFile.FileName;
            sounds[sound].DFSound.WaveData = sounds[sound].MemoryFile.Buffer;
        }

        /// <summary>
        /// Creates a PCM header for the sound, including the DATA prefix preceding raw sound bytes.
        /// </summary>
        /// <param name="sound">Sound index.</param>
        private void CreatePcmHeader(int sound)
        {
            Int32 headerLength = 44;
            Int32 dataLength = sounds[sound].MemoryFile.Length;
            Int32 fileLength = dataLength + 36;

            String sRIFF = "RIFF";
            String sWAVE = "WAVE";
            String sFmtID = "fmt ";
            String sDataID = "data";
            Int32 nFmtLength = 16;
            Int16 nFmtFormat = 1;
            Int16 nFmtChannels = 1;
            Int32 nFmtSampleRate = 11025;
            Int32 nFmtAvgBytesPerSec = 11025;
            Int16 nFmtBlockAlign = 1;
            Int16 nFmtBitsPerSample = 8;

            // Create header bytes
            byte[] header = new byte[headerLength];

            // Create memory stream and writer
            MemoryStream ms = new MemoryStream(header);
            BinaryWriter writer = new BinaryWriter(ms);

            // Write the RIFF tag and file length
            writer.Write(sRIFF.ToCharArray());
            writer.Write(fileLength);

            // Write the WAVE tag and fmt header
            writer.Write(sWAVE.ToCharArray());
            writer.Write(sFmtID.ToCharArray());

            // Write fmt information
            writer.Write(nFmtLength);
            writer.Write(nFmtFormat);
            writer.Write(nFmtChannels);
            writer.Write(nFmtSampleRate);
            writer.Write(nFmtAvgBytesPerSec);
            writer.Write(nFmtBlockAlign);
            writer.Write(nFmtBitsPerSample);

            // Write PCM data prefix
            writer.Write(sDataID.ToCharArray());
            writer.Write(dataLength);

            // Close writer
            writer.Close();

            // Assign header to sound
            sounds[sound].DFSound.WaveHeader = header;
        }

        #endregion

    }

}
