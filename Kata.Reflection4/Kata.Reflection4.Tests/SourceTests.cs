using FluentAssertions;
using Xunit;

namespace Kata.Reflection4.Tests;

public class SourceTests
{
    [Fact]
    public void NullTest()
    {
        Source.InvokeMethod(null).Should().BeNull();
    }

    [Fact]
    public void EmptyTest()
    {
        Source.InvokeMethod("").Should().Be("");
    }

    [Fact]
    public void UnknownTypeTest()
    {
        Source.InvokeMethod("unknownType").Should().BeNull();
    }

    [Fact]
    public void SmallObjectTest()
    {
        var returnValue = Source.InvokeMethod("testClass");
    }
}
