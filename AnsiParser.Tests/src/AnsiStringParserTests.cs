using System.Diagnostics;

using Microlithix.Text.Ansi;
using Microlithix.Text.Ansi.Element;

namespace Microlithix.Text.Ansi.Tests;

public class AnsiStringParserShould {

    private const string ESC = "\u001b";
    private readonly AnsiStringParser parser;

    public AnsiStringParserShould() {
        parser = new();
    }

    [Fact]
    public void Temp() {
        var result = parser.Parse($"\x1b[34mHello\x1b[0m");
        foreach (var element in result) {
            Debug.Print(element.ToString());
        }   
    }

    [Fact]
    public void InvokeWithSampleCode_ToStringShouldMatch() {
        var result = parser.Parse($"\x1b[34mHello\x1b[0m");
        var strings = result.Select(element => element.ToString());
        var expected = new List<string> {
            "AnsiControlSequence { Function = m, Parameters = [ 34 ] }",
            "AnsiPrintableString { Text = Hello }",
            "AnsiControlSequence { Function = m, Parameters = [ 0 ] }"
        };
        Assert.Equal(expected, strings);
    }

    [Fact]
    public void Parse_PrintableString() {
        // Check for proper handling of 7-bit ASCII,
        // extended ASCII, and UTF-16 characters.
        var result = parser.Parse("A \x7f\xea\u0102");
        var expected = new List<IAnsiStringParserElement> {
            new AnsiPrintableString("A \x7fêĂ")
        };
        Assert.Equal(expected, result);
        Assert.Equal("A \x7fêĂ", AnsiStringParser.PrintableString(result));
    }

