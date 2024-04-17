namespace Microlithic.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single ANSI escape sequence from the parsed input.
/// </summary>
/// 
/// <remarks>
/// Escape sequences consist of the escape code (0x1B) followed by zero or
/// more intermediate bytes in the range 0x20-0x2F, followed by exactly one
/// character code in the range 0x30-0x7E. In the present implementation,
/// invalid codes will also terminate the escape sequence.
/// 
/// An escape sequence can be complete in and of itself, or it can indicate
/// that another type of sequence follows, such as a control sequence or
/// a control string.
/// 
/// All of the self-contained escape sequences are reported in
/// <see cref="AnsiEscapeSequence"/> records. However, some escape
/// sequences that introduce other types of sequences will not themselves
/// be reported. Instead, the introduced sequences will be reported in
/// records specific to their own functionality. The following table lists
/// escape sequences that fall into this category, along with the types of
/// records that will be reported instead:
/// <list type="table">
///     <listheader>
///         <term>Escape Sequence</term>
///         <description>Reported Record Types</description>
///     </listheader>
///     <item>
///         <term><see cref="ControlCode.ESC"/>P (x01B)(0x50)</term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Device control string)
///         </description>
///     </item>
///     <item>
///         <term><see cref="ControlCode.ESC"/>X (0x1B)(0x58)</term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (General purpose string)
///         </description>
///     </item>
///     <item>
///         <term><see cref="ControlCode.ESC"/>[ (0x1B)(0x5B)</term>
///         <description>
///         <see cref="AnsiControlSequence"/> or
///         <see cref="AnsiPrivateControlSequence"/>
///         </description>
///     </item>
///     <item>
///         <term><see cref="ControlCode.ESC"/>] (0x1B)(0x5D)</term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Operating System Command)
///         </description>
///     </item>
///     <item>
///         <term><see cref="ControlCode.ESC"/>^ (0x1B)(0x5E)</term>
///         <description>
///         <see cref="AnsiControlStringInitiator"/> or
///         <see cref="AnsiControlString"/> (Privacy Message)
///         </description>
///     </item>
///     <item>
///         <term><see cref="ControlCode.ESC"/>_ (0x1B)(0x5F)</term>
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
    /// Creates a new record with the specified
    /// escape code and intermediate bytes.
    /// </summary>
    /// 
    /// <param name="code">
    /// Specifies the final character in the escape sequence.
    /// The character code must be in the range 0x30-0x7E.
    /// </param>
    /// <param name="intermediateBytes">
    /// Specifies any intermediate bytes in the escape sequence.
    /// Intermediate bytes must each be in the range 0x20-0x2F.
    /// </param>
    public AnsiEscapeSequence(char code, string intermediateBytes) {
        Code = code;
        IntermediateBytes = intermediateBytes;
    }
}
