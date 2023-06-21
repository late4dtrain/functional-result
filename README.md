# Late4dTrain.Result

[![.NET](https://github.com/late4dtrain/result/actions/workflows/ci.yml/badge.svg)](https://github.com/late4dtrain/result/actions/workflows/ci.yml)
[![Release to Nuget](https://github.com/late4dtrain/result/actions/workflows/release.yml/badge.svg)](https://github.com/late4dtrain/result/actions/workflows/release.yml)
[![NuGet version (GSoulavy.Csv.Net)](https://img.shields.io/nuget/v/Late4dTrain.Result.svg?style=flat-square)](https://www.nuget.org/packages/Late4dTrain.Result/)

# Late4dTrain C# Library

This repository contains a flexible and type-safe C# library for result-oriented error handling, which promotes clearer and more reliable code. The library is especially useful in environments where exceptions are costly or unwanted, such as in high-performance or concurrent systems.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Installation
Install the library using the following command:

```bash
dotnet add package Late4dTrain.Result
`````

## Usage
The `Late4dTrain` library contains the `Result` and `Result<TError, TValue>` classes, and `ResultAsyncExtensions` for async operations.

A `Result` object represents the result of an operation that can either succeed or fail, with an associated error of type `TError`.

Here is a simple example of using the `Result` class:

```csharp
var successResult = Result<string>.Ok();
var failResult = Result<string>.Fail("An error occurred");

if (successResult.IsSuccess)
{
    // Perform operation on success
}

if (!failResult.IsSuccess)
{
    Console.WriteLine(failResult.Error);  // Outputs: "An error occurred"
}
```
`Result<TError, TValue>` is a subclass of `Result`, which also carries a value of type `TValue` when the operation succeeds:
    
```csharp
var successResultWithValue = Result<string, int>.Ok(10);
var failResultWithValue = Result<string, int>.Fail("An error occurred");

if (successResultWithValue.IsSuccess)
{
Console.WriteLine(successResultWithValue.Value);  // Outputs: "10"
}

if (!failResultWithValue.IsSuccess)
{
Console.WriteLine(failResultWithValue.Error);  // Outputs: "An error occurred"
}
```
For async operations, use the extension methods from `ResultAsyncExtensions`. Here is an example:
```csharp
Task<Result<string>> taskResult = Task.FromResult(Result<string>.Ok());
await taskResult.OnSuccessAsync(async () => { await DoSomethingAsync(); });
```
Here are examples of each public method in the Result and `Result<TError, TValue>` classes and their usage:

1. `OnSuccess(Action action)`: This method executes the provided action if the result is successful.
```csharp
var successResult = Result<string>.Ok();
successResult.OnSuccess(() => Console.WriteLine("Operation was successful."));
// Outputs: "Operation was successful."
```
2. `OnSuccessAsync(Func<Task> action)`: This method asynchronously executes the provided action if the result is successful.
```csharp
var successResult = Result<string>.Ok();
await successResult.OnSuccessAsync(async () => { await Task.Delay(1000); Console.WriteLine("Operation was successful."); });
// Outputs: "Operation was successful." after a delay of 1 second.
```
3. `OnFail(Action action)`: This method executes the provided action if the result is a failure.

```csharp
var failResult = Result<string>.Fail("An error occurred");
failResult.OnFail(() => Console.WriteLine("Operation failed."));
// Outputs: "Operation failed."
```
4. `OnFail(Action<TError> action)`: This method executes the provided action, giving the error as a parameter, if the result is a failure.
```csharp
var failResult = Result<string>.Fail("An error occurred");
failResult.OnFail(err => Console.WriteLine($"Operation failed with error: {err}"));
// Outputs: "Operation failed with error: An error occurred"
```

5. `OnFailAsync(Func<Task> action)`: This method asynchronously executes the provided action if the result is a failure.
```csharp
var failResult = Result<string>.Fail("An error occurred");
```
6. `OnFailAsync(Func<TError, Task> action)`: This method asynchronously executes the provided action, giving the error as a parameter, if the result is a failure.
```csharp
var failResult = Result<string>.Fail("An error occurred");
await failResult.OnFailAsync(async err => { await Task.Delay(1000); Console.WriteLine($"Operation failed with error: {err}"); });
// Outputs: "Operation failed with error: An error occurred" after a delay of 1 second.
```
The `Result<TError, TValue>` class also has similar methods that take a value as a parameter when the operation is successful:

1. `OnSuccess(Action<TValue> action)`: This method executes the provided action, giving the value as a parameter, if the result is successful.
```csharp
var successResultWithValue = Result<string, int>.Ok(10);
successResultWithValue.OnSuccess(value => Console.WriteLine($"Operation was successful with value: {value}."));
// Outputs: "Operation was successful with value: 10."
```
2. `OnSuccessAsync(Func<TValue, Task> action)`: This method asynchronously executes the provided action, giving the value as a parameter, if the result is successful.
```csharp
var successResultWithValue = Result<string, int>.Ok(10);
await successResultWithValue.OnSuccessAsync(async value => { await Task.Delay(1000); Console.WriteLine($"Operation was successful with value: {value}."); });
// Outputs: "Operation was successful with value: 10." after a delay of 1 second.
```
Remember to use the async versions of these methods in asynchronous contexts to avoid blocking.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://opensource.org/licenses/MIT)

This project is licensed under the terms of the MIT license.
