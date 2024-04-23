namespace Microlithix.Text.Ansi.Element;

///----------------------------------------------------------------------------
/// <summary>
/// Represents a single parameter in a control sequence.
/// </summary>
/// 
/// <remarks>
/// <para>
/// As defined in ECMA-48, a control sequence may contain a string of
/// characters in the range <c>0x30</c>...<c>0x3f</c> representing one or more
/// parameters for the control function. Such a string is referred to as the
/// parameter string.
/// 
/// If the parameter string begins with a character in the range
/// <c>0x3c</c>...<c>0x3f</c>, then it is for private or experimental use
/// and not defined in ECMA-48. If, on the other hand, the parameter string
/// begins with a character in the range <c>0x30</c>...<c>0x3b</c>, then it
/// is a standardized parameter string.
/// 
/// A standardized parameter string may be divided into parameter sub-strings
/// by the semi-colon (;) character, with each parameter sub-string
/// representing one parameter for the control function. Each parameter
/// sub-string will be parsed into one <see cref="Parameter"/> instance.
/// 
/// Each parameter sub-string may be further divided into parts separated
/// by the colon (:) character, where each part consists of a sequence of
/// decimal digits representing a positive integer value.
/// 
/// A <see cref="Parameter"/> instance represents a single parameter parsed
/// from a single parameter sub-string into its separate integer parts.
/// 
/// Control sequences can contain multiple parameters
/// separated by the semi-colon (;) character. For example,
/// "<c>1;5:4;32</c>" is a valid sequence of three parameters,
/// with the second parameter having two parts (5 and 4).
/// </para>
/// <para>
/// Although the specification allows for a parameter to have
/// multiple parts, such use is rare. In most cases, a parameter
/// will consist of a single sequence of decimal digits representing
/// a single integer value.
/// </para>
/// <para>
/// Any part missing from the control sequence should be
/// interpreted as having an application-defined default value.
/// Such missing parts are represented in the <see cref="Parameter"/>
/// instance with a value of -1.
/// </para>
/// <para>
/// Note that ECMA-48 refers to the parameter sub-parts as "parameter
/// sub-strings".
/// </para>
/// </remarks>
///----------------------------------------------------------------------------
public record Parameter {
    /// <summary>
    /// In most cases, a parameter consists of a single integer
    /// value that can be accessed via the <see cref="Value"/>
    /// property. When a parameter consists of multiple integer
    /// values, you can use the <see cref="Parts"/> property to
    /// access all of them. Any part with a negative value
    /// indicates that the part should be interpreted as having
    /// an application-defined default value.
    /// </summary>
    public IList<int> Parts { get; init; }

    /// <summary>
    /// Returns the integer value of the first part of the parameter.
    /// In almost all cases, parameters have only one part and you
    /// can use this property to access the parameter's value.
    /// A value of -1 indicates that the parameter should be
    /// interpreted as having an application-defined default value.
    /// </summary>
    public int Value => GetPart(0);

    /// <summary>
    /// Creates a new <see cref="Parameter"/> instance
    /// from a specified list of parts, where each part
    /// is either a positive integer, or a value of -1
    /// indicating that the part should be interpreted
    /// as having an application-defined default value.
    /// </summary>
    /// 
    /// <param name="part">
    /// An integer representing one part of the parameter.
    /// </param>
    public Parameter(params int[] part) { Parts = new List<int>(part); }

    /// <summary>
    /// Creates a new <see cref="Parameter"/> instance
    /// with a single part having a value of -1, indicating
    /// that the parameter should be interpreted as having an
    /// application-defined default value.
    /// </summary>
    public Parameter() : this(-1) {}

    /// <summary>
    /// Returns one integer part of the Parameter.
    /// </summary>
    /// <param name="index">
    /// An index into the list of parts, where an
    /// index of zero refers to the first part.
    /// </param>
    /// <returns>
    /// The requested part of the Parameter. If the part
    /// doesn't exist, then the value -1 is returned to
    /// indicate that the application should treat the
    /// missing part as having a default value.
    /// </returns>
    public int GetPart(int index) => GetPartOrDefault(index, -1);

    /// <summary>
    /// Returns one integer part of the Parameter,
    /// or a default value if the part doesn't exist.
    /// </summary>
    /// <param name="index">
    /// An index into the list of parts, where an
    /// index of zero refers to the first part.
    /// </param>
    /// <param name="defaultValue">
    /// The default value to return if the part doesn't exist.
    /// If not specified, the default value will be 0.
    /// </param>
    /// <returns>
    /// The requested part of the Parameter. If the part
    /// doesn't exist, then the default value is returned.
    /// </returns>
    public int GetPartOrDefault(int index, int defaultValue = 0) {
        if (index < 0 || index >= Parts.Count || Parts[index] == -1) return defaultValue;
        return Parts[index];
    }

    /// <summary>
    /// Checks if the parameter is a
    /// <see cref="GraphicRenditionSelector.SetForegroundColor"/> or
    /// <see cref="GraphicRenditionSelector.SetBackgroundColor"/>
    /// parameter in legacy format.
    /// </summary>
    /// <returns>
    /// Returns true if the parameter contains only one part and it is
    /// a <see cref="GraphicRenditionSelector.SetForegroundColor"/> or
    /// <see cref="GraphicRenditionSelector.SetBackgroundColor"/>
    /// parameter.
    /// </returns>
    /// <remarks>
    /// For the return value of this method to have any meaning, the
    /// parameter must be part of a <see cref="ControlFunction.SGR"/>
    /// control sequence.
    /// </remarks>
    public bool IsLegacySGRParameter() {
        if (Parts.Count != 1) return false;
        if (Value == GraphicRenditionSelector.SetForegroundColor) return true;
        if (Value == GraphicRenditionSelector.SetBackgroundColor) return true;
        return false;
    }

    /// <summary>
    /// Returns a string representation of the Parameter.
    /// </summary>
    /// <returns>
    /// A string representation of the Parameter,
    /// including all of its parts.
    /// </returns>
    public override string ToString() => string.Join(":", Parts);
}
