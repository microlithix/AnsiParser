using Microlithix.Text.Ansi.Element;

namespace Microlithix.Text.Ansi;

///----------------------------------------------------------------------------
/// <summary>
/// Constants for indicating which control function should be
/// performed by an ANSI control sequence, as defined in ECMA-48.
/// </summary>
/// 
/// <remarks>
/// These string constants can be compared against
/// the <see cref="AnsiControlSequence.Function"/>
/// property of <see cref="AnsiControlSequence"/> and
/// <see cref="AnsiPrivateControlSequence"/> elements.
/// </remarks>
///----------------------------------------------------------------------------
public static class ControlFunction {
    /// <summary>
    /// Insert Character (`\u0040')
    /// </summary>
    public const string ICH  = "@";

    /// <summary>
    /// Cursor Up ('\u0041')
    /// </summary>
    public const string CUU  = "A";

    /// <summary>
    /// Cursor Down ('\u0042')
    /// </summary>
    public const string CUD  = "B";

    /// <summary>
    /// Cursor Forward/Right  ('\u0043')
    /// </summary>
    public const string CUF  = "C";

    /// <summary>
    /// Cursor Backward/Left ('\u0044')
    /// </summary>
    public const string CUB  = "D";

    /// <summary>
    /// Cursor Next Line ('\u0045')
    /// </summary>
    public const string CNL  = "E";

    /// <summary>
    /// Cursor Preceding Line ('\u0046')
    /// </summary>
    public const string CPL  = "F";

    /// <summary>
    /// Cursor Horizontal/Character Absolute ('\u0047')
    /// </summary>
    public const string CHA  = "G";

    /// <summary>
    /// Cursor Position ('\u0048')
    /// </summary>
    public const string CUP  = "H";

    /// <summary>
    /// Cursor Horizontal/Forward Tabulation  ('\u0049')
    /// </summary>
    public const string CHT  = "I";

    /// <summary>
    /// Erase in Display/Page ('\u004a')
    /// </summary>
    public const string ED   = "J";

    /// <summary>
    /// Erase in Line ('\u004b')
    /// </summary>
    public const string EL   = "K";

    /// <summary>
    /// Insert Line ('\u004c')
    /// </summary>
    public const string IL   = "L";

    /// <summary>
    /// Delete Line ('\u004d')
    /// </summary>
    public const string DL   = "M";

    /// <summary>
    /// Erase in Field ('\u004e')
    /// </summary>
    public const string EF   = "N";

    /// <summary>
    /// Erase in Area ('\u004f')
    /// </summary>
    public const string EA   = "O";

    /// <summary>
    /// Delete Character ('\u0050')
    /// </summary>
    public const string DCH  = "P";

    /// <summary>
    /// Select Editing Extent ('\u0051')
    /// </summary>
    public const string SEE  = "Q";

    /// <summary>
    /// Cursor/Active Position Report ('\u0052')
    /// </summary>
    public const string CPR  = "R";

    /// <summary>
    /// Scroll Up ('\u0053')
    /// </summary>
    public const string SU   = "S";

    /// <summary>
    /// Scroll Down ('\u0054')
    /// </summary>
    public const string SD   = "T";

    /// <summary>
    /// Next Page ('\u0055')
    /// </summary>
    public const string NP   = "U";

    /// <summary>
    /// Preceding Page ('\u0056')
    /// </summary>
    public const string PP   = "V";

    /// <summary>
    /// Cursor Tabulation Control ('\u0057')
    /// </summary>
    public const string CTC  = "W";

    /// <summary>
    /// Erase Character ('\u0058')
    /// </summary>
    public const string ECH  = "X";

    /// <summary>
    /// Cursor Vertical/Line Tabulation ('\u0059')
    /// </summary>
    public const string CVT  = "Y";

    /// <summary>
    /// Cursor Backward Tabulation ('\u005a')
    /// </summary>
    public const string CBT  = "Z";

    /// <summary>
    /// Start Reversed String ('\u005b')
    /// </summary>
    public const string SRS  = "[";

    /// <summary>
    /// Parallel Texts ('\u005c')
    /// </summary>
    public const string PTX = "\\";

    /// <summary>
    /// Start Directed String ('\u005d')
    /// </summary>
    public const string SDS  = "]";

