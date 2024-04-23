namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single control code from the parsed input.
/// </summary>
/// 
/// <remarks>
/// There are a total of 64 different <see cref="ControlCode">control codes
/// </see> defined or reserved in <see href="../docs/References.md#ecma-48">
/// ECMA-48</see>. The bit encodings for these control codes fall in the ranges
/// <c>0x00</c> ... <c>0x1f</c> (C0 codes) and <c>0x80</c> ... <c>0x9f</c> (C1 codes).
/// Except for the 7 control codes in the table below, each of these control codes
/// will result in the production of an <see cref="AnsiSolitaryControlCode"/>
/// element.
/// 
/// The following 7 control codes each require one or more additional
/// characters in order to complete the control sequence, so they are not
/// solitary control codes. Instead, once all of the required additional
/// characters have been received, the entire sequence will be reported as
/// one of the element types shown here:
/// <list type="table">
///     <listheader>
///         <term>Control Code</term>
///         <description>Element Type</description>
///     </listheader>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             (<c>0x1b</c>)
///         </term>
///         <description><see cref="AnsiEscapeSequence"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.DCS?displayProperty=nameWithType"/>
///             (<c>0x90</c>)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.SOS?displayProperty=nameWithType"/>
///             (<c>0x98</c>)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.CSI?displayProperty=nameWithType"/>
///             (<c>0x9b</c>)
///         </term>
///         <description><see cref="AnsiControlSequence"/> or
///         <see cref="AnsiPrivateControlSequence"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.OSC?displayProperty=nameWithType"/>
///             (<c>0x9d</c>)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.PM?displayProperty=nameWithType"/>
///             (<c>0x9e</c>)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.APC?displayProperty=nameWithType"/>
///             (<c>0x9f</c>)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
/// </list>
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiSolitaryControlCode : IAnsiStreamParserElement, IAnsiStringParserElement {
    /// <summary>
    /// The control code as a UTF-16 character.
    /// </summary>
    public char Code { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiSolitaryControlCode"/>
    /// instance with the specified control code.
    /// </summary>
    /// 
    /// <param name="code">
    /// A control code in the range <c>0x00</c>...<c>0x1a</c>, <c>0x1c</c>...<c>0x1f</c>,
    /// <c>0x80</c>...<c>0x8f</c>, <c>0x91</c>...<c>0x97</c>, <c>0x99</c>...<c>0x9a</c>, or <c>0x9c</c>.
    /// </param>
    public AnsiSolitaryControlCode(char code) {
        switch (code) {
            case >= '\u0000' and <= '\u001a':
            case >= '\u001c' and <= '\u001f':
            case >= '\u0080' and <= '\u008f':
            case >= '\u0091' and <= '\u0097':
            case >= '\u0099' and <= '\u009a':
            case '\u009c':
                Code = code;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(code),
                    $"The control code {code.ToHexString()} is not valid.");
        }
        Code = code;
    }
}
