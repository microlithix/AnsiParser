using Microlithic.Text.Ansi.Element;

namespace Microlithic.Text.Ansi.Tests;

public class ParameterShould
{

    [Fact]
    public void DefaultConstructor_ShouldSetDefaultValue() {
        Parameter parameter = new();
        Assert.Equal(-1, parameter.Value);
        Assert.Equal(-1, parameter.GetPart(0));
        Assert.Equal(-1, parameter.GetPart(1));
    }

    [Fact]
    public void ExplicitConstructor_ShouldSetParts() {
        Parameter parameter = new(1, -1, 2);
        Assert.Equal(1, parameter.Value);
        Assert.Equal(1, parameter.GetPart(0));
        Assert.Equal(-1, parameter.GetPart(1));
        Assert.Equal(2, parameter.GetPart(2));
        Assert.Equal(-1, parameter.GetPart(3));
    }

    [Fact]
    public void GetPartOrDefault_ShouldReturnDefaultWhenPartIsMissing() {
        Parameter parameter = new();
        Assert.Equal(10, parameter.GetPartOrDefault(0, 10));
        Assert.Equal(11, parameter.GetPartOrDefault(1, 11));

        parameter = new(1, -1, 2);
        Assert.Equal(1, parameter.GetPartOrDefault(0, 10));
        Assert.Equal(11, parameter.GetPartOrDefault(1, 11));
        Assert.Equal(0, parameter.GetPartOrDefault(1));
        Assert.Equal(2, parameter.GetPartOrDefault(2, 12));
        Assert.Equal(13, parameter.GetPartOrDefault(3, 13));
        Assert.Equal(0, parameter.GetPartOrDefault(3));
    }
}
