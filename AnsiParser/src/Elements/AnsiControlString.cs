namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a complete control string from the parsed input.
/// </summary>
///----------------------------------------------------------------------------
public record AnsiControlString : IAnsiStringParserElement {
    /// <summary>
    /// The type of the control string.
    /// </summary>
    public ControlStringType Type { get; init; }

    /// <summary>
    /// The content of the control string.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiControlString"/>
    /// record with the specified string type and content.
    /// </summary>
    /// 
    /// <param name="type">
    /// Specifies the type of the control string.
    /// </param>
    /// <param name="text">
    /// Specifies the content of the control string.
    /// </param>
    public AnsiControlString(ControlStringType type, string text) {
        Type = type;
        Text = text;
    }
}
