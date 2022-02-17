namespace Late4dTrain;

public class Result<TError>
{
    private readonly TError? _error;

    protected Result(bool isSuccess, TError? error)
    {
        if (isSuccess && error is { })
            throw new InvalidOperationException();

        if (!isSuccess && error is null)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        _error = error;
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

    public Result<TError> OnFailure(Action action)
    {
        if (!IsSuccess)
            action();

        return this;
    }

    public Result<TError> OnFailure(Action<TError> action)
    {
        if (!IsSuccess)
            action(_error!);

        return this;
    }
}

public sealed class Result<TError, TValue> : Result<TError>
{
    private readonly TValue? _value;

    internal Result(TValue? value, bool isSuccess, TError? error)
        : base(isSuccess, error) =>
        _value = value;

    public TValue Value => !IsSuccess ? throw new InvalidOperationException(): _value!;

    public Result<TError, TValue> OnSuccess(Action<TValue> action)
    {
        if (IsSuccess)
            action(_value!);

        return this;
    }
}
