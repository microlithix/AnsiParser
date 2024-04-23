namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single printable character from the parsed input.
/// </summary>
/// 
/// <remarks>
/// Printable characters appear in the input stream outside of
/// any control or escape sequences.
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiPrintableChar : IAnsiStreamParserElement {

    /// <summary>
    /// The printable character.
    /// </summary>
    public char Character { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiPrintableChar"/>
    /// instance with the specified character value.
    /// </summary>
    /// 
    /// <param name="character">
    /// Specifies the printable character value.
    /// </param>
    public AnsiPrintableChar(char character) => Character = character;
}
