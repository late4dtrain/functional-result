namespace Late4dTrain;

using System;

using FluentAssertions;

using Xunit;

using static Result<string>;

public class OnSuccessTests
{
    [Fact]
    public void OnSuccessWithoutResult()
    {
        var successCalled = false;
        var failCalled = false;

        var result = Ok(); // Creating the result object without value
        var func = () => result.Error;

        result
            .OnSuccess(() => successCalled = true)
            .OnFail(e => failCalled = true);

        successCalled.Should().BeTrue();
        failCalled.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        func.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void OnSuccessWithIntResult()
    {
        var successCalled = false;
        var failCalled = false;
        var capturedResult = 0;

        var result = Ok(42); // Creating the result object with value
        var func = () => result.Error;

        result
            .OnSuccess(
                i =>
                {
                    successCalled = true;
                    capturedResult = i;
                }
            )
            .OnFail(e => failCalled = true);

        successCalled.Should().BeTrue();
        failCalled.Should().BeFalse();
        result.Value.Should().Be(42);
        capturedResult.Should().Be(42);
        result.IsSuccess.Should().BeTrue();
        func.Should().Throw<InvalidOperationException>();
    }
}
