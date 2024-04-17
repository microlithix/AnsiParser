namespace Microlithic.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single ANSI control sequence from the parsed input.
/// </summary>
/// 
/// <remarks>
/// See <see href="../docs/ControlSequences.md">Control Sequences</see>
/// for a detailed description of ANSI control sequences and their formats.
/// </remarks>
///----------------------------------------------------------------------------
public record AnsiControlSequence : IAnsiStreamParserElement, IAnsiStringParserElement {
    /// <summary>
    /// The Function property determines how the Parameters should be
    /// interpreted, and the actions that the consuming application
    /// should initiate when it receives the control sequence. It
    /// consists of a character in the range 0x40-0x7E, optionally
    /// preceded by one or more characters in the range 0x20-0x2F.
    /// See <see cref="Microlithic.Text.Ansi.ControlFunction"/>
    /// for a standardized list of control sequence functions.
    /// </summary>
    public string Function { get; init; }

    /// <summary>
    /// A list of zero or more <see cref="Parameter"/>
    /// records parsed from the control sequence.
    /// </summary>
    public IList<Parameter> Parameters { get; init; }

    /// <summary>
    /// Creates a new <see cref="AnsiControlSequence"/> record
    /// from a function string and a list of parameters.
    /// </summary>
    /// 
    /// <param name="function">
    /// Specifies the control sequence function to be performed.
    /// This string must consist of zero or more optional characters in the
    /// range 0x20-0x2F, followed by exactly one character in the range
    /// 0x40-0x7E. See <see cref="ControlFunction"/> for a standardardized
    /// list of control sequence functions.
    /// </param>
    /// <param name="parameters">
    /// A list of zero or more <see cref="Parameter"/> records.
    /// </param>
    public AnsiControlSequence(string function, IList<Parameter> parameters) {
        Function = function;
        Parameters = parameters;
    }

    /// <summary>
    /// Creates a new <see cref="AnsiControlSequence"/> record from
    /// a function string and variable number of parameters.
    /// </summary>
    /// 
    /// <param name="function">
    /// Specifies the control sequence function to be performed.
    /// This string must consist of zero or more optional characters in the
    /// range 0x20-0x2F, followed by exactly one character in the range
    /// 0x40-0x7E. See <see cref="ControlFunction"/> for a standardardized
    /// list of control sequence functions.
    /// </param>
    /// <param name="parameters">
    /// A variable number of parameters, each of type <see cref="Parameter"/>.
    /// </param>
    public AnsiControlSequence(string function, params Parameter[] parameters) :
        this(function, new List<Parameter>(parameters)) { }

    /// <summary>
    /// Checks if this control sequence is an SGR
    /// sequence containing a legacy SGR parameter.
    /// </summary>
    /// 
    /// <returns>
    /// Returns <c>true</c> if the control sequence is a Select Graphic Rendition sequence
    /// (<see cref="ControlFunction.SGR">ControlFunction.SGR</see>) containing
    /// a <see href="..\docs\ControlSequences.md#legacy-sgr-parameters">
    /// legacy SGR parameter</see>. Returns <c>false</c> otherwise.
    /// </returns>
    public bool HasLegacySGRParameter() {
        // If the control function is not SGR, then the control
        // sequence obviously cannot contain a legacy SGR parameter.
        if (Function != ControlFunction.SGR) return false;

        foreach (Parameter parameter in Parameters) {
            if (parameter.IsLegacySGRParameter()) return true;
        }
        return false;
    }

