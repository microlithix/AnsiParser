# Introduction

This package contains modules for parsing character streams and strings with embedded control sequences that conform to specifications published in [ANSI X3.64](References.md#ansi-x364) and [ECMA-48](References.md#ecma-48), later adopted as [ISO/IEC 6429](References.md#isoiec-6429). Such coded control sequences are commonly referred to as *ANSI escape sequences* or *ANSI escape codes*. These control sequences are often used by terminals, terminal emulators, command shells, and other types of console applications.

Note that this package implements a parser only, and its output is a sequence of parsed elements containing a structured representation of the sequences in the input stream. It doesn't perform any actions based on the parsed elements other than providing them to the consuming application for subsequent processing. It is the responsibility of the consuming application to act upon the parsed elements.

However, the AnsiParser *Application Programming Interface* (API) defines many named constants and other values to assist with the interpretation of the parsed elements in accordance with the higher-level functionality described in [ECMA-48](References.md#ecma-48). The [API documentation](xref:Microlithix.Text.Ansi) and this document that you are xpresently reading will assist you with understanding [ECMA-48](References.md#ecma-48) and how all of its components fit together.

You can review a [formal description of ANSI character streams](FormalDescription.md) for a detailed understanding of their precise form.

[Repository](https://github.com/microlithix/AnsiParser)

[License](LICENSE.md)
