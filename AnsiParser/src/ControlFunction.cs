using Microlithic.Text.Ansi.Element;

namespace Microlithic.Text.Ansi;

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
/// <see cref="AnsiPrivateControlSequence"/> records.
/// </remarks>
///----------------------------------------------------------------------------
public static class ControlFunction {
    // The following strings defined in ECMA-48 identify standardized control
    // sequence functions. These identifiers will be found in the Function
    // property of AnsiControlSequence records generated by the parser.

    /// <summary>
    /// Insert Character
    /// </summary>
    public const string ICH  = "@"; // (0x40) Insert character

    /// <summary>
    /// Cursor Up
    /// </summary>
    public const string CUU  = "A"; // (0x41) Cursor up

    /// <summary>
    /// Cursor Down
    /// </summary>
    public const string CUD  = "B"; // (0x42) Cursor down

    /// <summary>
    /// Cursor Forward (Cursor Right)
    /// </summary>
    public const string CUF  = "C"; // (0x43) Cursor forward

    /// <summary>
    /// Cursor Backward (Cursor Left)
    /// </summary>
    public const string CUB  = "D"; // (0x44) Cursor back

    /// <summary>
    /// Cursor Next Line
    /// </summary>
    public const string CNL  = "E"; // (0x45) Cursor next line

    /// <summary>
    /// Cursor Preceding Line
    /// </summary>
    public const string CPL  = "F"; // (0x46) Cursor preceding line

    /// <summary>
    /// Cursor Horizontal Absolute (Cursor Character Absolute)
    /// </summary>
    public const string CHA  = "G"; // (0x47) Cursor horizontal absolute

    /// <summary>
    /// Cursor Position
    /// </summary>
    public const string CUP  = "H"; // (0x48) Cursor position

    /// <summary>
    /// Cursor Horizontal Tabulation (Cursor Forward Tabulation)
    /// </summary>
    public const string CHT  = "I"; // (0x49) Cursor horizontal tabulation

    /// <summary>
    /// Erase in Display (Erase in Page)
    /// </summary>
    public const string ED   = "J"; // (0x4A) Erase in display

    /// <summary>
    /// Erase in Line
    /// </summary>
    public const string EL   = "K"; // (0x4B) Erase in line

    /// <summary>
    /// Insert Line
    /// </summary>
    public const string IL   = "L"; // (0x4C) Insert line

    /// <summary>
    /// Delete Line
    /// </summary>
    public const string DL   = "M"; // (0x4D) Delete line

    /// <summary>
    /// Erase in Field
    /// </summary>
    public const string EF   = "N"; // (0x4E) Erase in field

    /// <summary>
    /// Erase in Area
    /// </summary>
    public const string EA   = "O"; // (0x4F) Erase in area

    /// <summary>
    /// Delete Character
    /// </summary>
    public const string DCH  = "P"; // (0x50) Delete character

    /// <summary>
    /// Select Editing Extent
    /// </summary>
    public const string SEE  = "Q"; // (0x51) Select editing extent

    /// <summary>
    /// Cursor Position Report (Active Position Report)
    /// </summary>
    public const string CPR  = "R"; // (0x52) Cursor position report

    /// <summary>
    /// Scroll Up
    /// </summary>
    public const string SU   = "S"; // (0x53) Scroll up

    /// <summary>
    /// Scroll Down
    /// </summary>
    public const string SD   = "T"; // (0x54) Scroll down

    /// <summary>
    /// Next Page
    /// </summary>
    public const string NP   = "U"; // (0x55) Next page

    /// <summary>
    /// Preceding Page
    /// </summary>
    public const string PP   = "V"; // (0x56) Preceding page

    /// <summary>
    /// Cursor Tabulation Control
    /// </summary>
    public const string CTC  = "W"; // (0x57) Cursor tabulation control

    /// <summary>
    /// Erase Character
    /// </summary>
    public const string ECH  = "X"; // (0x58) Erase character

    /// <summary>
    /// Cursor Vertical Tabulation (Cursor Line Tabulation)
    /// </summary>
    public const string CVT  = "Y"; // (0x59) Cursor vertical tabulation

    /// <summary>
    /// Cursor Backward Tabulation
    /// </summary>
    public const string CBT  = "Z"; // (0x5A) Cursor backward tabulation

    /// <summary>
    /// Start Reversed String
    /// </summary>
    public const string SRS  = "["; // (0x5B) Start reversed string

    /// <summary>
    /// Parallel Texts
    /// </summary>
    public const string PTX = "\\"; // (0x5C) Parallel texts

