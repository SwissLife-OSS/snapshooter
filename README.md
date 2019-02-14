# Snapshooter

[![Nuget](https://img.shields.io/nuget/v/Snapshooter.svg?style=flat)](https://www.nuget.org/packages/Snapshooter.Xunit/0.1.0-preview.3) [![GitHub Release](https://img.shields.io/github/release/SwissLife-OSS/Snapshooter.svg?style=flat)](https://github.com/SwissLife-OSS/Snapshooter/releases/latest) [![Build Status](https://dev.azure.com/swisslife-oss/swisslife-oss/_apis/build/status/Snapshooter.Release?branchName=master)](https://dev.azure.com/swisslife-oss/swisslife-oss/_build/latest?definitionId=6&branchName=master) [![Coverage Status](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_Snapshooter&metric=coverage)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_Snapshooter) [![Quality](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_Snapshooter&metric=alert_status)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_Snapshooter)

**Snapshooter is a snapshot unit testing tool for _.NET Core_ and _.NET Framework_**

_Snapshooter_ is a snapshot unit testing tool for the _.Net Core_ and _.Net Framework_ to simplify the result assertion of your unit tests. It is based on the idea of [Jest Snapshot Testing](https://jestjs.io/docs/en/snapshot-testing/).

## Getting Started

At the moment _Snapshooter_ only supports the Xunit test framework.
The first step is to install the _Snapshooter_ nuget package:

```
nuget install Snapshooter.Xunit
```

### Snapshot Assert

A snapshot unit test assert consists of the following three steps:

1. New Snapshot Creation
2. Review New Snapshot
3. Run Snapshot Test

#### 1. New Snapshot Creation

To create a new snapshot assert for a unit test , add a snapshot assert command `Snapshot.Match(yourResultObject);` in your unit test.

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

The first execution of the `Snapshot.Match(person)` will create a new snapshot of your result object and store it in the `__snapshots__/__new__` folder, which will be created next to your executing unit test file.

The name of your new snapshot file will be per default:
`<UnitTestClassName>.<TestMethodName>.snap`

#### Review New Snapshots

Review your new snapshot file `__snapshots__/__new__/<UnitTestClassName>.<TestMethodName>.snap` and if the snapshot is alright, then just move it from the `__new__` folder to the parent folder `__snapshots__`. Now the snapshot is in the `__snapshots__` folder and will be compared within the next unit test execution snapshot.

After the first execution of your snapshot assert, a new snapshot file will be created and saved in the `__snapshots__/__new__` folder.

The name of your snapshot file will be per default:
`<UnitTestClassName>.<TestMethodName>.snap`

This will create a new snapshot of your result object (json file \*.snap) and store it in the `__snapshots__/__new__` folder. The snapshots will automatically be generated and stored in the folder `__snapshots__` next to your executing unit test file.

This will create a new snapshot of your result object (json file \*.snap) and compares it with the already existing snapshot. The snapshots will automatically be generated and stored in the folder `__snapshots__` next to your executing unit test file.

If the snapshot in the new folder `__snapshots__/__new__` is valid
