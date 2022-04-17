# Suilder - SQL query builder
[![Build Status](https://img.shields.io/github/workflow/status/Ropticus/Suilder/Build?event=push)](https://github.com/Ropticus/Suilder/actions?query=workflow%3ABuild)
[![Test Status](https://img.shields.io/github/workflow/status/Ropticus/Suilder/Test?event=push&label=test)](https://github.com/Ropticus/Suilder/actions?query=workflow%3ATest)
[![Documentation Status](https://img.shields.io/readthedocs/suilder/latest)](https://suilder.readthedocs.io/en/latest/)
[![GitHub release](https://img.shields.io/github/release/Ropticus/Suilder)](https://github.com/Ropticus/Suilder/releases/latest)

Suilder is a SQL query builder for .NET.

It is focused on the use of [**alias objects**](#alias-objects) to reference tables and column names, there are different types of alias, that can use strings or lambda expressions, and support translation of names. The queries are built by combining smaller query fragments, allowing us to build dynamic queries easily.

This library is only a query builder, so you have to combine with any other library to execute the queries and mapping the result.

## Installing

| Package | Nuget | Download (full) |
|---------|-------|-----------------|
| Suilder | [![Nuget](https://img.shields.io/nuget/v/Suilder?logo=nuget)](https://www.nuget.org/packages/Suilder/) | [![GitHub release](https://img.shields.io/github/release/Ropticus/Suilder?logo=github)](https://github.com/Ropticus/Suilder/releases/latest) |
| Suilder.Engines | [![Nuget](https://img.shields.io/nuget/v/Suilder.Engines?logo=nuget)](https://www.nuget.org/packages/Suilder.Engines/) | Use full release. |

## Starting
At the start of your application:
```csharp
// Register your builder (only one per application because is registered globally)
ISqlBuilder sql = SqlBuilder.Register(new SqlBuilder());

// Initialize the SqlExp class to use their functions in lambda expressions (optional)
SqlExp.Initialize();

// Create a table builder and add your entity classes (optional)
ITableBuilder tableBuilder = new TableBuilder();
tableBuilder.Add<Person>();
tableBuilder.Add<Department>();

// Create an engine to compile the queries
IEngine engine = new SQLServerEngine(tableBuilder);
```

## The builder
In Suilder the queries are built by combining smaller query fragments. A query fragment is an object that implements the `IQueryFragment` interface and can be compiled to SQL. To create any `IQueryFragment` we use the methods of the `ISqlBuilder` interface.

Any type that does not implement the `IQueryFragment` interface, is interpreted as a **literal value** and added to the parameters of the query.

For example, a **string** will be added as a parameter of the query, and not as a column name in the SQL. To reference a table or column name you must use an **alias object**.

### Alias objects
**Alias objects** implements the `IAlias` interface, and is both the table and his alias. With an alias you can create an `IColumn` instance that contains the column name:
```csharp
// Create an alias
IAlias person = sql.Alias("person");
IAlias<Department> dept = sql.Alias<Department>();

// Get a column
IColumn col1 = person["Name"];
IColumn col2 = dept[x => x.Name];

// Get all columns
IColumn colAll1 = person.All;
IColumn colAll2 = dept.All;

// It works too
IColumn colAll3 = person["*"];
IColumn colAll4 = dept[x => x];

// Operator
IOperator op = dept[x => x.Id].Eq(person["DepartmentId"]);

// Select
ISelect select = sql.Select.Add(person["Id"], person["Name"]);

// From
IFrom from = sql.From(person);

// Join
IJoin join = sql.Join(dept).On(op);
```

### Lambda expressions
Lambda expressions are compiled to an `IQueryFragment`. When you use your **entity classes** in an expression, they are compiled to an `IAlias` or an `IColumn`.

Any member of a class that is not registered as a table, is invoked and the result is added as a query parameter. Functions are also executed, if you want to compile a function to SQL, you can [register your functions](https://suilder.readthedocs.io/en/latest/general/builder/#register-functions).

The following methods of the builder allow you to compile a lambda expression:

- **Alias**: compile to an alias instance (`IAlias`).
- **Col**: compile to a column instance (`IColumn`).
- **Val**: compile to a value, anything that returns a value like a column (`IColumn`), a function, or an arithmetic operator.
- **Op**: compile a boolean expression to a boolean operator.

> **Note**: in most cases you do not need to call these methods because other components accept a lambda expression and compile for you with the correct method.

```csharp
// Class alias
Person person = null;
Department dept = null;

// Create an alias
IAlias alias1 = sql.Alias(() => person);

// Get a column
IColumn col1 = sql.Col(() => person.Name);

// Get all columns
IColumn colAll1 = sql.Col(() => person);

// The "Val" method can be used for columns too
IColumn col2 = (IColumn)sql.Val(() => dept.Name);

// Arithmetic operators use the "Val" method
IOperator op1 = (IOperator)sql.Val(() => person.Salary + 100);

// Boolean operators use the "Op" method
IOperator op2 = sql.Op(() => person.Department.Id == dept.Id);

// Select
ISelect select = sql.Select.Add(() => person.Id, () => person.Name);

// From
IFrom from = sql.From(() => person);

// Join
IJoin join = sql.Join(() => dept).On(op2);
```

### Without alias
It is also possible to write queries without declaring an `IAlias` object.

```csharp
// Get a column
IColumn col1 = sql.Col("person.Id");
IColumn col2 = sql.Col<Person>("person.Id");
IColumn col3 = sql.Col<Person>(x => x.Id);

// Get all columns
IColumn col4 = sql.Col("person.*");
IColumn col5 = sql.Col<Person>("person.*");
IColumn col6 = sql.Col<Person>(x => x);

// Select
ISelect select = sql.Select.Add(sql.Col("person.Id"), sql.Col<Person>("Name"));

// From
IFrom from = sql.From("person");

// Join
IJoin join = sql.Join("department", "dept");
```

### Compile the query
To compile the query you need an [engine](https://suilder.readthedocs.io/en/latest/configuration/engines/) and call his **Compile** method:
```csharp
// Create your query
IAlias person = sql.Alias("person");
IQueryFragment query = sql.Query
    .Select(person.All)
    .From(person)
    .Where(person["Id"].Eq(1));

// Compile the query using the engine
QueryResult result = engine.Compile(query);

// This is the SQL result:
// SELECT "person".* FROM "person" WHERE "person"."Id" = @p0
result.Sql;

// This is a dictionary with the parameters:
// { ["@p0"] = 1 }
result.Parameters;
```

## Supported engines
You can use Suilder with any SQL engine, but there a list of supported engines that are already configured.

| Engine | Class name | Remarks |
|--------|------------|---------|
| MySQL | MySQLEngine | |
| Oracle Database | OracleDBEngine | By default it uses quoted uppercase names. |
| PostgreSQL | PostgreSQLEngine | By default it uses quoted lowercase names. |
| SQLite | SQLiteEngine | |
| SQL Server | SQLServerEngine | |

If your SQL engine is not in the list, it does not mean that you cannot use Suilder with them, but you have to [configure your engine](https://suilder.readthedocs.io/en/latest/configuration/engines/#engine-configuration).

## Examples
```csharp
IAlias person = sql.Alias("person");
IAlias<Department> dept = sql.Alias<Department>();

IQuery query = sql.Query
    .Select(person.All, dept.All)
    .From(person)
    .Left.Join(dept)
        .On(dept[x => x.Id].Eq(person["DepartmentId"]))
    .Where(sql.And
        .Add(person["Active"].Eq(true))
        .Add(dept[x => x.Id].Eq(10)))
    .OrderBy(x => x.Add(person["Name"]).Asc)
    .Offset(10, 50);
```

With lambda expressions:
```csharp
Person person = null;
Department dept = null;

IQuery query = sql.Query
    .Select(() => person, () => dept)
    .From(() => person)
    .Left.Join(() => dept)
        .On(() => dept.Id == person.Department.Id)
    .Where(() => person.Active && dept.Id == 10)
    .OrderBy(x => x.Add(() => person.Name).Asc)
    .Offset(10, 50);
```

You can create individual query fragments and combine them later:
```csharp
Person person = null;
Department dept = null;

// Create the fragments
ISelect select = sql.Select.Add(() => person, () => dept);
IFrom from = sql.From(() => person);
IJoin join = sql.Left.Join(() => dept)
    .On(() => dept.Id == person.Department.Id);
IOperator where = sql.Op(() => person.Active && dept.Id == 10);
IOrderBy orderBy = sql.OrderBy.Add(() => person.Name).Asc;
IOffset offset = sql.Offset(10, 50);

// Create the query
IQuery query = sql.Query
    .Select(select)
    .From(from)
    .Join(join)
    .Where(where)
    .OrderBy(orderBy)
    .Offset(offset);
```

## Documentation
For more information read the [documentation](https://suilder.readthedocs.io).