    /// <summary>
    /// Start Directed String
    /// </summary>
    public const string SDS  = "]"; // (0x5D) Start directed string

    /// <summary>
    /// Select Implicit Movement Direction
    /// </summary>
    public const string SIMD = "^"; // (0x5E) Select implicit movement direction

    /// <summary>
    /// Horizontal Position Absolute (Character Position Absolute)
    /// </summary>
    public const string HPA  = "`"; // (0x60) Horizontal position absolute

    /// <summary>
    /// Horizontal Position Forward (Character Position Forward)
    /// </summary>
    public const string HPR  = "a"; // (0x61) Horizontal position forward

    /// <summary>
    /// Repeat
    /// </summary>
    public const string REP  = "b"; // (0x62) Repeat

    /// <summary>
    /// Device Attributes
    /// </summary>
    public const string DA   = "c"; // (0x63) Device attributes

    /// <summary>
    /// Vertical Position Absolute (Line Position Absolute)
    /// </summary>
    public const string VPA  = "d"; // (0x64) Vertical position absolute

    /// <summary>
    /// Vertical Position Forward (Line Position Forward)
    /// </summary>
    public const string VPR  = "e"; // (0x65) Vertical position forward

    /// <summary>
    /// Horizontal and Vertical Position (Character and Line Position)
    /// </summary>
    public const string HVP  = "f"; // (0x66) Horizontal vertical position

    /// <summary>
    /// Tabulation Clear
    /// </summary>
    public const string TBC  = "g"; // (0x67) Tabulation clear

    /// <summary>
    /// Set Mode
    /// </summary>
    public const string SM   = "h"; // (0x68) Set mode

    /// <summary>
    /// Media Copy
    /// </summary>
    public const string MC   = "i"; // (0x69) Media copy

    /// <summary>
    /// Horizontal Position Backward (Character Position Backward)
    /// </summary>
    public const string HPB  = "j"; // (0x6A) Horizontal position backward

    /// <summary>
    /// Vertical Position Backward (Line Position Backward)
    /// </summary>
    public const string VPB  = "k"; // (0x6B) Vertical position backward

    /// <summary>
    /// Reset Mode
    /// </summary>
    public const string RM   = "l"; // (0x6C) Reset mode

    /// <summary>
    /// Select Graphic Rendition
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
    public const string SGR  = "m"; // (0x6D) Select graphic rendition

    /// <summary>
    /// Device Status Report
    /// </summary>
    public const string DSR  = "n"; // (0x6E) Device status report

    /// <summary>
    /// Define Area Qualification
    /// </summary>
    public const string DAQ  = "o"; // (0x6F) Define area qualification

    /// <summary>
    /// Scroll Left
    /// </summary>
    public const string SL   = " @"; // (0x20)(0x40) Scroll left

    /// <summary>
    /// Scroll Right
    /// </summary>
    public const string SR   = " A"; // (0x20)(0x41) Scroll right

    /// <summary>
    /// Graphic Size Modification
    /// </summary>
    public const string GSM  = " B"; // (0x20)(0x42) Graphic size modification

    /// <summary>
    /// Graphic Size Selection
    /// </summary>
    public const string GSS  = " C"; // (0x20)(0x43) Graphic size selection

    /// <summary>
    /// Font Selection
    /// </summary>
    public const string FNT  = " D"; // (0x20)(0x44) Font selection

    /// <summary>
    /// Thin Space Specification
    /// </summary>
    public const string TSS  = " E"; // (0x20)(0x45) Thin space specification

    /// <summary>
    /// Justify
    /// </summary>
    public const string JFY  = " F"; // (0x20)(0x46) Justify

    /// <summary>
    /// Spacing Increment
    /// </summary>
    public const string SPI  = " G"; // (0x20)(0x47) Spacing increment

    /// <summary>
    /// QUAD
    /// </summary>
    public const string QUAD = "H "; // (0x20)(0x48) Quad

    /// <summary>
    /// Select Size Unit
    /// </summary>
    public const string SSU  = " I"; // (0x20)(0x49) Select size unit

    /// <summary>
    /// Page Format Selection
    /// </summary>
    public const string PFS  = " J"; // (0x20)(0x4A) Page format selection

    /// <summary>
    /// Select Horizontal Spacing (Select Character Spacing)
    /// </summary>
    public const string SHS  = " K"; // (0x20)(0x4B) Select horizontal spacing

    /// <summary>
    /// Select Vertical Spacing (Select Line Spacing)
    /// </summary>
    public const string SVS  = " L"; // (0x20)(0x4C) Select vertical spacing

