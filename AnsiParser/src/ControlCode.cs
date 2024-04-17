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
/// Standardized control codes defined in ECMA-48.
/// </summary>
/// <remarks>
/// These constants include all C0 (0x00-0x1F) and C1 (0x80-0x9F) control
/// codes, plus the code 0x7F (DEL) which acts like a control code.
/// </remarks>
///----------------------------------------------------------------------------
public static class ControlCode {
    /// <summary>
    /// Null
    /// </summary>
    /// <remarks>
    /// NUL is used for media-fill or time-fill. NUL characters may be
    /// inserted into, or removed from, a data stream without affecting
    /// the information content of that stream, but such action may affect
    /// the information layout and/or the control of equipment.
    /// </remarks>
    public const char NUL = '\u0000';
    
    /// <summary>
    /// Start of heading
    /// </summary>
    /// <remarks>
    /// <para>
    /// SOH is used to indicate the beginning of a heading.
    /// </para>
    /// <para>
    /// The use of SOH is defined in ISO 1745.
    /// </para>
    /// </remarks>
    public const char SOH = '\u0001';

    /// <summary>
    /// Start of text
    /// </summary>
    /// <remarks>
    /// <para>
    /// STX is used to indicate the beginning of a text and the end
    /// of a heading.
    /// </para>
    /// <para>
    /// The use of STX is defined in ISO 1745.
    /// </para>
    /// </remarks>
    public const char STX = '\u0002';

    /// <summary>
    /// End of text
    /// </summary>
    /// <remarks>
    /// <para>
    /// ETX is used to indicate the end of a text.
    /// </para>
    /// <para>
    /// The use of ETX is defined in ISO 1745.
    /// </para>
    /// </remarks>
    public const char ETX = '\u0003';

    /// <summary>
    /// End of transmission
    /// </summary>
    /// <remarks>
    /// <para>
    /// EOT is used to indicate the conclusion of the transmission of
    /// one or more texts.
    /// </para>
    /// <para>
    /// The use of EOT is defined in ISO 1745
    /// </para>
    /// </remarks>
    public const char EOT = '\u0004';

    /// <summary>
    /// Enquiry
    /// </summary>
    /// <remarks>
    /// <para>
    /// ENQ is transmitted by a sender as a request for a response
    /// from a receiver.
    /// </para>
    /// <para>
    /// The use of ENQ is defined in ISO 1745.
    /// </para>
    /// </remarks>
    public const char ENQ = '\u0005';

    /// <summary>
    /// Acknowledge
    /// </summary>
    /// <remarks>
    /// <para>
    /// ACK is transmitted by a receiver as an affirmative response
    /// to the sender.
    /// </para>
    /// <para>
    /// The use of ACK is defined in ISO 1745.
    /// </para>
    /// </remarks>
    public const char ACK = '\u0006';

    /// <summary>
    /// Bell alert
    /// </summary>
    /// <remarks>
    /// BEL is used when there is a need to call for attention; 
    /// it may control alarm or attention devices.
    /// </remarks>
    public const char BEL = '\u0007';

    /// <summary>
    /// Backspace
    /// </summary>
    /// <remarks>
    /// <para>
    /// BS causes the active data position to be moved one character
    /// position in the data component in the direction opposite to
    /// that of the implicit movement.
    /// </para>
    /// <para>
    /// The direction of the implicit movement depends on the parameter
    /// value of <c>SELECT IMPLICIT MOVEMENT DIRECTION</c>
    /// (<see cref="ControlFunction.SIMD"/>).
    /// </para>
    /// </remarks>
    public const char BS  = '\u0008';

