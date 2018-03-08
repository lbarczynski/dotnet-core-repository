# Entity Framework Repository
[![Build Status](https://travis-ci.org/lbarczynski/entity-framework-repository.svg?branch=develop)](https://travis-ci.org/lbarczynski/entity-framework-repository)

Generic repository designed for collaborate with Entity Framework

> **WORK IN PROGRESS**

## Prerequisites

- Dotnet Core v2.0.0 or newer
- Python 2.7
- Zip (for make CLI)

## Installing

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

## Run tests

Before run test you have to call all methods from [Installing](##installing) sectuin. To run unit tests use following command:

```
$ ./cli test
```

## Authors

* **Łukasz Barczyński** - *Initial work* - [lbarczynski](https://github.com/lbarczynski)

See also the list of [contributors](https://github.com/lbarczynski/entity-framework-repository/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details