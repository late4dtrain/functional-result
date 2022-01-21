# Late4dTrain.Result

[![.NET](https://github.com/late4dtrain/result/actions/workflows/ci.yml/badge.svg)](https://github.com/late4dtrain/result/actions/workflows/ci.yml)
[![Release to Nuget](https://github.com/late4dtrain/result/actions/workflows/release.yml/badge.svg)](https://github.com/late4dtrain/result/actions/workflows/release.yml)
[![NuGet version (GSoulavy.Csv.Net)](https://img.shields.io/nuget/v/Late4dTrain.Result.svg?style=flat-square)](https://www.nuget.org/packages/Late4dTrain.Result/)

Result Object


```cs
using static Late4dTrain.Result<Error>; // 👀

void Main()
{
	// 1.) void success scenario
	var ok = GetOk();

	ok
	   .OnSuccess(OnSuccess) // will execute without params
	   .OnFailure(OnFailure); // wont execute

	if (ok.IsSuccess) { Console.WriteLine("No corresponding Value property"); }

	// 2.) int success scenario
	var okWithData = GetOkWithData();

	okWithData
	.OnSuccess(OnSuccessWithValue) // will execute with value passed in
	.OnFailure(OnFailure); // wont execute

	if (okWithData.IsSuccess)
		Console.WriteLine($"Success: {okWithData.Value}");

	// 3.) void failed scenario
	var failed = GetFailed();

	failed
		.OnSuccess(OnSuccess) // wont executed
		.OnFailure(OnFailure); // will execute with error passed in

	if (!failed.IsSuccess)
		Console.WriteLine($"Failed: {failed.Error.Message}");

	// 4.) int failed scenario
	var failedWithData = GetFailedWithData();
	
	failedWithData
		.OnSuccess(OnSuccessWithValue) // won't execute
		.OnFailure(OnFailure);
		
	if (!failed.IsSuccess)
		Console.WriteLine($"Failed: {failed.Error.Message}");
		
	// Exception throws, when it is quite obvious
	
	Console.WriteLine(ok.Error); // don't have error when it is successful
	Console.WriteLine(okWithData.Error); // don't have error when it is successful
	Console.WriteLine(failedWithData.Value); // don't have value when it is failed
}

public Result<Error> GetOk() => Ok();

public Result<int, Error> GetOkWithData() => Ok(42);

public Result<Error> GetFailed() => Fail(new Error("Very bad!"));
	
public Result<int, Error> GetFailedWithData() => Fail<int>(new Error("Failed, though I was expecting a value"));	


// Handlers
public void OnSuccess()
{
	Console.WriteLine("onSuccess, when it is void scenario");
}

public void OnSuccessWithValue(int value)
{
	Console.WriteLine($"onSuccess with expected value: {value}");
}

public void OnFailure(Error error)
{
	Console.WriteLine($"onFailure with error message: {error.Message}");
}

// Custom Error Object
public class Error
{
	public string Message { get; }

	public Error(string message)
	{
		Message = message;
	}
}
```