    /// <summary>
    /// Select Implicit Movement Direction ('\u005e')
    /// </summary>
    public const string SIMD = "^";

    /// <summary>
    /// Horizontal/Character Position Absolute ('\u0060')
    /// </summary>
    public const string HPA  = "`";

    /// <summary>
    /// Horizontal/Character Position Forward ('\u0061')
    /// </summary>
    public const string HPR  = "a";

    /// <summary>
    /// Repeat ('\u0062')
    /// </summary>
    public const string REP  = "b";

    /// <summary>
    /// Device Attributes ('\u0063')
    /// </summary>
    public const string DA   = "c";

    /// <summary>
    /// Vertical/Line Position Absolute ('\u0064')
    /// </summary>
    public const string VPA  = "d";

    /// <summary>
    /// Vertical/Line Position Forward ('\u0065')
    /// </summary>
    public const string VPR  = "e";

    /// <summary>
    /// Horizontal/Character and Vertical/Line Position ('\u0066')
    /// </summary>
    public const string HVP  = "f";

    /// <summary>
    /// Tabulation Clear ('\u0067')
    /// </summary>
    public const string TBC  = "g";

    /// <summary>
    /// Set Mode ('\u0068')
    /// </summary>
    public const string SM   = "h";

    /// <summary>
    /// Media Copy ('\u0069')
    /// </summary>
    public const string MC   = "i";

    /// <summary>
    /// Horizontal/Character Position Backward ('\u006a')
    /// </summary>
    public const string HPB  = "j";

    /// <summary>
    /// Vertical/Line Position Backward ('\u006b')
    /// </summary>
    public const string VPB  = "k";

    /// <summary>
    /// Reset Mode ('\u006c')
    /// </summary>
    public const string RM   = "l";

