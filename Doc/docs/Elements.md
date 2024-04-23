# Elements

Both <xref:Microlithix.Text.Ansi.AnsiStreamParser> and <xref:Microlithix.Text.Ansi.AnsiStringParser> parse character streams and strings into elements holding a structured representation of the input. All of the element types are defined in the <xref:Microlithix.Text.Ansi.Element> namespace and each implements one or both of the <xref:Microlithix.Text.Ansi.Element.IAnsiStreamParserElement> and <xref:Microlithix.Text.Ansi.Element.IAnsiStringParserElement> interfaces.

Elements implementing the <xref:Microlithix.Text.Ansi.Element.IAnsiStreamParserElement> interface are produced by instances of <xref:Microlithix.Text.Ansi.AnsiStreamParser>, while elements implementing the <xref:Microlithix.Text.Ansi.Element.IAnsiStringParserElement> interface are produced by instances of <xref:Microlithix.Text.Ansi.AnsiStringParser>.

Here is a summary table showing the different types of elements that can be produced:

Element Type | <xref:Microlithix.Text.Ansi.Element.IAnsiStreamParserElement> | <xref:Microlithix.Text.Ansi.Element.IAnsiStringParserElement>
-----------------------------------------------------------------|:--------:|:-------:
<xref:Microlithix.Text.Ansi.Element.AnsiPrintableChar>           | &#x2714; | &#x2716;
<xref:Microlithix.Text.Ansi.Element.AnsiPrintableString>         | &#x2716; | &#x2714;
<xref:Microlithix.Text.Ansi.Element.AnsiSolitaryControlCode>     | &#x2714; | &#x2714;
<xref:Microlithix.Text.Ansi.Element.AnsiEscapeSequence>          | &#x2714; | &#x2714;
<xref:Microlithix.Text.Ansi.Element.AnsiControlSequence>         | &#x2714; | &#x2714;
<xref:Microlithix.Text.Ansi.Element.AnsiPrivateControlSequence>  | &#x2714; | &#x2714;
<xref:Microlithix.Text.Ansi.Element.AnsiControlStringInitiator>  | &#x2714; | &#x2716;
<xref:Microlithix.Text.Ansi.Element.AnsiControlStringChar>       | &#x2714; | &#x2716;
<xref:Microlithix.Text.Ansi.Element.AnsiControlStringTerminator> | &#x2714; | &#x2716;
<xref:Microlithix.Text.Ansi.Element.AnsiControlString>           | &#x2716; | &#x2714;
