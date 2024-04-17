//=================================================================================================
// AnsiStreamParser
//
// Parses a UTF-16 character stream containing printable text, control codes, and ANSI
// escape sequences into a stream of IAnsiStreamParserElement instances suitable for consumption
// by applications such as terminal emulators. Note that this module implements a parser only,
// and not an interpreter. The interpretation of the elements is domain-dependent and left to
// the consuming application.
//
// References:
// https://vt100.net/emu/dec_ansi_parser
//
// The referenced implementation assumes that characters are single bytes, and it treats
// characters in the range 0xA0-0xFF identically to characters in the range 0x20-0x7F.
// In other words, it ignores the high-order bit for characters above 0x9F.
//
// The present implementation assumes that characters are 16-bit words with UTF-16 encoding.
// It therefore treats all characters above 0x9F as printable characters only.
//
// Other references:
// https://invisible-island.net/xterm/ctlseqs/ctlseqs.html
// https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
// https://en.wikipedia.org/wiki/ANSI_escape_code#Colors
//=================================================================================================

using Microlithix.Text.Ansi.Element;

namespace Microlithix.Text.Ansi;

internal enum StateId {
	Ground,
	Escape,
	EscapeIntermediate,
	CsiEntry,
	CsiIgnore,
	CsiIntermediate,
	CsiParam,
	CsiPrivateParam,
	CommandString,
	CharacterString,
}

///----------------------------------------------------------------------------
/// <summary>
/// Parses character streams containing ANSI escape code sequences into
/// a structured representation suitable for higher-level processing.
/// </summary>
///
/// <remarks>
/// The UTF-16 character input stream may contain printable text, control
/// codes, and ANSI escape sequences. It will be parsed into a stream of
/// <see href="../docs/Elements.md">elements</see> implementing the
/// <see cref="IAnsiStreamParserElement"/> interface suitable for
/// consumption by applications such as terminal emulators.
/// 
/// Note that this module implements a parser only, and not an interpreter.
/// The interpretation of the elements is domain-dependent and left to the
/// consuming application.
/// </remarks>
///----------------------------------------------------------------------------
public class AnsiStreamParser {

	///........................................................................
	/// <summary>
	/// Creates a new AnsiStreamParser instance with default settings.
	/// </summary>
    /// 
	/// <remarks>
    /// If you use this constructor, you will need to call the
	/// <see cref="Parse(char, Action{IAnsiStreamParserElement})"/>
    /// overload that accepts a callback function in order to receive
    /// output from the parser.
	/// </remarks>
	///........................................................................
	public AnsiStreamParser() : this(new AnsiParserSettings()) {}

	///........................................................................
	/// <summary>
	/// Creates a new AnsiStreamParser instance with specified settings.
	/// </summary>
    ///
    /// <param name="settings">
    /// An <see cref="AnsiParserSettings"/> record for configuring the
    /// behavior of the parser.
    /// </param>
    /// 
	/// <remarks>
    /// If you use this constructor, you will need to call the
	/// <see cref="Parse(char, Action{IAnsiStreamParserElement})"/>
    /// overload that accepts a callback function in order to receive
    /// output from the parser.
	/// </remarks>
	///........................................................................
	public AnsiStreamParser(AnsiParserSettings settings) {
		// Create and register the state handlers.
		stateHandlers = new();
		groundStateHandler = RegisterHandler(new StateHandlerForGround(this));
		RegisterHandler(new StateHandlerForEscape(this));
		RegisterHandler(new StateHandlerForEscapeIntermediate(this));
		RegisterHandler(new StateHandlerForCsiEntry(this));
		RegisterHandler(new StateHandlerForCsiParam(this));
		RegisterHandler(new StateHandlerForCsiPrivateParam(this));
		RegisterHandler(new StateHandlerForCsiIntermediate(this));
		RegisterHandler(new StateHandlerForCsiIgnore(this));
		RegisterHandler(new StateHandlerForCommandString(this));
		RegisterHandler(new StateHandlerForCharacterString(this));
		currentStateHandler = groundStateHandler;
        this.settings = settings;
    }

	///........................................................................
	/// <summary>
	/// Creates a new AnsiStreamParser instance with default settings
    /// and a specified callback function for receiving the parsed elements.
	/// </summary>
    /// 
	/// <param name="callback">
    /// A function that will be called for each parsed element.
    /// </param>
	///........................................................................
	public AnsiStreamParser(Action<IAnsiStreamParserElement> callback) :
		this(new AnsiParserSettings(), callback) {}

