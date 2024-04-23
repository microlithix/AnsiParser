# Control Strings

*Control strings* are sequences of characters intended for use by the consuming application, rather than for being displayed to the user like [printable characters](PrintableCharacters.md). Control strings are of two types: *character strings* and *command strings*:

```ebnf
<control-string> ::= <character-string> | <command-string>
```

The primary difference between the two types of strings is the set of characters allowed in each.

## Character Strings

Character strings are the most general type of control string. According to [ECMA-48](References.md#ecma-48), all characters except for [ControlCode.SOS](xref:Microlithix.Text.Ansi.ControlCode.SOS) (`0x98`), [ControlCode.ST](xref:Microlithix.Text.Ansi.ControlCode.ST) (`0x9c`), and their corresponding [escape sequences](EscapeSequences.md#fe-escape-sequences) are permitted in character strings.

> [!IMPORTANT]
> Here, the implmentation of AnsiStreamParser and AnsiStringParser deviates from the ECMA-48 specification. In addition to disallowing the characters mentioned above, *any* escape sequence and any character in the set `0x18`, `0x1a`, `0x80`...`0x9f` will terminate the character string.

Characters strings can be described using the following ([EBNF](Notation.md#extended-backus-naur-form)) notation:

```ebnf
<character-string>  ::= <string-initiator> <string-char>* <string-terminator>
<string-initiator>  ::= <SOS> | <ESC> <FE-SOS>
<string-char>       ::= 0x00...0x17 | 0x19 | 0x1c...0x7f | 0xa0...0xffff
<string-terminator> ::= <ST> | <ESC> <FE-ST>

<ESC>               ::= 0x1b
<FE-SOS>            ::= 0x58  // 'X'
<FT-ST>             ::= 0x5c  // '\'
<SOS>               ::= 0x98
<ST>                ::= 0x9c
```

> [!Note]
> Character strings are terminated by an explict `<string-terminator>`, or by the occurence of any character not in `<string-char>`.

The terminal values can be referenced in code using the following symbols defined by the API:

Terminal Value | Description                       | API Symbol
-------------- | ----------------------------------| ----------
`<ESC>`    | Escape control code                   | [ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC)
`<SOS>`    | Control code for *Start of String*    | [ControlCode.SOS](xref:Microlithix.Text.Ansi.ControlCode.SOS)
`<ST>`     | Control code for *String Terminator*  | [ControlCode.ST](xref:Microlithix.Text.Ansi.ControlCode.ST)
`<FE-SOS>` | Escape code for *Start of String*     | [EscapeCode.DCS](xref:Microlithix.Text.Ansi.EscapeCode.DCS)
`<FE-ST>`  | Escape code for *String Terminator*   | [EscapeCode.ST](xref:Microlithix.Text.Ansi.EscapeCode.ST)

## Command Strings

Command strings have four different types and their content is more restricted than that of character strings, disallowing all control codes except those in the range `0x08`...`0x0d`. They can be described using the following ([EBNF](Notation.md#extended-backus-naur-form)) notation:

```ebnf
<command-string>    ::= <command-initiator> | <command-char>* <string-terminator>

<command-initiator> ::= <dcs-initiator> | <osc-initiator> | <pm-initiator> | <apc-initiator>

<dcs-initiator>     ::= <DCS> | <ESC> <FE-DCS>  // Device control string
<osc-initiator>     ::= <OSC> | <ESC> <FE-OSC>  // Operating system command
<pm-initiator>      ::= <PM>  | <ESC> <FE-PM>   // Privacy message
<apc-initiator>     ::= <APC> | <ESC> <FE-APC>  // Application program command

<command-char>      ::= 0x08...0x0d | 0x20...0x7e | 0xa0...0xffff

<string-terminator> ::= <ST> | <ESC> <FE-ST>

<ESC>    ::= 0x1b
<FE-DCS> ::= 0x50  // 'P'
<FE-OSC> ::= 0x5d  // ']'
<FE-PM>  ::= 0x5e  // '^'
<FE-APC> ::= 0x5f  // '_'
<FE-ST>  ::= 0x5c  // '\'
<DCS>    ::= 0x90
<OSC>    ::= 0x9d
<PM>     ::= 0x9e
<APC>    ::= 0x9f
<ST>     ::= 0x9c
```

> [!Note]
> Command strings are terminated by an explict `<string-terminator>`, or by the occurence of any character not in `<command-char>`.

The terminal values can be referenced in code using the following symbols defined by the API:

Terminal Value | Description                                   | API Symbol
-------------- | --------------------------------------------- | ----------
`<ESC>`    | Escape control code                               | [ControlCode.ESC](xref:Microlithix.Text.Ansi.ControlCode.ESC)
`<DCS>`    | Control code for a *Device Control String*        | [ControlCode.DCS](xref:Microlithix.Text.Ansi.ControlCode.DCS)
`<OSC>`    | Control code for an *Operating System Command*    | [ControlCode.OSC](xref:Microlithix.Text.Ansi.ControlCode.OSC)
`<PM>`     | Control code for a *Privacy Message*              | [ControlCode.PM](xref:Microlithix.Text.Ansi.ControlCode.PM)
`<APC>`    | Control code for an *Application Program Command* | [ControlCode.APC](xref:Microlithix.Text.Ansi.ControlCode.APC)
`<ST>`     | Control code for a String Terminator              | [ControlCode.ST](xref:Microlithix.Text.Ansi.ControlCode.ST)
`<FE-DCS>` | Escape code for a *Device Control String*         | [EscapeCode.DCS](xref:Microlithix.Text.Ansi.EscapeCode.DCS)
`<FE-OSC>` | Escape code for an *Operating System Command*     | [EscapeCode.OSC](xref:Microlithix.Text.Ansi.EscapeCode.OSC)
`<FE-PM>`  | Escape code for a *Privacy Message*               | [EscapeCode.PM](xref:Microlithix.Text.Ansi.EscapeCode.PM)
`<FE-APC>` | Escape code for an *Application Program Command*  | [EscapeCode.APC](xref:Microlithix.Text.Ansi.EscapeCode.APC)
`<FE-ST>`  | Escape code for a String Terminator               | [EscapeCode.ST](xref:Microlithix.Text.Ansi.EscapeCode.ST)
