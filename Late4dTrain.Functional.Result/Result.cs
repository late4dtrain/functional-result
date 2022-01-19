namespace Late4dTrain.Functional;

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

    public static Result<T, TError> Fail<T>(TError error) => new(default, false, error);

    public static Result<TError> Ok() => new(true, default);

    public static Result<T, TError> Ok<T>(T value) => new(value, true, default);

    public Result<TError> OnSuccess(Action action)
    {
        if (IsSuccess)
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

public class Result<TValue, TError> : Result<TError>
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, TError? error)
        : base(isSuccess, error) =>
        _value = value;

    public TValue Value => !IsSuccess ? throw new InvalidOperationException(): _value!;

    public Result<TValue, TError> OnSuccess(Action<TValue> action)
    {
        if (IsSuccess)
            action(_value!);

        return this;
    }
}
