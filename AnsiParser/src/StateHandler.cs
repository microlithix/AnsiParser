//================================================================================================
// StateHandler
//
// StateHandler is a base class for implementing handlers for each state of the state machine.
//
// Derived classes should perform the following actions:
// - Override the OnEnter() method if any actions need to be taken when the state is entered.
// - Override the OnExit() method if any actions need to be taken when the state is exited.
// - Override the OnProcess() method to perform state-specific processing on each character.
//   - Invoke any required actions based on the input data and the current state:
//   - Invoke ChangeState() to cause a state transition when necessary.
//================================================================================================

using Microlithix.Text.Ansi.Element;

namespace Microlithix.Text.Ansi;

internal class StateHandler {

    private AnsiStreamParser state;
    protected char ch;

    public StateHandler(StateId stateId, AnsiStreamParser context) {
        StateId = stateId;
        state = context;
    }

    public StateId StateId { get; init; }

    public void Process(char ch) {
        this.ch = ch;
        OnProcess();
    }

    // Overridable state handler methods.
 
    public virtual void OnEnter() {
        // Override this method to perform any
        // required actions when a state is entered.
    }
    public virtual void OnProcess() {
        // Override this method to perform state-specific processing for a single
        // input character. The input character will be available in the 'ch' field.
    }
    public virtual void OnExit() {
        // Override this method to perform any
        // required actions when a state is exited.
    }

    // Actions

    protected void NewSequence() => state.NewSequence();
    protected void Intermediate() => state.Intermediate(ch);
    protected void Parameter() => state.Parameter(ch);
    protected void PrivateParameter() => state.PrivateParameter(ch);

    protected void DispatchChar() => state.DispatchChar(ch);
    protected void DispatchCtl() => state.DispatchCtl(ch);
    protected void DispatchEsc() => state.DispatchEsc(ch);
    protected void DispatchSeq() => state.DispatchSeq(ch);

    protected void StartAPC() => state.StartString(ControlStringType.ApplicationProgramCommand);
    protected void StartDCS() => state.StartString(ControlStringType.DeviceControlString);
    protected void StartOSC() => state.StartString(ControlStringType.OperatingSystemCommand);
    protected void StartPM() => state.StartString(ControlStringType.PrivacyMessage);
    protected void StartSOS() => state.StartString(ControlStringType.StartOfString);
    protected void DispatchControlStringChar() => state.DispatchControlStringChar(ch);
    protected void DispatchStringTerminator() => state.DispatchStringTerminator();

    protected void ChangeState(StateId newState, Action? transitionAction = null) =>
        state.ChangeState(newState, transitionAction);

    // Common helper methods useful for most derived classes.

     protected bool DispatchUnconditionalControlCode() {
        // Check if the current character is an unconditional control
        // code in the range 0x18,0x1a-0x1b,0x80-0x9f. If it is,
        // then handle it and return true, otherwise return false.
        // All of these codes operate unconditionaly regardless of
        // where they appear in the input stream. If a control string
        // sequence is in progress, it will be completed and dispatched
        // by the control string state handler. Any other control
        // sequence in progress will be discarded.
        switch (ch) {
            case ControlCode.CAN: // '\u0018'
            case ControlCode.SUB: // '\u001a'
                // Emit the control code and forcibly return to the Ground state.
                // These codes are typically used to cancel a control
                // sequence in progress without any other side effects.
                ChangeState(StateId.Ground, DispatchCtl);
                return true;
            case ControlCode.ESC: // '\u001b'
                // Immediately begin a new escape sequence.
                ChangeState(StateId.EscapeEntry);
                return true;
            case >= '\u0080' and <= '\u008f':
                // Emit the control code and forcibly return to the Ground state.
                ChangeState(StateId.Ground, DispatchCtl);
                return true;
            case ControlCode.DCS: // '\u0090'
                // Immediately begin a new DCS control string.
                ChangeState(StateId.CommandString, StartDCS);
                return true;
            case >= '\u0091' and <= '\u0097':
                // Emit the control code and forcibly return to the Ground state.
                ChangeState(StateId.Ground, DispatchCtl);
                return true;
            case ControlCode.SOS: // '\u0098'
                // Immediately begin a new SOS control string.
                ChangeState(StateId.CharacterString, StartSOS);
                return true;
            case ControlCode.SGC: // '\u0099'
            case ControlCode.SCI: // '\u009a'
                // Emit the control code and forcibly return to the Ground state.
                ChangeState(StateId.Ground, DispatchCtl);
                return true;
            case ControlCode.CSI: // '\u009b'
                // Immediately begin a new CSI control sequence.
                ChangeState(StateId.ControlSequenceEntry);
                return true;
            case ControlCode.ST:  // '\u009c'
                // Complete any control string sequence currently
                // in progress and return to the Ground state.
                // We don't need to explicitly produce a string terminator
                // element here because that will be handled automatically
                // by the exit condition of the control string state
                // handler if a control string is being processed.
                // If this control code appears outside of any
                // control string it will not produce an element.
                ChangeState(StateId.Ground);
                return true;
            case ControlCode.OSC: // '\u009d'
                // Immediately begin a new OSC command string.
                ChangeState(StateId.CommandString, StartOSC);
                return true;
            case ControlCode.PM:  // '\u009e'
                // Immediately begin a new PM control string.
                ChangeState(StateId.CommandString, StartPM);
                return true;
            case ControlCode.APC: // '\u009f'
                // Immediately begin a new APC control string.
                ChangeState(StateId.CommandString, StartAPC);
                return true;
            default:
                return false;
        }
    }

    protected bool DispatchConditionalControlCode() {
        // Check if the current character is a conditional control
        // code in the range 0x00-0x17,0x19,0x1c-0x1f. If it is,
        // then dispatch it and return true, otherwise return false.
        if (ch >= '\u0020') return false;
        if (ch == '\u0018') return false;
        if (ch == '\u001a') return false;
        if (ch == '\u001b') return false;
        DispatchCtl();
        return true;
    }

    protected bool DispatchControlCode() {
        // Check if the current character is a control code.
        // If it is, then dispatch it and return true.
        // Otherwise return false.
        if (DispatchUnconditionalControlCode()) return true;
        if (DispatchConditionalControlCode()) return true;
        return false;
    }

    protected class UnhandledCharacterCodeException : Exception {
        public UnhandledCharacterCodeException(char ch) : base(
            $"AnsiStreamParser internal error. Unhandled character code '{ch.ToHexString()}'."
        ) {}
    }
}

