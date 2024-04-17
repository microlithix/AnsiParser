# Getting Started

The easiest way to get started is to create a reference to the package from your project.

Most of the functionality of the package is available through two classes named <xref:Microlithix.Text.Ansi.AnsiStreamParser> and <xref:Microlithix.Text.Ansi.AnsiStringParser>. The former parses a single character on each invocation and returns parsed elements on the fly via a callback function. The latter parses an entire string on each invocation and returns the result as a list of parsed elements found in the string. Both ultimately provide the same functionality, so it is up to you to decide which is best suited for your application.

## AnsiStreamParser

<xref:Microlithix.Text.Ansi.AnsiStreamParser> parses one character at a time from a stream of character data. It expects the characters to be encoded in UTF-16 format, which is the standard encoding format for the .NET <xref:System.Char> type. It should generally also work with 7 and 8-bit codes that conform to [ECMA-35](References.md#ecma-35).

Here is a simple example of its usage:

```csharp
using Microlithix.Text.Ansi;
using Microlithix.Text.Ansi.Elements;

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

This example creates a new <xref:Microlithix.Text.Ansi.AnsiStreamParser> instance and provides a callback function that will be executed once for every parsed element produced by the parser. Then it simply calls the parser's <xref:Microlithix.Text.Ansi.AnsiStreamParser.Parse(System.Char)> method once for each character in the input stream. Executing this code will produce the following debug output:

```text
AnsiControlSequence { Parameters = [ 34 ], ControlFunction = m }
AnsiPrintableChar { Text = H }
AnsiPrintableChar { Text = e }
AnsiPrintableChar { Text = l }
AnsiPrintableChar { Text = l }
AnsiPrintableChar { Text = o }
AnsiControlSequence { Parameters = [ 0 ], ControlFunction = m }
```

The parsed elements will each be one of the [element types](Elements.md) implementing the <xref:Microlithix.Text.Ansi.Element.IAnsiStreamParserElement> interface.

Note that the parser preserves its state from one invocation to the next and produces a parsed element only when it has received the complete sequence of characters for that element. Therefore, there isn't a 1-to-1 mapping between invocations of the `Parse()` method and the production of elements. Rather, a single invocation of the `Parse()` method might result in the production zero, one, or two elements depending on the sequence of characters in the input stream.

## AnsiStringParser

<xref:Microlithix.Text.Ansi.AnsiStringParser> parses an entire string on each invocation of its <xref:Microlithix.Text.Ansi.AnsiStringParser.Parse(System.String)> method. It expects the strings to be encoded in UTF-16 format, which is the standard string encoding format for the .NET <xref:System.String> type. It should generally also work with 7 and 8-bit codes that conform to [ECMA-35](References.md#ecma-35).

<xref:Microlithix.Text.Ansi.AnsiStringParser> uses an internal instance of <xref:Microlithix.Text.Ansi.AnsiStreamParser> for its low-level parsing functionality. Unlike `AnsiStreamParser`, `AnsiStringParser` returns a list of element records rather than invoking a callback function for each element. Furthermore, the printable characters and the characters in control strings are consolidated into strings of characters returned in element records of type <xref:Microlithix.Text.Ansi.Element.AnsiPrintableString> and <xref:Microlithix.Text.Ansi.Element.AnsiControlString>, rather than being returned one character at a time.

Here is a simple example showing how `AnsiStringParser` can be used:

```csharp
using Microlithix.Text.Ansi;
using Microlithix.Text.Ansi.Elements;

AnsiStringParser parser = new();

List<IAnsiStringParserElement> elements = parser.Parse($"\x1b[34mHello\x1b[0m"));

foreach (element in elements) {
    Debug.Print(element);
}
```

This example creates a new `AnsiStringParser` instance and then simply calls its <xref:Microlithix.Text.Ansi.AnsiStringParser.Parse(System.String)> method once for each string to be parsed (in this case there is only a single string). The example above will produce the following debug output:

```text
AnsiControlSequence { Parameters = [ 34 ], ControlFunction = m }
AnsiPrintableString { Text = Hello }
AnsiControlSequence { Parameters = [ 0 ], ControlFunction = m }
```

The parsed elements will each be one of the [element types](Elements.md) implementing the <xref:Microlithix.Text.Ansi.Element.IAnsiStringParserElement> interface.

Note that the parser preserves its state from one invocation to the next, so an unterminated control sequence in one string may be continued or terminated by a character sequence in another string during a subsequent invocation.
