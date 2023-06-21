namespace Late4dTrain;

public class Result<TError>
{
    private readonly TError? _error;

    protected Result(bool isSuccess, TError? error)
    {
        switch (isSuccess)
        {
            case true when error is not null:
                throw new InvalidOperationException();
            case false when error is null:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                _error = error;
                break;
        }
    }

    public TError Error => _error ?? throw new InvalidOperationException();

    public bool IsSuccess { get; }

    public static Result<TError> Fail(TError error) => new(false, error);

    public static Result<TError, TValue> Fail<TValue>(TError error) => new(default, false, error);

    public static Result<TError> Ok() => new(true, default);

    public static Result<TError, TValue> Ok<TValue>(TValue value) => new(value, true, default);

    public Result<TError> OnSuccess(Action action)
    {
        if (IsSuccess)
            action();

        return this;
    }

    public async Task<Result<TError>> OnSuccessAsync(Func<Task> action)
    {
        if (IsSuccess)
            await action();

        return this;
    }

    public Result<TError> OnFail(Action action)
    {
        if (!IsSuccess)
            action();

        return this;
    }

    public Result<TError> OnFail(Action<TError> action)
    {
        if (!IsSuccess)
            action(_error!);

        return this;
    }

    public async Task<Result<TError>> OnFailAsync(Func<Task> action)
    {
        if (!IsSuccess)
            await action();

        return this;
    }

    public async Task<Result<TError>> OnFailAsync(Func<TError, Task> action)
    {
        if (!IsSuccess)
            await action(_error!);

        return this;
    }
}

public sealed class Result<TError, TValue> : Result<TError>
{
    private readonly TValue? _value;

    internal Result(TValue? value, bool isSuccess, TError? error)
        : base(isSuccess, error) =>
        _value = value;

    public TValue Value => !IsSuccess ? throw new InvalidOperationException() : _value!;

    public Result<TError, TValue> OnSuccess(Action<TValue> action)
    {
        if (IsSuccess)
            action(_value!);

        return this;
    }

    public async Task<Result<TError, TValue>> OnSuccessAsync(Func<TValue, Task> action)
    {
        if (IsSuccess)
            await action(_value!);

        return this;
    }
}

public static class ResultAsyncExtensions
{
    public static async Task<Result<TError>> OnSuccess<TError>(this Task<Result<TError>> result, Action action)
    {
        (await result).OnSuccess(action);

        return result.Result;
    }

    public static async Task<Result<TError, TValue>> OnSuccess<TError, TValue>(
        this Task<Result<TError, TValue>> result,
        Action<TValue> action)
    {
        (await result).OnSuccess(action);

        return result.Result;
    }

    public static async Task<Result<TError>> OnSuccessAsync<TError>(
        this Task<Result<TError>> result,
        Func<Task> action)
    {
        await (await result).OnSuccessAsync(action);

        return await result;
    }

    public static async Task<Result<TError, TValue>> OnSuccessAsync<TError, TValue>(
        this Task<Result<TError, TValue>> result,
        Func<TValue, Task> action)
    {
        await (await result).OnSuccessAsync(action);

        return await result;
    }

    public static async Task<Result<TError>> OnFail<TError>(this Task<Result<TError>> result, Action action)
    {
        (await result).OnFail(action);

        return result.Result;
    }

    public static async Task<Result<TError>> OnFail<TError>(this Task<Result<TError>> result, Action<TError> action)
    {
        (await result).OnFail(action);

        return result.Result;
    }

    public static async Task<Result<TError, TValue>> OnFail<TError, TValue>(
        this Task<Result<TError, TValue>> result,
        Action action)
    {
        (await result).OnFail(action);

        return result.Result;
    }

    public static async Task<Result<TError, TValue>> OnFail<TError, TValue>(
        this Task<Result<TError, TValue>> result,
        Action<TError> action)
    {
        (await result).OnFail(action);

        return result.Result;
    }

    public static async Task<Result<TError>> OnFailAsync<TError>(
        this Task<Result<TError>> result,
        Func<Task> action)
    {
        await (await result).OnFailAsync(action);

        return await result;
    }

    public static async Task<Result<TError>> OnFailAsync<TError>(
        this Task<Result<TError>> result,
        Func<TError, Task> action)
    {
        await (await result).OnFailAsync(action);

        return await result;
    }
}