    /// <summary>
    /// Identify Graphic Subrepertoire
    /// </summary>
    public const string IGS  = " M"; // (0x20)(0x4D) Identify graphic subrepertoire

    /// <summary>
    /// Identify Device Control String
    /// </summary>
    public const string IDCS = " O"; // (0x20)(0x4F) Identify device control string

    /// <summary>
    /// Page Position Absolute
    /// </summary>
    public const string PPA  = " P"; // (0x20)(0x50) Page position absolute

    /// <summary>
    /// Page Position Right (Page Position Forward)
    /// </summary>
    public const string PPR  = " Q"; // (0x20)(0x51) Page position forward

    /// <summary>
    /// Page Position Backward
    /// </summary>
    public const string PPB  = " R"; // (0x20)(0x52) Page position backward

    /// <summary>
    /// Select Presentation Directions
    /// </summary>
    public const string SPD  = " S"; // (0x20)(0x53) Select presentation directions

    /// <summary>
    /// Dimension Text Area
    /// </summary>
    public const string DTA  = " T"; // (0x20)(0x54) Dimension text area

    /// <summary>
    /// Set Line Home
    /// </summary>
    public const string SLH  = " U"; // (0x20)(0x55) Set line home

    /// <summary>
    /// Set Line Limit
    /// </summary>
    public const string SLL  = " V"; // (0x20)(0x56) Set line limit

    /// <summary>
    /// Function Key
    /// </summary>
    public const string FNK  = " W"; // (0x20)(0x57) Function key

    /// <summary>
    /// Select Print Quality and Rapidity
    /// </summary>
    public const string SPQR = " X"; // (0x20)(0x58) Select print quality and rapidity

    /// <summary>
    /// Sheet Eject and Feed
    /// </summary>
    public const string SEF  = " Y"; // (0x20)(0x59) Sheet eject and feed

    /// <summary>
    /// Presentation Expand or Contract
    /// </summary>
    public const string PEC  = " Z"; // (0x20)(0x5A) Presentation Expand or Contract

    /// <summary>
    /// Set Space Width
    /// </summary>
    public const string SSW  = " ["; // (0x20)(0x5B) Set space width

    /// <summary>
    /// Set Additional Character Separation
    /// </summary>
    public const string SACS =" \\"; // (0x20)(0x5C) Set additional character separation

    /// <summary>
    /// Select Alternative Presentation Variants
    /// </summary>
    public const string SAPV = " ]"; // (0x20)(0x5D) Select alternative presentation variants

    /// <summary>
    /// Selective Tabulation
    /// </summary>
    public const string STAB = " ^"; // (0x20)(0x5E) Selective tabulation

    /// <summary>
    /// Graphic Character Combination
    /// </summary>
    public const string GCC  = " _"; // (0x20)(0x5F) Graphic character combination

    /// <summary>
    /// Tabulation Aligned Trailing Edge
    /// </summary>
    public const string TATE = " `"; // (0x20)(0x60) Tabulation aligned trailing edge

    /// <summary>
    /// Tabulation Aligned Leading Edge
    /// </summary>
    public const string TALE = " a"; // (0x20)(0x61) Tabulation aligned leading edge

    /// <summary>
    /// Tabulation Aligned Centred
    /// </summary>
    public const string TAC  = " b"; // (0x20)(0x62) Tabulation aligned centred

    /// <summary>
    /// Tabulation Centred on Character
    /// </summary>
    public const string TCC  = " c"; // (0x20)(0x63) Tabulation centred on character

    /// <summary>
    /// Tabulation Stop Remove
    /// </summary>
    public const string TSR  = " d"; // (0x20)(0x64) Tabulation stop remove

    /// <summary>
    /// Select Character Orientation
    /// </summary>
    public const string SCO  = " e"; // (0x20)(0x65) Select character orientation

    /// <summary>
    /// Set Reduced Character Separation
    /// </summary>
    public const string SRCS = " f"; // (0x20)(0x66) Set reduced character separation

    /// <summary>
    /// Set Character Spacing
    /// </summary>
    public const string SCS  = " g"; // (0x20)(0x67) Set character spacing

    /// <summary>
    /// Set Line Spacing
    /// </summary>
    public const string SLS  = " h"; // (0x20)(0x68) Set line spacing

    /// <summary>
    /// Set Page Home
    /// </summary>
    public const string SPH  = " i"; // (0x20)(0x69) Set page home

    /// <summary>
    /// Set Page Limit
    /// </summary>
    public const string SPL  = " j"; // (0x20)(0x6A) Set page limit

    /// <summary>
    /// Select Character Path
    /// </summary>
    public const string SCP  = " k"; // (0x20)(0x6B) Select character path
}
