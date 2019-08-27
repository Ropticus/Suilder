# Contributing to Suilder
Thank you for your interest in contributing to **Suilder**.

## Contributing to code
### Coding guidelines
The following apply to C# code:
* Use 4 spaces for indentation.
* Max line length of 125 characters.
* Format correctly the code and trim trailing spaces.
* Follow language conventions.

### Run unit test
We use [xUnit](https://xunit.net/) for unit test.

Run all test:
```sh
dotnet test
```

Run **Suilder** test:
```sh
dotnet test "./Suilder.Test"
```

Run **Suilder.Engines** test:
```sh
dotnet test "./Suilder.Test.Engines"
```

### Run code coverage
We use [Coverlet](https://github.com/tonerdo/coverlet) for code coverage.

Run **Suilder** code coverage:
```sh
dotnet test "./Suilder.Test" /p:CollectCoverage=true /p:Include="[Suilder]*" /p:CoverletOutput="./coverage/" /p:CoverletOutputFormat="opencover"
```

Run **Suilder.Engines** code coverage:
```sh
dotnet test "./Suilder.Test.Engines" /p:CollectCoverage=true /p:Include="[Suilder.Engines]*" /p:CoverletOutput="./coverage/" /p:CoverletOutputFormat="opencover"
```

## Contributing to documentation
The documentation is built with [MkDocs](https://www.mkdocs.org/) and is in the [Suider-docs](https://github.com/Ropticus/Suilder-docs) repository.

To build the documentation locally, use the `mkdocs serve` command and open up http://127.0.0.1:8000/ in your browser.

## License
By contributing to **Suilder**, you agree that your contributions will be licensed under the [MIT license](https://choosealicense.com/licenses/mit/).