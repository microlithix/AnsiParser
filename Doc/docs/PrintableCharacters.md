# Printable Characters

Printable characters are intended to be displayed to the user, and they appear in the input stream outside of any control sequences. Since the same characters can also appear inside control sequences, an input stream with embedded ANSI control sequences does not represent a strictly context-free grammar.

## AnsiPrintableChar

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableChar> elements represent a single printable character. Using an extended Backus-Naur form ([EBNF](Notation.md#extended-backus-naur-form)), such a character in the input stream can be described as follows:

```ebnf
<printable-char> ::= 0x20...0x7f | 0xa0...0xffff
```

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableChar> elements implement the <xref:Microlithix.Text.Ansi.Element.IAnsiStreamParserElement> interface and are produced by instances of <xref:Microlithix.Text.Ansi.AnsiStreamParser>.

## AnsiPrintableString

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableString> elements represent a sequence of one or more printable characters in the input, uninterrupted by any control sequences. Using an extended Backus-Naur form ([EBNF](Notation.md#extended-backus-naur-form)), they can be described as follows:

```ebnf
<printable-string> ::= <printable-char>+
<printable-char> ::= 0x20...0x7f | 0xa0...0xffff
```

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableString> elements implement the <xref:Microlithix.Text.Ansi.Element.IAnsiStringParserElement> interface and are produced by instances of <xref:Microlithix.Text.Ansi.AnsiStringParser>.
