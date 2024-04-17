//================================================================================================
// AnsiStringParser
//
// Parses a string or sequence of strings into an ordered list of IAnsiStringParserElement
// instances representing printable text strings, control codes, and escape sequences in the
// input data.
//================================================================================================

using System.Text;

using Microlithic.Text.Ansi.Element;

namespace Microlithic.Text.Ansi;

///----------------------------------------------------------------------------
/// <summary>
/// Parses strings containing ANSI escape code sequences into a
/// structured representation suitable for higher-level processing.
/// </summary>
/// 
/// <remarks>
/// The UTF-16 character strings to be parsed may contain printable text,
/// control codes, and ANSI escape sequences. They will be parsed into a
/// stream of <see cref="IAnsiStringParserElement"/> instances suitable for
/// consumption by applications such as terminal emulators.
/// 
/// Note that this module implements a parser only, and not an interpreter.
/// The interpretation of the elements is domain-dependent and left to the
/// consuming application.
/// </remarks>
///----------------------------------------------------------------------------
public class AnsiStringParser {
	///........................................................................
	/// <summary>
	/// Creates a new <see cref="AnsiStringParser"/> instance with
    /// default settings.
	/// </summary>
	///........................................................................
	public AnsiStringParser() {
		parser = new(GatherElements);
	}

	///........................................................................
	/// <summary>
	/// Creates a new <see cref="AnsiStringParser"/> instance with
    /// specified settings.
	/// </summary>
    /// 
    /// <param name="settings">
    /// A <see cref="AnsiParserSettings"/> record for configuring the
    /// behavior of the parser.
    /// </param>
	///........................................................................
	public AnsiStringParser(AnsiParserSettings settings) {
        parser = new(settings, GatherElements);
	}

	///........................................................................
	/// <summary>
	/// Resets the parser to its initial state.
	/// </summary>
    /// 
	/// <remarks>
	/// Normally the parser remembers its state so that repeated calls to
	/// <see cref="Parse"/> will be interpreted as a single continuous
	/// sequence of characters. However, it may sometimes be necessary or
	/// useful to reset the state, such as when clearing the display.
	/// </remarks>
	///........................................................................
    public void Reset()	{
        parser.Reset();
        ClearState();
        ClearOutput();
    }

	///........................................................................
	/// <summary>
	/// Parses a string into an ordered list of
    /// <see cref="IAnsiStringParserElement"/> records.
	/// </summary>
    /// 
	/// <param name="text">
	/// The UTF-16 string to be parsed.
	/// </param>
    /// 
	/// <returns>
	/// An ordered list of records representing the printable text strings,
    /// control codes, and escape sequences found in the string.
	/// </returns>
    /// 
	/// <remarks>
	/// The character will be parsed in the context of the character
	/// stream already received by prior invocations of this method.
	/// </remarks>
	///........................................................................
	public List<IAnsiStringParserElement> Parse(string text) {
        ClearOutput();
	    foreach (char ch in text) parser.Parse(ch);
        return Consolidate();
	}

	///........................................................................
    /// <summary>
    /// Converts a list of parsed element records into a string containing
    /// only the printable characters.
    /// </summary>
    /// 
    /// <param name="elements">
    /// A list holding the records to convert.
    /// </param>
    /// 
    /// <returns>
    /// A string containing only the printable characters from the input records.
    /// </returns>
    /// 
    /// <remarks>
    /// All of the control codes and escape sequences in list of input records
    /// are ignored. But note that the DEL character (0x7F) is considered to
    /// be printable.
    /// </remarks>
    /// ........................................................................
	public static string PrintableString(List<IAnsiStringParserElement> elements) {
		StringBuilder builder = new();
		foreach (IAnsiStringParserElement element in elements) {
			if (element is AnsiPrintableString printableString) {
				builder.Append(printableString.Text);
			}
		}
		return builder.ToString();
	}

	//-----------------------
	// Non-public properties
	//-----------------------

    private AnsiStreamParser parser;
    private List<IAnsiStreamParserElement> streamElements = new();
	private string text = "";
	private bool printMode = false;

	//--------------------
	// Non-public methods
	//--------------------

	private void GatherElements(IAnsiStreamParserElement element) {
		// Gather the elements as they are produced by the
		// AnsiStreamParser instance and assemble them into a list.
        streamElements.Add(element);
	}

    private void ClearOutput() {
        streamElements.Clear();
    }

    private void ClearState() {
        text = "";
        printMode = false;
    }

    private List<IAnsiStringParserElement> Consolidate() {
		List<IAnsiStringParserElement> parsedElements = new();

		foreach (IAnsiStreamParserElement element in streamElements) {
			// If we have accumulated a print string and the next element
			// is not an AnsiPrintElement, then add a print element.
			if (printMode && element is not AnsiPrintableChar) {
				printMode = false;
				parsedElements.Add(new AnsiPrintableString(text));
			}

			// Then process the next element in the queue.
			IAnsiStringParserElement? consolidatedElement = ConsolidateElement(element);
			if (consolidatedElement is not null) parsedElements.Add(consolidatedElement);
		}

		// We have processed all of the elements.

		if (printMode) {
	        // Terminate an accumulated print string and
    	    // add it to the list of generated elements.
			printMode = false;
			parsedElements.Add(new AnsiPrintableString(text));
		}

		return parsedElements;
    }

	private IAnsiStringParserElement? ConsolidateElement(IAnsiStreamParserElement element) {
		// Consolidates IAnsiStreamParserElement instances into IAnsiStringParserElement
		// instances. If the element is already an IAnsiStringParserElement instance,
		// then it is returned as-is.

		if (element is IAnsiStringParserElement) return (IAnsiStringParserElement)element;

        switch (element) {
			case AnsiPrintableChar e:
				if (!printMode) text = "";
				printMode = true;
				text += e.Character;
				return null;
			case AnsiControlStringInitiator _:
				text = "";
				return null;
			case AnsiControlStringChar e:
				text += e.Character;
				return null;
			case AnsiControlStringTerminator e:
                return new AnsiControlString(e.Type, text);
		}

        throw new ArgumentOutOfRangeException(nameof(element), "Unknown type");
    }
}