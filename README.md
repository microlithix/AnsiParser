# ![Logo](https://raw.githubusercontent.com/microlithix/microlithix.github.io/main/docs/images/Logo-Round-100x100.png) Microlithix AnsiParser

This package contains modules for parsing character streams and strings with embedded control sequences that conform to specifications published in [ECMA-48](https://www.microlithix.com/AnsiParser/docs/References.html#ecma-48) and [ANSI X3.64](https://www.microlithix.com/AnsiParser/docs/References.html#ansi-x364), later adopted as [ISO/IEC 6429](https://www.microlithix.com/AnsiParser/docs/References.html#isoiec-6429). Such coded control sequences are commonly referred to as *ANSI escape sequences* or *ANSI escape codes*. These control sequences are often used by terminals, terminal emulators, command shells, and other types of console applications.

Note that this package implements a parser only, and its output is a sequence of parsed elements containing a structured representation of the sequences in the input stream. It doesn't perform any actions based on the parsed elements other than providing them to the consuming application for subsequent processing. It is the responsibility of the consuming application to act upon the parsed elements, and the [AnsiParser *Application Programming Interface*](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.html) (API) defines many named constants and other values to assist with the interpretation of the parsed elements in accordance with the higher-level functionality described in ECMA-48.

This work was inspired by Paul Flo Williams' [state machine description](https://vt100.net/emu/dec_ansi_parser) of the DEC VT500-Series parser, and by Joshua Haberman's [VTParse](https://github.com/haberman/vtparse) implementation of that state machine. See the [citations](https://www.microlithix.com/AnsiParser/docs/References.html#other-references) for details.

[Full Documentation](https://microlithix.com/AnsiParser/docs/Introduction.html)

[Release Notes](https://github.com/microlithix/AnsiParser/blob/main/RELEASENOTES.md)

[License](https://github.com/microlithix/AnsiParser/blob/main/LICENSE.md)

## Getting Started

To use AnsiParser in a `dotnet` project, type the following command line from within the project directory in order to create a reference to the latest version of the AnsiParser package:

```cmd
dotnet add package Microlithix.AnsiParser
```

Alternatively, you can create the package reference by copying the following line into the project file, specifying any [published version](https://www.nuget.org/packages/Microlithix.AnsiParser#versions-body-tab) that you wish to use:

```xml
<PackageReference Include="Microlithix.AnsiParser" Version="1.0.0" />
```

If you have any problems, verify that [NuGet.org](https://www.nuget.org/) has been configured as a package source by typing `dotnet nuget list source`.

Most of the functionality of the package is available through two classes named [AnsiStreamParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStreamParser.html) and [AnsiStringParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStringParser.html). The former parses a single character on each invocation and returns parsed elements on the fly via a callback function. The latter parses an entire string on each invocation and returns the result as a list of parsed elements found in the string. Both ultimately provide the same functionality, so it is up to you to decide which is best suited for your application.

## AnsiStreamParser

[AnsiStreamParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStreamParser.html) parses one character at a time from a stream of character data. It expects the characters to be encoded in [UTF-16](https://en.wikipedia.org/wiki/UTF-16) format, which is the standard encoding format for the .NET [char](https://learn.microsoft.com/dotnet/api/system.char) type. It should generally also work with 7 and 8-bit codes that conform to [ECMA-35](https://www.microlithix.com/AnsiParser/docs/References.html#ecma-35).

Here is a simple example of its usage:

```csharp
using Microlithix.Text.Ansi;
using Microlithix.Text.Ansi.Element;

AnsiStreamParser parser = new(ParsedElementCallback);

string inputStream = $"\x1b[34mHello\x1b[0m";

foreach (char ch in inputStream) {
    parser.Parse(ch);
}

void ParsedElementCallback(IAnsiStreamParserElement element) {
    // Handle the parsed element.
    Debug.Print($"{element}");
}
```

This example creates a new [AnsiStreamParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStreamParser.html) instance and provides a callback function that will be executed once for every parsed element produced by the parser. Then it simply calls the parser's [Parse(char)](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStreamParser.Parse.html#Microlithix_Text_Ansi_AnsiStreamParser_Parse_System_Char_) method once for each character in the input stream. Executing this code will produce the following debug output:

```text
AnsiControlSequence { Parameters = [ 34 ], ControlFunction = m }
AnsiPrintableChar { Text = H }
AnsiPrintableChar { Text = e }
AnsiPrintableChar { Text = l }
AnsiPrintableChar { Text = l }
AnsiPrintableChar { Text = o }
AnsiControlSequence { Parameters = [ 0 ], ControlFunction = m }
```

The parsed elements will each be one of the [element types](https://microlithix.com/AnsiParser/docs/Elements.html) implementing the [IAnsiStreamParserElement](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.Element.IAnsiStreamParserElement.html) interface. Strings are returned one character at a time in elements of type [AnsiPrintableChar](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.Element.AnsiPrintableChar.html) or [AnsiControlStringChar](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.Element.AnsiControlStringChar.html).

Note that the parser preserves its state from one invocation to the next and produces a parsed element only when it has received the complete sequence of characters for that element. Therefore, there isn't a 1-to-1 mapping between invocations of the `Parse()` method and the production of elements. Rather, a single invocation of the `Parse()` method might result in the production zero, one, or two elements depending on the sequence of characters in the input stream.

## AnsiStringParser

[AnsiStringParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStringParser.html) parses an entire string on each invocation of its [Parse(string)](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStringParser.Parse.html#Microlithix_Text_Ansi_AnsiStringParser_Parse_System_String_) method. It expects the strings to be encoded in [UTF-16](https://en.wikipedia.org/wiki/UTF-16) format, which is the standard string encoding format for the .NET [string](https://learn.microsoft.com/dotnet/api/system.string) type. It should generally also work with 7 and 8-bit codes that conform to [ECMA-35](https://www.microlithix.com/AnsiParser/docs/References.html#ecma-35).

[AnsiStringParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStringParser.html) uses an internal instance of [AnsiStreamParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStreamParser.html) for its low-level parsing functionality. Unlike `AnsiStreamParser`, `AnsiStringParser` returns a list of elements rather than invoking a callback function for each element. Furthermore, the printable characters and the characters in control strings are consolidated into strings of characters returned in elements of type [AnsiPrintableString](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.Element.AnsiPrintableString.html) and [AnsiControlString](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.Element.AnsiControlString.html), rather than being returned one character at a time.

Here is a simple example showing how `AnsiStringParser` can be used:

```csharp
using Microlithix.Text.Ansi;
using Microlithix.Text.Ansi.Element;

AnsiStringParser parser = new();

List<IAnsiStringParserElement> elements = parser.Parse($"\x1b[34mHello\x1b[0m");

foreach (IAnsiStringParserElement element in elements) {
    Debug.Print($"{element}");
}
```

This example creates a new [AnsiStringParser](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStringParser.html) instance and then simply calls its [Parse(string)](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.AnsiStringParser.Parse.html#Microlithix_Text_Ansi_AnsiStringParser_Parse_System_String_) method once for each string to be parsed (in this case there is only a single string). The example above will produce the following debug output:

```text
AnsiControlSequence { Parameters = [ 34 ], ControlFunction = m }
AnsiPrintableString { Text = Hello }
AnsiControlSequence { Parameters = [ 0 ], ControlFunction = m }
```

The parsed elements will each be one of the [element types](https://www.microlithix.com/AnsiParser/docs/Elements.html) implementing the [IAnsiStringParserElement](https://www.microlithix.com/AnsiParser/api/Microlithix.Text.Ansi.Element.IAnsiStringParserElement.html) interface.

Note that the parser preserves its state from one invocation to the next, so an unterminated control sequence in one string may be continued or terminated by a character sequence in another string during a subsequent invocation.
