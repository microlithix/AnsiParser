# Printable Characters

Printable characters are intended to be displayed to the user, and they appear in the input stream outside of any control sequences. Since the same characters can also appear inside control sequences, an input stream with embedded ANSI control sequences does not represent a strictly context-free grammar.

## AnsiPrintableChar

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableChar> records represent a single printable character. Using an extended Backus-Naur form ([EBNF](EBNF.md)), such a character in the input stream can be described as follows:

```bnf
<printable-char> ::= "\u0020"..."\u007f" | "\u00A0"..."\uFFFF"
```

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableChar> records implement the <xref:Microlithix.Text.Ansi.Element.IAnsiStreamParserElement> interface and are produced by instances of <xref:Microlithix.Text.Ansi.AnsiStreamParser>.

## AnsiPrintableString

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableString> records represent a sequence of one or more printable characters in the input, uninterrupted by any control sequences. Using an extended Backus-Naur form ([EBNF](EBNF.md)), they can be described as follows:

```bnf
<printable-string> ::= <printable-char>+
<printable-char> ::= "\u0020"..."\u007f" | "\u00A0"..."\uFFFF"
```

<xref:Microlithix.Text.Ansi.Element.AnsiPrintableString> records implement the <xref:Microlithix.Text.Ansi.Element.IAnsiStringParserElement> interface and are produced by instances of <xref:Microlithix.Text.Ansi.AnsiStringParser>.
