# Element Types

Both <xref:Microlithic.Text.Ansi.AnsiStreamParser> and <xref:Microlithic.Text.Ansi.AnsiStringParser> parse character streams and strings into element records holding a structured representation of the input. All of the element record types are defined in the <xref:Microlithic.Text.Ansi.Element> namespace and each implements one or both of the <xref:Microlithic.Text.Ansi.Element.IAnsiStreamParserElement> and <xref:Microlithic.Text.Ansi.Element.IAnsiStringParserElement> interfaces.

Elements implementing the <xref:Microlithic.Text.Ansi.Element.IAnsiStreamParserElement> interface are produced by instances of <xref:Microlithic.Text.Ansi.AnsiStreamParser>, while elements implementing the <xref:Microlithic.Text.Ansi.Element.IAnsiStringParserElement> interface are produced by instances of <xref:Microlithic.Text.Ansi.AnsiStringParser>.

Here is a summary table showing the different types of element records that can be produced:

Element Type | <xref:Microlithic.Text.Ansi.Element.IAnsiStreamParserElement> | <xref:Microlithic.Text.Ansi.Element.IAnsiStringParserElement>
-----------------------------------------------------------------|:--------:|:-------:
<xref:Microlithic.Text.Ansi.Element.AnsiPrintableChar>           | &#x2714; | &#x2716;
<xref:Microlithic.Text.Ansi.Element.AnsiPrintableString>         | &#x2716; | &#x2714;
<xref:Microlithic.Text.Ansi.Element.AnsiSolitaryControlCode>     | &#x2714; | &#x2714;
<xref:Microlithic.Text.Ansi.Element.AnsiEscapeSequence>          | &#x2714; | &#x2714;
<xref:Microlithic.Text.Ansi.Element.AnsiControlSequence>         | &#x2714; | &#x2714;
<xref:Microlithic.Text.Ansi.Element.AnsiPrivateControlSequence>  | &#x2714; | &#x2714;
<xref:Microlithic.Text.Ansi.Element.AnsiControlStringInitiator>  | &#x2714; | &#x2716;
<xref:Microlithic.Text.Ansi.Element.AnsiControlStringChar>       | &#x2714; | &#x2716;
<xref:Microlithic.Text.Ansi.Element.AnsiControlStringTerminator> | &#x2714; | &#x2716;
<xref:Microlithic.Text.Ansi.Element.AnsiControlString>           | &#x2716; | &#x2714;
