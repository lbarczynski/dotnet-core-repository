___
# **WORK IN PROGRESS** 
___
# Dotnet Core Repository

| Build status      | [![Build Status](https://travis-ci.org/lbarczynski/dotnet-core-repository.svg?branch=develop)](https://travis-ci.org/lbarczynski/dotnet-core-repository) |
| ------------- |:-------------:|
| Entity Framework Repository      | [![NuGet](https://img.shields.io/nuget/v/BAPPS.Repository.EntityFramework.Core.svg)](https://www.nuget.org/packages/BAPPS.Repository.EntityFramework.Core) |

Generic .Net Core repositories designed to collaborate with *Entity Framework Core* and deliver custom implementation like in-memory or file-based repositories.

## Using
### BAPPS.Repository.EntityFramework.Core
To install the [BAPPS.Repository.EntityFramework.Core](http://nuget.org/packages/BAPPS.Repository.EntityFramework.Core), run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    PM> Install-Package BAPPS.Repository.EntityFramework.Core

## Contribution
### Prerequisites

- Dotnet Core v2.0.0 or newer
- Python 2.7
- Zip (for make CLI)

### Build with CLI tools

A step by step series of examples that tell you have to get a development env running

First what you have to do is compile **CLI tools**:

```
make cli
```

After that you can build project:

```
./cli clean
./cli restore
./cli build
```

All **CLI tools** functions you can list with following command:

```
./cli -h
```

### Run tests with CLI tools

Before run test you have to call all methods from [Installing](##installing) sectuin. To run unit tests use following command:

```
$ ./cli test
```

## Authors

* **Łukasz Barczyński** - *Initial work* - [lbarczynski](https://github.com/lbarczynski)

See also the list of [contributors](https://github.com/lbarczynski/entity-framework-repository/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details  