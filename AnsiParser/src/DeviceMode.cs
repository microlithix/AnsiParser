namespace Microlithix.Text.Ansi;

/// <summary>
/// The DeviceMode class defines a set of static constants representing
/// device modes. Device modes indicate the ways in which a device transmits,
/// receives, processes, or images data. The use of modes has been deprecated
/// in order to ensure data compatibility and ease of interchange with a
/// variety of equipment (<see href="../docs/References.md#ecma-48">ECMA-48</see>).
/// </summary>
/// <remarks>
/// The ECMA-48 Standard is intended to be applicable to a very large range
/// of devices in which there are variations. Some of these variations have
/// been formalized in the form of modes. They deal with the ways in which a
/// device transmits, receives, processes, or images data. Each mode has two
/// states.
/// 
/// The states of the modes may be established explicitly in the data stream
/// by the control functions <c>SET MODE</c> (<see cref="ControlFunction.SM">SET MODE</see>)
/// and <c>RESET MODE</c> (<see cref="ControlFunction.RM"/>) or may be
/// established by agreement between sender and recipient. In an
/// implementation, some or all of the modes may have one state only.
/// 
/// As noted in the summary, to ensure data compatibility and ease of
/// interchange with a variety of equipment the use of modes is deprecated.
/// If modes have to be implemented for backward compatibility it is
/// recommended that the reset state of the modes be the initial state.
/// Otherwise, explicit agreements will have to be negotiated between
/// sender and recipient, to the detriment of "blind" interchange.
/// </remarks>
public static class DeviceMode {
    /// <summary>
    /// Guarded Area Transfer Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>GUARD (RESET)</c></term>
    ///         <description>
    ///         Only the contents of unguarded areas in an eligible area are
    ///         transmitted or transferred.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>ALL (SET)</c></term>
    ///         <description>
    ///         The contents of guarded as well as of unguarded areas in an
    ///         eligible area are transmitted or transferred.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: No control functions are affected.
    /// </remarks>
    public const int GATM = 1;

    /// <summary>
    /// Keyboard Action Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>ENABLED (RESET)</c></term>
    ///         <description>
    ///         All or part of the manual input
    ///         facilities are enabled to be used.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>DISABLED (SET)</c></term>
    ///         <description>
    ///         All or part of the manual input
    ///         facilities are disabled.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: No control functions are affected.
    /// </remarks>
    public const int KAM = 2;

    /// <summary>
    /// Control Representation Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>CONTROL (RESET)</c></term>
    ///         <description>
    ///         All control functions are performed as defined; the
    ///         way formator functions are processed depends on the
    ///         setting of the <c>FORMAT EFFECTOR ACTION MODE</c>
    ///         (<c><see cref="FEAM"/></c>). A device may choose to
    ///         image the graphical representations of control
    ///         functions in addition to performing them.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>GRAPHIC (SET)</c></term>
    ///         <description>
    ///         All control functions, except <c>RESET MODE</c> 
    ///         (<c><see cref="ControlFunction.RM"/></c>), are treated
    ///         as graphic characters. A device may choose to perform
    ///         some control functions in addition to storing them and
    ///         imaging their graphical representations.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: All control functions except
    /// <c><see cref="ControlFunction.RM"/></c> are affected.
    /// </remarks>
    public const int CRM = 3;

    /// <summary>
    /// Insertion Replacement Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>REPLACE (RESET)</c></term>
    ///         <description>
    ///         The graphic symbol of a graphic character or of a control
    ///         function, for which a graphical representation is required,
    ///         replaces (or, depending upon the implementation, is combined
    ///         with) the graphic symbol imaged at the active presentation
    ///         position.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>INSERT (SET)</c></term>
    ///         <description>
    ///         The graphic symbol of a graphic character or of a control
    ///         function, for which a graphical representation is required,
    ///         is inserted at the active presentation position.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: Only control functions for which a graphical representation is
    /// required are affected.
    /// </remarks>
    public const int IRM = 4;

    /// <summary>
    /// Status Report Transfer Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>NORMAL (RESET)</c></term>
    ///         <description>
    ///         Status reports in the form of <c>DEVICE CONTROL STRING</c>s
    ///         (<c><see cref="ControlCode.DCS"/></c>) are not generated
    ///         automatically.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>DIAGNOSTIC (SET)</c></term>
    ///         <description>
    ///         Status reports in the form of <c>DEVICE CONTROL STRING</c>s
    ///         (<c><see cref="ControlCode.DCS"/></c>) are included in every
    ///         data stream transmitted or transferred.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: No control functions are affected.
    /// </remarks>
    public const int SRTM = 5;

    /// <summary>
    /// Erasure Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>PROTECT (RESET)</c></term>
    ///         <description>
    ///         Only the contents of unprotected areas
    ///         are affected by an erasure control function.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>ALL (SET)</c></term>
    ///         <description>
    ///         The contents of protected as well as of unprotected
    ///         areas are affected by an erasure control function.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: Control functions affected are:
    /// <c><see cref="ControlFunction.EA"/></c>,
    /// <c><see cref="ControlFunction.ECH"/></c>,
    /// <c><see cref="ControlFunction.ED"/></c>,
    /// <c><see cref="ControlFunction.EF"/></c>, and
    /// <c><see cref="ControlFunction.EL"/></c>.
    /// </remarks>
    public const int ERM = 6;

