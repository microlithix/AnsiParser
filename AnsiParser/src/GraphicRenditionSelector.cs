namespace Microlithix.Text.Ansi;

///----------------------------------------------------------------------------
/// <summary>
/// Standardized parameter values for selecting graphic rendition
/// modes via Select Graphic Rendition (SGR) control sequences.
/// </summary>
/// <remarks>
/// These integer parameter values represent standardized SGR functions.
/// They are defined in ECMA-48 except where otherwise noted.
/// </remarks>
///----------------------------------------------------------------------------
public static class GraphicRenditionSelector {

    /// <summary>
    /// Default Rendition (implementation defined)
    /// </summary>
    /// <remarks>
    /// Cancels the effect of any preceding occurrence of a 
    /// <see href="..\docs\ControlSequences.md#select-graphic-rendition">
    /// Select Graphic Rendition control sequence</see> in the data stream
    /// regardless of the setting of the <see cref="DeviceMode.GRCM">
    /// GRAPHIC RENDITION COMBINATION MODE (GRCM)</see>.
    /// </remarks>
    public const int Reset = 0;

    /// <summary>
    /// Bold or Increased Intensity
    /// </summary>
    public const int Bold = 1;

    /// <summary>
    /// Decreased Intensity or Second Color
    /// </summary>
    public const int Faint = 2;

    /// <summary>
    /// Italicized
    /// </summary>
    public const int Italic = 3;

    /// <summary>
    /// Singly Underlined
    /// </summary>
    public const int Underline = 4;

    /// <summary>
    /// Slowly Blinking (less than 150 per minute)
    /// </summary>
    public const int SlowBlink = 5;

    /// <summary>
    /// Rapidly Blinking (150 per minute or more)
    /// </summary>
    public const int RapidBlink = 6;

    /// <summary>
    /// Negative or Inverse Image
    /// </summary>
    public const int Inverse = 7;

    /// <summary>
    /// Concealed or Hidden Characters
    /// </summary>
    public const int Hide = 8;

    /// <summary>
    /// Crossed-Out (characters are still legible)
    /// </summary>
    public const int StrikeThrough = 9;

    /// <summary>
    /// Primary (default) Font
    /// </summary>
    public const int DefaultFont = 10;

    /// <summary>
    /// First Alternative Font
    /// </summary>
    public const int AltFont1 = 11;

    /// <summary>
    /// Second Alternative Font
    /// </summary>
    public const int AltFont2 = 12;

    /// <summary>
    /// Third Alternative Font
    /// </summary>
    public const int AltFont3 = 13;

    /// <summary>
    /// Fourth Alternative Font
    /// </summary>
    public const int AltFont4 = 14;

    /// <summary>
    /// Fifty Alternative Font
    /// </summary>
    public const int AltFont5 = 15;

    /// <summary>
    /// Sixty Alternative Font
    /// </summary>
    public const int AltFont6 = 16;

    /// <summary>
    /// Seventh Alternative Font
    /// </summary>
    public const int AltFont7 = 17;

    /// <summary>
    /// Eighth Alternative Font
    /// </summary>
    public const int AltFont8 = 18;

    /// <summary>
    /// Nineth Alternative Font
    /// </summary>
    public const int AltFont9 = 19;

    /// <summary>
    /// Fraktur (Gothic)
    /// </summary>
    public const int Fraktur = 20;

    /// <summary>
    /// Doubly Underlined
    /// </summary>
    public const int DoubleUnderline = 21;

    /// <summary>
    /// Normal Color or Normal Intensity (neither bold nor faint)
    /// </summary>
    public const int NormalIntensity = 22;

    /// <summary>
    /// Not Italicized, Not Fraktur
    /// </summary>
    public const int NotItalic = 23;

    /// <summary>
    /// Not Underlined (neither singly nor doubly)
    /// </summary>
    public const int NotUnderlined = 24;

