namespace Microlithic.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single private ANSI control sequence from the parsed input.
/// </summary>
/// 
/// <remarks>
/// A private ANSI control sequence is a control sequence containing
/// a private parameter string.
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiPrivateControlSequence : IAnsiStreamParserElement, IAnsiStringParserElement {
    /// <summary>
    /// The Function property determines how the Parameters should be
    /// interpreted, and the actions that the consuming application
    /// should initiate when it receives the control sequence. It
    /// consists of a character in the range 0x40-0x7E, optionally
    /// preceded by one or more charactders in the range 0x20-0x2F.
    /// See <see cref="Microlithic.Text.Ansi.ControlFunction"/>
    /// for a standardized list of control sequence functions.
    /// </summary>
    public string Function { init; get; }

    /// <summary>
    /// A string of parameter bytes from the control sequence representing
    /// a "private parameter string" that is application-defined.
    /// Private parameter strings begin with a byte in the range 0x3C-0x3F.
    /// </summary>
    public string Parameters { init; get; }

    /// <summary>
    /// Creates a new <see cref="AnsiPrivateControlSequence"/>
    /// record from a function string and a parameter string.
    /// </summary>
    /// <param name="function">
    /// Specifies the control sequence function to be performed.
    /// This string must consist of zero or more optional characters in the
    /// range 0x20-0x2F, followed by exactly one character in the range
    /// 0x40-0x7E.
    /// </param>
    /// <param name="parameters">
    /// Specifies the parameters for the control sequence function.
    /// This private parameter string must consist of one character
    /// in the range 0x3C-0x3F, followed by zero or more characters
    /// in the range 0x30-0x3F. It's interpretation is application-defined.
    /// </param>
    public AnsiPrivateControlSequence(string function, string parameters) {
        Function = function;
        Parameters = parameters;
    }
}
