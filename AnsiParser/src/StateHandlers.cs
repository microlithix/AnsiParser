namespace Microlithix.Text.Ansi;

internal class StateHandlerForGround : StateHandler {
    // This is the initial state of the parser. Except for characters
    // that are part of escape and control sequences, it emits all
    // other characters as elements of type AnsiPrintElement.

    public StateHandlerForGround(AnsiStreamParser context) :
        base(StateId.Ground, context) {}

	public override void OnProcess() {
		if (DispatchControlCode()) return;
		DispatchChar();
	}
}

internal class StateHandlerForEscape : StateHandler {

    public StateHandlerForEscape(AnsiStreamParser context) :
        base(StateId.EscapeEntry, context) {}

    public override void OnEnter() => NewSequence();

	public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u002f':
				ChangeState(StateId.EscapeIntermediate, Intermediate);
				return;
			case '\u0050':
				ChangeState(StateId.CommandString, StartDCS);
				return;
			case '\u0058':
				ChangeState(StateId.CharacterString, StartSOS);
				return;
			case '\u005b':
				ChangeState(StateId.ControlSequenceEntry);
				return;
			case '\u005d':
				ChangeState(StateId.CommandString, StartOSC);
				return;
			case '\u005e':
				ChangeState(StateId.CommandString, StartPM);
				return;
			case '\u005f':
				ChangeState(StateId.CommandString, StartAPC);
				return;
			case >= '\u0030' and <= '\u004f':
			case >= '\u0051' and <= '\u0057':
			case >= '\u0059' and <= '\u005a':
			case '\u005c':
			case >= '\u0060' and <= '\u007e':
			case >= '\u00a0':
				ChangeState(StateId.Ground, DispatchEsc);
				return;
			case '\u007f': return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForEscapeIntermediate : StateHandler {

    public StateHandlerForEscapeIntermediate(AnsiStreamParser context) :
        base(StateId.EscapeIntermediate, context) {}

	public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u002f':
				Intermediate();
				return;
			case >= '\u0030' and <= '\u007e':
				ChangeState(StateId.Ground, DispatchEsc);
				return;
			case '\u007f': return; // Ignore character.
			case >= '\u00a0':
				ChangeState(StateId.Ground, DispatchEsc);
				return;
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiEntry : StateHandler {

    public StateHandlerForCsiEntry(AnsiStreamParser context) :
        base(StateId.ControlSequenceEntry, context) {}

    public override void OnEnter() => NewSequence();

	public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u002f':
				ChangeState(StateId.ControlSequenceIntermediate, Intermediate);
				return;
			case >= '\u0030' and <= '\u003b':
				ChangeState(StateId.ControlSequenceParameter, Parameter);
				return;
			case >= '\u003c' and <= '\u003f':
				ChangeState(StateId.ControlSequencePrivateParameter, PrivateParameter);
				return;
			case >= '\u0040' and <= '\u007e':
				ChangeState(StateId.Ground, DispatchSeq);
				return;
			case '\u007f':
			case >= '\u00a0':
				return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiParam : StateHandler {

    public StateHandlerForCsiParam(AnsiStreamParser context) :
        base(StateId.ControlSequenceParameter, context) {}

	public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u002f':
				ChangeState(StateId.ControlSequenceIntermediate, Intermediate);
				return;
			case >= '\u0030' and <= '\u003b':
				Parameter();
				return;
			case >= '\u003c' and <= '\u003f':
				ChangeState(StateId.ControlSequenceIgnore);
				return;
			case >= '\u0040' and <= '\u007e':
				ChangeState(StateId.Ground, DispatchSeq);
				return;
			case '\u007f':
			case >= '\u00a0':
				return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiPrivateParam : StateHandler {

	public StateHandlerForCsiPrivateParam(AnsiStreamParser context) :
		base(StateId.ControlSequencePrivateParameter, context) {}

    public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u002f':
				ChangeState(StateId.ControlSequenceIntermediate, Intermediate);
				return;
			case >= '\u0030' and <= '\u003f':
				PrivateParameter();
				return;
			case >= '\u0040' and <= '\u007e':
				ChangeState(StateId.Ground, DispatchSeq);
				return;
			case '\u007f':
			case >= '\u00a0':
				return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
    }
}

internal class StateHandlerForCsiIntermediate : StateHandler {

    public StateHandlerForCsiIntermediate(AnsiStreamParser context) :
        base(StateId.ControlSequenceIntermediate, context) {}

	public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u002f':
				Intermediate();
				return;
			case >= '\u0030' and <= '\u003f':
				ChangeState(StateId.ControlSequenceIgnore);
				return;
			case >= '\u0040' and <= '\u007e':
				ChangeState(StateId.Ground, DispatchSeq);
				return;
			case '\u007f':
			case >= '\u00a0':
				return; // Ignore character.
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCsiIgnore : StateHandler {
	// We end up in this state when the parser encounters a malformed CSI sequence.
	// This state consumes any remaining characters until the end of the CSI
	// sequence is reached, and then it discards the entire malformed CSC sequence.

    public StateHandlerForCsiIgnore(AnsiStreamParser context) :
        base(StateId.ControlSequenceIgnore, context) {}

	public override void OnProcess() {
		if (DispatchControlCode()) return;

		switch (ch) {
			case >= '\u0020' and <= '\u003f':
				// Ignore
				return;
			case >= '\u0040' and <= '\u007e':
				ChangeState(StateId.Ground);
				return;
			case '\u007f':
				// Ignore character.
				return;
			case >= '\u00a0':
				ChangeState(StateId.Ground);
				return;
		}
		throw new UnhandledCharacterCodeException(ch);
	}
}

internal class StateHandlerForCommandString : StateHandler {
	// Handler for ECMA-48 command strings.
	
	// According to ECMA-48, command strings allow characters in the ranges 0x08-0x0d
	// and 0x20-0x7e. The string terminator (ST) sequence (0x9c or 0x1b 0x5c) terminates
	// a command string. The behavior of other C0 and C1 control codes and code 0x7f is
	// not defined.

	// The present implementation treats all undefined C0 and C1 control codes as string
	// terminators. This behavior is consistent with Xterm, which can terminate a command
	// string with 0x07 (BEL).
	
	// After terminating the string, C0 control codes apart from 0x18, 0x1a, and 0x1b
	// are ignored, while those three codes and all C1 control codes elicit their normal
	// unconditional behaviors. Code 0x7f is ignored. Futhermore, all codes from 0xa0
	// through 0xffff are allowed in the command string.
	
    public StateHandlerForCommandString(AnsiStreamParser context) :
        base(StateId.CommandString, context) {}

	public override void OnProcess() {
		if (DispatchUnconditionalControlCode()) return;

		switch (ch) {
			case >= '\u0008' and <= '\u000d':
				// Handle the C0 codes that are specifically allowed in command strings.
				DispatchControlStringChar();
				return;
			case <= '\u0007':
			case >= '\u000e' and <= '\u0017':
			case '\u0019':
			case >= '\u001c' and <= '\u001f':
				// All other conditional C0 codes terminate
				// the command string but are otherwise ignored.
				ChangeState(StateId.Ground);
				return;
			case >= '\u0020' and <= '\u007e':
				// ECMA-48 permits these codes in all control strings.
				DispatchControlStringChar();
				return;
			case '\u007f':
				// Behavior for command strings is undefined in ECMA-48.
				// We just ignore this code.
				return;
			case >= '\u00a0':
				// Behavior undefined in ECMA-48.
				// Allow all other UTF-16 characters in the command string.
				DispatchControlStringChar();
				return;
		}
		throw new UnhandledCharacterCodeException(ch);
	}

    public override void OnExit() => DispatchStringTerminator();
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
	// Furthermore, codes 0x18, 0x1a, 0x1b, and all C1 control codes are treated as
	// string terminators for character strings, rather than just SOS and ST sequences
	// as specified in ECMA-48.

    public StateHandlerForCharacterString(AnsiStreamParser context) :
        base(StateId.CharacterString, context) {}

	public override void OnProcess() {
		if (DispatchUnconditionalControlCode()) return;

		// All other codes are allowed in character strings.
		DispatchControlStringChar();
	}

    public override void OnExit() => DispatchStringTerminator();
}