    /// <summary>
    /// Steady (not blinking)
    /// </summary>
    public const int NotBlinking = 25;

    /// <summary>
    /// Proportional Spacing (as specified in CCITT Recommendation T.61)
    /// </summary>
    public const int ProportionalSpacing = 26;

    /// <summary>
    /// Positive (not inverse) Image
    /// </summary>
    public const int NotInverse = 27;

    /// <summary>
    /// Revealed (not hidden) Characters
    /// </summary>
    public const int NotHidden = 28;

    /// <summary>
    /// Not Crossed Out
    /// </summary>
    public const int NotStrikeThrough = 29;

    /// <summary>
    /// Black Character Display
    /// </summary>
    public const int ForegroundBlack = 30;

    /// <summary>
    /// Red Character Display
    /// </summary>
    public const int ForegroundRed = 31;

    /// <summary>
    /// Green Character Display
    /// </summary>
    public const int ForegroundGreen = 32;

    /// <summary>
    /// Yellow Character Display
    /// </summary>
    public const int ForegroundYellow = 33;

    /// <summary>
    /// Blue Character Display
    /// </summary>
    public const int ForegroundBlue = 34;

    /// <summary>
    /// Magenta Character Display
    /// </summary>
    public const int ForegroundMagenta = 35;

    /// <summary>
    /// Cyan Character Display
    /// </summary>
    public const int ForegroundCyan = 36;

    /// <summary>
    /// White Character Display
    /// </summary>
    public const int ForegroundWhite = 37;

    /// <summary>
    /// Sets the foreground color as specified in ISO 8613-6
    /// [CCITT Recommendation T.416].
    /// </summary>
    /// <remarks>
    /// See <see href="../docs/ControlSequences.md#setforegroundcolor-and-setbackgroundcolor">
    /// SetForegroundColor and SetBackgroundColor</see> for full usage details.
    /// </remarks>
    public const int SetForegroundColor = 38;

    /// <summary>
    /// Default Character Display Color (implementation-defined)
    /// </summary>
    public const int ForegroundDefault = 39;

    /// <summary>
    /// Black Background
    /// </summary>
    public const int BackgroundBlack = 40;

    /// <summary>
    /// Red Background
    /// </summary>
    public const int BackgroundRed = 41;

    /// <summary>
    /// Green Background
    /// </summary>
    public const int BackgroundGreen = 42;

    /// <summary>
    /// Yellow Background
    /// </summary>
    public const int BackgroundYellow = 43;

    /// <summary>
    /// Blue Background
    /// </summary>
    public const int BackgroundBlue = 44;

    /// <summary>
    /// Magenta Background
    /// </summary>
    public const int BackgroundMagenta = 45;

    /// <summary>
    /// Cyan Background
    /// </summary>
    public const int BackgroundCyan = 46;

    /// <summary>
    /// White Background
    /// </summary>
    public const int BackgroundWhite = 47;

    /// <summary>
    /// Sets the background color as specified in ISO 8613-6
    /// [CCITT Recommendation T.416].
    /// </summary>
    /// <remarks>
    /// See <see href="../docs/ControlSequences.md#setforegroundcolor-and-setbackgroundcolor">
    /// SetForegroundColor and SetBackgroundColor</see> for full usage details.
    /// </remarks>
    public const int SetBackgroundColor = 48;

    /// <summary>
    /// Default Background Color (implementation-defined)
    /// </summary>
    public const int BackgroundDefault = 49;

    /// <summary>
    /// Cancel Proportional Spacing (as specified in CCITT Recommendation T.61)
    /// </summary>
    public const int NotProportionalSpacing = 50;

    /// <summary>
    /// Framed
    /// </summary>
    public const int Framed = 51;

    /// <summary>
    ///  Encircled
    /// </summary>
    public const int Encircled = 52;

    /// <summary>
    /// Overlined
    /// </summary>
    public const int Overlined = 53;

