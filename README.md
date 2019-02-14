# Snapshooter

## [![Nuget](https://img.shields.io/nuget/v/Snapshooter.svg?style=flat)](https://www.nuget.org/packages/Snapshooter.Xunit/0.1.0-preview.3) [![GitHub Release](https://img.shields.io/github/release/SwissLife-OSS/Snapshooter.svg?style=flat)](https://github.com/SwissLife-OSS/Snapshooter/releases/latest) [![Build Status](https://dev.azure.com/swisslife-oss/swisslife-oss/_apis/build/status/Snapshooter.Release?branchName=master)](https://dev.azure.com/swisslife-oss/swisslife-oss/_build/latest?definitionId=6&branchName=master) [![Coverage Status](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_Snapshooter&metric=coverage)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_Snapshooter) [![Quality](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_Snapshooter&metric=alert_status)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_Snapshooter)

**Snapshooter is a snapshot testing tool for _.NET Core_ and _.NET Framework_**

_Snapshooter_ is a flexible snapshot testing tool to simplify the result validation in your unit tests in .Net. It is based on the idea of [Jest Snapshot Testing](https://jestjs.io/docs/en/snapshot-testing/).

## Getting Started

At the moment _Snapshooter_ only supports the Xunit test framework.

To get started, install the _Snapshooter Xunit_ nuget package:

```bash
dotnet add package Snapshooter.Xunit
```

### Assert with Snapshots

To assert your test results with snapshots in your unit tests, follow the following steps:

#### 1. Add snapshot assert statement

Insert a snapshot assert statement `Snapshot.Match(yourResultObject);` into your unit test.

Example:

```csharp
/// <summary>
/// Tests if the new created person is valid.
/// </summary>
[Fact]
public void CreatePersonSnapshotTest()
{
    // arrange
    var serviceClient = new ServiceClient();

    // act
    TestPerson person = serviceClient.CreatePerson(
        Guid.Parse("2292F21C-8501-4771-A070-C79C7C7EF451"), "David", "Mustermann");

    // assert
    Snapshot.Match(person);
}
```

#### 2. Run the unit test to create a new Snapshot

The `Snapshot.Match(person)` statement creates a new snapshot of your result object and stores it in the
`__snapshots__/__new__` folder, which will be created next to your executing unit test file.

Snapshot name: `<UnitTestClassName>.<TestMethodName>.snap`

#### 3. Review an confirm new snapshot

Review your new snapshot file `__snapshots__/__new__/<UnitTestClassName>.<TestMethodName>.snap` and if the snapshot content is valid, then move it from the `__new__` folder to the parent folder `__snapshots__`.

#### 4. Run unit test to assert

Now the `Snapshot.Match(person)` statement will create again a snapshot of your test result and compare it against your reviewed snapshot in the `__snapshots__` folder, which next to your unit test file.

## Ignore Fields in Snapshots

If some fields in your snapshot shall be ignored during snapshot assertion, then the following ignore options can be used:

```csharp
[Fact]
public void CreatePersonSnapshot_IgnoreId()
{
    // arrange
    var serviceClient = new ServiceClient();

    // act
    TestPerson person = serviceClient.CreatePerson("Hans", "Muster");

    // assert
    Snapshot.Match<Person>(testPerson, matchOptions => matchOptions.IgnoreField("Size"));
}
```

The fields to ignore will be located via JsonPath, therefore you are very flexible and you can also ignore fields from child objects or arrays.

Ignore Examples:

```csharp
// Ignores the field 'StreetNumber' of the child node 'Address' of the person
Snapshot.Match<Person>(person, matchOptions => matchOptions.IgnoreField("Address.StreetNumber"));

// Ignores the field 'Name' of the child node 'Country' of the child node 'Address' of the person
Snapshot.Match<Person>(person, matchOptions => matchOptions.IgnoreField("Address.Country.Name"));

// Ignores the field 'Id' of the first person in the 'Relatives' array of the person
Snapshot.Match<Person>(person, matchOptions => matchOptions.IgnoreField("Relatives[0].Id"));

// Ignores the field 'Name' of all 'Children' nodes of the person
Snapshot.Match<Person>(person, matchOptions => matchOptions.IgnoreField("Children[*].Name"));
```

## Assert Fields in Snapshots

Sometimes there are fields in a snapshot, which you want to assert separately against another value.

For Example, the Id field of a 'Person' is always newly generated in a service,
therefore you receive in the test always a Person with a new id (Guid).
Now if you want to check that the id is not an empty Guid, the `Assert` option can be used.

```csharp
/// <summary>
/// Tests if the new created person is valid and the person id is not empty.
/// </summary>
[Fact]
public void CreatePersonSnapshot_AssertId()
{
    // arrange
    var serviceClient = new ServiceClient();

    // act
    TestPerson person = serviceClient.CreatePerson("Hans", "Muster"); // --> id is created within the service

    // assert
    Snapshot.Match<Person>(testPerson, matchOption => matchOption.Assert(
                    fieldOption => Assert.NotEqual(Guid.Empty, fieldOption.Field<Guid>("Id"))));
}
```

The fields to assert will be located via JsonPath, therefore you are very flexible and you can also ignore fields from child objects or arrays.

Assert Examples:

```csharp
// Assert the field 'Street' of the 'Address' of the person
Snapshot.Match<Person>(person, matchOption => matchOption.Assert(
                    fieldOption => Assert.Equal(15, fieldOption.Field<int>("Address.StreetNumber"))));

// Asserts the field 'Code' of the field 'Country' of the 'Address' of the person
Snapshot.Match<Person>(person, matchOption => matchOption.Assert(
                    fieldOption => Assert.Equal("De", fieldOption.Field<CountryCode>("Address.Country.Code"))));

// Asserts the fist 'Id' field of the 'Relatives' array of the person
Snapshot.Match<Person>(person, > matchOption.Assert(
                    fieldOption => Assert.NotNull(fieldOption.Field<string>("Relatives[0].Id"))));

// Asserts every 'Id' field of all the 'Relatives' of the person
Snapshot.Match<Person>(person, > matchOption.Assert(
                    fieldOption => Assert.NotNull(fieldOption.Field<string>("Relatives[*].Id"))));

```

The Snapshooter assert functionality is not limited to Xunit asserts, it also could be used
Fluent Assertsions or another assert tool.

## Concatenate Ignore / Asserts / IsType checks

All the ignore, isType or assert field checks can be concatenated.

```csharp
[Fact]
public void Match_ConcatenateFieldChecksTest_SuccessfulMatch()
{
    // arrange
    var serviceClient = new ServiceClient();

    // act
    TestPerson person = serviceClient.CreatePerson("Hans", "Muster");

    // act & assert
    Snapshot.Match<TestPerson>(testPerson, matchOption => matchOption
            .Assert(option => Assert.NotEqual(Guid.Empty, option.Field<Guid>("Id")))
            .IgnoreField<DateTime>("CreationDate")
            .Assert(option => Assert.Equal(-58, option.Field<int>("Address.StreetNumber")))
            .Assert(option => testChild.Should().BeEquivalentTo(option.Field<TestChild>("Children[3]")))
            .IgnoreField<TestCountry>("Address.Country")
            .Assert(option => Assert.Null(option.Field<TestCountry>("Relatives[0].Address.Plz"))));
}
```
