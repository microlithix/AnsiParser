namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Indicates the end of a control string in the parsed input.
/// </summary>
///----------------------------------------------------------------------------
public record AnsiControlStringTerminator : IAnsiStreamParserElement {
    /// <summary>
    /// Indicates the type of the control string being terminated.
    /// </summary>
    public ControlStringType Type { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiControlStringTerminator"/>
    /// record for the specified string type.
    /// </summary>
    /// 
    /// <param name="type">
    /// Specifies the type of the control string to be terminated.
    /// </param>
    public AnsiControlStringTerminator(ControlStringType type) {
        Type = type;
    }
}
