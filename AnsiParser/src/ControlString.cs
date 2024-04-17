namespace Microlithix.Text.Ansi;

///----------------------------------------------------------------------------
/// <summary>
/// Constants representing the different types
/// of control strings defined in ECMA-48.
/// </summary>
///----------------------------------------------------------------------------
public enum ControlStringType {
    /// <summary>
    /// Application Program Command (APC)
    /// </summary>
    ApplicationProgramCommand,

    /// <summary>
    /// Device Control String (DCS)
    /// </summary>
    DeviceControlString,

    /// <summary>
    /// Operating System Command (OSC)
    /// </summary>
    OperatingSystemCommand,

    /// <summary>
    /// Privacy Messsage (PM)
    /// </summary>
    PrivacyMessage,

    /// <summary>
    /// General purpose character string (SOS)
    /// </summary>
    StartOfString
}
