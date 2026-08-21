using System;
using System.Text;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using TerminatorUnity.Game.Asset;
using UnityEngine;

namespace TerminatorUnity
{
    public class FSTextFile
    {
        private readonly BsaFile textFile = new BsaFile();

        private string[] availableRecords;

        public struct TextEntry
        {
            public string filename;

            public byte[] bytes;
        }

        public FSTextFile(string filePath, FileUsage usage, bool readOnly)
        {
            LoadFile(filePath, usage, readOnly);
        }

        private void LoadFile(string filePath, FileUsage usage, bool readOnly)
        {
            Debug.Log($"Attempting to load text file at {filePath}");

            bool success = textFile.Load(filePath, usage, readOnly, typedIndices: false);

            if (success)
            {
                availableRecords = new string[textFile.Count];
                Debug.Log($"Loaded {textFile.Count} text records.");

                for (int x = 0; x < textFile.Count; x++)
                {
                    string recordName = textFile.GetRecordName(x);
                    availableRecords[x] = recordName;
                }

            }
        }

        public string[] GetAvailableRecords()
        {
            return (this.availableRecords != null) ? this.availableRecords : new string[0];
        }

        public byte[] GetTextRecordRaw(uint recordId)
        {
            if (recordId >= availableRecords.Length)
            {
                throw new ArgumentOutOfRangeException("recordId");
            }

            return this.textFile.GetRecordBytes(Convert.ToInt32(recordId));
        }

        public byte[] GetTextRecordRaw(string recordName)
        {
            int recordId = Array.IndexOf(availableRecords, recordName);

            if (recordId < 0)
            {
                throw new ArgumentException("Requested record name not found", "recordName");
            }

            return this.textFile.GetRecordBytes(recordId);
        }

        public string GetTextRecord(uint recordId)
        {
            return Decode(GetTextRecordRaw(recordId));
        }

        public string GetTextRecord(string recordName)
        {
            return Decode(GetTextRecordRaw(recordName));
        }

        private string Decode(byte[] rawText)
        {
            byte[] decoded = VigenereDecoder.Decode(rawText);
            return Encoding.ASCII.GetString(decoded);
        }

    }
}