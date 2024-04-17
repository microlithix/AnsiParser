# Appendix 2 - Extended Backus-Naur Form

This documentation makes use of an extended version of [Backus-Naur](https://en.wikipedia.org/wiki/Backus%E2%80%93Naur_form) form (EBNF) for describing the syntax of control sequences. The extensions allow the syntax descriptions to be more concise and more easily comprehensible than standard BNF. There are many different forms of "extended" BNF notation in widespread use, and this document does not attempt to conform to any particular one of them. Instead, it defines extensions here that are best suited to its own requirements.

The following sections provide examples using both BNF and EBNF notation so that you can see how to write equivalent descriptions in each form.

## Terminals

### Literal Terminals

Literal terminals are enclosed in quotation marks:

```bnf
<item> ::= A    ; BNF
<item> ::= "A"  ; EBNF
```

### Literal Terminal Ranges

The string `...` can be used to indicate a range of literal terminals, representing the occurrence of one item from the range:

```bnf
<digit> ::= 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9   ; BNF
<digit> ::= "0"..."9"                               ; EBNF
```

Here, the range refers to the underlying character encoding.

### Named Terminals

Named terminals refer to named objects defined by the API. They are not enclosed in quotation marks or angle brackets:

```bnf
<item> ::= <graphic-rendition-selector-underline>   ; BNF
<graphic-rendition-selector-underline> ::= 4        ; BNF

<item> ::= GraphicRenditionSelector.Underline       ; EBNF
```

Here, [GraphicRenditionSelector.Underline](xref:Microlithic.Text.Ansi.GraphicRenditionSelector.Underline) refers to the constant value "4" defined by the API.

### Unicode Terminals

Unicode terminals can be represented by their equivalent hexadecimal encodings in C# strings:

```bnf
<item> ::= A        ; BNF
<item> ::= "\u0041" ; EBNF
```

This feature allows for the representation of non-printable characters.

## Repetition

Square brackets `[` and `]` around an expression indicate the occurence of zero or one instance if that expression:

```bnf
<optional> ::= E | <item>  ; BNF
<optional> ::= [<item>]    ; EBNF

The character `*` after an expression indicates the occurrence of zero or more instances of that expression:

```bnf
<list> ::= E | <list> <item>    ; BNF
<list> ::= <item>*              ; EBNF
```

The character `+` after an expression indicates the occurrence of one or more instances of that expression:

```bnf
<list> ::= <item> | <list> <item>  ; BNF
<list> ::= <item>+                 ; EBNF
```

## Grouping

The characters `(` and `)` can surround a sequence of expressions, allowing them to be treated as a group. A repetition symbol such as `*` can then be applied to the group as a whole:

```bnf
<list> ::= E | <list> <key> <value> ; BNF
<list> ::= (<key> <value>)*         ; EBNF
```
