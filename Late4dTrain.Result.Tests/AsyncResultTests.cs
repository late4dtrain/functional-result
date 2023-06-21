namespace Late4dTrain;

using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

using static Result<string>;

public class AsyncResultTests
{
    [Fact]
    public async Task OnSuccess_FromAsync_Result()
    {
        var successCalled = false;

        var result = Task.FromResult(Ok()); // Creating the result object without value

        await result
            .OnSuccess(() => successCalled = true);

        successCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnSuccess_FromAsync_GenericResult()
    {
        var successCalled = false;
        var unboxedValue = 0;

        var result = Task.FromResult(Ok(42)); // Creating the result object with value

        await result
            .OnSuccess(
                i =>
                {
                    successCalled = true;
                    unboxedValue = i;
                }
            );

        successCalled.Should().BeTrue();
        unboxedValue.Should().Be(42);
    }

    [Fact]
    public async Task OnSuccessAsync_FromSync_Result()
    {
        var successCalled = false;

        var result = Ok(); // Creating the result object without value

        await result
            .OnSuccessAsync(async () => await Task.Run(() => successCalled = true));

        successCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnSuccessAsync_FromAsync_Result()
    {
        var successCalled = false;

        var result = Task.FromResult(Ok()); // Creating the result object without value

        await result
            .OnSuccessAsync(async () => await Task.Run(() => successCalled = true));

        successCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnSuccessAsync_FromSync_GenericResult()
    {
        var successCalled = false;
        var unboxedValue = 0;

        var result = Ok(42); // Creating the result object with value

        await result
            .OnSuccessAsync(
                async i =>
                {
                    await Task.Run(() => successCalled = true);
                    unboxedValue = i;
                }
            );

        successCalled.Should().BeTrue();
        unboxedValue.Should().Be(42);
    }

    [Fact]
    public async Task OnSuccessAsync_FromAsync_GenericResult()
    {
        var successCalled = false;
        var unboxedValue = 0;

        var result = Task.FromResult(Ok(42)); // Creating the result object with value

        await result
            .OnSuccessAsync(
                async i =>
                {
                    await Task.Run(() => successCalled = true);
                    unboxedValue = i;
                }
            );

        successCalled.Should().BeTrue();
        unboxedValue.Should().Be(42);
    }

    [Fact]
    public async Task OnFail_FromAsync_Result()
    {
        var failCalled = false;

        var result = Task.FromResult(Fail("Bad things happen")); // Creating the result object without value

        await result
            .OnFail(() => failCalled = true);

        failCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnFail_WithParam_FromAsync_Result()
    {
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Task.FromResult(Fail("Bad things happen")); // Creating the result object without value

        await result
            .OnFail(
                e =>
                {
                    failCalled = true;
                    capturedError = e;
                }
            );

        failCalled.Should().BeTrue();
        capturedError.Should().Be("Bad things happen");
    }

    [Fact]
    public async Task OnFail_FromAsync_GenericResult()
    {
        var failCalled = false;

        var result = Task.FromResult(Fail<int>("Bad things happen")); // Creating the result object without value

        await result
            .OnFail(() => failCalled = true);

        failCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnFail_WithPAram_FromAsync_GenericResult()
    {
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Task.FromResult(Fail<int>("Bad things happen")); // Creating the result object without value

        await result
            .OnFail(
                e =>
                {
                    failCalled = true;
                    capturedError = e;
                }
            );

        failCalled.Should().BeTrue();
        capturedError.Should().Be("Bad things happen");
    }

    [Fact]
    public async Task OnFailAsync_FromSync_Result()
    {
        var failCalled = false;

        var result = Fail("Bad things happen"); // Creating the result object without value

        await result
            .OnFailAsync(async () => await Task.Run(() => failCalled = true));

        failCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnFailAsync_WithParam_FromSync_Result()
    {
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Fail("Bad things happen"); // Creating the result object without value

        await result
            .OnFailAsync(
                async e =>
                {
                    await Task.Run(() => failCalled = true);
                    capturedError = e;
                }
            );

        failCalled.Should().BeTrue();
        capturedError.Should().Be("Bad things happen");
    }

    [Fact]
    public async Task OnFailAsync_FromAsync_Result()
    {
        var failCalled = false;

        var result = Task.FromResult(Fail("Bad things happen")); // Creating the result object without value

        await result
            .OnFailAsync(async () => await Task.Run(() => failCalled = true));

        failCalled.Should().BeTrue();
    }

    [Fact]
    public async Task OnFailAsync_WithParam_FromAsync_Result()
    {
        var failCalled = false;
        var capturedError = string.Empty;

        var result = Task.FromResult(Fail("Bad things happen")); // Creating the result object without value

        await result
            .OnFailAsync(
                async e =>
                {
                    await Task.Run(() => failCalled = true);
                    capturedError = e;
                }
            );

        failCalled.Should().BeTrue();
        capturedError.Should().Be("Bad things happen");
    }
}
