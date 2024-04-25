# Microlithix AnsiParser

## Introduction

This package contains modules for parsing character streams and strings with embedded control sequences that conform to specifications published in [ECMA-48](References.md#ecma-48) and [ANSI X3.64](References.md#ansi-x364), later adopted as [ISO/IEC 6429](References.md#isoiec-6429). Such coded control sequences are commonly referred to as *ANSI escape sequences* or *ANSI escape codes*. These control sequences are often used by terminals, terminal emulators, command shells, and other types of console applications.

Note that this package implements a parser only, and its output is a sequence of parsed elements containing a structured representation of the sequences in the input stream. It doesn't perform any actions based on the parsed elements other than providing them to the consuming application for subsequent processing. It is the responsibility of the consuming application to act upon the parsed elements.

However, the AnsiParser *Application Programming Interface* (API) defines many named constants and other values to assist with the interpretation of the parsed elements in accordance with the higher-level functionality described in ECMA-48. The [API reference](xref:Microlithix.Text.Ansi) documentation and this document that you are presently reading will assist you with understanding ECMA-48 and how all of its components fit together.

See [Appendix 1](Notation.md) for a description of the notation used in this document. You can also review a [formal description](FormalDescription.md) of ANSI character streams for a detailed understanding of their precise form.

This work was inspired by Paul Flo Williams' [state machine description](https://vt100.net/emu/dec_ansi_parser) of the DEC VT500-Series parser, and by Joshua Haberman's [VTParse](https://github.com/haberman/vtparse) implementation of that state machine. See the [citations](References.md#other-references) for details.

[Repository](https://github.com/microlithix/AnsiParser)

[License](LICENSE.md)
