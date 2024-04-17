namespace Microlithic.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single character in a control string from the parsed input.
/// </summary>
/// 
/// <remarks>
/// The consuming application should accumulate or process
/// <see cref="AnsiControlStringChar"/> records until it receives
/// a <see cref="AnsiControlStringTerminator"/> record.
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiControlStringChar : IAnsiStreamParserElement {
    /// <summary>
    /// Indicates the type of the control
    /// string in which this character appears.
    /// </summary>
    public ControlStringType Type { get; init; }

    /// <summary>
    /// The character from the control string.
    /// </summary>
    public char Character { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiControlStringChar"/>
    /// record with the specified string type and character.
    /// </summary>
    /// 
    /// <param name="type">
    /// Specifies the type of the control
    /// string in which this character appears.
    /// </param>
    /// <param name="character">
    /// Specifies a character that is
    /// part of the control string.
    /// </param>
    public AnsiControlStringChar(ControlStringType type, char character) {
        Type = type;
        Character = character;
    }
}
