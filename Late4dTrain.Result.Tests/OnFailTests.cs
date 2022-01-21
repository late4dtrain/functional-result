namespace Late4dTrain;

using System;

using FluentAssertions;

using Xunit;

using static Late4dTrain.Result<string>;

public class OnFailTests
{
    [Fact]
    public void OnFailWithIntResult()
    {
        var successCalled = false;
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Fail<int>("It went wrong..."); // Creating the failed result object without value (Result<Error, Value>)
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

        var result = Fail("It went wrong..."); // Creating the failed result object without value

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
