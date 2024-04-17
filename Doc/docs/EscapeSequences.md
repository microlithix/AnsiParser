# Escape Sequences

Escape sequences are initiated by the presence of the escape control code [ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC) (`"\u001b"`) in the input stream, followed by a character in the range `"\u0020"..."\u007e"`. There are several different types of escape sequences, indentified by the specific character code following the escape control code:

```bnf
<escape-sequence> ::= <nf-sequence> | <fp-sequence> | <fe-sequence> | <fs-sequence>

<nf-sequence>     ::= "\u001b" ("\u0020"..."\u002f")+ "\u0030"..."\u007e"
<fp-sequence>     ::= "\u001b" "\u0030"..."\u003f"
<fe-sequence>     ::= "\u001b" ("\u0040"..."\u004f" | "\u0051"..."\u0057" | "\u0059"..."\u005a" | "\u005c")
<fs-sequence>     ::= "\u001b" "\u0060"..."\u007e"
```

## Fe Escape Sequences

The `<fe-sequence>` type was defined in order to allow 7-bit character codes to access the C1 control codes in the range `"\u0080"..."\u009f"`, which normally require an 8-bit code. Each `<fe-sequence>` maps to a C1 control code by taking its second character code and adding 64 (hexadecimal `"\x40"`). For example, the sequence `"\u001b\u0040"` invokes the same function as the `"\u0080"` control code, the sequence `"\u001b\u0041"` invokes the same function as the `"\u0081"` control code, etc.

The following escape sequences map to "[introducers](ControlCodes.md)" rather than to [solitary control codes](ControlCodes.md):

Escape Sequence | Introduced Element
-------------|-------------------
`"\u001b\u0050"` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Device Control String)
`"\u001b\u0058"` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Start of String)
`"\u001b\u005b"` | [Control Sequence](ControlSequences.md)
`"\u001b\u005d"` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Operating System Command)
`"\u001b\u005e"` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Privacy Message)
`"\u001b\u005f"` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Application Program Command)
