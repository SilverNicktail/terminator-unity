namespace XnGine
{
    /// <summary>
    /// Enumeration of available XnGine asset types, for referencing/fetching
    /// them from the asset folder.
    /// </summary>
    /// 
    /// Made this to keep the asset folder interface tight, so we don't need to
    /// have repetititve "get paths", "check existence", etc methods for each
    /// type. The Java coder in me considered having a set of interfaces called
    /// IPaletteProvider, IFactionProvider, etc, but that's over-designing.
    /// We'd end up with more interfaces than XnGine games that exist. This is
    /// still flexible and expandable.
    /// 
    /// TODO: Flesh out with all asset types as I learn about them.
    public enum AssetType
    {
        COLOR_PALETTE,

        ENEMY_MODEL_ARCHIVE,

        FONT,

        HEIGHT_MAP,

        IMAGE_ARCHIVE,

        MAP_ARCHIVE,

        MAP_BLOCK_ARCHIVE,

        MISSION_ARCHIVE,

        MODEL_ARCHIVE,

        MUSIC,

        MUSIC_ARCHIVE,

        SFX_ARCHIVE,

        TEXTURE,

        VIDEO,

        WOODS_ARCHIVE
    }
}