    /// <summary>
    /// Character/horizontal tabulation
    /// </summary>
    /// <remarks>
    /// <para>
    /// HT causes the active presentation position to be moved to the
    /// following character tabulation stop in the presentation component.
    /// </para>
    /// <para>
    /// In addition, if that following character tabulation stop has been
    /// set by <c>TABULATION ALIGN CENTRE</c> (<see cref="ControlFunction.TAC"/>),
    /// <c>TABULATION ALIGN LEADING EDGE</c> (<see cref="ControlFunction.TALE"/>),
    /// <c>TABULATION ALIGN TRAILING EDGE</c> (<see cref="ControlFunction.TATE"/>)
    /// or <c>TABULATION CENTRED ON CHARACTER</c> (<see cref="ControlFunction.TCC"/>),
    /// HT indicates the beginning of a string of text which is to be positioned
    /// within a line according to the properties of that tabulation stop. The end
    /// of the string is indicated by the next occurrence of HT or <c>CARRIAGE RETURN</c>
    /// (<see cref="ControlCode.CR"/>) or <c>NEXT LINE</c> (<see cref="ControlCode.NEL"/>)
    /// in the data stream.
    /// </para>
    /// </remarks>
    public const char HT  = '\u0009';


    /// <summary>
    /// Line feed
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the <c>DEVICE COMPONENT SELECT MODE</c> (<see cref="DeviceMode.DCSM"/>)
    /// is set to <c>PRESENTATION</c>, <c>LF</c> causes the active presentation position
    /// to be moved to the corresponding character position of the following line in
    /// the presentation component.
    /// </para>
    /// <para>
    /// If the <c>DEVICE COMPONENT SELECT MODE</c> (<see cref="DeviceMode.DCSM"/>)
    /// is set to <c>DATA</c>, <c>LF</c> causes the active data position to be moved to
    /// the corresponding character position of the following line in the data component.
    /// </para>
    /// </remarks>
    public const char LF  = '\u000A';

    /// <summary>
    /// Line/vertical tabulation
    /// </summary>
    /// <remarks>
    /// <para>
    /// </para>
    /// <para>
    /// </para>
    /// </remarks>
    public const char VT  = '\u000B';

    /// <summary>
    /// Form feed
    /// </summary>
    public const char FF  = '\u000C';

    /// <summary>
    /// Carriage return
    /// </summary>
    public const char CR  = '\u000D';

    /// <summary>
    /// Shift out
    /// </summary>
    public const char SO  = '\u000E';

    /// <summary>
    /// Shift in
    /// </summary>
    public const char SI  = '\u000F';

    /// <summary>
    /// Data link escape
    /// </summary>
    public const char DLE = '\u0010';

    /// <summary>
    /// Device control one
    /// </summary>
    public const char DC1 = '\u0011';

    /// <summary>
    /// Device control two
    /// </summary>
    public const char DC2 = '\u0012';

    /// <summary>
    /// Device control three
    /// </summary>
    public const char DC3 = '\u0013';

    /// <summary>
    /// Device control four
    /// </summary>
    public const char DC4 = '\u0014';

    /// <summary>
    /// Negative acknowledge
    /// </summary>
    public const char NAK = '\u0015';

    /// <summary>
    /// Synchronous idle
    /// </summary>
    public const char SYN = '\u0016';

    /// <summary>
    /// End of transmission block
    /// </summary>
    public const char ETB = '\u0017';

    /// <summary>
    /// Cancel
    /// </summary>
    public const char CAN = '\u0018';

    /// <summary>
    /// End of medium
    /// </summary>
    public const char EM  = '\u0019';

    /// <summary>
    /// Substitute
    /// </summary>
    public const char SUB = '\u001A';

    /// <summary>
    /// Escape
    /// </summary>
    public const char ESC = '\u001B';

    /// <summary>
    /// File separator
    /// </summary>
    public const char FS  = '\u001C';

    /// <summary>
    /// Group separator
    /// </summary>
    public const char GS  = '\u001D';

    /// <summary>
    /// Record separator
    /// </summary>
    public const char RS  = '\u001E';

    /// <summary>
    /// Unit separator
    /// </summary>
    public const char US  = '\u001F';

