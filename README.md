# kr.bbon.EntityFrameworkCore.Extensions Package

[![](https://img.shields.io/nuget/v/kr.bbon.EntityFrameworkCore.Extensions)](https://www.nuget.org/packages/kr.bbon.EntityFrameworkCore.Extensions) [![](https://img.shields.io/nuget/dt/kr.bbon.EntityFrameworkCore.Extensions)](https://www.nuget.org/packages/kr.bbon.EntityFrameworkCore.Extensions) ![publish to nuget](https://github.com/bbonkr/kr.bbon.EntityFrameworkCore.Extensions/workflows/publish%20to%20nuget/badge.svg)

## Features

### Sort method

Extends IQueryable<T> interface.

Sort elements with filed name that Use OderBy, OrderByDescending, ThenBy, ThenByDescending methods.

Signature:

`Sort(string fileName, bool isAscending)`

Example code:

See Example project. `example/Example.App`

```csharp
using(var ctx = new ExampleDbContext()){
    var result =ctx.Documents.Sort(nameof(Document.Content)).Sort(nameof(Document.Id), false);
}
```

## License

Follow the [.net license](https://dotnet.microsoft.com/platform/free) and the [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) license.
