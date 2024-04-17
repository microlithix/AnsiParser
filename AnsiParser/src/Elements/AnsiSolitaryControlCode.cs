namespace Microlithic.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single control code from the parsed input.
/// </summary>
/// 
/// <remarks>
/// There are a total of 64 different <see cref="ControlCode">control codes
/// </see> defined or reserved in <see href="../docs/References.md#ecma-48">
/// ECMA-48</see>. The bit encodings for these control codes fall in the ranges
/// <c>'\u0000'</c>...<c>'\u001f'</c> (C0 codes) and <c>0x80</c> ... <c>0x9f</c> (C1 codes). Except for the 7 control
/// codes in the table below, each of these control codes will result in the
/// production of an <see cref="AnsiSolitaryControlCode"/> element record.
/// 
/// The following 7 control codes each require one or more additional
/// characters in order to complete the control sequence, so they are not
/// solitary control codes. Instead, once all of the required additional
/// characters have been received, the entire sequence will be reported as
/// one of the element types shown here:
/// <list type="table">
///     <listheader>
///         <term>Control Code</term>
///         <description>Record Type</description>
///     </listheader>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             (<c>0x1b</c>)
///         </term>
///         <description><see cref="AnsiEscapeSequence"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.DCS?displayProperty=nameWithType"/>
///             (0x90)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.SOS?displayProperty=nameWithType"/>
///             (0x98)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.CSI?displayProperty=nameWithType"/>
///             (0x9b)
///         </term>
///         <description><see cref="AnsiControlSequence"/> or
///         <see cref="AnsiPrivateControlSequence"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.OSC?displayProperty=nameWithType"/>
///             (0x9d)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.PM?displayProperty=nameWithType"/>
///             (0x9e)
///         </term>
///         <description><see cref="AnsiControlStringInitiator"/></description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithic.Text.Ansi.ControlCode.APC?displayProperty=nameWithType"/>
///             (0x9f)
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
    /// Creates a new record with the specified control code.
    /// </summary>
    /// 
    /// <param name="code">
    /// A control code in the range 0x00-0x1A, 0x1C-0x1F,
    /// 0x80-0x8F, 0x91-0x97, 0x99-0x9A, or 0x9C.
    /// </param>
    public AnsiSolitaryControlCode(char code) {
        switch (code) {
            case >= (char)0x00 and <= (char)0x1A:
            case >= (char)0x1C and <= (char)0x1F:
            case >= (char)0x80 and <= (char)0x8F:
            case >= (char)0x91 and <= (char)0x97:
            case >= (char)0x99 and <= (char)0x9A:
            case (char)0x9C:
                Code = code;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(code),
                    $"The control code {code.ToHexString()} is not valid.");
        }
        Code = code;
    }
}
