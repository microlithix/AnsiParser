# Appendix 1 - Notation

Both this document and the associated API documentation use the following notational conventions.

## Character literals

If a character has a printable form such as the letter A, then it can easily be represented as a C# `char` literal (e.g. `'A'`). Some non-printable characters also have specific literal representations in C#. For example, the newline character can be represented with the literal `'\n'`. However, many non-printable characters lack specific literal representations in C#, so they must be represented by their underlying bit encodings. For example, the ESC control code has a bit pattern corresponding to the hexadecimal value `0x1b`, so it can be represented by the C# character literal `'\x1b'` or `'\u001b'`.

The following C# literal representations for the ESC control code are all equivalent:

```csharp
'\x1b'      // The bit encoding as an 8-bit hexadecimal number.
'\x001b'    // The bit encoding as a 16-bit hexadecimal number.
'\u001b'    // The bit encoding as a 16-bit hexadecimal number representing a UTF-16 code point.
(char)0x1b  // The bit encoding as a hexadecimal integer literal cast to a char type.
(char)27    // The bit encoding as a decimal integer literal cast to a char type.
```

For compactness and clarity, this documentation primarily uses the hexadecimal integer literal representation, and omits the cast to the `char` type when the context makes it clear that it refers to a character encoding. For the first 256 Unicode characters the 2-digit 8-bit form is used, so the ESC control code would be represented as `0x1b`. The remaining Unicode character encodings are represented as full 4-digit 16-bit hexadecimal values (e.g. `0x1e73`). Unlike this documentation, the source code itself generally uses the `'\u001b'` form in order to make it's `char` type explicit without the need for a cast.

In most cases throughout this documentation, it is more instructive to show most of the characters using their underlying bit encodings, rather than showing only the non-printable characters in that manner. An advantage of this approach is that it allows the clear, compact representation of character ranges. For example, the characters with encodings from `0x3c` to `0x3f` correspond to the printable characters `<`, `=`, `>`, and `?`. That set could also be represented as a range (e.g. `<` ... `?`). Unfortunately, with this notation it is not at all clear which specific characters are in the set, or even how many characters are in the set. Answering such questions would require consulting a full character encoding table. However, representing the range using the underlying bit encodings solves this problem (e.g. `0x3c` ... `0x3f`). From this representation it is immediately obvious that there are 4 characters in the set, and each can be referenced easily and uniquely by its bit encoding without requiring any knowledge of its printable form.

## Extended Backus-Naur Form

This documentation makes use of an extended version of [Backus-Naur form](https://en.wikipedia.org/wiki/Backus%E2%80%93Naur_form) (BNF) for describing the syntax of control sequences. The extensions allow the syntax descriptions to be more concise and more easily comprehensible than they would be with standard BNF. There are many different extended BNF notations in widespread use, and this document does not attempt to conform to any particular one of them. Instead, it defines extensions here that are best suited to its own requirements.

The following sections define the extensions and provide examples using both BNF and extended BNF (EBNF) notation so that you can see how to write equivalent descriptions using each form.

### Comments

This version of EBNF notation borrows its line-comment syntax from C#. Two consecutive forward slashes (`//`) and everything that follows them on the same line are to be interpreted solely as a note to the reader. Comments are not part of the EBNF notation itself. For example, both of these expressions represent exactly the same thing in EBNF notation:

```ebnf
<item> ::= <expression>
<item> ::= <expression> // This is a comment.
```

### Non-Terminals

Non-terminals are enclosed within angle brackets in both BNF and EBNF, and the same expansion symbol (`::=`) is used for both:

```ebnf
<item> ::= <expression> // BNF
<item> ::= <expression> // EBNF
```

In EBNF, the text inside the angle brackets may contain any alphanumeric character and the dash (`-`) character, but it must begin with an alphabetic character.

### Terminals

Terminals represent the final explicit terms in an expansion, requiring no further expansion themselves. They are referred to as "terminals" because they terminate the expansion.

#### Literal Terminals

Literal terminals are enclosed in single or double quotes in order to distinguish them from [named terminals](#named-terminals), [coded terminals](#coded-terminals), and [non-terminals](#non-terminals):

```ebnf
<item> ::= "A"  // BNF

<item> ::= 'A'  // EBNF
<item> ::= "A"  // Alternative EBNF
```

Literal terminals conform to the standard C# character literal syntax, so the backslash character (`\`) acts as an escape character inside the literal:

```ebnf
<item> ::= '\n'  // A linefeed character as a literal terminal in EBNF.
```

The backslash itself must be escaped if it is to be included in the literal terminal:

```ebnf
<item> ::= '\\'  // A single backslash as a literal terminal in EBNF.
```

#### Named Terminals

Named terminals refer to named objects defined by the API. They follow the same rules as C# identifiers, such as the requirement that they must begin with an alphabetic character or an underscore:

```ebnf
<item> ::= <graphic-rendition-selector-underline>   // BNF cannot explicitly reference API symbols.
<graphic-rendition-selector-underline> ::= "4"      // The BNF notation must define its own symbols using non-terminals.

<item> ::= GraphicRenditionSelector.Underline       // EBNF
```

Here, [GraphicRenditionSelector.Underline](xref:Microlithix.Text.Ansi.GraphicRenditionSelector.Underline) refers to the constant value "4" defined by the API.

#### Coded Terminals

A coded terminal represents the bit encoding of a terminal, allowing non-printable terminals such as control codes to be specified. Coded terminals begin with a digit, and are typically written in hexadecimal format:

```ebnf
<item> ::= "A"   // BNF cannot represent bit encodings explicitly.
<item> ::= 0x41  // EBNF coded literal.

<item> ::= 0x08  // EBNF (cannot be represented using BNF)
```

#### Terminal Ranges

The ellipsis (`...`) can be used to indicate a range of literal terminals, representing the occurrence of one item from the range:

```ebnf
<digit> ::= "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"  // BNF
<digit> ::= '0'...'9'                                                  // EBNF
```

Note that the range is defined in terms of the underlying character encoding. Ranges are most often defined using coded terminals, since that makes the range more explicit and obvious:

```ebnf
<digit> ::= 0x30...0x39 // EBNF
```

### Repetition

Square brackets `[` and `]` around an expression indicate the occurence of zero or one instance of that expression:

```ebnf
<optional> ::= "" | <item>  // BNF
<optional> ::= [<item>]     // EBNF
```

The character `*` after an expression indicates the occurrence of zero or more instances of that expression:

```ebnf
<list> ::= "" | <list> <item>  // BNF
<list> ::= <item>*             // EBNF
```

The character `+` after an expression indicates the occurrence of one or more instances of that expression:

```ebnf
<list> ::= <item> | <list> <item>  // BNF
<list> ::= <item>+                 // EBNF
```

### Grouping

The characters `(` and `)` can surround a sequence of expressions, allowing them to be treated as a group. A repetition symbol such as `*` can then be applied to the group as a whole:

```ebnf
<list> ::= "" | <list> <key> <value>  // BNF
<list> ::= (<key> <value>)*           // EBNF
```
