using Xunit;
using System.Threading.Tasks;

namespace Snapshooter.Xunit3.Tests;

public class XunitSnapshotFullNameReaderTests
{
    [Fact]
    public void ReadSnapshotFullName_ResolveSnapshotFileName_ResolvedSuccessfully()
    {
        // arrange
        var snapshotFullNameResolver = new XunitSnapshotFullNameReader();

        // act
        SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

        // assert
        Assert.Equal(
            $"{nameof(XunitSnapshotFullNameReaderTests)}." +
            $"{nameof(ReadSnapshotFullName_ResolveSnapshotFileName_ResolvedSuccessfully)}",
            snapshotFullName.Filename);
    }
    
    [Fact]
    public async Task ReadSnapshotFullName_ResolveFactSnapshotNameAsync_ResolvedSuccessfully()
    {
        // arrange
        var snapshotFullNameResolver = new XunitSnapshotFullNameReader();
        await Task.Delay(1);

        // act
        SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

        // assert
        await Task.Delay(1);
        Assert.Equal(
            $"{nameof(XunitSnapshotFullNameReaderTests)}." +
            $"{nameof(ReadSnapshotFullName_ResolveFactSnapshotNameAsync_ResolvedSuccessfully)}",
            snapshotFullName.Filename);
    }

    #pragma warning disable xUnit1026 // Theory methods should use all of their parameters

    [Theory]
    [InlineData("testString1", 5)]
    [InlineData("testString2", 6)]
    [InlineData("testString3", 7)]
    public void ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters(
        string param1, int param2)
    {
        // arrange
        var snapshotFullNameResolver = new XunitSnapshotFullNameReader();

        // act
        SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

        // assert
        Assert.Equal(
            $"{nameof(XunitSnapshotFullNameReaderTests)}." +
            $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters)}",
            snapshotFullName.Filename);
    }

    [Theory]
    [InlineData("testString1", 5)]
    [InlineData("testString2", 6)]
    [InlineData("testString3", 7)]
    public async Task ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
       string param1, int param2)
    {
        // arrange
        var snapshotFullNameResolver = new XunitSnapshotFullNameReader();
        await Task.Delay(1);

        // act
        SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

        // assert
        await Task.Delay(1);
        Assert.Equal(
            $"{nameof(XunitSnapshotFullNameReaderTests)}." +
            $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}",
            snapshotFullName.Filename);
    }

    #pragma warning restore xUnit1026 // Theory methods should use all of their parameters
}
