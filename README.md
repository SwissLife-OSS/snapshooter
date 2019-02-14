# Snapshooter

[![Build Status](https://dev.azure.com/swisslife-oss/swisslife-oss/_apis/build/status/SwissLife-OSS.Snapshooter?branchName=master)](https://dev.azure.com/swisslife-oss/swisslife-oss/_build/latest?definitionId=2&branchName=master) [![Coverage Status](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_Snapshooter&metric=coverage)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_Snapshooter) [![Quality](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_Snapshooter&metric=alert_status)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_Snapshooter)
---

**Snapshooter is a snapshot testing tool for _.NET Core_ and _.NET Framework_**

_Snapshooter_ is a flexible snapshot testing tool to simplify the result validation in your unit tests in .Net. It is based on the idea of [Jest Snapshot Testing](https://jestjs.io/docs/en/snapshot-testing/).

## Getting Started

At the moment  _Snapshooter_ only supports the Xunit test framework. 

To get started, install the _Snapshooter Xunit_ nuget package: 

```bash
nuget install Snapshooter.Xunit
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
```__snapshots__/__new__``` folder, which will be created next to your executing unit test file. 

Snapshot name: ```<UnitTestClassName>.<TestMethodName>.snap```

#### 3. Review an confirm new snapshot

Review your new snapshot file ```__snapshots__/__new__/<UnitTestClassName>.<TestMethodName>.snap``` and if the snapshot content is    valid, then move it from the ```__new__``` folder to the parent folder ```__snapshots__```.

#### 4. Run unit test to assert

Now the `Snapshot.Match(person)` statement will create again a snapshot of your test result and compare it against your reviewed snapshot in the ```__snapshots__``` folder, which next to your unit test file.


## Ignore Fields in Snapshots
If some fields in your snapshot shall be ignored during snapshot assertion, then the following ignore options can be used:
```csharp
 Snapshot.Match<TestPerson>(testPerson, matchOptions => matchOptions.IgnoreField("Size"));
```

The fields to ignore will be located via JsonPath, therefore you are very flexible and you can also ignore fields from child objects or arrays.
Examples:
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

