# Formal Description of ANSI Character Streams

Below is a formal description of standard-compliant ANSI streams using [EBNF](Notation.md#extended-backus-naur-form) notation. The original [ANSI X3.64](References.md#ansi-x364) and [ECMA-48](References.md#ecma-48) standards contemplated only 7-bit and 8-bit codes, whereas this version has been extended to encompass [UTF-16](https://en.wikipedia.org/wiki/UTF-16).

## EBNF Description

```ebnf
<ansi-stream>               ::= <ansi-element>*

<ansi-element>              ::= <printable-string> | <control-element>

<pritable-string>           ::= <printable-char>+
<printable-char>            ::= 0x20...0x7f | 0xa0...0xffff

<control-element>           ::= <solitary-control-code> | <introduced-sequence>

<solitary-control-code>     ::= <solitary-C0-code> | <solitary-C1-code>
<solitary-C0-code>          ::= 0x00...0x1a | 0x1c...0x1f
<solitary-C1-code>          ::= 0x80...0x8f | 0x91...0x97 | 0x99...0x9a

<introduced-sequence>       ::= <escape-sequence> | <control-sequence> | <control-string>

<escape-sequence>           ::= <nf-sequence> | <fp-sequence> | <fe-sequence> | <fs-sequence>
<nf-sequence>               ::= <ESC> (0x20...0x2f)+ 0x30...0x7e
<fp-sequence>               ::= <ESC> 0x30...0x3f
<fe-sequence>               ::= <ESC> (0x40...0x4f | 0x51...0x57 | 0x59...0x5a)
<fs-sequence>               ::= <ESC> 0x60...0x7e

<control-sequence>          ::= <standard-control-sequence> | <private-control-sequence>

<standard-control-sequence> ::= <cs-initiator> <parameter-string> <control-function>
<parameter-string>          ::= <parameter-substring> (";" <parameter-substring>)*
<parameter-substring>       ::= <part> (":" <part>)*
<part>                      ::= <digit>*
<digit>                     ::= '0'...'9'

<private-control-sequence>  ::= <cs-initiator> <private-parameter-string> <control-function>
<private-parameter-string>  ::= <ppi> <parameter-byte>*
<ppi>                       ::= 0x3c...0x3f
<parameter-byte>            ::= 0x30...0x3f

<cs-initiator>              ::= <CSI> | <ESC> <FE-CSI>

<control-function>          ::= <intermediate-byte>* <final-byte>
<intermediate-byte>         ::= 0x20...0x2f
<final-byte>                ::= 0x40...0x7e

<control-string>            ::= <character-string> | <command-string>

<character-string>          ::= <string-initiator> <string-char>* <string-terminator>
<string-initiator>          ::= <SOS> | <ESC> <FE-SOS>
<string-char>               ::= 0x00...0x17 | 0x19 | 0x1c...0x7f | 0xa0...0xffff

<command-string>            ::= <command-initiator> | <command-char>* <string-terminator>
<command-initiator>         ::= <dcs-initiator> | <osc-initiator> | <pm-initiator> | <apc-initiator>
<dcs-initiator>             ::= <DCS> | <ESC> <FE-DCS>
<osc-initiator>             ::= <OSC> | <ESC> <FE-OSC>
<pm-initiator>              ::= <PM>  | <ESC> <FE-PM>
<apc-initiator>             ::= <APC> | <ESC> <FE-APC>
<command-char>              ::= 0x08...0x0d | 0x20...0x7e | 0xa0...0xffff

<string-terminator>         ::= <ST> | <ESC> <FE-ST>

// The following terminals have corresponding symbols defined in
// the API that can be use to refer to them when writing code.

<ESC>    ::= 0x1b  // API symbol: ControlCode.ESC
<FE-DCS> ::= 0x50  // API symbol: EscapeCode.DCS ('P')
<FE-SOS> ::= 0x58  // API symbol: EscapeCode.SOS ('X')
<FE-CSI> ::= 0x5b  // API symbol: EscapeCode.CSI ('[')
<FE-ST>  ::= 0x5c  // API symbol: EscapeCode.ST  ('\')
<FE-OSC> ::= 0x5d  // API symbol: EscapeCode.OSC (']')
<FE-PM>  ::= 0x5e  // API symbol: EscapeCode.PM  ('^')
<FE-APC> ::= 0x5f  // API symbol: EscapeCode.APC ('_')
<DCS>    ::= 0x90  // API symbol: ControlCode.DCS
<SOS>    ::= 0x98  // API symbol: ControlCode.SOS
<CSI>    ::= 0x9b  // API symbol: ControlCode.CSI
<ST>     ::= 0x9c  // API symbol: ControlCode.ST
<OSC>    ::= 0x9d  // API symbol: ControlCode.OSC
<PM>     ::= 0x9e  // API symbol: ControlCode.PM
<APC>    ::= 0x9f  // API symbol: ControlCode.APC
```

> [!Note]
> As a convention, the names of non-terminals have been written in lower case and the names of terminals have been written in upper case.
> [!Note]
> Control codes of type `<solitary-control-code>` appear independently in the input stream without any associated preceding or succeeding characters.
> [!Note]
> The API <xref:Microlithix.Text.Ansi.ControlCode> class defines symbolic constants for all control codes and the API <xref:Microlithix.Text.Ansi.EscapeCode> class defines symbolic constants for all characters associated with escape sequences of type `<fe-sequence>`.
> [!Note]
> See [ECMA-35](References.md#ecma-35) for detailed discussion of nF, Fp, Fe, and Fs escape sequences.
> [!Important]
> There are many possible illegal sequences that do not conform to the EBNF grammar, such as `<ESC>` followed immediately by another control code, or `<ST>` not preceded by a corresponding string initiator, etc. The parser's [state machine implementation](StateDiagram.md) guarantees a deterministic response to all possible sequences, although it cannot guarantee a return to its ground state. For example, an `<SOS>` code followed by nothing but valid string content characters will cause the parser to remain in its string-processing state indefinitely.