    /// <summary>
    /// Select Graphic Rendition ('\u006d')
    /// </summary>
    /// <remarks>
    /// Default value: <see cref="GraphicRenditionSelector.Reset"/> (0)
    /// <para>
    /// SGR is used to establish one or more graphic rendition aspects for
    /// subsequent text. The established aspects remain in effect until the
    /// next occurrence of SGR in the data stream, depending on the setting of
    /// the <c>GRAPHIC RENDITION COMBINATION MODE</c>
    /// (<c><see cref="DeviceMode.GRCM"/></c>). Each graphic rendition
    /// aspect is specified by one or more of the following parameter
    /// values defined in <see cref="GraphicRenditionSelector"/>:
    /// </para>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Parameter Value</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Reset"/> (0)</term>
    ///         <description>
    ///         Default rendition (implementation-defined), cancels the
    ///         effect of any preceding occurrence of SGR in the data stream
    ///         regardless of the setting of the <c>GRAPHIC RENDITION
    ///         COMBINATION MODE</c> (<c><see cref="DeviceMode.GRCM"/></c>).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Bold"/> (1)</term>
    ///         <description>
    ///         Bold or increased intensity.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Faint"/> (2)</term>
    ///         <description>
    ///         Faint, decreased intensity or second colour.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Italic"/> (3)</term>
    ///         <description>
    ///         Italicized.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Underline"/> (4)</term>
    ///         <description>
    ///         Singly underlined.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.SlowBlink"/> (5)</term>
    ///         <description>
    ///         Slowly blinking (less than 150 per minute).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.RapidBlink"/> (6)</term>
    ///         <description>
    ///         Rapidly blinking (150 per minute or more).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Inverse"/> (7)</term>
    ///         <description>
    ///         Negative image.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Hide"/> (8)</term>
    ///         <description>
    ///         Concealed characters.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.StrikeThrough"/> (9)</term>
    ///         <description>
    ///         Crossed-out (characters still legible but marked as to be deleted).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.DefaultFont"/> (10)</term>
    ///         <description>
    ///         Primary (default) font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont1"/> (11)</term>
    ///         <description>
    ///         First alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont2"/> (12)</term>
    ///         <description>
    ///         Second alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont3"/> (13)</term>
    ///         <description>
    ///         Third alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont4"/> (14)</term>
    ///         <description>
    ///         Fourth alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont5"/> (15)</term>
    ///         <description>
    ///         Fifth alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont6"/> (16)</term>
    ///         <description>
    ///         Sixth alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont7"/> (17)</term>
    ///         <description>
    ///         Seventh alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont8"/> (18)</term>
    ///         <description>
    ///         Eighth alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.AltFont9"/> (19)</term>
    ///         <description>
    ///         Ninth alternative font.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.Fraktur"/> (20)</term>
    ///         <description>
    ///         Fraktur (Gothic).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.DoubleUnderline"/> (21)</term>
    ///         <description>
    ///         Doubly underlined.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NormalIntensity"/> (22)</term>
    ///         <description>
    ///         Normal color or normal intensity (neither bold nor faint).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NotItalic"/> (23)</term>
    ///         <description>
    ///         Not italicized, not fraktur.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NotUnderlined"/> (24)</term>
    ///         <description>
    ///         Not underlined (neither singly nor doubly).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NotBlinking"/> (25)</term>
    ///         <description>
    ///         Steady (not blinking).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ProportionalSpacing"/> (26)</term>
    ///         <description>
    ///         Reserved for proportional spacing as specified in CCITT Recommendation T.61.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NotInverse"/> (27)</term>
    ///         <description>
    ///         Positive image.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NotHidden"/> (28)</term>
    ///         <description>
    ///         Revealed characters.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.NotStrikeThrough"/> (29)</term>
    ///         <description>
    ///         Not crossed out.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundBlack"/> (30)</term>
    ///         <description>
    ///         Black display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundRed"/> (31)</term>
    ///         <description>
    ///         Red display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundGreen"/> (32)</term>
    ///         <description>
    ///         Green display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundYellow"/> (33)</term>
    ///         <description>
    ///         Yellow display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundBlue"/> (34)</term>
    ///         <description>
    ///         Blue display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundMagenta"/> (35)</term>
    ///         <description>
    ///         Magenta display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundCyan"/> (36)</term>
    ///         <description>
    ///         Cyan display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.ForegroundWhite"/> (37)</term>
    ///         <description>
    ///         White display.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="GraphicRenditionSelector.SetForegroundColor"/> (38)</term>
    ///         <description>
    ///         <para>
    ///         Set foreground colour as specified in ISO 8613-6 [CCITT Recommendation T.416]).
    ///         </para>
    ///         <para>
    ///         See <see cref="GraphicRenditionSelector.SetForegroundColor"/> for details.
    ///         </para>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>... (...)</term>
    ///         <description>
    ///         ...
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const string SGR  = "m";

    /// <summary>
    /// Device Status Report ('\u006e')
    /// </summary>
    public const string DSR  = "n";

    /// <summary>
    /// Define Area Qualification ('\u006f')
    /// </summary>
    public const string DAQ  = "o";

    /// <summary>
    /// Scroll Left ("\u0020\u0040")
    /// </summary>
    public const string SL   = " @";

    /// <summary>
    /// Scroll Right ("\u0020\u0041")
    /// </summary>
    public const string SR   = " A";

    /// <summary>
    /// Graphic Size Modification ("\u0020\u0042")
    /// </summary>
    public const string GSM  = " B";

    /// <summary>
    /// Graphic Size Selection ("\u0020\u0043")
    /// </summary>
    public const string GSS  = " C";

    /// <summary>
    /// Font Selection ("\u0020\u0044")
    /// </summary>
    public const string FNT  = " D";

    /// <summary>
    /// Thin Space Specification ("\u0020\u0045")
    /// </summary>
    public const string TSS  = " E";

    /// <summary>
    /// Justify ("\u0020\u0046")
    /// </summary>
    public const string JFY = " F";

    /// <summary>
    /// Spacing Increment ("\u0020\u0047")
    /// </summary>
    public const string SPI  = " G";

    /// <summary>
    /// QUAD ("\u0020\u0048")
    /// </summary>
    public const string QUAD = "H ";

    /// <summary>
    /// Select Size Unit ("\u0020\u0049")
    /// </summary>
    public const string SSU  = " I";

    /// <summary>
    /// Page Format Selection ("\u0020\u004a")
    /// </summary>
    public const string PFS  = " J";

    /// <summary>
    /// Select Horizontal/Character Spacing ("\u0020\u004b")
    /// </summary>
    public const string SHS  = " K";

