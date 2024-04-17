# Formal Description of ANSI Character Streams

Below are formal descriptions of standard-compliant ANSI streams using both [EBNF](EBNF.md) and a state diagram.

## EBNF Description

```bnf
<ansi-stream>               ::= <ansi-element>*

<ansi-element>              ::= <printable-string> | <control-element>

<pritable-string>           ::= <printable-char>+
<printable-char>            ::= "\u0020"..."\u007f" | "\u00A0"..."\uFFFF"

<control-element>           ::= <solitary-control-code> | <introduced sequence>

<solitary-control-code>     ::= "\u0000"..."\u001a" | "\u001c"..."\u001f" |
                                "\u0080"..."\u008f" | "\u0091"..."\u0097" |
                                "\u0099"..."\u009a" | "\u009c"

<introduced-sequence>       ::= <escape-sequence> | <control-sequence> | <control-string>

<escape-sequence>           ::= <nf-sequence> | <fp-sequence> | <fe-sequence> | <fs-sequence>
<nf-sequence>               ::= "\u001b" ("\u0020"..."\u002f")+ "\u0030"..."\u007e"
<fp-sequence>               ::= "\u001b" "\u0030"..."\u003f"
<fe-sequence>               ::= "\u001b" ("\u0040"..."\u004f" | "\u0051"..."\u0057" | "\u0059"..."\u005a" | "\u005c")
<fs-sequence>               ::= "\u001b" "\u0060"..."\u007e"

<control-sequence>          ::= <standard-control-sequence> | <private-control-sequence>

<standard-control-sequence> ::= <csi> <parameter-string> <control-function>
<parameter-string>          ::= <parameter-substring> (";" <parameter-substring>)*
<parameter-substring>       ::= <part> (":" <part>)*
<part>                      ::= <digit>*
<digit>                     ::= "0"..."9"

<private-control-sequence>  ::= <csi> <private-parameter-string> <control-function>
<private-parameter-string>  ::= <ppi> <parameter-byte>*
<ppi>                       ::= "\u003C"..."\u003F"
<parameter-byte>            ::= "\u0030"..."\u003F"

<csi>                       ::= ControlCode.CSI | ControlCode.ESC EscapeCode.CSI
<control-function>          ::= <intermediate-byte>* <final-byte>
<intermediate-byte>         ::= "\u0020"..."\u002f"
<final-byte>                ::= "\u0040"..."\u007e"

<control-string>            ::= <character-string> | <command-string>

<character-string>          ::= <string-initiator> <string-char>* <string-terminator>
<string-initiator>          ::= ControlCode.SOS | ControlCode.ESC EscapeCode.SOS
<string-char>               ::= "\u0000"..."\u0017" | "\u0019" | "\u001c"..."\u007f" | "\u00a0"..."\uffff"

<command-string>            ::= <command-initiator> | <command-char>* <string-terminator>
<command-initiator>         ::= <dcs-initiator> | <osc-initiator> | <pm-initiator> | <apc-initiator>
<dcs-initiator>             ::= ControlCode.DCS | ControlCode.ESC EscapeCode.DCS
<osc-initiator>             ::= ControlCode.OSC | ControlCode.ESC EscapeCode.OSC
<pm-initiator>              ::= ControlCode.PM  | ControlCode.ESC EscapeCode.PM
<apc-initiator>             ::= ControlCode.APC | ControlCode.ESC EscapeCode.APC
<command-char>              ::= "\u0008"..."\u000d" | "\u0020"..."\u007e" | "\u00a0"..."\uffff"

<string-terminator>         ::= (ControlCode.ST | ControlCode.ESC EscapeCode.ST)
```

## State Diagram

TBD.
