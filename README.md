<!-- markdownlint-disable MD033 -->
# <img width="10%" title="Logo" alt="Logo" src="./Doc/images/Logo.svg"> Microlithic AnsiParser
<!-- markdownlint-enable MD033 -->

This package contains modules for parsing character streams and strings with embedded control sequences that conform to ECMA-48 and ANSI X3.64, later adopted as ISO/IEC 6429. Such coded control sequences are commonly referred to as *ANSI escape sequences* or *ANSI escape codes*. These control sequences are often used by terminals, terminal emulators, command shells, and other types of console applications.

Note that this package implements a parser only, and its output is a sequence of parsed elements containing a structured representation of the sequences in the input stream. It doesn't perform any actions based on the parsed elements other than providing them to the consuming application for subsequent processing. It is the responsibility of the consuming application to act upon the parsed elements.

However, the API and its associated documentation does define many named constants and other values to assist with the interpretation of the parsed elements in accordance with the higher-level functionality described in ECMA-48. The API documentation in particular will assist you with understanding ECMA-48 and how all of its components fit together.

[Full Documentation](https://microlithix.github.io/AnsiParser/docs/Introduction.html)

[License](LICENSE.md)

## Getting Started

Most of the functionality of the package is available through two classes named `AnsiStreamParser` and `AnsiStringParser`. The former parses a single character on each invocation and returns parsed elements on the fly via a callback function. The latter parses an entire string on each invocation and returns the result as a list of parsed elements found in the string. Both ultimately provide the same functionality, so it is up to you to decide which is best suited for your application.

## AnsiStreamParser

`AnsiStreamParser` parses one character at a time from a stream of character data. It expects the characters to be encoded in UTF-16 format, which is the standard encoding format for the .NET `System.Char` type. It should generally also work with 7 and 8-bit codes that conform to ECMA-35.

Here is a simple example of its usage:

```csharp
using Microlithic.Text.Ansi;
using Microlithic.Text.Ansi.Elements;

AnsiStreamParser parser = new(ParsedElementCallback);

string inputStream = $"\x1b[34mHello\x1b[0m";

foreach (ch in inputStream) {
    parser.Parse(ch);
}

void ParsedElementCallback(IAnsiStreamParserElement element) {
    // Handle the parsed element.
    Debug.Print(element);
}
```

This example creates a new `AnsiStreamParser` instance and provides a callback function that will be executed once for every parsed element produced by the parser. Then it simply calls the parser's `Parse(System.Char)` method once for each character in the input stream. Executing this code will produce the following debug output:

```text
AnsiControlSequence { Parameters = [ 34 ], ControlFunction = m }
AnsiPrintableChar { Text = H }
AnsiPrintableChar { Text = e }
AnsiPrintableChar { Text = l }
AnsiPrintableChar { Text = l }
AnsiPrintableChar { Text = o }
AnsiControlSequence { Parameters = [ 0 ], ControlFunction = m }
```

The parsed elements will each be one of the [element types](https://microlithix.github.io/AnsiParser/docs/Elements.md) implementing the `IAnsiStreamParserElement` interface.

## AnsiStringParser

`AnsiStringParser` parses an entire string on each invocation of its `Parse(System.String)` method. It expects the strings to be encoded in UTF-16 format, which is the standard string encoding format for the .NET `System.String` type. It should generally also work with 7 and 8-bit codes that conform to ECMA-35.

`AnsiStringParser` uses an internal instance of `AnsiStreamParser` for its low-level parsing functionality. Unlike `AnsiStreamParser`, `AnsiStringParser` returns a list of element records rather than invoking a callback function for each element. Furthermore, the printable characters and the characters in control strings are consolidated into strings of characters returned in element records of type `AnsiPrintableString` and `AnsiControlString`, rather than being returned one character at a time.

Here is a simple example showing how `AnsiStringParser` can be used:

```csharp
using Microlithic.Text.Ansi;
using Microlithic.Text.Ansi.Elements;

AnsiStringParser parser = new();

List<IAnsiStringParserElement> elements = parser.Parse($"\x1b[34mHello\x1b[0m"));

foreach (element in elements) {
    Debug.Print(element);
}
```

This example creates a new `AnsiStringParser` instance and then simply calls its `Parse(System.String)` method once for each string to be parsed (in this case there is only a single string). The example above will produce the following debug output:

```text
AnsiControlSequence { Parameters = [ 34 ], ControlFunction = m }
AnsiPrintableString { Text = Hello }
AnsiControlSequence { Parameters = [ 0 ], ControlFunction = m }
```

The parsed elements will each be one of the element types implementing the `IAnsiStringParserElement` interface.

Note that the parser preserves its state from one invocation to the next, so an unterminated control sequence in one string may be continued or terminated by a character sequence in another string during a subsequent invocation.
