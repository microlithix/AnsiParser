# Control Sequences

Control sequences will be parsed into one of two element types depending on whether or not the sequence contains a private parameter string. Control sequences without a private parameter string will be parsed into standard <xref:Microlithix.Text.Ansi.Element.AnsiControlSequence> elements in accordance with Section 5.4 of [ECMA-48](References.md#ecma-48), while control sequences with private parameter strings will be parsed into <xref:Microlithix.Text.Ansi.Element.AnsiPrivateControlSequence> elements.

In Backus-Naur form, a control sequence can be described at a high level as follows:

```ebnf
<control-sequence> ::= <standard-control-sequence> | <private-control-sequence>
```

## Standard Control Sequences

Using an extended Backus-Naur form ([EBNF](Notation.md#extended-backus-naur-form)), a standard control sequence can be described as follows:

```ebnf
<standard-control-sequence> ::= <cs-initiator> <parameter-string> <control-function>

<cs-initiator>              ::= <CSI> | <ESC> <FE-CSI>
<CSI>                       ::= 0x9b
<ESC>                       ::= 0x1b
<FE-CSI>                    ::= 0x5b  // '['

<parameter-string>          ::= <parameter-substring> (";" <parameter-substring>)*
<parameter-substring>       ::= <part> (":" <part>)*
<part>                      ::= <digit>*
<digit>                     ::= "0"..."9"

<control-function>          ::= <intermediate-byte>* <final-byte>
<intermediate-byte>         ::= 0x20...0x2f
<final-byte>                ::= 0x40...0x7e
```

Here, `<csi>` is the control sequence introducer ([ControlCode.CSI](xref:Microlithix.Text.Ansi.ControlCode.CSI)), `<esc>` is the ESC control code ([ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC)), and `[` is the control sequence introducer escape code ([EscapeCode.CSI](xref:Microlithix.Text.Ansi.EscapeCode.CSI)). A control sequence consists of a control sequence initiator followed by a parameter string and terminated by a control function.

A parameter string consists of one or more parameter substrings separated by "`;`". And each parameter substring consists of one or more parts, separated by "`:`". Each part is a sequence of zero or more digits, so missing parts, missing parameter substrings, and even an entirely missing parameter string are all permissible. When evaluating the parameter string, a missing parameter part is to be interpreted as if the parameter part were present with a default value.

When parsed into an <xref:Microlithix.Text.Ansi.Element.AnsiControlSequence> element, any missing parameter part at the beginning or within a parameter substring will be assigned a value of -1, indicating to the consuming application that it should use a default value for that particular parameter or parameter part. Missing parameter parts at the end of a parameter substring and missing parameters from the end of the parameter string may not be present at all in the parsed output. In these cases, the consuming application is also to assign default values to the missing parameter parts. So the rule for consuming applications is very simple: if a parameter part is missing or has a value of -1, it should be assigned its default value.

The `<control-function>` consists of zero or more intermediate bytes followed by exactly one final byte. The <xref:Microlithix.Text.Ansi.ControlFunction> class defines constants for all of the control functions described in [ECMA-48](References.md#ecma-48).

Here are some examples of C# strings containing valid control sequences with a control function of `"m"` and various types of parameters:

```csharp
ControlCode.ESC + "m"             // No parameters specified. All parameters assumed to have default values.
ControlCode.ESC + "[m"            // Alternate (more commonly used) control sequence introducer.
ControlCode.ESC + "[5m"           // One parameter with one part equal to '5'.
ControlCode.ESC + "[5:22m"        // One parameter with two parts equal to '5' and '22'.
ControlCode.ESC + "[1;3m"         // Two parameters, each with one part.
ControlCode.ESC + "[1;3:4m"       // Two parameters, the second parameter having two parts.
ControlCode.ESC + "[;3m"          // Two parameters, the first having a default value.
ControlCode.ESC + "[38:2::4:5:6m" // One parameter with 6 parts, the third part having a default value.
```

## Private Control Sequences

A private control sequence holds a parameter string in an application-defined format, and can be described as follows:

```ebnf
<private-control-sequence> ::= <cs-initiator> <private-parameter-string> <control-function>

<cs-initiator>              ::= <CSI> | <ESC> <FE-CSI>
<CSI>                       ::= 0x9b
<ESC>                       ::= 0x1b
<FE-CSI>                    ::= 0x5b  // '['

<private-parameter-string> ::= <ppi> <parameter-byte>*
<ppi>                      ::= 0x3c...0x3f
<parameter-byte>           ::= 0x30...0x3f

<control-function>         ::= <intermediate-byte>* <final-byte>
<intermediate-byte>        ::= 0x20...0x2f
<final-byte>               ::= 0x40...0x7e
```

It is distinguished from a standard control sequence by the fact that its parameter string begins with one of the private parameter introducer `<ppi>` characters, which are not valid in the parameter string of a standard control sequence. The detailed form of the parameter string is application-defined and not described in ECMA-48.

## Select Graphic Rendition

When the control function is `"m"` ([ControlFunction.SGR](xref:Microlithix.Text.Ansi.ControlFunction.SGR)), the control sequence is to be interpreated as a Select Graphic Rendition (SGR) sequence. [ECMA-48](References.md#ecma-48) specifies that an SGR control sequence may contain multiple parameters, each of which specifies a particular graphic rendition aspect such as bold, italicized, underlined, etc. The <xref:Microlithix.Text.Ansi.GraphicRenditionSelector> class defines constants for the permissible parameter values. For example, the following control sequence will cause subsequent printable characters to be bold and underlined:

```csharp
ControlCode.ESC + "[1;4m"
```

### SetForegroundColor and SetBackgroundColor

Two of the possible graphic rendition selector parameter values, <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor> (`"38"`) and <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor> (`"48"`), are not defined explicitly in [ECMA-48](References.md#ecma-48). Rather, ECMA-48 refers to  [ISO/IEC 8613-6 \[CCITT Recommendation T.416\]](References.md#isoiec-8613-6-ccitt-recommendation-t416) for their definition.

According to ISO/IEC 8613-6, the <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor> and <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor> parameters are to include one or more additional parameter parts that are used to select a specific color.

The first part after the foreground/background selector value indicates what type of color specifier follows. There are 6 possible types, enumerated in <xref:Microlithix.Text.Ansi.ColorSelectionFormat>, and each type requires a specific number of additional parameter parts to follow. The EBNF syntax for these parameter substrings is as follows:

```ebnf
<set-color>            ::= <color-region> ':' <color-type>

<color-region>         ::= <set-foreground-color> | <set-background-color>
<set-foreground-color> ::= '3' '8'
<set-background-color> ::= '4' '8'

<color-type>           ::= <ct-private> | <ct-transparent> | <ct-rgb> | <ct-cmy> | <ct-cmyk> | <ct-palette>

<ct-private>           ::= '0'
<ct-transparent>       ::= '1'
<ct-rgb>               ::= '2' ':' <color-space> ':' <r> ':' <g> ':' <b>
<ct-cmy>               ::= '3' ':' <color-space> ':' <c> ':' <m> ':' <y>
<ct-cmyk>              ::= '4' ':' <color-space> ':' <c> ':' <m> ':' <y> ':' <k>
<ct-palette>           ::= '5' ':' <palette-index>

<color-space>          ::= // Undefined (see note below)

// The following non-terminals each define a range of zero or
// more decimal digits that is to be interpreted as a positive
// integer value. When zero digits are present, the application
// is to treat the parameter as if it had a default value.

<r>                    ::= ('0'...'9')*
<g>                    ::= ('0'...'9')*
<b>                    ::= ('0'...'9')*
<c>                    ::= ('0'...'9')*
<m>                    ::= ('0'...'9')*
<y>                    ::= ('0'...'9')*
<k>                    ::= ('0'...'9')*
<palette-index>        ::= ('0'...'9')*
```

> [!Note]
> The interpretation of the `<color-space>` parameter part is not defined in [ISO/IEC 8613-6 \[CCITT Recommendation T.416\]](References.md#isoiec-8613-6-ccitt-recommendation-t416). In practice, this part typically has zero digits present in order to indicate that the application should use a default value.

To provide a complete example using the above syntax, a control sequence specifying both an RGB foreground color and an RGB background color might appear as follows:

```csharp
ControlCode.ESC + "[38:2::150:150:150;48:2::20:20:20"
```

Here, there are two parameters, each with 6 parts. The third part specifying the color space identifier has been omitted, indicating that the application should use a default color space.

### Legacy SGR Parameters

Unfortunately, [ISO/IEC 8613-6 \[CCITT Recommendation T.416\]](References.md#isoiec-8613-6-ccitt-recommendation-t416) contains ambiguities which have led to quite a bit of confusion and some incorrect implementations related to [SGR](#select-graphic-rendition) control sequences with parameters `38` (<xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor>) and `48` (<xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor>). For example, the specification states that parameter values `38` and `48` are to be followed by a "parameter substring", but it doesn't ever define that term. The term "parameter substring" is defined precisely in [ECMA-48](References.md#ecma-48) where it is taken to refer to an entire parameter inclusive of all of its parts, separated from other parameter substrings by the semicolon (`";"`) character. If that same definition is applied in ISO/IEC 8613-6, then a literal reading would imply that parameters `38` and `48` should be delimited from the following parts with a semicolon rather than a colon (`":"`). With this interpretation, the example above would be encoded as:

```csharp
ControlCode.ESC + "[38;2::150:150:150;48;2::20:20:20"
```

which according to ECMA-48 should be viewed a 4 separate parameters rather than 2. To make matters worse, when the popular [*xterm*](https://en.wikipedia.org/wiki/Xterm) application was patched to enable extended colour support, the developer apparently didn't have access to the ISO/IEC 8613-6 specification and had to guess at the correct implementation. It was decided that all values should be encoded as separate parameters, and the color space identifier was omitted entirely. With that implementation, the example above is encoded as:

```csharp
ControlCode.ESC + "[38;2;150;150;150;48;2;20;20;20"
```

Both of these non-compliant implementations can be referred to as "legacy SGR parameters", and both <xref:Microlithix.Text.Ansi.AnsiStreamParser> and <xref:Microlithix.Text.Ansi.AnsiStringParser> will detect them and automatically convert them into the standardized format.

If you wish to disable this automatic conversion and process all of the parameters in their native form, you can supply an instance of an <xref:Microlithix.Text.Ansi.AnsiParserSettings> record to the parser constructor with its <xref:Microlithix.Text.Ansi.AnsiParserSettings.PreserveLegacySGRParameters> property set to `true`.