    /// <summary>
    /// Delete character
    /// </summary>
    /// <remarks>
    /// Not a C0 or C1 control code, but acts more
    /// like a control code than a printable character.
    /// </remarks>
    public const char DEL = '\u007F';

    // C1 control codes: 0x80-0x9F

    /// <summary>
    /// Padding character
    /// </summary>
    public const char PAD = '\u0080';

    /// <summary>
    /// High octet preset
    /// </summary>
    public const char HOP = '\u0081';

    /// <summary>
    /// Break permitted here
    /// </summary>
    public const char BPH = '\u0082';

    /// <summary>
    /// No break here
    /// </summary>
    public const char NBH = '\u0083';

    /// <summary>
    /// Index
    /// </summary>
    public const char IND = '\u0084';

    /// <summary>
    /// Next line
    /// </summary>
    public const char NEL = '\u0085';

    /// <summary>
    /// Start of selected area
    /// </summary>
    public const char SSA = '\u0086';

    /// <summary>
    /// End of selected area
    /// </summary>
    public const char ESA = '\u0087';

    /// <summary>
    /// Character/horizontal tabulation set
    /// </summary>
    public const char HTS = '\u0088';

    /// <summary>
    /// Character/horizontal tabulation with justification
    /// </summary>
    public const char HTJ = '\u0089';

    /// <summary>
    /// Line/vertical tabulation set
    /// </summary>
    public const char VTS = '\u008A';

    /// <summary>
    /// Partial line forward/down
    /// </summary>
    public const char PLD = '\u008B';

    /// <summary>
    /// Partial line backward/up
    /// </summary>
    public const char PLU = '\u008C';

    /// <summary>
    /// Reverse line feed/index
    /// </summary>
    public const char RI  = '\u008D';

    /// <summary>
    /// Single-shift 2
    /// </summary>
    public const char SS2 = '\u008E';

    /// <summary>
    /// Single-shift 3
    /// </summary>
    public const char SS3 = '\u008F';

    /// <summary>
    /// Device control string
    /// </summary>
    /// <remarks>
    /// <para>
    /// DCS is used as the opening delimiter of a control string for
    /// device control use. The command string following may consist
    /// of bit combinations in the range 0x08 to 0x0E and 0x20 to 0x7E.
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
    public const char DCS = '\u0090';

    /// <summary>
    /// Private use 1
    /// </summary>
    public const char PU1 = '\u0091';

    /// <summary>
    /// Private use 2
    /// </summary>
    public const char PU2 = '\u0092';

    /// <summary>
    /// Set transmit state
    /// </summary>
    public const char STS = '\u0093';

    /// <summary>
    /// Cancel character
    /// </summary>
    public const char CCH = '\u0094';

    /// <summary>
    /// Message waiting
    /// </summary>
    public const char MW  = '\u0095';

    /// <summary>
    /// Start of protected area
    /// </summary>
    public const char SPA = '\u0096';

    /// <summary>
    /// End of protected area
    /// </summary>
    public const char EPA = '\u0097';

    /// <summary>
    /// Start of string
    /// </summary>
    public const char SOS = '\u0098';

    /// <summary>
    /// Single graphic character introducer
    /// </summary>
    public const char SGC = '\u0099';

    /// <summary>
    /// Single character introducer
    /// </summary>
    public const char SCI = '\u009A';

    /// <summary>
    /// Control sequence introducer
    /// </summary>
    public const char CSI = '\u009B';

    /// <summary>
    /// String terminator
    /// </summary>
    public const char ST  = '\u009C';

    /// <summary>
    /// Operating system command
    /// </summary>
    public const char OSC = '\u009D';

    /// <summary>
    /// Privacy message
    /// </summary>
    public const char PM  = '\u009E';

    /// <summary>
    /// Application program command
    /// </summary>
    public const char APC = '\u009F';
}
