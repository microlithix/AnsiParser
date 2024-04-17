# Notation

This document and its associated API documentation use the following notational conventions.

## Character literals

If a character has a printable form such as the letter A, then it can be represented as a C# `char` literal using the printable form (e.g. `'A'`). Some non-printable characters also have specific literal representations in C#. For example, the newline character can be represented with the literal `'\n'`. However, many non-printable characters don't have specific literal representations in C#, so they must be represented by their underlying bit encodings. For example, the ESC control code has a bit pattern corresponding to the hexadecimal value 1b, so it can be reprsented by the character literal `'\x1b'`.

The following literal character representations for the ESC control code are all equivalent:

```csharp
'\x1b'      // The bit encoding as an 8-bit hexadecimal number.
'\x001b'    // The bit encoding as a 16-bit hexadecimal number.
'\u001b'    // The bit encoding as a 16-bit hexadecimal number representing a UTF-16 code point.
(char)0x1b  // The bit encoding as a hexadecimal integer literal cast to a char type.
(char)27    // The bit encoding as a decimal integer literal cast to a char type.
```

For compactness and clarity, this documentation primarily uses the hexadecimal integer literal representation, and omits the cast to the `char` type when the context makes it clear that it refers to a character encoding. So the ESC control code would be represented simply as `0x1b`. For the first 256 Unicode characters, the two leading zeros are omitted and the 8-bit form is used. The remaining Unicode character encodings are represented by the full 16-bit hexadecimal value.

In most cases throughout this documentation, it is more instructive to show all characters using their underlying bit encodings, rather than showing only the non-printable characters in that way. Doing so allows for the compact representation of ranges of characters, rather than needing to list them all separately. For example, the characters with encodings from `0x3c` to `0x3f` correspond to the printable characters `<`, `=`, `>`, and `?`. That set could also be represented as a range (e.g. `<` ... `?`). Unfortunately, with this notation it is not at all clear which specific characters are in the set, or even how many characters are in the set. Answering such questions would require consulting a full character encoding table. However, representing the range using the underlying bit encodings solves this problem (e.g. `\x3c` ... `\x3f`). From this representation it is immediately obvious that there are 4 characters in the set, and each can be referenced easily and uniquely by its bit encoding without ever needing to know its printable form.
