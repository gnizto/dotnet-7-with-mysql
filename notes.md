## Entity Framework

It doesn't need to write any SQL query to interact with the database.

### Install

```bash
dotnet add package Microsoft.EntityFramework
dotnet add package Microsoft.EntityFramework.SqlServer
```

### Setting It Up

The class that will be using the Entity Framework needs to extend the DbContext from the Entity Framework:

```c#
using Microsoft.EntityFrameworkCore;

public class DataContextEF : DbContext
```

It needs a DbSet related to the model which is gonna be used on the `OnModelCreating`:

```c#
public DbSet<Computer>? Computer { get; set; }
```

To connect with the SQL Server, it's needed to override the `OnConfiguring` method:

```c#
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    if (!options.IsConfigured)
    {
        options.UseSqlServer("connectionString",
            options => options.EnableRetryOnFailure());
    }
}
```

To map the model based on the table info, it's needed to override the method `OnModelCreating` hooked with the `DbSet` type:

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.HasDefaultSchema("TutorialAppSchema");

    modelBuilder.Entity<Computer>()
        .HasKey(computer => computer.ComputerId);
        // .HasNoKey();
        // .ToTable("Computer", "TutorialAppSchema");
        // .ToTable("TableName", "SchemaName");
}
```

### Using It

Create an object from the `DataContextEF` type:

```c#
DataContextEF entityFramework = new DataContextEF(config);
```

To **Load a data** call the `Add` method with the object model as an argument:

```c#
entityFramework.Add(myPC);
entityFramework.SaveChanges();
```

To retrieve the values from the database access with a code like the following:

```c#
IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
```

## Dapper

A more detailed way to interact with the database, it makes possible to customize the interactions.

### Install

```bash
dotnet add package Dapper
dotnet add package Microsoft.Data.SqlClient
```

### Setting It Up

For each interaction it's needed to connect with the database using the `SqlConnection`:

```c#
using System.Data;
using Microsoft.Data.SqlClient;

// IDbConnection is from System.Data
// SqlConnection is from Microsoft.Data.SqlClient

IDbConnection dbConnection = new SqlConnection(_connectionString);
```

To load the data from the database it can be done in two ways:

1. Load a single information:

```c#
dbConnection.QuerySingle<T>(sql);
```

2. Load multiple information at the same time:

```c#
// Returning an IEnumerable
IEnumerable<T> results = dbConnection.Query<T>(sql);

// To return a List
List<T> results = dbConnection.Query<T>(sql).ToList();
```

## Config

Used to read JSON configuration files.

### Install

```bash
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
```

### Using It

Have a JSON file with the configuration named `appSettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true"
  }
}
```

To make the file readable when debugging, inside the project file `csproj` in `<ItemGroup>` add:

```xml
<None Update="appSettings.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
</None>
```

On the file where it will be used build it:

```c#
IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
```

To access the information read the object as follows:

```c#
config.GetConnectionString("DefaultConnection")
```
