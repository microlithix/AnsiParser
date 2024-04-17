namespace Microlithic.Text.Ansi;

internal class StateHandlerForGround : StateHandler {
    // This is the initial state of the parser. Except for characters
    // that are part of escape and control sequences, it emits all
    // other characters as elements of type AnsiPrintElement.

    public StateHandlerForGround(AnsiStreamParser context) :
        base(StateId.Ground, context) {}

	public override void OnProcess() {
		if (ProcessControlCode()) return;
		Print();
	}
}

internal class StateHandlerForEscape : StateHandler {

    public StateHandlerForEscape(AnsiStreamParser context) :
        base(StateId.Escape, context) {}

    public override void OnEnter() => Clear();

	public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x2F:
				ChangeState(StateId.EscapeIntermediate, Collect);
				return;
			case (char)0x50:
				ChangeState(StateId.CommandString, StartDCS);
				return;
			case (char)0x58:
				ChangeState(StateId.CharacterString, StartSOS);
				return;
			case (char)0x5E:
				ChangeState(StateId.CommandString, StartPM);
				return;
			case (char)0x5F:
				ChangeState(StateId.CommandString, StartAPC);
				return;
			case (char)0x5B:
				ChangeState(StateId.CsiEntry);
				return;
			case (char)0x5D:
				ChangeState(StateId.CommandString, StartOSC);
				return;
			case >= (char)0x30 and <= (char)0x4F:
			case >= (char)0x51 and <= (char)0x57:
			case >= (char)0x59 and <= (char)0x5A:
			case (char)0x5C:
			case >= (char)0x60 and <= (char)0x7E:
				ChangeState(StateId.Ground, EscDispatch);
				return;
			case >= (char)0x7F: return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForEscapeIntermediate : StateHandler {

    public StateHandlerForEscapeIntermediate(AnsiStreamParser context) :
        base(StateId.EscapeIntermediate, context) {}

	public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x2F:
				Collect();
				return;
			case >= (char)0x30 and <= (char)0x7E:
				ChangeState(StateId.Ground, EscDispatch);
				return;
			case (char)0x7F: return; // Ignore character.
			case >= (char)0xA0:
				ChangeState(StateId.Ground, EscDispatch);
				return;
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiEntry : StateHandler {

    public StateHandlerForCsiEntry(AnsiStreamParser context) :
        base(StateId.CsiEntry, context) {}

    public override void OnEnter() => Clear();

	public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x2F:
				ChangeState(StateId.CsiIntermediate, Collect);
				return;
			case >= (char)0x30 and <= (char)0x3B:
				ChangeState(StateId.CsiParam, Param);
				return;
			case >= (char)0x3C and <= (char)0x3F:
				ChangeState(StateId.CsiPrivateParam, PrivateParam);
				return;
			case >= (char)0x40 and <= (char)0x7E:
				ChangeState(StateId.Ground, CsiDispatch);
				return;
			case >= (char)0x7F: return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiParam : StateHandler {

    public StateHandlerForCsiParam(AnsiStreamParser context) :
        base(StateId.CsiParam, context) {}

	public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x2F:
				ChangeState(StateId.CsiIntermediate, Collect);
				return;
			case >= (char)0x30 and <= (char)0x3B:
				Param();
				return;
			case >= (char)0x3C and <= (char)0x3F:
				ChangeState(StateId.CsiIgnore);
				return;
			case >= (char)0x40 and <= (char)0x7E:
				ChangeState(StateId.Ground, CsiDispatch);
				return;
			case >= (char)0x7F: return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiPrivateParam : StateHandler {

	public StateHandlerForCsiPrivateParam(AnsiStreamParser context) :
		base(StateId.CsiPrivateParam, context) {}

    public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x2F:
				ChangeState(StateId.CsiIntermediate, Collect);
				return;
			case >= (char)0x30 and <= (char)0x3F:
				PrivateParam();
				return;
			case >= (char)0x40 and <= (char)0x7E:
				ChangeState(StateId.Ground, CsiDispatch);
				return;
			case >= (char)0x7F: return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
    }
}

internal class StateHandlerForCsiIntermediate : StateHandler {

    public StateHandlerForCsiIntermediate(AnsiStreamParser context) :
        base(StateId.CsiIntermediate, context) {}

	public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x2F:
				Collect();
				return;
			case >= (char)0x30 and <= (char)0x3F:
				ChangeState(StateId.CsiIgnore);
				return;
			case >= (char)0x40 and <= (char)0x7E:
				ChangeState(StateId.Ground, CsiDispatch);
				return;
			case >= (char)0x7F: return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiIgnore : StateHandler {
	// We end up in this state when the parser encounters a malformed CSI sequence.
	// This state consumes any remaining characters until the end of the CSI
	// sequence is reached, and then it discards the entire malformed CSC sequence.

    public StateHandlerForCsiIgnore(AnsiStreamParser context) :
        base(StateId.CsiIgnore, context) {}

	public override void OnProcess() {
		if (ProcessControlCode()) return;

		switch (ch) {
			case >= (char)0x20 and <= (char)0x3F:
				// Ignore
				return;
			case >= (char)0x40 and <= (char)0x7E:
				ChangeState(StateId.Ground);
				return;
			case (char)0x7F:
				// Ignore character.
				return;
			case >= (char)0xA0:
				ChangeState(StateId.Ground);
				return;
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCommandString : StateHandler {
	// Handler for ECMA-48 command strings.
	
	// According to ECMA-48, command strings allow characters in the ranges 0x08-0x0D
	// and 0x20-0x7E. The string terminator (ST) sequence (0x9C or 0x1B 0x5C) terminates
	// a command string. The behavior of other C0 and C1 control codes and code 0x7F is
	// not defined.

	// The present implementation treats all undefined C0 and C1 control codes as string
	// terminators. This behavior is consistent with Xterm, which can terminate a command
	// string with 0x07 (BEL).
	
	// After terminating the string, C0 control codes apart from 0x18, 0x1A, and 0x1B
	// are ignored, while those three codes and all C1 control codes elicit their normal
	// unconditional behaviors. Code 0x7F is ignored. Futhermore, all codes from 0xA0
	// through 0xFFFF are allowed in the command string.
	
    public StateHandlerForCommandString(AnsiStreamParser context) :
        base(StateId.CommandString, context) {}

	public override void OnProcess() {
		if (ProcessUnconditionalControlCode()) return;

		switch (ch) {
			case >= (char)0x08 and <= (char)0x0D:
				// Handle the C0 codes that are specifically allowed in command strings.
				PutChar();
				return;
			case <= (char)0x07:
			case >= (char)0x0E and <= (char)0x1F:
				// All other C0 codes terminate the command
				// string but are otherwise ignored.
				ChangeState(StateId.Ground);
				return;
			case >= (char)0x20 and <= (char)0x7E:
				// ECMA-48 permits these codes in all control strings.
				PutChar();
				return;
			case (char)0x7F:
				// Behavior for command strings is undefined in ECMA-48.
				// We just ignore this code.
				return;
			case >= (char)0xA0:
				// Behavior undefined in ECMA-48.
				// Allow all other UTF-16 characters in the command string.
				PutChar();
				return;
		}
		throw new UnhandledCharacterCodeException(ch);
	}

    public override void OnExit() => TerminateString();
}

internal class StateHandlerForCharacterString : StateHandler {
	// Handler for ECMA-48 command strings.
	
	// According to ECMA-48, character strings allow all characters except for sequences
	// representing SOS and ST. That almost implies that only SOS strings are character
	// strings, and all other control strings are command strings. If DCS and PM strings
	// were character strings, then why wouldn't sequences representing DCS and PM also be
	// disallowed?

	// However, it is arguable that DCS and PM strings are character strings rather than
	// command strings. They don't have the word "command" in their names, and DEC terminals
	// allow characters in DCS strings that are not permitted in command strings.

	// In light of these considerations, the present implementation treats DCS, PM, and
	// SOS strings as character strings, while treating APC and OSC as command strings.
	// Furthermore, codes 0x18, 0x1A, 0x1B, and all C1 control codes are treated as
	// string terminators for character strings, rather than just SOS and ST sequences
	// as specified in ECMA-48.

    public StateHandlerForCharacterString(AnsiStreamParser context) :
        base(StateId.CharacterString, context) {}

	public override void OnProcess() {
		if (ProcessUnconditionalControlCode()) return;

		// All other codes are allowed in character strings.
		PutChar();
	}

    public override void OnExit() => TerminateString();
}