	///........................................................................
	/// <summary>
	/// Creates a new AnsiStreamParser instance with specified settings
    /// and a specified callback function for receiving the parsed elements.
	/// </summary>
    /// 
	/// <param name="callback">
    /// A function that will be called for each parsed element.
    /// </param>
    /// <param name="settings">
    /// An <see cref="AnsiParserSettings"/> record for configuring the
    /// behavior of the parser.
    /// </param>
	///........................................................................
	public AnsiStreamParser(
		AnsiParserSettings settings,
		Action<IAnsiStreamParserElement> callback
	) : this(settings) {
		resultCallback = callback;
	}

	///........................................................................
	/// <summary>
	/// Resets the parser to its initial state.
	/// </summary>
    /// 
	/// <remarks>
	/// Normally the parser remembers its state so
    /// that repeated calls to <see cref="Parse(char)"/> or
    /// <see cref="Parse(char, Action{IAnsiStreamParserElement})"/>
    /// will be interpreted as a single continuous stream of
	/// characters. However, it may sometimes be necessary or
	/// useful to reset the state, such as when clearing the display.
	/// </remarks>
	///........................................................................
	public void Reset() {
		Clear();
		currentStateHandler = groundStateHandler;
	}

	///........................................................................
	/// <summary>
	/// Parses a single character.
    /// 
    /// The output will be sent to the callback method provided to the
    /// constructor.
	/// </summary>
    /// 
	/// <param name="ch">
    /// The UTF-16 character to be parsed
    /// </param>
    /// 
	/// <exception cref="System.InvalidOperationException">
    ///	Calling this method on an instance of <see cref="AnsiStreamParser"/>
    ///	created using its parameterless contructor will throw an exception.
    /// </exception>
    /// 
    /// <remarks>
    /// Call this method repeatedly with a stream of characters to generate
    /// an ordered list of <see cref="IAnsiStreamParserElement"/> records
    /// representing printable text strings, control codes, and escape
    /// sequences in the character stream.
    /// 
    /// A callback method needs to have been provided to the constructor
    /// when the <see cref="AnsiStreamParser"/> instance was created.
    /// 
    /// The callback method is invoked once for each generated record.
    /// Note that a single call to this method may result in the generation
    /// of zero, one, or more records and callback invocations.
	///
	/// The character will be parsed in the context of any character
	/// stream already received by prior invocations of this method.
	/// </remarks>
	///........................................................................
	public void Parse(char ch) {
		if (resultCallback is null) throw new System.InvalidOperationException(
			"This method cannot be invoked because the object instance was " +
			"created without providing a callback function to the constructor");
        currentStateHandler.Process(ch);
	}

	///........................................................................
	/// <summary>
	/// Parses a single character.
    /// 
	/// The output will be sent to the provided callback method.
	/// </summary>
    /// 
	/// <param name="callback">
    /// The function to receive the parsed results
    /// </param>
	/// <param name="ch">
    /// The UTF-16 character to be parsed
    /// </param>
    /// 
	/// <remarks>
    /// Call this method repeatedly with a stream of characters to generate
    /// an ordered list of <see cref="IAnsiStreamParserElement"/> records
    /// representing printable text strings, control codes, and escape
    /// sequences in the character stream.
    /// 
    /// The callback method is invoked once for each generated record.
    /// Note that a single call to this method may result in the generation
    /// of zero, one, or more records and callback invocations.
	///
	/// The character will be parsed in the context of the character
	/// stream already received by prior invocations of this method.
	/// </remarks>
	///........................................................................
	public void Parse(char ch, Action<IAnsiStreamParserElement> callback) {
		Action<IAnsiStreamParserElement>? savedCallback = resultCallback;
		try	{
			resultCallback = callback;
			Parse(ch);
		}
		finally {
			resultCallback = savedCallback;
		}
	}

    //--------------------
    // Non-public members
    //--------------------

    AnsiParserSettings settings = new();
    internal Action<IAnsiStreamParserElement>? resultCallback;
	internal Dictionary<StateId, StateHandler> stateHandlers;
	private StateHandler groundStateHandler;

	// Internal state that evolves as characters are received and processed.
	private StateHandler currentStateHandler;
	private string intermediateChars = "";
	private IList<Parameter> parameters = new List<Parameter>();
	private string privateParameters = "";
	private ControlStringType stringType = ControlStringType.StartOfString;

	internal void Emit(IAnsiStreamParserElement element) => resultCallback?.Invoke(element);

	internal void ChangeState(StateId newState, Action? transitionAction = null) {
		// Every state change must be performed via a call to this method.
		// ChangeState() performs the following 4 tasks in this order:
		// 1. Invokes any exit action for the current state.
		// 2. Invokes any provided transition action.
		// 3. Enters the new state.
		// 4. Invokes any entry action for the new state.
		currentStateHandler.OnExit();
		transitionAction?.Invoke();
		currentStateHandler = stateHandlers[newState];
		currentStateHandler.OnEnter();
	}

