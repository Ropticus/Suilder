# Contributing to Suilder
Thank you for your interest in contributing to **Suilder**.

## Contributing to code
### Coding guidelines
The following apply to C# code:
* Use 4 spaces for indentation.
* Max line length of 125 characters.
* Format correctly the code and trim trailing spaces.
* Follow language conventions.

### Run unit tests
We use [xUnit](https://xunit.net/) for unit testing.

Run all tests:
```sh
dotnet test
```

Run **Suilder** tests:
```sh
dotnet test "./Suilder.Test"
```

Run **Suilder.Engines** tests:
```sh
dotnet test "./Suilder.Test.Engines"
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

## Contributing to documentation
The documentation is built with [MkDocs](https://www.mkdocs.org/) and is in the [Suider-docs](https://github.com/Ropticus/Suilder-docs) repository.

To build the documentation locally, use the `mkdocs serve` command and open up http://127.0.0.1:8000/ in your browser.

## License
By contributing to **Suilder**, you agree that your contributions will be licensed under the [MIT license](https://choosealicense.com/licenses/mit/).