    [Fact]
    public void Parse_SolitaryC0ControlCharacters() {
        // Test all solitary C0 control characters. Note that ESC is not a
        // solitary control characters since it initiates an escape sequence.
        var result = parser.Parse($"{
            ControlCode.NUL}{ControlCode.SOH}{ControlCode.STX}{ControlCode.ETX}{
            ControlCode.EOT}{ControlCode.ENQ}{ControlCode.ACK}{ControlCode.BEL}{
            ControlCode.BS}{ControlCode.HT}{ControlCode.LF}{ControlCode.VT}{
            ControlCode.FF}{ControlCode.CR}{ControlCode.SO}{ControlCode.SI}{
            ControlCode.DLE}{ControlCode.DC1}{ControlCode.DC2}{ControlCode.DC3}{
            ControlCode.DC4}{ControlCode.NAK}{ControlCode.SYN}{ControlCode.ETB}{
            ControlCode.CAN}{ControlCode.EM}{ControlCode.SUB}{
            ControlCode.FS}{ControlCode.GS}{ControlCode.RS}{ControlCode.US}");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiSolitaryControlCode(ControlCode.NUL),
            new AnsiSolitaryControlCode(ControlCode.SOH),
            new AnsiSolitaryControlCode(ControlCode.STX),
            new AnsiSolitaryControlCode(ControlCode.ETX),
            new AnsiSolitaryControlCode(ControlCode.EOT),
            new AnsiSolitaryControlCode(ControlCode.ENQ),
            new AnsiSolitaryControlCode(ControlCode.ACK),
            new AnsiSolitaryControlCode(ControlCode.BEL),
            new AnsiSolitaryControlCode(ControlCode.BS),
            new AnsiSolitaryControlCode(ControlCode.HT),
            new AnsiSolitaryControlCode(ControlCode.LF),
            new AnsiSolitaryControlCode(ControlCode.VT),
            new AnsiSolitaryControlCode(ControlCode.FF),
            new AnsiSolitaryControlCode(ControlCode.CR),
            new AnsiSolitaryControlCode(ControlCode.SO),
            new AnsiSolitaryControlCode(ControlCode.SI),
            new AnsiSolitaryControlCode(ControlCode.DLE),
            new AnsiSolitaryControlCode(ControlCode.DC1),
            new AnsiSolitaryControlCode(ControlCode.DC2),
            new AnsiSolitaryControlCode(ControlCode.DC3),
            new AnsiSolitaryControlCode(ControlCode.DC4),
            new AnsiSolitaryControlCode(ControlCode.NAK),
            new AnsiSolitaryControlCode(ControlCode.SYN),
            new AnsiSolitaryControlCode(ControlCode.ETB),
            new AnsiSolitaryControlCode(ControlCode.CAN),
            new AnsiSolitaryControlCode(ControlCode.EM),
            new AnsiSolitaryControlCode(ControlCode.SUB),
            new AnsiSolitaryControlCode(ControlCode.FS),
            new AnsiSolitaryControlCode(ControlCode.GS),
            new AnsiSolitaryControlCode(ControlCode.RS),
            new AnsiSolitaryControlCode(ControlCode.US),
        };
        Assert.Equal(expected, result);
        Assert.Equal("", AnsiStringParser.PrintableString(result));
    }

    [Fact]
    public void Parse_EscapenF() {
        // An nF Escape sequence is ESC followed
        // by any number of characters in the range 0x20-0x2f
        // followed by a character in the range 0x30-0x7e.
        var result = parser.Parse($"{ControlCode.ESC} !\"#$%&'()*+,-./@");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiEscapeSequence('@', " !\"#$%&'()*+,-./"),
        };
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Parse_EscapeFp() {
        // An Fp Escape sequence is ESC followed
        // by a character in the range 0x30-0x3f.
        var result = parser.Parse(
            $"{ControlCode.ESC}0" +
            $"{ControlCode.ESC}7" + 
            $"{ControlCode.ESC}8" +
            $"{ControlCode.ESC} /?");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiEscapeSequence('0', ""),
            new AnsiEscapeSequence('7', ""),
            new AnsiEscapeSequence('8', ""),
            new AnsiEscapeSequence('?', " /"),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_EscapeFe() {
        // An Fp Escape sequence is ESC followed
        // by a character in the range 0x40-0x5f.
        var result = parser.Parse(
            $"{ControlCode.ESC}@" +
            $"{ControlCode.ESC}Z");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiEscapeSequence('@', ""),
            new AnsiEscapeSequence('Z', ""),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_EscapeFs() {
        // An Fs Escape sequence is ESC followed
        // by a character in the range 0x60-0x7e.
        var result = parser.Parse(
            $"{ControlCode.ESC}`" +
            $"{ControlCode.ESC}~");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiEscapeSequence('`', ""),
            new AnsiEscapeSequence('~', ""),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_EscapeClear() {
        // Every Escape sequence must clear any intermediate characters
        // in order to avoid contamination from prior Escape sequences.
        var result = parser.Parse(
            $"{ControlCode.ESC}$+0" +
            $"{ControlCode.ESC}0");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiEscapeSequence('0', "$+"),
            new AnsiEscapeSequence('0', ""),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_CsiViaC1() {
        // Test control sequence introducer
        // initiated by a C1 control code.
        var result = parser.Parse($"{ControlCode.CSI}1;23;456j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence("j",
                new Parameter(1),
                new Parameter(23),
                new Parameter(456)
            ),
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Parse_CsiViaEscape() {
        // Test control sequence introducer
        // initiated by an escape sequence.
        var result = parser.Parse($"{ControlCode.ESC}[1;23;456j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence("j",
                new Parameter(1),
                new Parameter(23),
                new Parameter(456)
            ),
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Parse_CsiWithDefaultParameterValues() {
        var result = parser.Parse($"{ControlCode.ESC}[;;j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence("j",
                new Parameter(-1),
                new Parameter(-1),
                new Parameter(-1)
            ),
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Parse_CsiWithParameterSubvalues() {
        // The subvalue separator character ':' should
        // create parameters with multiple values.
        var result = parser.Parse($"{ControlCode.ESC}[1:2:3;4:5:6j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence("j",
                new Parameter(1, 2, 3),
                new Parameter(4, 5, 6)
            ),
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Parse_CsiWithDefaultParameterSubvalues() {
        // Unspecified parameters and unspecified parameter subvalues
        // should be initialized to the default value indicator (-1).
        var result = parser.Parse($"{ControlCode.ESC}[::;;::j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence("j",
                new Parameter(-1, -1, -1),
                new Parameter(-1), 
                new Parameter(-1, -1, -1)
            ),
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Parse_CsiPrivateParameter() {
        var result = parser.Parse($"{ControlCode.CSI}<123j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiPrivateControlSequence("j", "<123"),
        };
        Assert.Equivalent(expected, result, true);
    }

    [Fact]
    public void Parse_CsiPrivateParameterWithIntermediateBytes() {
        var result = parser.Parse($"{ControlCode.CSI}?123+/j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiPrivateControlSequence("+/j", "?123"),
        };
        Assert.Equivalent(expected, result, true);
    }

    [Fact]
    public void Parse_CsiWithEmbeddedC0Codes() {
        // C0 control codes embeded in a CSI sequence should be executed
        // immediately without any further impact on the CSI sequence decoding.
        // Any embeddeed ControlCode.DEL codes should be ignored.
        var result = parser.Parse(
            $"{ControlCode.CSI}" +
            $"{ControlCode.NUL}1;2" +
            $"{ControlCode.BEL}3;4" +
            $"{ControlCode.DEL}56 +j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiSolitaryControlCode(ControlCode.NUL),
            new AnsiSolitaryControlCode(ControlCode.BEL),
            new AnsiControlSequence(" +j",
                new Parameter(1),
                new Parameter(23),
                new Parameter(456)
            ),
        };
        Assert.Equivalent(expected, result, true);
    }

    [Fact]
    public void Parse_CsiAfterCsi() {
        // Every CSI sequence must begin by clearing any intermediate characters
        // and parameters in order to avoid contamination from prior Escape sequences.
        var result = parser.Parse($"{ControlCode.CSI}1;23;456 +j{ControlCode.CSI}j");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence(" +j",
                new Parameter(1),
                new Parameter(23),
                new Parameter(456)
            ),
            new AnsiControlSequence("j")
        };
        Assert.Equivalent(expected, result, true);
    }

    [Fact]
    public void Parse_CsiWithInvalidSequences() {
        // If the first character of the parameter string is not a
        // private (or experimental) indicator from the set "<=>?"
        // then no private (or experimental) indicator character
        // should appear anywhere else in the parameter string.
        // If it does, then the entire CSI sequence will be ignored
        // and discarded.
        var result = parser.Parse(
            $"{ControlCode.CSI}1>;23;456 +jA" +
            $"{ControlCode.CSI}1;23;4=56 +jB" +
            $"{ControlCode.CSI}1;23;4?56 +:jC");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiPrintableString("ABC"),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_StringSplitByControlCharacter() {
        // Control characters embedded in normal text should flush out
        // the print buffer via an AnsiString element and then
        // execute immediately. Processing should then continue
        // normally from the ground state.
        var result = parser.Parse($"ABC{ControlCode.BEL}DEF");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiPrintableString("ABC"),
            new AnsiSolitaryControlCode(ControlCode.BEL),
            new AnsiPrintableString("DEF"),
        };
        Assert.Equal(expected, result);
        Assert.Equal("ABCDEF", AnsiStringParser.PrintableString(result));
    }

    [Fact]
    public void Parse_OsCommand() {
        // Test both direct control code entry/exit and Fe escape sequence entry/exit.
        // When an OSC string is terminated by an ST control code, we won't receive
        // the ST control code. But when it is terminated by an ESC\ sequence,
        // we do see the terminating escape sequence.
        var result = parser.Parse(
            $"{ControlCode.OSC}OSC1{ControlCode.ST}" +
            $"{ControlCode.ESC}]OSC2{ControlCode.ESC}\\");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.OperatingSystemCommand, $"OSC1"),
            new AnsiControlString(ControlStringType.OperatingSystemCommand, "OSC2"),
            new AnsiEscapeSequence('\\', ""),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_AppProgramCommand() {
        // Test both direct control code entry/exit and Fe escape sequence entry/exit.
        // When an application program command is terminated by an ST control code, we
        // won't receive the ST control code. But when it is terminated by an ESC\ sequence,
        // we do see the terminating escape sequence.
        var result = parser.Parse(
            $"{ControlCode.APC}APC1{ControlCode.ST}" +
            $"{ControlCode.ESC}_APC2{ControlCode.ESC}\\");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.ApplicationProgramCommand, "APC1"),
            new AnsiControlString(ControlStringType.ApplicationProgramCommand, "APC2"),
            new AnsiEscapeSequence('\\', ""),
        };
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(ControlCode.BS)]
    [InlineData(ControlCode.HT)]
    [InlineData(ControlCode.LF)]
    [InlineData(ControlCode.VT)]
    [InlineData(ControlCode.FF)]
    [InlineData(ControlCode.CR)]
    public void Parse_CommandStringsWithAllowedC0Codes(char code) {
        // Allowed C0 control codes should appear in the command string.
        var result = parser.Parse(
            $"{ControlCode.DCS}DCS{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}PDCS{code}2{ControlCode.ST}" +
            $"{ControlCode.OSC}OSC{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}]OSC{code}2{ControlCode.ST}" +
            $"{ControlCode.PM}PM{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}^PM{code}2{ControlCode.ST}" +
            $"{ControlCode.APC}APC{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}_APC{code}2{ControlCode.ST}");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.DeviceControlString, $"DCS{code}1"),
            new AnsiControlString(ControlStringType.DeviceControlString, $"DCS{code}2"),
            new AnsiControlString(ControlStringType.OperatingSystemCommand, $"OSC{code}1"),
            new AnsiControlString(ControlStringType.OperatingSystemCommand, $"OSC{code}2"),
            new AnsiControlString(ControlStringType.PrivacyMessage, $"PM{code}1"),
            new AnsiControlString(ControlStringType.PrivacyMessage, $"PM{code}2"),
            new AnsiControlString(ControlStringType.ApplicationProgramCommand, $"APC{code}1"),
            new AnsiControlString(ControlStringType.ApplicationProgramCommand, $"APC{code}2"),
        };
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(ControlCode.NUL)]
    [InlineData(ControlCode.BEL)]
    [InlineData(ControlCode.SO)]
    [InlineData(ControlCode.US)]
    public void Parse_CommandStringsWithDisallowedC0Codes(char code) {
        // Disallowed C0 control codes should terminate the command string.
        // The code 0x7f (DEL) should be ignored. Check only the edge cases.
        var result = parser.Parse(
            $"{ControlCode.DCS}DCS{ControlCode.DEL}{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}PDCS{ControlCode.DEL}{code}2{ControlCode.ST}" +
            $"{ControlCode.OSC}OSC{ControlCode.DEL}{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}]OSC{ControlCode.DEL}{code}2{ControlCode.ST}" +
            $"{ControlCode.PM}PM{ControlCode.DEL}{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}^PM{ControlCode.DEL}{code}2{ControlCode.ST}" +
            $"{ControlCode.APC}APC{ControlCode.DEL}{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}_APC{ControlCode.DEL}{code}2{ControlCode.ST}");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.DeviceControlString, "DCS"),
            new AnsiPrintableString("1"),
            new AnsiControlString(ControlStringType.DeviceControlString, "DCS"),
            new AnsiPrintableString("2"),
            new AnsiControlString(ControlStringType.OperatingSystemCommand, "OSC"),
            new AnsiPrintableString("1"),
            new AnsiControlString(ControlStringType.OperatingSystemCommand, "OSC"),
            new AnsiPrintableString("2"),
            new AnsiControlString(ControlStringType.PrivacyMessage, "PM"),
            new AnsiPrintableString("1"),
            new AnsiControlString(ControlStringType.PrivacyMessage, "PM"),
            new AnsiPrintableString("2"),
            new AnsiControlString(ControlStringType.ApplicationProgramCommand, "APC"),
            new AnsiPrintableString("1"),
            new AnsiControlString(ControlStringType.ApplicationProgramCommand, "APC"),
            new AnsiPrintableString("2"),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_DeviceControlString() {
        // Test both direct control code entry/exit and Fe escape sequence entry/exit.
        // When a device control string is terminated by an ST control code, we won't
        // receive the ST control code. But when it is terminated by an ESC\ sequence,
        // we do see the terminating escape sequence.
        var result = parser.Parse(
            $"{ControlCode.DCS}DCS1{ControlCode.ST}" +
            $"{ControlCode.ESC}PDCS2{ControlCode.ESC}\\");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.DeviceControlString, "DCS1"),
            new AnsiControlString(ControlStringType.DeviceControlString, "DCS2"),
            new AnsiEscapeSequence('\\', ""),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_PrivacyMessage() {
        // Test both direct control code entry/exit and Fe escape sequence entry/exit.
        // When a privacy message is terminated by an ST control code, we won't receive
        // the ST control code. But when it is terminated by an ESC\ sequence,
        // we do see the terminating escape sequence.
        var result = parser.Parse(
            $"{ControlCode.PM}Pm1{ControlCode.ST}" +
            $"{ControlCode.ESC}^Pm2{ControlCode.ESC}\\");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.PrivacyMessage, "Pm1"),
            new AnsiControlString(ControlStringType.PrivacyMessage, "Pm2"),
            new AnsiEscapeSequence('\\', ""),
        };
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(ControlCode.NUL)]
    [InlineData(ControlCode.BEL)]
    [InlineData(ControlCode.ETB)]
    [InlineData(ControlCode.EM)]
    [InlineData(ControlCode.FS)]
    [InlineData(ControlCode.DEL)]
    public void Parse_SosString(char code) {
        // Test both C1 control code entry/exit and Fe escape sequence entry/exit.
        // When a string is terminated by an ST control code, we won't receive
        // the ST control code. But when it is terminated by an ESC\ sequence,
        // we do see the terminating escape sequence.
        // Also check for the allowance of some of the codes allowed in SOS
        // strings that are not allowed in other command strings.
        var result = parser.Parse(
            $"{ControlCode.SOS}SOS{code}1{ControlCode.ST}" +
            $"{ControlCode.ESC}XSOS{code}2{ControlCode.ESC}\\");
        var expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.StartOfString, $"SOS{code}1"),
            new AnsiControlString(ControlStringType.StartOfString, $"SOS{code}2"),
            new AnsiEscapeSequence('\\', ""),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_StringSplitInStream_ReturnSingleStringElement() {
        var result = parser.Parse($"{ControlCode.SOS}SO");
        var expected = new List<IAnsiStringParserElement>();
        Assert.Equal(expected, result);

        result = parser.Parse($"S1{ControlCode.ST}");
        expected = new List<IAnsiStringParserElement>() {
            new AnsiControlString(ControlStringType.StartOfString, "SOS1"),
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_EscapeSequenceSplitInStream_ReturnsSingleEscapeSequence() {
        var result = parser.Parse($"{ControlCode.ESC}[1;2");
        var expected = new List<IAnsiStringParserElement>();
        Assert.Equal(expected, result);

        result = parser.Parse($"3;456j");
        expected = new List<IAnsiStringParserElement>() {
            new AnsiControlSequence("j",
                new Parameter(1),
                new Parameter(23),
                new Parameter(456)
            ),
        };
        Assert.Equivalent(expected, result, true);
    }
}

public class AnsiStreamParserStatesShould {

    private readonly AnsiStreamParser parser;

    public AnsiStreamParserStatesShould() {
        parser = new((e) => {});
    }

    [Fact]
    public void Process_AllStateHandlersWithAllInputCodes_ShouldNotThrow() {
        // Check that every state handles every possible
        // character code without throwing any exceptions.
        foreach (StateHandler handler in parser.stateHandlers.Values) {
            foreach (int i in Enumerable.Range(0, 0x10000)) {
                handler.Process((char)i);
            }
        }
    }
}