    /// <summary>
    /// Line Editing Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>FOLLOWING (RESET)</c></term>
    ///         <description>
    ///         <para>
    ///         If the <c>DEVICE COMPONENT SELECT MODE</c>
    ///         (<c><see cref="DCSM"/></c>) is set to <c>PRESENTATION</c>,
    ///         a line insertion causes the contents of the active line (the
    ///         line that contains the active presentation position) and of the
    ///         following lines in the presentation component to be shifted in
    ///         the direction of the line progression; a line deletion causes
    ///         the contents of the lines following the active line to be
    ///         shifted in the direction opposite to that of the line
    ///         progression.
    ///         </para>
    ///         <para>
    ///         If the <c>DEVICE COMPONENT SELECT MODE</c>
    ///         (<c><see cref="DeviceMode.DCSM"/></c>) is set to <c>DATA</c>,
    ///         a line
    ///         insertion causes the contents of the active line (the line
    ///         that contains the active data position) and of the following
    ///         lines in the data component to be shifted in the direction of
    ///         the line progression; a line deletion causes the contents of
    ///         the lines following the active line to be shifted in the
    ///         direction opposite to that of the line progression.
    ///         </para>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>PRECEDING (SET)</c></term>
    ///         <description>
    ///         <para>
    ///         If the <c>DEVICE COMPONENT SELECT MODE</c>
    ///         (<c><see cref="DeviceMode.DCSM"/></c>) is set to
    ///         <c>PRESENTATION</c>, a line insertion causes the contents
    ///         of the active line (the line that contains the active
    ///         presentation position) and of the preceding lines to be
    ///         shifted in the direction opposite to that of the line
    ///         progression; a line deletion causes the contents of the
    ///         lines preceding the active line to be shifted in the
    ///         direction of the line progression.
    ///         </para>
    ///         <para>
    ///         If the <c>DEVICE COMPONENT SELECT MODE</c>
    ///         (<c><see cref="DeviceMode.DCSM"/></c>) is set to <c>DATA</c>,
    ///         a line insertion causes the contents of the active line (the
    ///         line that contains the active data position) and of the
    ///         preceding lines to be shifted in the direction opposite to
    ///         that of the line progression; a line deletion causes the
    ///         contents of the lines preceding the active line to be shifted
    ///         in the direction of the line progression.
    ///         </para>
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: Control functions affected are:
    /// <c><see cref="ControlFunction.DL"/></c> and
    /// <c><see cref="ControlFunction.IL"/></c>.
    /// </remarks>
    public const int VEM = 7;

    /// <summary>
    /// Bi-Directional Support Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>EXPLICIT (RESET)</c></term>
    ///         <description>
    ///         Control functions are performed in the data component or in
    ///         the presentation component, depending on the setting of the
    ///         <c>DEVICE COMPONENT SELECT MODE</c> (<c><see cref="DCSM"/></c>).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>IMPLICIT (SET)</c></term>
    ///         <description>
    ///         Control functions are performed in the data component. All
    ///         bi-directional aspects of data are handled by the device itself.
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int BDSM = 8;

    /// <summary>
    /// Device Component Select Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>PRESENTATION (RESET)</c></term>
    ///         <description>
    ///         Certain control functions are performed in the presentation
    ///         component. The active presentation position (or the active
    ///         line, where applicable) in the presentation component is the
    ///         reference position against which the relevant control
    ///         functions are performed.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>DATA (SET)</c></term>
    ///         <description>
    ///         Certain control functions are performed in the data component.
    ///         The active data position (or the active line, where applicable)
    ///         in the data component is the reference position against which
    ///         the relevant control functions are performed.
    ///         </description>
    ///     </item>
    /// </list>
    /// NOTE: Control functions affected are 
    /// <c><see cref="ControlFunction.CPR"/></c>, 
    /// <c><see cref="ControlCode.CR"/></c>,
    /// <c><see cref="ControlFunction.DCH"/></c>,
    /// <c><see cref="ControlFunction.DL"/></c>,
    /// <c><see cref="ControlFunction.EA"/></c>,
    /// <c><see cref="ControlFunction.ECH"/></c>,
    /// <c><see cref="ControlFunction.ED"/></c>,
    /// <c><see cref="ControlFunction.EF"/></c>,
    /// <c><see cref="ControlFunction.ICH"/></c>,
    /// <c><see cref="ControlFunction.IL"/></c>,
    /// <c><see cref="ControlCode.LF"/></c>,
    /// <c><see cref="ControlCode.NEL"/></c>,
    /// <c><see cref="ControlCode.RI"/></c>,
    /// <c><see cref="ControlFunction.SLH"/></c>,
    /// <c><see cref="ControlFunction.SLL"/></c>,
    /// <c><see cref="ControlFunction.SPH"/></c>,
    /// <c><see cref="ControlFunction.SPL"/></c>, and
    /// <c><see cref="ControlFunction.CPR"/></c>.
    /// </remarks>
    public const int DCSM = 9;

    /// <summary>
    /// Character Editing Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int HEM = 10;

    /// <summary>
    /// Positioning Unit Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int PUM = 11;

    /// <summary>
    /// Send/Receive Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int SRM = 12;

    /// <summary>
    /// Format Effector Action Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int FEAM = 13;

    /// <summary>
    /// Format Effector Transfer Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int FETM = 14;

    /// <summary>
    /// Multiple Area Transfer Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int MATM = 15;

    /// <summary>
    /// Transfer Termination Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int TTM = 16;

    /// <summary>
    /// Selected Area Transfer Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int SATM = 17;

    /// <summary>
    /// Tabulation Stop Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int TSM = 18;

    /// <summary>
    /// Shall not be used
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int Reserved1 = 19;

    /// <summary>
    /// Shall not be used
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int Reserved2 = 20;

    /// <summary>
    /// Graphic Rendition Combination Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int GRCM = 21;

    /// <summary>
    /// Zero Default Mode
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Setting</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>(RESET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>(SET)</c></term>
    ///         <description>
    ///         <c></c>
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public const int ZDM = 22;
}