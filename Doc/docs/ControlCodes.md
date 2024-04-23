# Control Codes

[Control Codes](xref:Microlithix.Text.Ansi.ControlCode) are single non-displayable characters that appear in the input stream. They are typically used by the consuming application to modify its own behavior or internal state, or to initiate [Escape Sequences](EscapeSequences.md), [Control Sequences](ControlSequences.md), and [Control Strings](ControlStrings.md).

Control codes have character encodings in the ranges `0x00`...`0x1f` ([C0 control codes](https://en.wikipedia.org/wiki/C0_and_C1_control_codes)) and `0x80`...`0x9f` ([C1 control codes](https://en.wikipedia.org/wiki/C0_and_C1_control_codes)). The C0 codes were originally defined in [ISO/IEC 646 (ASCII)](References.md#isoiec-646), and the C1 codes were originally defined in [ECMA-48](References.md#ecma-48). Most control codes are "solitary", meaning that they exist as independent characters in the input stream, and they do not have any impact on the parsing of subsequent characters.

Using an extended Backus-Naur form ([EBNF](Notation.md#extended-backus-naur-form)), solitary control codes can be described as follows:

```ebnf
<solitary-control-code> ::= <solitary-C0-code> | <solitary-C1-code>
<solitary-C0-code>      ::= 0x00...0x1a | 0x1c...0x1f
<solitary-C1-code>      ::= 0x80...0x8f | 0x91...0x97 | 0x99...0x9a
```

The remaining control codes are "introducers" or "terminators", meaning that they indicate the beginning or end of a variable-length sequence or control string in the input. These control codes are not themselves represented as parsed elements. Rather, it is the sequences and control strings they introduce or terminate that will be produced as structured elements by the parser. These codes and the elements they produce are the following:

+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| Control | API Symbol                                                    | Associated Elements                                              | Description                                                      |
| Code    |                                                               |                                                                  |                                                                  |
+=========+===============================================================+==================================================================|==================================================================|
| `0x1b`  | [ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC) | <xref:Microlithix.Text.Ansi.Element.AnsiEscapeSequence>          | [Escape Sequence](EscapeSequences.md)                            |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x90`  | [ControlCode.DCS](xref:Microlithix.Text.Ansi.ControlCode.DCS) | <xref:Microlithix.Text.Ansi.Element.AnsiControlString>\          | [Device Control String](ControlStrings.md#command-strings)       |
|         |                                                               | <xref:Microlithix.Text.Ansi.Element.AnsiControlStringInitiator>  |                                                                  |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x98`  | [ControlCode.SOS](xref:Microlithix.Text.Ansi.ControlCode.SOS) | <xref:Microlithix.Text.Ansi.Element.AnsiControlString>\          | [Start of String](ControlStrings.md#character-strings)           |
|         |                                                               | <xref:Microlithix.Text.Ansi.Element.AnsiControlStringInitiator>  |                                                                  |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x9b`  | [ControlCode.CSI](xref:Microlithix.Text.Ansi.ControlCode.CSI) | <xref:Microlithix.Text.Ansi.Element.AnsiControlSequence>         | [Control Sequence Introducer](ControlSequences.md)               |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x9c`  | [ControlCode.ST](xref:Microlithix.Text.Ansi.ControlCode.ST)   | <xref:Microlithix.Text.Ansi.Element.AnsiControlString>\          | [String Terminator](ControlStrings.md)                           |
|         |                                                               | <xref:Microlithix.Text.Ansi.Element.AnsiControlStringTerminator> |                                                                  |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x9d`  | [ControlCode.OSC](xref:Microlithix.Text.Ansi.ControlCode.OSC) | <xref:Microlithix.Text.Ansi.Element.AnsiControlString>\          | [Operating System Command](ControlStrings.md#command-strings)    |
|         |                                                               | <xref:Microlithix.Text.Ansi.Element.AnsiControlStringInitiator>  |                                                                  |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x9e`  | [ControlCode.PM](xref:Microlithix.Text.Ansi.ControlCode.PM)   | <xref:Microlithix.Text.Ansi.Element.AnsiControlString>\          | [Privacy Message](ControlStrings.md#command-strings)             |
|         |                                                               | <xref:Microlithix.Text.Ansi.Element.AnsiControlStringInitiator>  |                                                                  |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+
| `0x9f`  | [ControlCode.APC](xref:Microlithix.Text.Ansi.ControlCode.APC) | <xref:Microlithix.Text.Ansi.Element.AnsiControlString>\          | [Application Program Command](ControlStrings.md#command-strings) |
|         |                                                               | <xref:Microlithix.Text.Ansi.Element.AnsiControlStringInitiator>  |                                                                  |
+---------+---------------------------------------------------------------+------------------------------------------------------------------+------------------------------------------------------------------+

The API symbols allow you to reference the control codes as named constants when writing application code. These particular symbols probably won't be needed when parsing input sequences because the parser will interpret the codes for you. But they can be helpful when constructing strings that will be sent to the parser or to an ANSI-compatible terminal.