    /// <summary>
    /// Returns a copy of this record with any legacy SGR parameters
    /// converted to standard SGR parameters.
    /// </summary>
    /// 
    /// <returns>
    /// A copy of this <see cref="AnsiControlSequence"/> record with any
    /// <see href="..\docs\ControlSequences.md#legacy-sgr-parameters">
    /// legacy SGR parameters</see> converted to
    /// <see href="..\docs\ControlSequences.md#select-graphic-rendition">
    /// standard SGR parameters</see>.
    /// </returns>
    /// 
    /// <remarks>
    /// Returns the unmodified original record if the control sequence
    /// is not a Select Graphic Rendition
    /// (<see cref="ControlFunction.SGR">ControlFunction.SGR</see>) sequence,
    /// or if the control sequence doesn't contain
    /// any legacy SGR parameters.
    /// </remarks>
    public AnsiControlSequence ConvertLegacySGRParameters() {
        bool isLegacySGRParameter = false;
        Parameter convertedParameter = new();
        IList<Parameter> newParameters = new List<Parameter>();

        // Don't convert anything if the control sequence
        // has no legacy SGR color selector parameters.
        if (!HasLegacySGRParameter()) return this;

        foreach (Parameter parameter in Parameters) {
            if (isLegacySGRParameter) {
                if (ConvertLegacySGRParameter(parameter, convertedParameter)) {
                    // The convertedParameter is complete.
                    isLegacySGRParameter = false;
                }
                continue;
            }

            if (parameter.IsLegacySGRParameter()) {
                // Start building a new parameter with the correct canonical form.
                convertedParameter = new Parameter(parameter.Value);
                newParameters.Add(convertedParameter);
                isLegacySGRParameter = true;
            } else {
                // Otherwise we just retain the parameter as-is.
                newParameters.Add(parameter);
            }
        }

        return this with { Parameters = newParameters };
    }

    private bool ConvertLegacySGRParameter(Parameter parameter, Parameter convertedParameter) {
        // Incorporates parameter into convertedParameter.
        // Returns true if convertedParameter is complete, or false
        // if convertedParmeter still requires more parameter parts.

        if (convertedParameter.Parts.Count == 1) {
            // First time called for this convertedParameter.
            // Get the color selection mode part and any other
            // parts that might be present in the next parameter.
            foreach (int part in parameter.Parts) {
                convertedParameter.Parts.Add(part);
            }

            // If the next parameter contained more than one part, then
            // we assume that it completes the legacy parameter value.
            if (convertedParameter.Parts.Count > 2) return true;

            // These two color selection modes don't require any further parts.
            // For an implementation-defined color selection mode, we cannot
            // know how many further parts are required, so we must assume
            // that none are required. This assumption should be safe because
            // an implementation-defined color selection parameter should
            // never be in a legacy format anyway.
            if (parameter.Value == ColorSelectionFormat.ImplementationDefined) return true;
            if (parameter.Value == ColorSelectionFormat.Transparent) return true;

            return false;
        }

        switch (convertedParameter.Parts[1]) {
            case ColorSelectionFormat.RGB:
            case ColorSelectionFormat.CMY:
                // The RGB and CMY color selectors each use
                // a total of 6 parts for their full specifiers.
                return ConvertNonPalettizedColorSelector(6);
            case ColorSelectionFormat.CMYK:
                // The CMYK color selector uses a total
                // of 7 parts for its full specifier.
                return ConvertNonPalettizedColorSelector(7);
            case ColorSelectionFormat.PaletteIndex:
                // The PaletteIndex color selector uses a total
                // of 3 parts for its full specifier, with the
                // third part being the palette index.
                if (convertedParameter.Parts.Count < 3) {
                    convertedParameter.Parts.Add(parameter.Value);
                }
                return convertedParameter.Parts.Count == 3;
        }
        return false;

        bool ConvertNonPalettizedColorSelector(int numParts) {
            // Collects all the parts of a non-palettized RGB,
            // CMY, or CMYK color selector. Returns true when
            // all the required parts have been collected.

            // The third parameter part of a non-palettized
            // color selector specifies the color space,
            // but it isn't included in the legacy format.
            // Therefore we just insert a default value
            // indicator for the color space part.
            if (convertedParameter.Parts.Count == 2) {
                convertedParameter.Parts.Add(-1);
            }

            // Collect the next part.
            if (convertedParameter.Parts.Count < numParts) {
                convertedParameter.Parts.Add(parameter.Value);
            }
            return convertedParameter.Parts.Count == numParts;
        }
    }

    /// <summary>
    /// Returns a string representation of this AnsiControlSequence.
    /// </summary>
    /// <returns>
    /// A string representation of the AnsiControlSequence,
    /// including its control function and all of its parameters.
    /// </returns>
    public override string ToString() {
        string parameters = string.Join(", ", Parameters);
        return $"{nameof(AnsiControlSequence)} " +
            $"{{ Function = {Function}, Parameters = [ {parameters} ] }}";
    }
}
