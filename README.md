### Install NuGet package
- [NuGet Gallery | EntityFrameworkCore.SqlProfile](https://www.nuget.org/packages/EntityFrameworkCore.SqlProfile)

```
PM> Install-Package EntityFrameworkCore.SqlProfile
```

### Edit Startup.cs
Insert code ```options.UseSqlProfile();``` and ```routes.MapSqlProfile();``` and required ```using``` directive into Startup.cs as follows.

```cs
using EntityFrameworkCore.SqlProfile; // Add
....
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddDbContext<SqliteDbContext>(options =>
    {
        options.UseSqlite(Configuration.GetConnectionString("Sqlite"));
        options.UseSqlProfile(); // Add when use Sqlite
    });
    ....
    services.AddDbContext<SqlServerDbContext>(options =>
    {
        options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
        options.UseSqlProfile(); // Add when use SqlServer
    });
    ....
}
....
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseMvc(routes =>
    {
        routes.MapSqlProfile(); // Add
	....
    });
}
```
## View SQL via Browser
```
Eg. input https://localhost:44393/logger
```
![screenshot](https://github.com/188867052/EntityFrameworkCore.SqlProfile/blob/master/log.png)

## Technologies

* [ASP.NET Core 2.2](https://docs.microsoft.com/en-us/aspnet/core)
* [C# 7.3](https://docs.microsoft.com/en-us/dotnet/csharp)

## My projects
* [Asp.Net](https://github.com/188867052/Asp.Net)
* [Route.Generator](https://github.com/188867052/Route.Generator)
* [DapperExtension](https://github.com/188867052/DapperExtension)
* [DependencyInjection.Analyzer](https://github.com/188867052/DependencyInjection.Analyzer)
* [EntityFrameworkCore.SqlProfile](https://github.com/188867052/EntityFrameworkCore.SqlProfile)
