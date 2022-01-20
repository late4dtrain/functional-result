namespace Late4dTrain.Functional.ResultTests;

using System;

using FluentAssertions;

using Xunit;

public class OnFailTests
{
    [Fact]
    public void OnFailWithIntResult()
    {
        var successCalled = false;
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Result<string>.Fail<int>("It went wrong...");
        var func = () => result.Value;

        result
            .OnSuccess((i) => successCalled = true)
            .OnFailure((e) =>
                {
                    failCalled = true;
                    capturedError = e;
                }
            );

        successCalled.Should().BeFalse();
        failCalled.Should().BeTrue();
        result.Error.Should().Be("It went wrong...");
        capturedError.Should().Be("It went wrong...");
        func.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void OnFailWithoutResult()
    {
        var successCalled = false;
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Result<string>.Fail("It went wrong...");

        result
            .OnSuccess(() => successCalled = true)
            .OnFailure((e) =>
                {
                    failCalled = true;
                    capturedError = e;
                }
            );

        successCalled.Should().BeFalse();
        failCalled.Should().BeTrue();
        result.Error.Should().Be("It went wrong...");
        capturedError.Should().Be("It went wrong...");
    }

}
