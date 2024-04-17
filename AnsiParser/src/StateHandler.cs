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

using Microlithic.Text.Ansi.Element;

namespace Microlithic.Text.Ansi;

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

    protected void Clear() => state.Clear();
    protected void Print() => state.Print(ch);
    protected void Collect() => state.Collect(ch);
    protected void Param() => state.Param(ch);
	protected void PrivateParam() => state.PrivateParam(ch);
	protected void Execute() => state.Execute(ch);
    protected void EscDispatch() => state.EscDispatch(ch);
    protected void CsiDispatch() => state.CsiDispatch(ch);
	protected void PutChar() => state.PutChar(ch);
	protected void StartAPC() => state.StartString(ControlStringType.ApplicationProgramCommand);
	protected void StartDCS() => state.StartString(ControlStringType.DeviceControlString);
	protected void StartOSC() => state.StartString(ControlStringType.OperatingSystemCommand);
	protected void StartPM() => state.StartString(ControlStringType.PrivacyMessage);
	protected void StartSOS() => state.StartString(ControlStringType.StartOfString);
	protected void TerminateString() => state.TerminateString();
    protected void ChangeState(StateId newState, Action? transitionAction = null) =>
        state.ChangeState(newState, transitionAction);

    // Common helper methods useful for most derived classes.

 	protected bool ProcessUnconditionalControlCode() {
		// Each of the following control codes operates unconditionally with
		// a fixed behavior regardless of where it appears in the input stream.
        // Therefore, these codes all interrupt any control sequence in progress.
        // If the sequence in progress is a control string, it will be terminated.
        // Any other control sequence in progress will be discarded.
		switch (ch) {
			case ControlCode.CAN: // 0x18
			case ControlCode.SUB: // 0x1A
                // Emit the control code and forcibly return to the Ground state.
                // These codes are typically used to cancel a control
                // sequence in progress without any side effects.
				ChangeState(StateId.Ground, Execute);
				return true;
			case ControlCode.ESC: // 0x1B
                // Immediately begin a new escape sequence.
				ChangeState(StateId.Escape);
				return true;
			case >= (char)0x80 and <= (char)0x8F:
                // Emit the control code and forcibly return to the Ground state.
				ChangeState(StateId.Ground, Execute);
				return true;
			case ControlCode.DCS: // 0x90
                // Immediately begin a new DCS control string.
				ChangeState(StateId.CommandString, StartDCS);
				return true;
			case >= (char)0x91 and <= (char)0x97:
                // Emit the control code and forcibly return to the Ground state.
				ChangeState(StateId.Ground, Execute);
				return true;
			case ControlCode.SOS: // 0x98
                // Immediately begin a new SOS control string.
				ChangeState(StateId.CharacterString, StartSOS);
				return true;
			case ControlCode.SGC: // 0x99
			case ControlCode.SCI: // 0x9A
                // Emit the control code and forcibly return to the Ground state.
				ChangeState(StateId.Ground, Execute);
				return true;
			case ControlCode.CSI: // 0x9B
                // Immediately begin a new CSI control sequence.
				ChangeState(StateId.CsiEntry);
				return true;
			case ControlCode.ST:  // 0x9C
                // Forcibly return to the Ground state.
				ChangeState(StateId.Ground);
				return true;
			case ControlCode.OSC: // 0x9D
                // Immediately begin a new OSC command string.
				ChangeState(StateId.CommandString, StartOSC);
				return true;
			case ControlCode.PM:  // 0x9E
                // Immediately begin a new PM control string.
				ChangeState(StateId.CommandString, StartPM);
				return true;
			case ControlCode.APC: // 0x9F
                // Immediately begin a new APC control string.
				ChangeState(StateId.CommandString, StartAPC);
				return true;
			default:
				return false;
		}
	}

	protected bool ProcessConditionalControlCode(bool execute = true) {
		// The following control codes are executed in some states and
		// ignored in others. The 'execute' parameter can be set to false
		// in order to ignore the control code. This method returns true
		// if the provided character is a conditional control code,
		// regardless of whether or not it was executed.
		if (ch >= (char)0x20) return false;
		if (ch == (char)0x1B) return false;
		if (ch == (char)0x1A) return false;
		if (ch == (char)0x18) return false;
		if (execute) Execute();
		return true;
	}

	protected bool ProcessControlCode(bool execute = true) {
		// Process the provided character if it is a control code.
		// Unconditional control codes are always executed, while
		// the execution of conditional control codes is determined
		// by the 'execute' parameter. This method returns true if
		// the provided character is a control code in the range
		// 0x00-0x1F or 0x80-0x9F, regardless of whether or not
		// it was executed.
		if (ProcessUnconditionalControlCode()) return true;
		if (ProcessConditionalControlCode(execute)) return true;
		return false;
	}

	protected class UnhandledCharacterCodeException : Exception {
		public UnhandledCharacterCodeException(char ch) : base(
			$"AnsiStreamParser internal error. Unhandled character code '{ch.ToHexString()}'."
		) {}
	}
}

