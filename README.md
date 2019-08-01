# Suilder - SQL query builder
Suilder is a SQL query builder for .NET.

Suilder is focused on the use of [**alias objects**](#alias-objects) to reference tables and column names, there are different types of alias, that can use strings or lambda expressions, and support translation of names. The queries are built by combining smaller query fragments, allowing us to build dynamic queries easily.

This library is only a query builder, so you have to combine with any other library to execute the queries and mapping the result.

## Install
Builder:
> https://www.nuget.org/packages/Suilder/

Engines:
> https://www.nuget.org/packages/Suilder.Engines/

## Starting
At the start of your application:
```csharp
//Register your builder (only one per application because is registered globally)
ISqlBuilder sql = SqlBuilder.Register(new SqlBuilder());

//Initialize the SqlExp class to use their functions in lambda expressions (optional)
SqlExp.Initialize();

//Create a table builder and add your entity classes (optional)
TableBuilder tableBuilder = new TableBuilder()
    .Add<Person>()
    .Add<Department>();

//Create an engine to compile the queries
IEngine engine = new SQLServer(tableBuilder);
```

## The builder
In Suilder the queries are built by combining smaller query fragments. A query fragment is an object that implements the interface `IQueryFragment` and can be compiled to SQL. To create any `IQueryFragment` we use the methods of the `ISqlBuilder` interface.

For any method of the builder or an `IQueryFragment`, that accept an `object` as argument, anything that not implements the `IQueryFragment` interface, is interpreted as a **literal value** and added as a parameter.

For example, if you pass a **string**, is added as a **string literal** parameter and not a column name. To reference a table or column name you must use an **alias object**.

### Alias objects
**Alias objects** implements the interface `IAlias`, and is both the table and his alias. With an alias you can create an `IColumn` instance that contains the column name:
```csharp
//Create an alias
IAlias person = sql.Alias("person");
IAlias<Department> dept = sql.Alias<Department>();

//Get a column
IColumn col1 = person["Name"];
IColumn col2 = dept[x => x.Name];

//Get all columns
IColumn colAll1 = person.All; //person["*"]; It works too
IColumn colAll2 = dept.All; //dept[x => x]; It works too

//Operator
IOperator op = dept[x => x.Id].Eq(person["DepartmentId"]);

//Select
ISelect select = sql.Select.Add(person["Id"], person["Name"]);

//From
IFrom from = sql.From(person);

//Join
IJoin join = sql.Join(dept).On(op);
```

### Lambda expressions
Lambda expressions are compiled to an `IQueryFragment`. When you use your **entity classes** in an expression, they are compiled to an `IAlias<T>` or an `IColumn`.

Any member of a class that is not registered as a table, is invoked and the result is added as a parameter value. Functions are also executed, if you want to compile a function to SQL, you can [register your functions](https://ropticus.github.io/Suilder-docs/functions#register-functions).

The following methods of the builder allow you to compile a lambda expression:
* **Alias**: compile to an alias instance (`IAlias<T>`).
* **Col**: compile to a column instance (`IColumn`).
* **Val**: compile to a value, anything that returns a value like a column (`IColumn`), a function, or an arithmetic operator.
* **Op**: compile a boolean expression to a boolean operator.

> **Note**: in most cases you do not need to call these methods because other components accept a lambda expression and compile for you with the correct method.

```csharp
//Class alias
Person person = null;
Department dept = null;

//Create an alias
IAlias alias1 = sql.Alias(() => person);

//Get a column
IColumn col1 = sql.Col(() => person.Name);

//Get all columns
IColumn colAll1 = sql.Col(() => person);

//The "Val" method can be used for columns too
IColumn col2 = (IColumn)sql.Val(() => dept.Name);

//Arithmetic operators use the "Val" method
IOperator func = (IOperator)sql.Val(() => person.Salary + 100);

//Boolean operators use the "Op" method
IOperator op = sql.Op(() => person.Department.Id == dept.Id);

//Select
ISelect select = sql.Select.Add(() => person.Id, () => person.Name);

//From
IFrom from = sql.From(() => person);

//Join
IJoin join = sql.Join(() => dept).On(op);
```

### Without alias
It's also possible to write queries without declaring an `IAlias` object.

```csharp
//Get a column
IColumn col1 = sql.Col("person.Id");
IColumn col2 = sql.Col<Person>("person.Id");

//Get all columns
IColumn col3 = sql.Col("person.*");
IColumn col4 = sql.Col<Person>("person.*");

//Select
ISelect select = sql.Select.Add(sql.Col("person.Id"), sql.Col("person.Name"));

//From
IFrom from = sql.From("person");

//Join
IJoin join = sql.Join("department", "dept");
```

### Compile the query
To compile the query you need an [engine](https://ropticus.github.io/Suilder-docs/engines) and call his **Compile** method:
```csharp
//Create your query
IAlias person = sql.Alias("person");
IQueryFragment query = sql.Query
    .Select(person.All)
    .From(person)
    .Where(person["Id"].Eq(1));

//Compile the query using the engine
QueryResult result = engine.Compile(query);

//This is the SQL result
result.Sql; //SELECT "person".* FROM "person" WHERE "person"."Id" = @p0

//This is a dictionary with the parameters
result.Parameters; //{ ["@p0"] = 1 }
```

## Supported engines
You can use Suilder with any SQL engine, but there a list of supported engines that are already configured.

Engine | Class name | Remarks
-------|------------|--------
MySQL | MySQL |
Oracle Database | OracleDB | By default it uses quoted uppercase names.
PostgreSQL | PostgreSQL | By default it uses quoted lowercase names.
SQLite | SQLite |
SQL Server | SQLServer |

If your SQL engine is not in the list, it does not mean that you cannot use Suilder with them, but you have to [configure your engine](https://ropticus.github.io/Suilder-docs/engines#engine-configuration).

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

//Create the fragments
ISelect select = sql.Select.Add(() => person, () => dept);
IFrom from = sql.From(() => person);
IJoin join = sql.Left.Join(() => dept)
    .On(() => dept.Id == person.Department.Id);
IOperator where = sql.Op(() => person.Active && dept.Id == 10);
IOrderBy orderBy = sql.OrderBy.Add(() => person.Name).Asc;
IOffset offset = sql.Offset(10, 50);

//Create the query
IQuery query = sql.Query
    .Select(select)
    .From(from)
    .Join(join)
    .Where(where)
    .OrderBy(orderBy)
    .Offset(offset);
```

## Documentation
For more information read the [documentation](https://ropticus.github.io/Suilder-docs).