namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a consecutive sequence of one or more printable
/// characters from the parsed input.
/// </summary>
/// 
/// <remarks>
/// Printable characters appear in the input stream outside of
/// any control or escape sequences.
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiPrintableString : IAnsiStringParserElement {

    /// <summary>
    /// The content of the printable string.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiPrintableString"/>
    /// instance with the specified content.
    /// </summary>
    /// 
    /// <param name="text">
    /// Specifies the content of the printable string.
    /// </param>
    public AnsiPrintableString(string text) => Text = text;
}
