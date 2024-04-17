# Control Strings

*Control strings* are sequences of characters intended for use by the consuming application, rather than for being displayed to the user like [printable characters](PrintableCharacters.md). Control strings are of two types: *character strings* and *command strings*:

```bnf
<control-string> ::= <character-string> | <command-string>
```

The primary difference between the two types of strings is the set of characters allowed in each.

## Character Strings

Character strings are the most general type of control string. According to [ECMA-48](References.md#ecma-48), all characters except for [ControlCode.SOS](xref:Microlithix.Text.Ansi.ControlCode.SOS) (`"\u0098"`), [ControlCode.ST](xref:Microlithix.Text.Ansi.ControlCode.ST) (`"\u009c"`), and their corresponding [escape sequence equivalents](EscapeSequences.md#fe-escape-sequences) are permitted in character strings.

> [!IMPORTANT]
> Here, the implmentation of AnsiStreamParser and AnsiStringParser deviates from the ECMA-48 specification. In addition to disallowing the characters mentioned above, *any* escape sequence and any character in the set `"\u0018", "\u001a", "\u0080"..."\u009f"` will terminate the character string.

Characters strings can be described using the following ([EBNF](EBNF.md)) notation:

```bnf
<character-string>          ::= <string-initiator> <string-char>* <string-terminator>
<string-initiator>          ::= ControlCode.SOS | ControlCode.ESC EscapeCode.SOS
<string-char>               ::= "\u0000"..."\u0017" | "\u0019" | "\u001c"..."\u007f" | "\u00a0"..."\uffff"
<string-terminator>         ::= (ControlCode.ST | ControlCode.ESC EscapeCode.ST)
```

Character strings are terminated by an explict `<string-terminator>`, or by the occurence of any character not in `<string-char>`.

## Command Strings

Command strings have four different types and their content is more restricted than that of character strings, disallowing all control codes except those in the range `"\u0008"..."\u000d"`. They can be described using the following ([EBNF](EBNF.md)) notation:

```bnf
<command-string>            ::= <command-initiator> | <command-char>* <string-terminator>
<command-initiator>         ::= <dcs-initiator> | <osc-initiator> | <pm-initiator> | <apc-initiator>
<dcs-initiator>             ::= ControlCode.DCS | ControlCode.ESC EscapeCode.DCS
<osc-initiator>             ::= ControlCode.OSC | ControlCode.ESC EscapeCode.OSC
<pm-initiator>              ::= ControlCode.PM  | ControlCode.ESC EscapeCode.PM
<apc-initiator>             ::= ControlCode.APC | ControlCode.ESC EscapeCode.APC
<command-char>              ::= "\u0008"..."\u000d" | "\u0020"..."\u007e" | "\u00a0"..."\uffff"
<string-terminator>         ::= (ControlCode.ST | ControlCode.ESC EscapeCode.ST)
```

Command strings are terminated by an explict `<string-terminator>`, or by the occurence of any character not in `<command-char>`.
