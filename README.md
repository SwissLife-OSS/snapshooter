# Snapshooter

**Snapshooter is a snapshot unit testing library for _.NET Core_ and _.NET Framework_**

_Snapshooter_ is a snapshot unit testing tool for the _.Net Core_ and _.Net Framework_ to simplify the result assertion of your unit tests. It is based on the idea of [Jest Snapshot Testing](https://jestjs.io/docs/en/snapshot-testing/).

## Getting Started

At the moment the _Snapshooter_ is only supported for the Xunit test framework. 
The first step is to install the nuget package in your test project. 

```
nuget install Snapshooter.Xunit
```

### Create Simple Snapshot Test
To create a snapshot of the result object in your unit test, just use `AssertSnapshot.Match<T>(yourResultObject)`. This will serialize your result object in a json file and save it in the folder __snapshots__ next to your unit test file.

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
	Snapshot.Match<TestPerson>(person);
}
```