    /// <summary>
    /// Select Vertical/Line Spacing ("\u0020\u004c")
    /// </summary>
    public const string SVS  = " L";

    /// <summary>
    /// Identify Graphic Subrepertoire ("\u0020\u004d")
    /// </summary>
    public const string IGS  = " M";

    /// <summary>
    /// Identify Device Control String ("\u0020\u004f")
    /// </summary>
    public const string IDCS = " O";

    /// <summary>
    /// Page Position Absolute ("\u0020\u0050")
    /// </summary>
    public const string PPA  = " P";

    /// <summary>
    /// Page Position Right/Forward ("\u0020\u0051")
    /// </summary>
    public const string PPR  = " Q";

    /// <summary>
    /// Page Position Backward ("\u0020\u0052")
    /// </summary>
    public const string PPB  = " R";

    /// <summary>
    /// Select Presentation Directions ("\u0020\u0053")
    /// </summary>
    public const string SPD  = " S";

    /// <summary>
    /// Dimension Text Area ("\u0020\u0054")
    /// </summary>
    public const string DTA  = " T";

    /// <summary>
    /// Set Line Home ("\u0020\u0055")
    /// </summary>
    public const string SLH  = " U";

    /// <summary>
    /// Set Line Limit ("\u0020\u0056")
    /// </summary>
    public const string SLL  = " V";

    /// <summary>
    /// Function Key ("\u0020\u0057")
    /// </summary>
    public const string FNK  = " W";

    /// <summary>
    /// Select Print Quality and Rapidity ("\u0020\u0058")
    /// </summary>
    public const string SPQR = " X";

    /// <summary>
    /// Sheet Eject and Feed ("\u0020\u0059")
    /// </summary>
    public const string SEF  = " Y";

    /// <summary>
    /// Presentation Expand or Contract ("\u0020\u005a")
    /// </summary>
    public const string PEC  = " Z";

    /// <summary>
    /// Set Space Width ("\u0020\u005b")
    /// </summary>
    public const string SSW  = " [";

    /// <summary>
    /// Set Additional Character Separation ("\u0020\u005c")
    /// </summary>
    public const string SACS =" \\";

    /// <summary>
    /// Select Alternative Presentation Variants ("\u0020\u005d")
    /// </summary>
    public const string SAPV = " ]";

    /// <summary>
    /// Selective Tabulation ("\u0020\u005e")
    /// </summary>
    public const string STAB = " ^";

    /// <summary>
    /// Graphic Character Combination ("\u0020\u005f")
    /// </summary>
    public const string GCC  = " _";

    /// <summary>
    /// Tabulation Aligned Trailing Edge ("\u0020\u0060")
    /// </summary>
    public const string TATE = " `";

    /// <summary>
    /// Tabulation Aligned Leading Edge ("\u0020\u0061")
    /// </summary>
    public const string TALE = " a";

    /// <summary>
    /// Tabulation Aligned Centred ("\u0020\u0062")
    /// </summary>
    public const string TAC  = " b";

    /// <summary>
    /// Tabulation Centred on Character ("\u0020\u0063")
    /// </summary>
    public const string TCC  = " c";

    /// <summary>
    /// Tabulation Stop Remove ("\u0020\u0064")
    /// </summary>
    public const string TSR  = " d";

    /// <summary>
    /// Select Character Orientation ("\u0020\u0065")
    /// </summary>
    public const string SCO  = " e";

    /// <summary>
    /// Set Reduced Character Separation ("\u0020\u0066")
    /// </summary>
    public const string SRCS = " f";

    /// <summary>
    /// Set Character Spacing ("\u0020\u0067")
    /// </summary>
    public const string SCS  = " g";

    /// <summary>
    /// Set Line Spacing ("\u0020\u0068")
    /// </summary>
    public const string SLS  = " h";

    /// <summary>
    /// Set Page Home ("\u0020\u0069")
    /// </summary>
    public const string SPH  = " i";

    /// <summary>
    /// Set Page Limit ("\u0020\u006a")
    /// </summary>
    public const string SPL  = " j";

    /// <summary>
    /// Select Character Path ("\u0020\u006b")
    /// </summary>
    public const string SCP  = " k";
}
