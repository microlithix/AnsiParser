namespace Microlithic.Text.Ansi;

/// <summary>
/// Create an instance of this record and pass it to the
/// parser's constructor to configure the behavior of the parser.
/// </summary>
public record AnsiParserSettings {

    /// <summary>
    /// When this property is <c>false</c>, the parser will convert
    /// <see href="../docs/ControlSequences.md#legacy-sgr-parameters">
    /// legacy SGR parameters</see> to the standardized format defined
    /// in <see href="../docs/References.md#ecma-48">ECMA-48</see> and
    /// <see href="../docs/References.md#isoiec-8613-6-ccitt-recommendation-t416">
    /// ISO/IEC 8613-6</see>.
    /// When this property is <c>true</c>, then any legacy
    /// SGR parameters will be preserved in their original form.
    /// The default value is <c>false</c>.
    /// </summary>
    /// <remarks>
    /// See <see href="../docs/ControlSequences.md#legacy-sgr-parameters">
    /// Legacy SGR Parameters</see> for a discussion of standardized and
    /// legacy SGR parameters.
    /// </remarks>
    public bool PreserveLegacySGRParameters { get; init; } = false;
}

class Foo {
    void bar() {
        AnsiParserSettings s = new();
        
    }
}
