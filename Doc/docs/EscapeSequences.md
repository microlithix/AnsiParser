# Escape Sequences

Escape sequences are initiated by the presence of the escape control code [ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC) (`0x1b`) in the input stream, followed by a character in the range `0x20`...`0x7e`. There are several different types of escape sequences, indentified by the specific character code following the escape control code:

```ebnf
<escape-sequence> ::= <nf-sequence> | <fp-sequence> | <fe-sequence> | <fs-sequence>

<nf-sequence>     ::= <ESC> (0x20...0x2f)+ 0x30...0x7e
<fp-sequence>     ::= <ESC> 0x30...0x3f
<fe-sequence>     ::= <ESC> (0x40...0x4f | 0x51...0x57 | 0x59...0x5a | 0x5c)
<fs-sequence>     ::= <ESC> 0x60...0x7e

<ESC>             ::= 0x1b
```

## Fe Escape Sequences

The `<fe-sequence>` type was defined in order to allow 7-bit character codes to access the C1 control codes in the range `0x80`...`0x9f`, which normally require an 8-bit code. Each `<fe-sequence>` maps to a C1 control code by taking its second character code and adding 64 (hexadecimal `0x40`). For example, the sequence `0x1b 0x40` invokes the same function as the `0x80` control code, the sequence `0x1b 0x41` invokes the same function as the `0x81` control code, etc.

The following escape sequences map to "[introducers](ControlCodes.md)" rather than to [solitary control codes](ControlCodes.md):

Escape Sequence | Introduced Element
-------------|-------------------
`0x1b 0x50` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Device Control String)
`0x1b 0x58` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Start of String)
`0x1b 0x5b` | [Control Sequence](ControlSequences.md)
`0x1b 0x5d` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Operating System Command)
`0x1b 0x5e` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Privacy Message)
`0x1b 0x5f` | [Control String](ControlStrings.md) or [Control String Initiator](ControlStrings.md) (Application Program Command)
