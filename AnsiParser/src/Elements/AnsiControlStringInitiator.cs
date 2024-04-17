namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Indicates the start of a control string in the parsed input.
/// </summary>
/// 
/// <remarks>
/// The consuming application should expect this record to be
/// followed by zero or more <see cref="AnsiControlStringChar"/> records
/// and a single <see cref="AnsiControlStringTerminator"/> record.
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiControlStringInitiator : IAnsiStreamParserElement {

    /// <summary>
    /// Indicates the type of the control string being initiated.
    /// </summary>
    public ControlStringType Type { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiControlStringInitiator"/>
    /// record for the specified string type.
    /// </summary>
    /// 
    /// <param name="type">
    /// Specifies the type of the control string to be initiated.
    /// </param>
    public AnsiControlStringInitiator(ControlStringType type) {
        Type = type;
    }
}
