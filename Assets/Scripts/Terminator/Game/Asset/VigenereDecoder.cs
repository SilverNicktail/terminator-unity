namespace TerminatorUnity.Game.Asset
{
    /// <summary>
    /// Some assets within Future Shock are encoded using a Vigenere cipher.
    /// This utility class contains the logic required to decode them.
    /// </summary>
    public static class VigenereDecoder
    {
        private static readonly byte[] ENCODING_KEY = new byte[] {
            0xDD, 0x83, 0x65, 0x57, 0xEA, 0x78, 0x08,
            0x48, 0xB8, 0x01, 0x38, 0x94, 0x08, 0xDD,
            0x3F, 0xC2, 0xBE, 0xAB, 0x76, 0xC6, 0x14
        };

        public static byte[] Decode(byte[] rawRecord)
        {
            uint keyIdx = 0;
            byte[] converted = new byte[rawRecord.Length];
            for (uint textIdx = 0; textIdx < rawRecord.Length; textIdx++)
            {
                converted[textIdx] = (byte)(rawRecord[textIdx] - ENCODING_KEY[keyIdx]);
                keyIdx = (++keyIdx >= ENCODING_KEY.Length) ? 0 : keyIdx;
            }

            return converted;
        }


    }

}