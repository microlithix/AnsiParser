//================================================================================================
// Control Codes
//
// Some content in this document was obtained from ECMA-48. Although ECMA-48 itself is not covered
// under any explicit copyright notice, this work nevertheless abides by ECMA's "fair use" policy
// as described at https://ecma-international.org/policies/by-ipr/ecma-text-copyright-policy/.
//================================================================================================

using Microlithix.Text.Ansi.Element;

namespace Microlithix.Text.Ansi;

///----------------------------------------------------------------------------
/// <summary>
/// Standardized escape codes (ESC Fe) defined in ECMA-48.
/// </summary>
/// <remarks>
/// </remarks>
///----------------------------------------------------------------------------
public static class EscapeCode {
    /// <summary>
    /// Padding character
    /// </summary>
    public const char PAD = '\u0040';

    /// <summary>
    /// High octet preset
    /// </summary>
    public const char HOP = '\u0041';

    /// <summary>
    /// Break permitted here
    /// </summary>
    public const char BPH = '\u0042';

    /// <summary>
    /// No break here
    /// </summary>
    public const char NBH = '\u0043';

    /// <summary>
    /// Index
    /// </summary>
    public const char IND = '\u0044';

    /// <summary>
    /// Next line
    /// </summary>
    public const char NEL = '\u0045';

    /// <summary>
    /// Start of selected area
    /// </summary>
    public const char SSA = '\u0046';

    /// <summary>
    /// End of selected area
    /// </summary>
    public const char ESA = '\u0047';

    /// <summary>
    /// Character/horizontal tabulation set
    /// </summary>
    public const char HTS = '\u0048';

    /// <summary>
    /// Character/horizontal tabulation with justification
    /// </summary>
    public const char HTJ = '\u0049';

    /// <summary>
    /// Line/vertical tabulation set
    /// </summary>
    public const char VTS = '\u004a';

    /// <summary>
    /// Partial line forward/down
    /// </summary>
    public const char PLD = '\u004b';

    /// <summary>
    /// Partial line backward/up
    /// </summary>
    public const char PLU = '\u004c';

    /// <summary>
    /// Reverse line feed/index
    /// </summary>
    public const char RI  = '\u004d';

    /// <summary>
    /// Single-shift 2
    /// </summary>
    public const char SS2 = '\u004e';

    /// <summary>
    /// Single-shift 3
    /// </summary>
    public const char SS3 = '\u004f';

    /// <summary>
    /// Device control string
    /// </summary>
    /// <remarks>
    /// <para>
    /// DCS is used as the opening delimiter of a control string for
    /// device control use. The command string following may consist
    /// of bit combinations in the range 0x08 to 0x0e and 0x20 to 0x7e.
    /// The control string is closed by the terminating delimiter
    /// <c>STRING TERMINATOR</c> (<see cref="ST"/>).
    /// </para>
    /// <para>
    /// The command string represents either one or more commands for the
    /// receiving device, or one or more status reports from the sending
    /// device. The purpose and the format of the command string are specified
    /// by the most recent occurrence of <c>IDENTIFY DEVICE CONTROL STRING</c>
    /// (<see cref="ControlFunction.IDCS"/>), if any, or depend on the sending
    /// and/or the receiving device.
    /// </para>
    /// </remarks>
    public const char DCS = '\u0050';

    /// <summary>
    /// Private use 1
    /// </summary>
    public const char PU1 = '\u0051';

    /// <summary>
    /// Private use 2
    /// </summary>
    public const char PU2 = '\u0052';

    /// <summary>
    /// Set transmit state
    /// </summary>
    public const char STS = '\u0053';

    /// <summary>
    /// Cancel character
    /// </summary>
    public const char CCH = '\u0054';

    /// <summary>
    /// Message waiting
    /// </summary>
    public const char MW  = '\u0055';

    /// <summary>
    /// Start of protected area
    /// </summary>
    public const char SPA = '\u0056';

    /// <summary>
    /// End of protected area
    /// </summary>
    public const char EPA = '\u0057';

    /// <summary>
    /// Start of string
    /// </summary>
    public const char SOS = '\u0058';

    /// <summary>
    /// Single graphic character introducer
    /// </summary>
    public const char SGC = '\u0059';

    /// <summary>
    /// Single character introducer
    /// </summary>
    public const char SCI = '\u005a';

    /// <summary>
    /// Control sequence introducer
    /// </summary>
    public const char CSI = '\u005b';

    /// <summary>
    /// String terminator
    /// </summary>
    public const char ST  = '\u005c';

    /// <summary>
    /// Operating system command
    /// </summary>
    public const char OSC = '\u005d';

    /// <summary>
    /// Privacy message
    /// </summary>
    public const char PM  = '\u005e';

    /// <summary>
    /// Application program command
    /// </summary>
    public const char APC = '\u005f';
}
