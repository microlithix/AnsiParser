# Control Sequences

Control sequences will be parsed into one of two element types depending on whether or not the sequence contains a private parameter string. Control sequences without a private parameter string will be parsed into standard <xref:Microlithix.Text.Ansi.Element.AnsiControlSequence> records in accordance with Section 5.4 of [ECMA-48](References.md#ecma-48), while control sequences with private parameter strings will be parsed into <xref:Microlithix.Text.Ansi.Element.AnsiPrivateControlSequence> records.

In Backus-Naur form, a control sequence can be described as follows:

```bnf
<control-sequence> ::= <standard-control-sequence> | <private-control-sequence>
```

## Standard Control Sequences

Using an extended Backus-Naur form ([EBNF](EBNF.md)), a standard control sequence can be described as follows:

```bnf
<standard-control-sequence> ::= <csi> <parameter-string> <control-function>
<csi>                       ::= ControlCode.CSI | ControlCode.ESC EscapeCode.CSI
<parameter-string>          ::= <parameter-substring> (";" <parameter-substring>)*
<parameter-substring>       ::= <part> (":" <part>)*
<part>                      ::= <digit>*
<digit>                     ::= "0"..."9"
<control-function>          ::= <intermediate-byte>* <final-byte>
<intermediate-byte>         ::= "\u0020"..."\u002f"
<final-byte>                ::= "\u0040"..."\u007e"
```

where [ControlCode.CSI](xref:Microlithix.Text.Ansi.ControlCode.CSI) is the control sequence introducer (`"\u009b"`), [ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC) is the ESC control code (`"\u001b"`), and [EscapeCode.CSI](xref:Microlithix.Text.Ansi.EscapeCode.CSI) is the control sequence introducer escape code (`[`). A control sequence consists of the control sequence introducer `<csi>` followed by a parameter string and terminated by a control function.

A parameter string consists of one or more parameter substrings separated by "`;`". And each parameter substring consists of one or more parts, separated by "`:`". Each part is a sequence of zero or more digits, so missing parts, missing parameter substrings, and even an entirely missing parameter string are all permissible. When evaluating the parameter string, a missing parameter part is to be interpreted as if the parameter part were present with a default value.

When parsed into an <xref:Microlithix.Text.Ansi.Element.AnsiControlSequence> record, any missing parameter part at the beginning or within a parameter substring will be assigned a value of -1, indicating to the consuming application that it should use a default value for that particular parameter or parameter part. Missing parameter parts at the end of a parameter substring and missing parameters from the end of the parameter string may not be present at all in the parsed output. In these cases, the consuming application is also to assign default values to the missing parameter parts. So the rule for consuming applications is very simple: if a parameter part is missing or has a value of -1, it should be assigned its default value.

The `<control-function>` consists of zero or more intermediate bytes followed by exactly one final byte. The <xref:Microlithix.Text.Ansi.ControlFunction> class defines constants for all of the control functions described in [ECMA-48](References.md#ecma-48).

Here are some examples of valid control sequences with a control function of `"m"` and various types of parameters:

```csharp
"\u009bm"             // No parameters specified. All parameters assumed to have default values.
"\u001b[m"            // Alternate (more commonly used) control sequence introducer.
"\u001b[5m"           // One parameter with one part equal to '5'.
"\u001b[5:22m"        // One parameter with two parts equal to '5' and '22'.
"\u001b[1;3m"         // Two parameters, each with one part.
"\u001b[1;3:4m"       // Two parameters, the second parameter having two parts.
"\u001b[;3m"          // Two parameters, the first having a default value.
"\u001b[38:2::4:5:6m" // One parameter with 6 parts, the third part having a default value.
```

## Private Control Sequences

A private control sequence holds a parameter string in an application-defined format, and can be described as follows:

```bnf
<private-control-sequence> ::= <csi> <private-parameter-string> <control-function>
<csi>                      ::= ControlCode.CSI | ControlCode.ESC "["
<private-parameter-string> ::= <ppi> <parameter-byte>*
<ppi>                      ::= "\u003C"..."\u003F"
<parameter-byte>           ::= "\u0030"..."\u003F"
<control-function>         ::= <intermediate-byte>* <final-byte>
<intermediate-byte>        ::= "\u0020"..."\u002F"
<final-byte>               ::= "\u0040"..."\u007E"
```

It is distinguished from a standard control sequence by the fact that its parameter string begins with one of the private parameter introducer `<ppi>` characters, which are not valid in the parameter string of a standard control sequence. The detailed form of the parameter string is application-defined and not described in ECMA-48.

## Select Graphic Rendition

When the control function is `"m"` ([ControlFunction.SGR](xref:Microlithix.Text.Ansi.ControlFunction.SGR)), the control sequence is to be interpreated as a Select Graphic Rendition (SGR) sequence. [ECMA-48](References.md#ecma-48) specifies that an SGR control sequence may contain multiple parameters, each of which specifies a particular graphic rendition aspect such as bold, italicized, underlined, etc. The <xref:Microlithix.Text.Ansi.GraphicRenditionSelector> class defines constants for the permissible parameter values. For example, the following control sequence will cause subsequent printable characters to be bold and underlined:

```csharp
"\u001b[1;4m"
```

### SetForegroundColor and SetBackgroundColor

Two of the graphic rendition selector constants, `38` (<xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor>) and `48` (<xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor>), are not defined explicitly in [ECMA-48](References.md#ecma-48). Rather, ECMA-48 refers to  [ISO/IEC 8613-6 \[CCITT Recommendation T.416\]](References.md#isoiec-8613-6-ccitt-recommendation-t416) for their definition.

According to ISO/IEC 8613-6, <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor> and <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor> are to be followed by one or more parameter parts that are used to select the foreground color and the background color, respectively.

The first part after the foreground/background selector value indicates what type of color specifier follows. There are 5 possible types, enumerated in <xref:Microlithix.Text.Ansi.ColorSelectionFormat>, and each type requires a specific number of additional parameter parts to follow:

```bnf
<sc> ":" "0"                                                    // 2 parts in total, specifying an implementation-defined color.
<sc> ":" "1"                                                    // 2 parts in total, specifying "transparent".
<sc> ":" "2" ":" <color-space> ":" <R> ":" <G> ":" <B>          // 6 parts in total, specifying a direct color in RGB space.
<sc> ":" "3" ":" <color-space> ":" <C> ":" <M> ":" <Y>          // 6 parts in total, specifying a direct color in CMY space.
<sc> ":" "4" ":" <color-space> ":" <C> ":" <M> ":" <Y> ":" <K>  // 7 parts in total, specifying a direct color in CMYK space.
<sc> ":" "5" ":" <palette-index>
```

Here, `<sc>` is either <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor> or <xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor> and the type is the integer value 0 through 5 that follows it. For example, a control sequence specifying both an RGB foreground color and an RGB background color might appear as follows:

```csharp
"\u001b[38:2::150:150:150;48:2::20:20:20"
```

Here, there are two parameters, each with 6 parts. The third part specifying the color space identifier has been omitted, indicating that the application should use a default color space.

### Legacy SGR Parameters

Unfortunately, [ISO/IEC 8613-6 \[CCITT Recommendation T.416\]](References.md#isoiec-8613-6-ccitt-recommendation-t416) contains ambiguities which have led to quite a bit of confusion and some incorrect implementations related to [SGR](#select-graphic-rendition) control sequences with parameters `38` (<xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetForegroundColor>) and `48` (<xref:Microlithix.Text.Ansi.GraphicRenditionSelector.SetBackgroundColor>). For example, the specification states that parameter values `38` and `48` are to be followed by a "parameter substring", but it doesn't ever define that term. The term "parameter substring" is defined precisely in [ECMA-48](References.md#ecma-48) where it is taken to refer to an entire parameter inclusive of all of its parts, separated from other parameter substrings by the semicolon (`";"`) character. If that same definition is applied in ISO/IEC 8613-6, then a literal reading would imply that parameters `38` and `48` should be delimited from the following parts with a semicolon rather than a colon (`":"`). With this interpretation, the example above would be encoded as:

```csharp
"\u001b[38;2::150:150:150;48;2::20:20:20"
```

which according to ECMA-48 should be viewed a 4 separate parameters rather than 2. To make matters worse, when the popular [*xterm*](https://en.wikipedia.org/wiki/Xterm) application was patched to enable extended colour support, the developer apparently didn't have access to the ISO/IEC 8613-6 specification and had to guess at the correct implementation. It was decided that all values should be encoded as separate parameters, and the color space identifier was omitted entirely. With that implementation, the example above is encoded as:

```csharp
"\u001b[38;2;150;150;150;48;2;20;20;20"
```

Both of these non-compliant implementations can be referred to as "legacy SGR parameters", and both <xref:Microlithix.Text.Ansi.AnsiStreamParser> and <xref:Microlithix.Text.Ansi.AnsiStringParser> will detect them and automatically convert them into the standardized format.

If you wish to disable this automatic conversion and process all of the parameters in their native form, you can supply an instance of an <xref:Microlithix.Text.Ansi.AnsiParserSettings> record to the parser constructor with its <xref:Microlithix.Text.Ansi.AnsiParserSettings.PreserveLegacySGRParameters> property set to `true`.
