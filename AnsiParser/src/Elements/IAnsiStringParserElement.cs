namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a parsed element generated by an
/// <see cref="AnsiStringParser"/> instance.
/// </summary>
///
/// <remarks>
/// <see cref="AnsiStringParser"/> instances produce lists of elements
/// that each implement this interface. These elements hold either a single
/// control or escape sequence, or a string consisting of one or more
/// characters that appear consecutively in a printable string or a
/// control string parsed from the input.
/// </remarks>
///----------------------------------------------------------------------------
public interface IAnsiStringParserElement {}
