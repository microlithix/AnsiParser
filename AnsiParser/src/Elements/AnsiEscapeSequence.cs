namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single ANSI escape sequence from the parsed input.
/// </summary>
/// 
/// <remarks>
/// Escape sequences consist of the escape code (<c>0x1b</c>) followed by zero
/// or more intermediate character codes in the range <c>0x20</c>...<c>0x2f</c>,
/// followed by exactly one character code in the range <c>0x30</c>...<c>0x7e</c>.
/// In the present implementation, invalid codes will terminate the escape
/// sequence.
/// 
/// An escape sequence can be complete in and of itself, or it can indicate
/// that another type of sequence follows, such as a control sequence or
/// a control string.
/// 
/// All of the self-contained escape sequences are parsed into
/// <see cref="AnsiEscapeSequence"/> elements. However, some escape
/// sequences that introduce other types of sequences will not themselves
/// be parsed into elements. Instead, the introduced sequences will be parsed into
/// element types specific to their own functionality. The following table lists
/// escape sequences that fall into this category, along with the types of
/// elements into which the introduced sequences will be parsed:
/// <list type="table">
///     <listheader>
///         <term>Escape Sequence</term>
///         <description>Parsed Element Types</description>
///     </listheader>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             <c>P</c>
///         </term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Device control string)
///         </description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             <c>X</c>
///         </term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (General purpose string)
///         </description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             <c>[</c>
///         </term>
///         <description>
///         <see cref="AnsiControlSequence"/> or
///         <see cref="AnsiPrivateControlSequence"/>
///         </description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             <c>]</c>
///         </term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Operating System Command)
///         </description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             <c>^</c>
///         </term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Privacy Message)
///         </description>
///     </item>
///     <item>
///         <term>
///             <a href="xref:Microlithix.Text.Ansi.ControlCode.ESC?displayProperty=nameWithType"/>
///             <c>_</c>
///         </term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Application Program Command)
///         </description>
///     </item>
/// </list>
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiEscapeSequence : IAnsiStreamParserElement, IAnsiStringParserElement {
    /// <summary>
    /// The final character code as a single UTF-16 character.
    /// </summary>
    public char Code { get; init; }

    /// <summary>
    /// The intermediate bytes as a string of characters.
    /// </summary>
    public string IntermediateBytes { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiEscapeSequence"/> instance
    /// with the specified escape code and intermediate bytes.
    /// </summary>
    /// 
    /// <param name="code">
    /// Specifies the final character in the escape sequence.
    /// The character code must be in the range
    /// <c>0x30</c>...<c>0x7e</c>.
    /// </param>
    /// <param name="intermediateBytes">
    /// Specifies any intermediate bytes in the escape sequence.
    /// The character codes for intermediate bytes must each be
    /// in the range <c>0x20</c>...<c>0x2f</c>.
    /// </param>
    public AnsiEscapeSequence(char code, string intermediateBytes) {
        Code = code;
        IntermediateBytes = intermediateBytes;
    }
}
