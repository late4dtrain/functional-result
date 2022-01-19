namespace Late4dTrain.Functional.ResultTests;

using System;

using FluentAssertions;

using Xunit;

public class OnSuccessTests
{
    [Fact]
    public void OnSuccessWithIntResult()
    {
        var successCalled = false;
        var failCalled = false;
        var capturedResult = 0;

        var result = Result<string>.Ok(42);
        var func = () => result.Error;

        result
            .OnSuccess((i) =>
                {
                    successCalled = true;
                    capturedResult = i;
                }
            )
            .OnFailure((e) => failCalled = true);

        successCalled.Should().BeTrue();
        failCalled.Should().BeFalse();
        result.Value.Should().Be(42);
        capturedResult.Should().Be(42);
        func.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void OnSuccessWithoutResult()
    {
        var successCalled = false;
        var failCalled = false;

        var result = Result<string>.Ok();
        var func = () => result.Error;

        result
            .OnSuccess(() => successCalled = true)
            .OnFailure((e) => failCalled = true);

        successCalled.Should().BeTrue();
        failCalled.Should().BeFalse();

        func.Should().Throw<InvalidOperationException>();
    }
}
