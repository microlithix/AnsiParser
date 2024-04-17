using System.Diagnostics;

using Microlithix.Text.Ansi;
using Microlithix.Text.Ansi.Element;

namespace Microlithix.Text.Ansi.Tests;

public class AnsiStreamParserShould {

    private const string ESC = "\u001b";
    private AnsiStreamParser parser;

    public AnsiStreamParserShould() {
        AnsiParserSettings settings = new() {
            PreserveLegacySGRParameters = false
        };
        parser = CreateParser();
    }

    AnsiStreamParser CreateParser(bool preserveLegacySGRParameters = false) {
        AnsiParserSettings settings = new() {
            PreserveLegacySGRParameters = preserveLegacySGRParameters
        };
        return new(settings);
    }

    List<IAnsiStreamParserElement> ParseString(string s) {
        List<IAnsiStreamParserElement> results = new();
        foreach (char ch in s) parser.Parse(ch, (element) => results.Add(element));
        return results;
    }

    [Fact]
    public void InvokeParseWithoutCallback_ShouldThrow() {
        Assert.Throws<System.InvalidOperationException>(() => parser.Parse('A'));
    }

    [Fact]
    public void InvokeWithSampleCode_ToStringShouldMatch() {
        var results = ParseString($"\x1b[34mHello\x1b[0m");
        var strings = results.Select(element => element.ToString());
        var expected = new List<string> {
            "AnsiControlSequence { Function = m, Parameters = [ 34 ] }",
            "AnsiPrintableChar { Character = H }",
            "AnsiPrintableChar { Character = e }",
            "AnsiPrintableChar { Character = l }",
            "AnsiPrintableChar { Character = l }",
            "AnsiPrintableChar { Character = o }",
            "AnsiControlSequence { Function = m, Parameters = [ 0 ] }"
        };
        Assert.Equal(expected, strings);
    }

    [Fact]
    public void InvokeWithCanonicalSGRSetForegroundColorDefaults_ShouldParseToCanonicalFormat() {
        var results = ParseString($"\x1b[38:2::3:4:5m");
        AnsiControlSequence? element = results[0] as AnsiControlSequence;
        Assert.NotNull(element);
        var result = element.ConvertLegacySGRParameters().ToString();
        var expected = "AnsiControlSequence { Function = m, Parameters = [ 38:2:-1:3:4:5 ] }";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void InvokeWithLegacySGRSetForegroundColor_ShouldParseToCanonicalFormat() {
        var result = ParseString($"\x1b[38;2;3;4;5m");
        var expected = new List<IAnsiStreamParserElement> {
            new AnsiControlSequence("m", new Parameter(38, 2, -1, 3, 4, 5))
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void InvokeWithLegacySGRSetForegroundColorDefaults_ShouldParseToCanonicalFormat() {
        var result = ParseString($"\x1b[38;2;;;m");
        var expected = new List<IAnsiStreamParserElement> {
            new AnsiControlSequence("m", new Parameter(38, 2, -1, -1, -1, -1))
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void InvokeWithLegacy2SGRSetForegroundColor_ShouldParseToCanonicalFormat() {
        var result = ParseString($"\x1b[38;2::3:4:5m");
        var expected = new List<IAnsiStreamParserElement> {
            new AnsiControlSequence("m", new Parameter(38, 2, -1, 3, 4, 5))
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void InvokeWithPreserveLegacySGRParameters_ShouldPreseveLegacyParameters() {
        parser = CreateParser(true);
        var result = ParseString($"\x1b[38;2;3;4;5m");
        var expected = new List<IAnsiStreamParserElement> {
            new AnsiControlSequence("m", new(38), new(2), new(3), new(4), new(5))
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Temp() {

        //var a = new AnsiControlElement();
        //var p = new AnsiStreamParser()
        //var a = new AnsiPrivateCsiElement('a', "", "");
        //var b = new AnsiPrintElement()
        //var a = new AnsiCsiElement('a', "", )
        //a.Para
        //new AnsiStreamParser()
        /*
        BinaryReader b;
        b.Read()
        parser.Parse()
        new Ansi
        */

        string inputStream = $"\x1b[34mHello\x1b[0m";

        var results = ParseString(inputStream);
        foreach (var element in results) {
            Debug.Print(element.ToString());
        }   
    }
}