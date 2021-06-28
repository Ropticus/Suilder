# Contributing to Suilder
Thank you for your interest in contributing to **Suilder**.

## Report an issue
To report a bug or a problem open an [issue](https://github.com/Ropticus/Suilder/issues).

## Start a discussion
To ask questions or suggest new features open a [discussion](https://github.com/Ropticus/Suilder/discussions).

## Contributing to the documentation
The documentation is built with [MkDocs](https://www.mkdocs.org/) and is in the [Suider-docs](https://github.com/Ropticus/Suilder-docs) repository.

## Contributing to the code
Look for issues with the label ["help wanted"](https://github.com/Ropticus/Suilder/issues/?q=label%3A"help+wanted"+is%3Aissue+is%3Aopen) and submit a [pull request](https://github.com/Ropticus/Suilder/pulls).

### Coding guidelines
The following apply to **C#** code:

- Use 4 spaces for indentation.
- Max line length of 125 characters.
- Format correctly the code and trim trailing spaces.
- Follow language conventions.

### Run unit tests
We use [xUnit](https://xunit.net/) for unit testing.

Run all tests:
```sh
dotnet test -c Release
```

Run **Suilder** tests:
```sh
dotnet test "./Suilder.Test" -c Release
```

Run **Suilder.Engines** tests:
```sh
dotnet test "./Suilder.Test.Engines" -c Release
```

### Run code coverage
We use [Coverlet](https://github.com/coverlet-coverage/coverlet) for code coverage.

Run all code coverage:
```sh
dotnet test --collect:"XPlat Code Coverage"
```

Run **Suilder** code coverage:
```sh
dotnet test "./Suilder.Test" --collect:"XPlat Code Coverage"
```

Run **Suilder.Engines** code coverage:
```sh
dotnet test "./Suilder.Test.Engines" --collect:"XPlat Code Coverage"
```

### Run performance tests
We use [BenchmarkDotNet](https://benchmarkdotnet.org/) for benchmarking.

Run all benchmarks:
```sh
dotnet run -p "./Suilder.Performance" -c Release -- -f *
```

## License
By contributing to **Suilder**, you agree that your contributions will be licensed under the [MIT license](LICENSE.txt).