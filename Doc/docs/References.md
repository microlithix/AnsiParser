# References

This page contains a list of references cited elsewhere in the documentation.

## Specifications

### ANSI X3.64

"ANSI X3.64-1979: Additional Controls for Use with American National Standard Code for Information Interchange", American National Standards Institute, Inc., 1430 Broadway, New York, NY 10018.

### ECMA-35

"Standard ECMA-35: Character Code Structure and Extension Techniques" ([PDF](https://ecma-international.org/wp-content/uploads/ECMA-35_6th_edition_december_1994.pdf)) (Sixth ed.). [Ecma International](https://en.wikipedia.org/wiki/Ecma_International). December 1994.

### ECMA-48

"Standard ECMA-48: Control Functions for Coded Character Sets" ([PDF](https://www.ecma-international.org/wp-content/uploads/ECMA-48_5th_edition_june_1991.pdf)) (Fifth ed.). [Ecma International](https://en.wikipedia.org/wiki/Ecma_International). June 1991.

Excerpts from ECMA-48 have been included in this documention under ECMA International's [fair use policy](https://ecma-international.org/policies/by-ipr/ecma-text-copyright-policy/). If any works derived from this documentation retain such excerpts, they should also retain the associated citations.

### ISO/IEC 646

"International Standard ISO/IEC 646: Information technology - ISO 7-bit coded character set for information interchange". December 1991, International Organization for Standardization, Chemin de Blandonnet 8, CP 401, 1214 Vernier, Geneva, Switzerland.

### ISO/IEC 6429

"International Standard ISO/IEC 6429: Information technology - Control functions for coded character sets". Third edition, December 15, 1992, International Organization for Standardization, Chemin de Blandonnet 8, CP 401, 1214 Vernier, Geneva, Switzerland.

### ISO/IEC 8613-6 [CCITT Recommendation T.416]

"ITU-T Recommendation T.416 (03/93) - Information Technology - Open Document Architecture (ODA) and Interchange Format: Character Content Architectures", International Telecommunication Union, 1994.

## Other References

[1] Williams, Paul Flo. "A parser for DEC's ANSI-compatible video terminals." VT100.net. <https://vt100.net/emu/dec_ansi_parser>.

- The development of AnsiParser was inspired by this work, which describes a state machine implementation for DEC VT500-Series video terminals and provides a very helpful state diagram. While DEC VT500-Series terminals extend the ECMA-48 specification in some areas and fall short of a full implementation in other areas, AnsiParser provides a more complete and strict implemention of the specification and does not attempt to go beyond it. However, AnsiParser could easily be extended to cover the full DEC VT500-Series implementation by adding another processing layer, primarily to handle the DEC processing of Device Control Strings if that functionality is required.

[2] Haberman, Joshua. "VTParse - an implementation of Paul Williams' DEC compatible state machine parser". <https://github.com/haberman/vtparse>

## Other Related Projects

- <https://github.com/taterbase/libvt100>
- <https://github.com/paultag/libansiescape>
- <https://github.com/diggernet/VTParser>
- <https://github.com/F1LT3R/parse-ansi>