    /// <summary>
    /// Not Framed and Not Encircled
    /// </summary>
    public const int NotFramedOrEncircled = 54;

    /// <summary>
    /// Not Overlined
    /// </summary>
    public const int NotOverlined = 55;

    /// <summary>
    /// Reserved for Future Standardization
    /// </summary>
    public const int Reserved1 = 56;

    /// <summary>
    /// Reserved for Future Standardization
    /// </summary>
    public const int Reserved2 = 57;

    /// <summary>
    /// Reserved for Future Standardization
    /// </summary>
    public const int Reserved3 = 58;

    /// <summary>
    /// Reserved for Future Standardization
    /// </summary>
    public const int Reserved4 = 59;

    /// <summary>
    /// Ideogram Underline or Right Side Line
    /// </summary>
    public const int IdeogramUnderline = 60;

    /// <summary>
    /// Ideogram Double Underline or Double Line on the Right Side
    /// </summary>
    public const int IdeogramDoubleUnderline = 61;

    /// <summary>
    /// Ideogram Overline or Left Side Line
    /// </summary>
    public const int IdeogramOverline = 62;

    /// <summary>
    /// Ideogram Double Overline or Double Line on the Left Side
    /// </summary>
    public const int IdeogramDoubleOverline = 63;

    /// <summary>
    /// Ideogram Stress Marking
    /// </summary>
    public const int IdeogramStressMark = 64;

    /// <summary>
    /// Cancel Effects of all Ideogram Rendition Aspects
    /// </summary>
    public const int IdeogramCancel = 65;

    /// <summary>
    /// Bright Black Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightBlack = 90;

    /// <summary>
    /// Bright Red Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightRed = 91;

    /// <summary>
    /// Bright Green Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightGreen = 92;

    /// <summary>
    /// Bright Yellow Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightYellow = 93;

    /// <summary>
    /// Bright Blue Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightBlue = 94;

    /// <summary>
    /// Bright Magenta Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightMagenta = 95;

    /// <summary>
    /// Bright Cyan Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightCyan = 96;

    /// <summary>
    /// Bright White Character Display
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int ForegroundBrightWhite = 97;

    /// <summary>
    /// Bright Black Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightBlack = 100;

    /// <summary>
    /// Bright Red Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightRed = 101;

    /// <summary>
    /// Bright Green Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightGreen = 102;

    /// <summary>
    /// Bright Yellow Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightYellow = 103;

    /// <summary>
    /// Bright Blue Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightBlue = 104;

    /// <summary>
    /// Bright Magenta Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightMagenta = 105;

    /// <summary>
    /// Bright Cyan Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightCyan = 106;

    /// <summary>
    /// Bright White Background
    /// </summary>
    /// <remarks>
    /// Non-standard. Originally implemented by aixterm.
    /// </remarks>
    public const int BackgroundBrightWhite = 107;
}

/// <summary>
/// Indicates how a colour is to be specified, in accordance with
/// ISO 8613-6 [CCITT Recommendation T.416]. The specification includes
/// five formats in which a colour can be specified.
/// </summary>
public static class ColorSelectionFormat {
    /// <summary>
    /// Implementation-Defined Color Format
    /// </summary>
    /// <remarks>
    /// Only applicable for the character foreground color.
    /// </remarks>
    public const int ImplementationDefined = 0;

    /// <summary>
    /// Transparent Color
    /// </summary>
    public const int Transparent = 1;

    /// <summary>
    /// Direct Color in RGB Space
    /// </summary>
    public const int RGB = 2;

    /// <summary>
    /// Direct Color in CMY Space
    /// </summary>
    public const int CMY = 3;

    /// <summary>
    /// Direct Color in CMYK Space
    /// </summary>
    public const int CMYK = 4;

    /// <summary>
    /// Indexed Color
    /// </summary>
    /// <remarks>
    /// Usually an index into a 256-element color palette.
    /// </remarks>
    public const int PaletteIndex = 5;
}