	// Internal Actions

	internal void Clear() {
		// Drops any accumulated private flag, intermediate characters,
		// final character, and parameters in order to reset the parser
		// on entry into the Escape, CsiEntry, and DcsEntry states.
		// This action will flush out any lingering data from erroneous
		// control sequences that weren't terminated properly.
		intermediateChars = "";
		parameters = new List<Parameter>();
		privateParameters = "";
		stringType = ControlStringType.StartOfString;
	}

	internal void Collect(char ch) {
		// Accumulates any private marker and intermediate characters
		// for later use.
		intermediateChars += ch;
	}

	internal void Param(char ch) {
		// Parse a character from a parameter sequence.
		
		// <parameter sequence> := <parameter>[;<parameter>]
		// <parameter> := <parameter value>[:<parameter value>]
		// <parameter value> := [0..9]

		// Parameter sequences consist of one or more
		// parameters separated by semicolon ';' characters.
		// Parameters consist of one or more parameter
		// values separated by colon ':' characters.
		// Parameter values consist of zero or more decimal digits.
		// The absence of any decimal digits indicates that the
		// parameter value should be set to a default value.

		// The 'ch' parameter must contain a decimal digit, a colon, or a semicolon.
		if (ch < (char)0x30 || ch > (char)0x3B) return;

		if (parameters.Count < 1) {
			// Create the first parameter, initialized with a single
			// parameter value set equal to the default indicator.
			parameters.Add(new Parameter());
		}		
		if (ch == ';') {
			// We have finished parsing the current parameter.
			// Create another parameter, initialized with a single
			// parameter value set equal to the default indicator.
			parameters.Add(new Parameter());
			return;
		}

		// Get the most recently created parameter. This
		// parameter is still in the process of being parsed.
		Parameter parameter = parameters.Last();

		if (ch == ':') {
			// We have finsished parsing the current parameter value.
			// Create another parameter value, initialized with the default indicator.
			parameter.Parts.Add(-1);
			return;
		}

		// Get the index of the most recently create parameter value.
		// This parameter value is still in the process of being parsed.
		int j = parameter.Parts.Count - 1;

		// If the parameter has a value less than 1, then it
		// still indicates that it should have its default value.
		// But we have now encountered an explicit decimal digit,
		// so set the value to zero so that we can start
		// accumulating the explicit value for the parameter.
		if (parameter.Parts[j] < 0) parameter.Parts[j] = 0;
		
		// Accumulate the next parameter digit.
		parameter.Parts[j] = parameter.Parts[j] * 10 + ch - '0';
	}

	internal void PrivateParam(char ch ) {
		// Parse a character from a private parameter sequence.
		privateParameters += ch;
	}

	// Actions that generate element records.

	internal void Execute(char ch) {
		// Executes a C0 or C1 control function.
		// These control function take no parameters.
		Emit(new AnsiSolitaryControlCode(ch));
	}

	internal void Print(char ch) {
		// Displays a character glyph.
		// Occurs only in the ground state outside of any escape sequences.
		Clear();
		Emit(new AnsiPrintableChar(ch));
	}

	internal void EscDispatch(char ch) {
		// The final character of an escape sequence has arrived, so determine
		// the control function to be executed from the intermediate character(s)
		// and final character, and execute it. The intermediate characters are
		// available because Collect() stored them as they arrived.
		Emit(new AnsiEscapeSequence(ch, intermediateChars));
	}

	internal void CsiDispatch(char ch) {
        // The final character of a CSI escape seqence has arrived.
        if (!string.IsNullOrEmpty(privateParameters)) {
            Emit(new AnsiPrivateControlSequence($"{intermediateChars}{ch}", privateParameters));
            return;
        }
        AnsiControlSequence element = new AnsiControlSequence($"{intermediateChars}{ch}", parameters);
		if (settings.PreserveLegacySGRParameters) Emit(element);
		else Emit(element.ConvertLegacySGRParameters());
    }

	internal void StartString(ControlStringType type) {
		stringType = type;
		Emit(new AnsiControlStringInitiator(type));
	}
	
	internal void PutChar(char ch) =>
		Emit(new AnsiControlStringChar(stringType, ch));

	internal void TerminateString() =>
		Emit(new AnsiControlStringTerminator(stringType));

	// Private methods.

	private StateHandler RegisterHandler(StateHandler handler) {
		// Register a new state handler in the handlers
		// Dictionary, keyed by its StateId.

		StateId id = handler.StateId;
		if (stateHandlers.ContainsKey(id)) throw new Exception(
			$"AnsiStreamParser internal error: A handler for {id} is already registered.");
		
		stateHandlers[id] = handler;
		return handler;
	}
}
