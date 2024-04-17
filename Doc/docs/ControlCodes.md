# Control Codes

[Control Codes](xref:Microlithix.Text.Ansi.ControlCode) are single non-displayable characters that appear in the input stream. They are typically used by the consuming application to modify its own behaviour or internal state, or to initiate [Escape Sequences](EscapeSequences.md), [Control Sequences](ControlSequences.md), and [Control Strings](ControlStrings.md).

Control codes have character encodings in the ranges `"\u0000"..."\u001f"` (so-called [C0 control codes](https://en.wikipedia.org/wiki/C0_and_C1_control_codes)) and `"\u0080"..."\u009f"` (so-called [C1 control codes](https://en.wikipedia.org/wiki/C0_and_C1_control_codes)). The C0 codes were originally defined in [ISO/IEC 646 (ASCII)](References.md#isoiec-646), and the C1 codes were originally defined in [ECMA-48](References.md#ecma-48). Most control codes are "solitary", meaning that they exist as independent characters in the input stream, and they do not have any impact on the parsing of subsequent characters.

Using an extended Backus-Naur form ([EBNF](EBNF.md)), solitary control codes can be described as follows:

```csharp
<solitary-control-code> ::= "\u0000"..."\u001a" | "\u001c"..."\u001f" |
                            "\u0080"..."\u008f" | "\u0091"..."\u0097" |
                            "\u0099"..."\u009a" | "\u009c"
```

The remaining control codes are "introducers", meaning that they indicate the beginning of a variable-length sequence or control string in the input. These control codes are not themselves represented as parsed elements. Rather, it is the sequences and control strings they introduce that will be produced as structured elements by the parser. These codes and the elements they introduce are the following:

Control Code | Introduced Element
-------------|-------------------
[ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC) (`"\u001b"`) | [Escape Sequence](EscapeSequences.md)
[ControlCode.DCS](xref:Microlithix.Text.Ansi.ControlCode.DCS) (`"\u0090"`) | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Device Control String)
[ControlCode.SOS](xref:Microlithix.Text.Ansi.ControlCode.SOS) (`"\u0098"`) | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Start of String)
[ControlCode.CSI](xref:Microlithix.Text.Ansi.ControlCode.CSI) (`"\u009b"`) | [Control Sequence](ControlSequences.md)
[ControlCode.OSC](xref:Microlithix.Text.Ansi.ControlCode.OSC) (`"\u009d"`) | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Operating System Command)
[ControlCode.PM](xref:Microlithix.Text.Ansi.ControlCode.PM) (`"\u009e"`) | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Privacy Message)
[ControlCode.APC](xref:Microlithix.Text.Ansi.ControlCode.APC) (`"\u009f"`) | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Application Program Command)
